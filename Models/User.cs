using System;
using System.Collections.Generic;

namespace ProjectWork.Models
{
    public class User
    {
        //Dati Anagrafici e Recapiti
        public string Ssn { get; set; } //SSN(Codice Fiscale) questa sarà la nostra pk
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Pob { get; set; } //Place of Birth
        public string Address { get; set; } //Indirizzo di Residenza
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //Algoritmi ottenutti dall'invio della password
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }

        //Lista dei post per la relazioone 1-N
        public List<Post> Posts { get; set; }





        /*
         * 
         * 
         *  - le prime tre lettere del codice fiscale sono prese dal cognome (solitamente prima, seconda e terza consonante)
            - le seconde tre dal nome (solitamente prima, terza e quarta consonante)
            - le ultime due cifre dell'anno di nascita
            - una lettera per il mese (A = Gennaio, B, C, D, E, H, L, M, P, R, S, T = Dicembre)
            - il giorno di nascita: in caso di sesso femminile si aggiunge 40 per cui è chiaro che se si trova scritto, ad esempio, 52, non può che trattarsi di una donna nata il 12 del mese.
            - Codice del comune (quattro caratteri)
            - Carattere di controllo, per verificare la correttezza del codice fiscale.
         * 
       
         * 
         */

        //Metodo per effettuare il calcolo del CF
        public string CreateSsn()
        {
            string ris = "";

            //il contatore può aiutare nei casi di nommi/cognomi
            //aventi meno consonanti
            int counter = 0;

            //CONTROLLO DEL COGNOME

            string lastNameConsonanti = "";

            for (int i = 0; i < LastName.Length; i++)
            {

                if (Consonante(LastName[i]))
                {

                    lastNameConsonanti += LastName[i];

                }


            }

            if (lastNameConsonanti.Length >= 3)
            {
                ris += lastNameConsonanti[0] + lastNameConsonanti[1] + lastNameConsonanti[2];
            }
            else if (lastNameConsonanti.Length == 2)
            {
                ris += lastNameConsonanti; //Vengono inserite le prime 2 lettere
                                           //che sono le uniche consonanti del cognome
                foreach (char lettera in FirstName)
                {
                    if (!Consonante(lettera))
                    {
                        ris += lettera; //Aggiungiamo infine a ris la prima vocale trovata
                        break;          // del cognome
                    }
                }


            }
            else if (lastNameConsonanti.Length == 1)
            {
                ris += lastNameConsonanti; //Aggiungiamo la sola è unica consonante trovata

                foreach (char lettera in FirstName)
                {
                    if (counter > 2)
                        break;

                    if (!Consonante(lettera))  //Aggiungiamo a ris le prime 2 vocali del cognome
                    {
                        ris += lettera;
                        counter++;
                    }


                }
            }

            //CONTROLLO DEL NOME

            //resettiamo il nostro contatore per calcoli successivi
            counter = 0;

            string firstNameConsonanti = "";

            for (int i = 0; i < FirstName.Length; i++)
            {

                if (Consonante(FirstName[i]))
                {

                    firstNameConsonanti += FirstName[i];

                }


            }

            if (firstNameConsonanti.Length >= 3)
            {
                ris += firstNameConsonanti[0] + firstNameConsonanti[1] + firstNameConsonanti[2];
            }
            else if (firstNameConsonanti.Length == 2)
            {
                ris += firstNameConsonanti; //Vengono inserite le prime 2 lettere
                                            //che sono le uniche consonanti del nome
                foreach (char lettera in FirstName)
                {
                    if (!Consonante(lettera))
                    {
                        ris += lettera; //Aggiungiamo infine a ris la prima vocale trovata
                        break;          // del nome
                    }
                }


            }
            else if (firstNameConsonanti.Length == 1)
            {
                ris += firstNameConsonanti; //Aggiungiamo la sola è unica consonante trovata

                foreach (char lettera in FirstName)
                {
                    if (counter > 2)
                        break;

                    if (!Consonante(lettera))  //Aggiungiamo a ris le prime 2 vocali del nome
                    {
                        ris += lettera;
                        counter++;
                    }


                }
            }

            //resettiamo il nostro contatore per calcoli successivi
            counter = 0;


            //CALCOLO ANNO

            ris += GetYear().ToString()[2] + GetYear().ToString()[3];

            //AGGIUNTA LETTERA DEL MESE

            ris += GetMonthLetter();

            //AGGIUNTA DEL GIORNO DI NASCITA

            ris += GetDay().ToString();





            return "";

        }//Fine Metodo Calcolo Codice Fiscale




        public bool Consonante(char lettera)
        {
            bool ris = false;



            if (
                lettera != 'a' &&
                lettera != 'e' &&
                lettera != 'i' &&
                lettera != 'o' &&
                lettera != 'u' &&
                lettera != 'y'
               )
            {
                ris = true;
            }


            return ris;
        }

        public int GetYear()
        {
            int ris = 0;

            //Separiamo la Data dall'Orario, poi prendiamo l'anno
            //separandolo dal resto della data
            string anno = Dob.ToString().Split("T")[0].Split("-")[0];

            ris = int.Parse(anno);

            return ris;
        }

        public int GetDay()
        {
            int ris = 0;

            //Separiamo la Data dall'Orario, poi prendiamo il giorno
            //separandolo dal resto della data
            string giorno = Dob.ToString().Split("T")[0].Split("-")[2];

            ris = int.Parse(giorno);

            switch (Gender.ToLower()) //Nel caso l'Utente fosse di sesso Femminile
            {                         //deve essere aggiunto 40 alla parte corrispondente
                case "donna":         // il giorno di nascita  
                    ris += 40;
                    break;

                case "d":
                    ris += 40;
                    break;

                case "femmina":
                    ris += 40;
                    break;

                case "f":
                    ris += 40;
                    break;

                case "woman":
                    ris += 40;
                    break;

                case "w":
                    ris += 40;
                    break;

                case "female":
                    ris += 40;
                    break;
            }



            return ris;
        }

        //Metodo che restituisce la lettera corrispondente al mese di nascita
        public string GetMonthLetter()
        {
            string ris = "";

            //Separiamo la Data dall'Orario, poi prendiamo il mese
            //separandolo dal resto della data
            string mese = Dob.ToString().Split("T")[0].Split("-")[1];

            switch (mese)
            {
                case "01":   // Gennaio
                    ris = "A";
                    break;

                case "02":   // Febbraio
                    ris = "B";
                    break;

                case "03":   // Marzo
                    ris = "C";
                    break;

                case "04":   // Aprile
                    ris = "D";
                    break;

                case "05":   // Maggio
                    ris = "E";
                    break;

                case "06":   // Giugno
                    ris = "H";
                    break;

                case "07":   // Luglio
                    ris = "L";
                    break;

                case "08":   // Agosto
                    ris = "M";
                    break;

                case "09":   // Settembre
                    ris = "P";
                    break;

                case "10":   // Ottobre
                    ris = "R";
                    break;

                case "11":   // Novembre
                    ris = "S";
                    break;

                case "12":   // Dicembre
                    ris = "T";
                    break;



            }



            return ris;
        }

    }




}

