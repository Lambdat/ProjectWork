using Microsoft.IdentityModel.Tokens;
using ProjectWork.Data;
using ProjectWork.DTOs;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWork.Services
{
    public class AuthService : IAuthService
    {

        private readonly DataContext _db;

        public AuthService(DataContext db)
        {
            _db = db;
        }

        //Metodo per effettuare l'accesso sia Ssn sia o Username
        public string Login(string ssn, string username, string password)
        {
            var user = _db.Users.FirstOrDefault(user => user.Ssn == ssn.ToUpper() || user.Username==username.ToLower());


            if (user is null)
            {
                throw new UserNotFoundException();
            }

            if (VerifyPassword(password, user.HashedPassword, user.PasswordSalt))
            {
                // Restituire un token
                return CreateToken(user);
            }

            throw new BadCredentialsException();
        }

        private string CreateToken(User user)
        {
            // Andiamo a definire le parti del payload, che servono ad identificare l'utente
            var claims = new List<Claim>
            {
                // Così stiamo dicendo che con l ssn e con lo username identifichiamo univocamente l'utente
                // Quando generiamo il codice token, esso conterrà queste informazioni               
                new Claim(ClaimTypes.NameIdentifier, user.Ssn),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Conviene esternalizzare la chiave segreta
            var secret = "Super secret very very long long crowbar";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Passando il salt al costruttore, stiamo decidendo noi la chiave
            using var hmac = new HMACSHA512(passwordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            // SequenceEqual => Compara due sequenze
            return hash.SequenceEqual(passwordHash);
        }

        //Metodo per effettuare Registrazione di un nuovo Utente
        public bool Register(UserRegistrationRequest request)
        {
            var (hash, salt) = HashPassword(request.Password);

            User u = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                Gender = request.Gender,
                Pob = request.Pob,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                HashedPassword = hash,
                PasswordSalt = salt,
            };

            //Con il richiamo dei metodi andiamo a calcolare e caricare queste proprietà
            u.Ssn=u.CreateSsn();

            u.CreateUsername(); //Questo è un metodo void che altera lo stato dell'oggetto
                                //andava bene anche se avesse restituito string

            if (UserExists(u.Ssn))
            {
                return false; //Registrazione Non Avvenuta
            }

            _db.Users.Add(u); //Questa è come se fosse l'operazione Create (Crud) di uno User
                                //invece di farla nel suo service, la facciamo dopo una registrazione
            _db.SaveChanges();  //effetuata con successo

            return true;

        }

        //Metodo che dato un codice fiscale controlla se già vi è uno esistente
        private bool UserExists(string ssn)
        {

            return _db.Users.Any(utente => utente.Ssn == ssn);
        }

        //Metodo che data una password restituisce una Tupla(insieme di valori)rappresentanti
        //gli array di byte della password e del sale
        private (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password)
        {
            using var hms = new HMACSHA512(); //Facciamo uno using interno per non fare hms.Dispose()
            var salt = hms.Key; //Prendiamo il sale che corrisponde alla chiave dell'algoritmo

            //Creaiamo una variabile, all'interno
            //Calcoliamo l hash del nostro oggetto passandogli
            //il Buffer della nostra password data in input
            var hash = hms.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (hash, salt); //Restituiamo la Tupla(byte[] ... ,byte[])
        }

    }
}
