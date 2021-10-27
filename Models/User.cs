using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ProjectWork.Models
{
    public class User
    {
        //Dati Anagrafici e Recapiti

        [Key] //Dato che SSN non segue la convenzione EF di avere Id (per generarne la PK)
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Mettiamo queste 2 specifiche
        public string Ssn 
        {            
            get => Ssn; 
            set
            {
                if (value.Length == 16)
                    Ssn = value;
                else
                    throw new Exception("Numero di Caratteri Diversi per il Codice Fiscale");
            } 
        }

        //SSN(Codice Fiscale) questa sarà la nostra pk
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Pob { get; set; } //Place of Birth
        public string Address { get; set; } //Indirizzo di Residenza
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Post> Posts { get; set; }

        //Algoritmi ottenutti dall'invio della password
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }


        /*
         * 
         * 
         *  - le prime tre lettere del codice fiscale sono prese dal cognome (solitamente prima, seconda e terza consonante)
            - le seconde tre dal nome (solitamente prima, terza e quarta consonante se il numero di consonanti sono >=3)
            - le ultime due cifre dell'anno di nascita
            - una lettera per il mese (A = Gennaio, B, C, D, E, H, L, M, P, R, S, T = Dicembre)
            - il giorno di nascita: in caso di sesso femminile si aggiunge 40 per cui è chiaro che se si trova scritto, ad esempio, 52, non può che trattarsi di una donna nata il 12 del mese.
            - Codice del comune (quattro caratteri)
            - Carattere di controllo, per verificare la correttezza del codice fiscale.
         * 
       
         * 
         */

        //Metodo per effettuare il calcolo del CF
        //è void perché altera lo stato dell'oggetto
        public void CreateSsn()
        {
            string ris = "";

            //il contatore può aiutare nei casi di nommi/cognomi
            //aventi meno consonanti
            int counter = 0;


            //AGGIUNTA CONSONANTI COGNOME


            if (Consonanti(LastName).Length >= 3)
                ris += Consonanti(LastName)[0] + "" + Consonanti(LastName)[1] + "" + Consonanti(LastName)[2];
            else if (Consonanti(LastName).Length == 2)
            {
                ris += Consonanti(LastName);

                for (int i = 0; i < LastName.Length; i++)
                {
                    if (!Consonante(LastName[i].ToString()))
                    {
                        ris += LastName[i].ToString();
                        break;
                    }
                }
            }
            else if (Consonanti(LastName).Length == 1)
            {
                ris += Consonanti(LastName);

                for (int i = 0; i < LastName.Length; i++)
                {
                    if (counter > 2)
                        break;


                    if (!Consonante(LastName[i].ToString()))
                    {
                        ris += LastName[i].ToString();
                        counter++;
                    }
                }

            }

            //AGGIUNTA CONSONANTI DEL NOME

            //resettiamo il nostro contatore per calcoli successivi
            counter = 0;

            if (Consonanti(FirstName).Length > 4)
            {
                ris += Consonanti(FirstName)[0] + "" + Consonanti(FirstName)[2] + "" + Consonanti(FirstName)[3];

            }

            if (Consonanti(FirstName).Length == 3)
            {
                ris += Consonanti(FirstName);
            }
            else if (Consonanti(FirstName).Length == 2)
            {
                ris += Consonanti(FirstName);

                for (int i = 0; i < FirstName.Length; i++)
                {
                    if (!Consonante(FirstName[i].ToString()))
                    {
                        ris += FirstName[i].ToString();
                        break;
                    }
                }
            }
            else if (Consonanti(FirstName).Length == 1)
            {
                ris += Consonanti(FirstName);

                for (int i = 0; i < FirstName.Length; i++)
                {
                    if (counter > 2)
                        break;


                    if (!Consonante(FirstName[i].ToString()))
                    {
                        ris += FirstName[i].ToString();
                        counter++;
                    }
                }

            }

            //CALCOLO ANNO

            ris += GetYear();

            //AGGIUNTA LETTERA DEL MESE

            ris += GetMonthLetter();

            //AGGIUNTA DEL GIORNO DI NASCITA

            ris += GetDay();


            //AGGIUNTA DEL CODICE CATASTALE

            ris += GetCadastralCode();

            //CODICE DI CONTROLLO(16° CARATTERE)

            ris += GetControlCode(ris).ToString().ToUpper();



            Ssn = ris;

        }//Fine Metodo Calcolo Codice Fiscale




        //Metodo che restituisce le consonanti del cognome
        public string Consonanti(string nomeOCognome)
        {
            string ris = "";

            foreach (char lettera in nomeOCognome)
            {
                if (Consonante(lettera.ToString()))
                {
                    ris += lettera;
                }
            }


            return ris;
        }


        //Metodo d'Appoggio per il controllo della lettera se è consonante
        public bool Consonante(string lettera)
        {
            bool ris = false;

            string vocali = "aeiouy";

            if (!vocali.Contains(lettera.ToLower()))
            {
                ris = true;
            }


            return ris;

        }

        //Metodo per ottenere le ultime due cifre dell'anno
        public string GetYear()
        {

            int anno = Dob.Year;

            string ris = anno.ToString().Substring(2, 2);




            return ris;

        }

        //Metodo per ottenere il giorno
        public string GetDay()
        {
            int ris = 0;



            ris = Dob.Day;

            switch (Gender.ToLower()) //Nel caso l'Utente fosse di sesso Femminile
            {                         //deve essere aggiunto 40 alla parte corrispondente
                case "donna":         // il giorno di nascita  
                case "d":
                case "femmina":
                case "f":
                case "woman":
                case "w":
                case "female":
                    ris += 40;
                    break;

            }

            if (ris >= 1 && ris <= 9)
                return "0" + ris.ToString(); //aggiungiamo lo 0 in caso non sia una decina
            else
                return ris.ToString();
        }

        //Metodo che restituisce la lettera corrispondente al mese di nascita
        public string GetMonthLetter()
        {
            string ris = "";


            int mese = Dob.Month;

            switch (mese)
            {
                case 1:   // Gennaio
                    ris = "A";
                    break;

                case 2:   // Febbraio
                    ris = "B";
                    break;

                case 3:   // Marzo
                    ris = "C";
                    break;

                case 4:   // Aprile
                    ris = "D";
                    break;

                case 5:   // Maggio
                    ris = "E";
                    break;

                case 6:   // Giugno
                    ris = "H";
                    break;

                case 7:   // Luglio
                    ris = "L";
                    break;

                case 8:   // Agosto
                    ris = "M";
                    break;

                case 9:   // Settembre
                    ris = "P";
                    break;

                case 10:   // Ottobre
                    ris = "R";
                    break;

                case 11:   // Novembre
                    ris = "S";
                    break;

                case 12:   // Dicembre
                    ris = "T";
                    break;



            }



            return ris;
        }


        public string GetCadastralCode()
        {

            string ris = "";
            string path = @$".\CodiciCatastali\{Pob[0].ToString().ToUpper()} - CodiciCatastali.txt";

            //Dal Percorso relativo in alto andiamo a prendere a renderlo assoluto(aggiungendo C:/.../)
            //Andiamo a rimpiazzare eventuali righe del percorso assoluto con spazi vuoti
            string fullPath = Path.GetFullPath(path).Replace(@"\bin\Debug\net5.0", "");


            if (File.Exists(fullPath))
            {

                string[] righe = File.ReadAllLines(fullPath);

                foreach (string riga in righe)
                {

                    if (riga.Contains(Pob.ToUpper()))
                    {

                        string[] comuni = riga.Split(";");
                        ris = comuni[0];
                        break;

                    }
                }

            }
            else
            {
                throw new FileNotFoundException("File dati non trovati al percorso: \n" + path);

            }


            return ris;


        }



        public string GetControlCode(string ssn) //1 3                                           
        {                                  //RFCNTN98B24F839
            string ris = "";

            int sommaDispari = 0;
            int sommaPari = 0;
            string lettereDispari = "";
            string letterePari = "";

            for (int i = 0; i < ssn.Length; i++)
            {
                if (i % 2 == 0) //La posizione iesima è pari,
                {               // ma il carattere(nel cf) è dispari
                    lettereDispari += ssn[i];
                }
                else
                {
                    letterePari += ssn[i];
                }
            }

            foreach (char letteraDispari in lettereDispari)
            {
                switch (letteraDispari.ToString().ToLower())
                {
                    case "a":
                    case "0":
                        sommaDispari += 1;
                        break;
                    case "b":
                    case "1":
                        sommaDispari += 0;
                        break;
                    case "c":
                    case "2":
                        sommaDispari += 5;
                        break;
                    case "d":
                    case "3":
                        sommaDispari += 7;
                        break;
                    case "e":
                    case "4":
                        sommaDispari += 9;
                        break;
                    case "f":
                    case "5":
                        sommaDispari += 13;
                        break;
                    case "g":
                    case "6":
                        sommaDispari += 15;
                        break;
                    case "h":
                    case "7":
                        sommaDispari += 17;
                        break;
                    case "i":
                    case "8":
                        sommaDispari += 19;
                        break;
                    case "j":
                    case "9":
                        sommaDispari += 21;
                        break;
                    case "k":
                        sommaDispari += 2;
                        break;
                    case "l":
                        sommaDispari += 4;
                        break;
                    case "m":
                        sommaDispari += 18;
                        break;
                    case "n":
                        sommaDispari += 20;
                        break;
                    case "o":
                        sommaDispari += 11;
                        break;
                    case "p":
                        sommaDispari += 3;
                        break;
                    case "q":
                        sommaDispari += 6;
                        break;
                    case "r":
                        sommaDispari += 8;
                        break;
                    case "s":
                        sommaDispari += 12;
                        break;
                    case "t":
                        sommaDispari += 14;
                        break;
                    case "u":
                        sommaDispari += 16;
                        break;
                    case "v":
                        sommaDispari += 10;
                        break;
                    case "w":
                        sommaDispari += 22;
                        break;
                    case "x":
                        sommaDispari += 25;
                        break;
                    case "y":
                        sommaDispari += 24;
                        break;
                    case "z":
                        sommaDispari += 23;
                        break;

                }


            }

            foreach (char letteraPari in letterePari)
            {
                switch (letteraPari.ToString().ToLower())
                {
                    case "a":
                    case "0":
                        sommaPari += 0;
                        break;
                    case "b":
                    case "1":
                        sommaPari += 1;
                        break;
                    case "c":
                    case "2":
                        sommaPari += 2;
                        break;
                    case "d":
                    case "3":
                        sommaPari += 3;
                        break;
                    case "e":
                    case "4":
                        sommaPari += 4;
                        break;
                    case "f":
                    case "5":
                        sommaPari += 5;
                        break;
                    case "g":
                    case "6":
                        sommaPari += 6;
                        break;
                    case "h":
                    case "7":
                        sommaPari += 7;
                        break;
                    case "i":
                    case "8":
                        sommaPari += 8;
                        break;
                    case "j":
                    case "9":
                        sommaPari += 9;
                        break;
                    case "k":
                        sommaPari += 10;
                        break;
                    case "l":
                        sommaPari += 11;
                        break;
                    case "m":
                        sommaPari += 12;
                        break;
                    case "n":
                        sommaPari += 13;
                        break;
                    case "o":
                        sommaPari += 14;
                        break;
                    case "p":
                        sommaPari += 15;
                        break;
                    case "q":
                        sommaPari += 16;
                        break;
                    case "r":
                        sommaPari += 17;
                        break;
                    case "s":
                        sommaPari += 18;
                        break;
                    case "t":
                        sommaPari += 19;
                        break;
                    case "u":
                        sommaPari += 20;
                        break;
                    case "v":
                        sommaPari += 21;
                        break;
                    case "w":
                        sommaPari += 22;
                        break;
                    case "x":
                        sommaPari += 23;
                        break;
                    case "y":
                        sommaPari += 24;
                        break;
                    case "z":
                        sommaPari += 25;
                        break;

                }



            }

            double totale = (sommaDispari + sommaPari) % 26;

            switch (totale)
            {
                case 0:
                    ris = "A";
                    break;

                case 1:
                    ris = "B";
                    break;

                case 2:
                    ris = "C";
                    break;

                case 3:
                    ris = "D";
                    break;

                case 4:
                    ris = "E";
                    break;

                case 5:
                    ris = "F";
                    break;

                case 6:
                    ris = "G";
                    break;

                case 7:
                    ris = "H";
                    break;

                case 8:
                    ris = "I";
                    break;

                case 9:
                    ris = "J";
                    break;

                case 10:
                    ris = "K";
                    break;

                case 11:
                    ris = "L";
                    break;

                case 12:
                    ris = "M";
                    break;

                case 13:
                    ris = "N";
                    break;

                case 14:
                    ris = "O";
                    break;

                case 15:
                    ris = "P";
                    break;

                case 16:
                    ris = "Q";
                    break;

                case 17:
                    ris = "R";
                    break;

                case 18:
                    ris = "S";
                    break;

                case 19:
                    ris = "T";
                    break;

                case 20:
                    ris = "U";
                    break;

                case 21:
                    ris = "V";
                    break;

                case 22:
                    ris = "W";
                    break;

                case 23:
                    ris = "X";
                    break;

                case 24:
                    ris = "Y";
                    break;

                case 25:
                    ris = "Z";
                    break;





            }

            return ris;
        }

    }



}

