/*
 * Elternklasse
 * Autor: LG
 * Erstellt: April 2023
 */

namespace Sem4_DateiHandling_PersonenKlassen
{
    public class Person
    {
        private string famName;
        Address adr;

        public Person()
        {
            famName = "not Assigned!";
            adr = new Address();
        }
       
        public Person(string famName, Address adr)
        {
            FamName = famName;
            Adr = adr;
        }

        public Person(
            string famName, 
            string city="", 
            string street="", 
            int cityCode=99999)
        {
            FamName= famName;
            Adr = new Address(city,cityCode,street);    
        }

        public string FamName
        {
            get
            {
                return famName;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    famName = value;
                }
            }
        }

        public Address Adr
        {
            get => adr;
            set
            {
                if (value != null)
                    adr = value;
                else
                    adr = new Address();
            }
        }
    }
}
