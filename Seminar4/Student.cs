using System;
/*
 * Vorlesung3: Einfache Klasse für Studierendenobjekte
 *          mit Feldern (Instanzvariable), Properties 
 *          mit value-Überprüfung in den set-Methoden und 
 *          Konstruktoren und
 *          Memberobjekt Adresse
 * Autor: LG
 * Version: April 2023
 */
namespace Sem4_DateiHandling_PersonenKlassen
{
    public class Student: Person
    {
        //fields, Instanz- oder Klassenvariable
        private static int matriculationNo = 1000000;
        private string course;
        private int matrNo;

        public Student():base()
        {
            MatrNo = 9999999;
            Course = "not assigned";
        }

        public Student(
            string famName,
            string course,
            string city="",
            int ccode=99999,
            string street = "") :base(famName,city,street,ccode)
        {
            MatrNo = GetMatriculationNo();
            Course = course;
        }
        
        public Student(
            string famName,
            string course):base(famName)
        {
            MatrNo = GetMatriculationNo(); ;
            Course = course;
        }
        
        public Student(
            string famName,
            string course,
            Address address):base(famName,address)
        {
            //ReadLastMatriculationNo()
            MatrNo = GetMatriculationNo(); ;
            //WriteLastMatrikulationNo
            Course = course;
        }

        //Properties
        
        public int MatrNo
        {
            get => matrNo;
            set
            {
                //if (value > 1000000)
                if(value.ToString().Length == 7)
                {
                    matrNo = value;
                    matriculationNo = value;
                }
            }
        }
        public string Course
        {
            get => course;
            set
            {
                if (value != null && value.Length > 0)
                {
                    course = value;
                }
            }
        }

        public int GetMatriculationNo()
        {
            return ++matriculationNo;
        }

        private void WriteLastMatriculationNo()
        {
            matriculationNo = matrNo;
            string path = @"..\..\lastMatriculationNo.txt";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(matriculationNo);
                    sw.Flush();
                }
            }
        }

    }
}
