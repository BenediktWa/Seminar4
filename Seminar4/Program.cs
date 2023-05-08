/*
 * S4/V4: Datei-Handling mit FileStream- und StreamReader-/
 *        StreamWriter-Klasse
 * Autor: LG
 * Erstellt: Mai 2023
 */
using Sem4_DateiHandling_PersonenKlassen;
using System.IO;

namespace Sem4_DateiHandling_PersonenKlassen
{
    internal class Program
    {
        const int MAXCOUNT = 10;
        static void Main(string[] args)
        {
            int count = 0; //Zähler für Studierenden-Datensätze
            string famName;
            int mNo, cCode, actCount = 0;
            string course, city, street;
            char inputChar;

            Student[] students = new Student[MAXCOUNT];

            bool ok = ReadFromFile(ref students, ref actCount);
            if(ok) PrintData(students);

            do
            {
                if (actCount < MAXCOUNT)
                {
                    ReadDataSetFromConsole(ref students, ref actCount);

                    Console.Write("\n Einen weiteren Datensatz [j/n]? ");
                    inputChar = (char)Console.Read();
                    if (inputChar == 'n') break;

                }
                else
                {
                    Console.WriteLine("\n\t Speicherlimit!");
                    break;
                }
            } while (true);

            PrintData(students);
            if (actCount > 0) //Es sind Datensätze vorhanden
            {
                WriteToFile(students, actCount);//Speichern der Datensätze in einer Datei
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Lesen eines Studierendendatensatzes von der Konsole und
        /// Erzeugen eines neuen Studierendenobjekts
        /// </summary>
        /// <param name="students">Array</param>
        /// <param name="count">aktuelle Anzahl</param>
        private static void ReadDataSetFromConsole(ref Student[] students, ref int count)
        {
            {

                string famName;
                int mNo, cCode;
                string course, city, street;
                bool ok = true;

                do
                {
                    if (count < MAXCOUNT)
                    {
                        //Einlesen der Parameter über die Konsole
                        do
                        {
                            Console.Write("\n Familienname: ");
                            famName = Console.ReadLine();
                            if (famName != null && famName.Length > 2) ok = true;
                            else
                            {
                                ok = false;
                                Console.WriteLine("\n Fehler: ");
                            }
                        } while (!ok);

                        do
                        {
                            Console.Write("\n Wohnort: ");
                            city = Console.ReadLine();
                            if (city != null && city.Length > 3)
                                ok = true;
                            else
                            {
                                ok = false;
                                Console.WriteLine("\n Fehler: ...");
                            }
                        } while (!ok);

                        do
                        {
                            Console.Write("\n PLZ: ");
                            string codeString = Console.ReadLine();
                            ok = Int32.TryParse(codeString, out cCode);
                            if (ok && codeString.Length == 5) ok = true;
                            else ok = false;
                            if (!ok)
                            {
                                Console.WriteLine("\n Fehler:  ");
                            }

                        } while (!ok);

                        do
                        {
                            Console.Write("\n Straße und Hausnummer: ");
                            street = Console.ReadLine();
                            if (street != null && street.Length > 5)
                                ok = true;
                            else
                            {
                                ok = false;
                                Console.WriteLine("\n Fehler: ...");
                            }
                        } while (!ok);

                        do
                        {
                            Console.Write("\n Studiengang: ");
                            course = Console.ReadLine();
                            if (course != null && course.Length > 5)
                                ok = true;
                            else
                            {
                                ok = false;
                                Console.WriteLine("\n Fehler: ...");
                            }
                        } while (!ok);

                        students[count++] = new Student(famName, course, new Address(city, cCode, street));
                    }
                } while (!ok);
            }

        }

        /// <summary>
        /// Ausgabe aller Studierendendatensätze
        /// </summary>
        /// <param name="students"></param>
        private static void PrintData(Student[] students)
        {
            Console.WriteLine("\n\t****** Ausgabe der Studierendandatensätze *****");
            Console.WriteLine("\n\t{0,-20}{1,-20}{2,-30}", " Matrikelnummer", "Familienname", "Studiengang");

            foreach (Student student in students)

                if (student != null) //Damit kein Problem entsteht mit den leeren Datensätzen
                    Console.WriteLine("\\n\\t{0,-20}{1,-20}{2,-30}",
                    student.MatrNo,
                    student.FamName,
                    student.Course);
                else break;
            
        }

        /// <summary>
        /// Speichern aller Studierendendatensätzen in einer Textdatei
        /// (strukturierte Daten)
        /// </summary>
        /// <param name="students">Array</param>
        /// <param name="count">Aktuelle Anzahl</param>
        private static void WriteToFile(Student[] students, int count)
        {
            string path = @"..\..\students.txt"; //Verwendung von relativen Pfaden!
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {


                using (StreamWriter sw = new StreamWriter(fs))
                {



                    for (int i = 0; i < count; i++)
                    {
                        sw.Write("{0,15},{1,20},{2,20},{3,20},{4,20},{5,10}#",//Ist bei der Formatierung auch ohne Komma möglich, mit Komma wird das Komme mit gespeichert
                            students[i].MatrNo,
                            students[i].FamName,
                            students[i].Course,
                            students[i].Adr.Residence,
                            students[i].Adr.Street,
                            students[i].Adr.PostalCode);
                        sw.Flush(); //jeder Datensatz wird sofort geschrieben, ansonsten wird erst der Puffer gefüllt und dann erst geschrieben
                    }
                }
            }
            //sw.Close(); kann weg gelassen werden bei using() sehe Zeile 170,174
            //fs.Close();
        }

        /// <summary>
        /// Lesen aus einer Datei
        /// </summary>
        /// <param name="student"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static bool ReadFromFile(ref Student[] students, ref int count)
        {
            bool ok = false;
            string path = @"..\..\students.txt";
            /*Verwendung von relativen Pfaden! //@, damit man den \ verwenden kann 
             * als Verzeihnistrenner (entschärft die Bedeutung des \
             */
            string line = "";
            int number;


            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.CanRead) //Prüfen auf Lesbarkeit
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            //Lesen des gesamten Dateiinhalts
                            line = sr.ReadLine();
                            string[] studentStringArray = line.Split('#'); //Split gibt String Arrays zurück, unterteilt nach dem #
                            //Durch das # am Ende erhält das Array Datensätze
                            foreach (string studentString in studentStringArray)
                            {
                                string[] items = studentString.Split(','); //Split nach dem Komma //Aufgrund der Überladung egal ob "," oder ','

                                if (count < MAXCOUNT && !string.IsNullOrEmpty(items[0])) //oder if (count < MAXCOUNT && items[0] != "") => um nicht den letzten leeren Datensatz mit ab zu speichern
                                {
                                    students[count] = new Student(); //Erstellung eines neuen Objektes
                                    ok = Int32.TryParse(items[0].Trim(), out number); //Trim entfernt vor und nachlaufende Leerzeichen

                                    if (ok)
                                    {
                                        students[count].MatrNo = number;
                                        students[count].FamName = items[1].Trim();
                                        students[count].Course = items[2].Trim();
                                        students[count].Adr.Residence = items[3].Trim();
                                        students[count].Adr.Street = items[4].Trim();

                                        ok = Int32.TryParse(items[5].Trim(), out number);
                                        if (ok) students[count].Adr.PostalCode = number;
                                        else break;

                                        count++;
                                    }//if

                                    else break;

                                }//if
                                
                            }//foreach
                            
                        }//while !eof

                    }//Streamreader

                }//Lesbarkeit

            }//Filestream
                return ok;
        }//ReadFromFile

    }//Program

}//Namespace


