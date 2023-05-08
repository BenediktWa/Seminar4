/*
 * V3: Einfache Klasse für Adressobjekte
 * Autor: LG
 * Erstellt: April 2023
 */

namespace Sem4_DateiHandling_PersonenKlassen
{
    public class Address
    {
        private string residence;
        private string street;
        private int postalCode;
        //Haus-/Appartment-Nummer
        //Bundesland
        //...

        public Address()
        {
            Residence = "not assigned!";
            Street = "not assigned!";
            PostalCode = 99999;
        }

        public Address(
				string residence, 
				int postalCode, 
				string street)
        {
            Residence = residence;
            Street = street;
            PostalCode = postalCode;
        }

        public string Residence
        {
            get => residence;
            set
            {
                if (value != null && value.Length > 2)
                    residence = value;
                else
                    residence = "not assigned!";
            }
        }
        public string Street
        {
            get => street;
            set
            {
                if (value != null && value.Length > 2)
                    street = value;
                else
                    street = "not assigned!";
            }
        }
        public int PostalCode
        {
            get => postalCode;
            set
            {
                /*
                 * Kleinste PLZ in D. --> Dresden (01067)
                 * Größte PLZ in D. 
				 * 		--> Körner und Weinbergen (99998)
                 */
                if (value >= 1067 && value < 99999)
                    postalCode = value;
                else
                    postalCode = 99999;
            }
        }
    }
}
