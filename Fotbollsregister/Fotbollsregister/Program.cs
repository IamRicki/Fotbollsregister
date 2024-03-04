using System;
using System.Collections.Generic;
using System.IO; // Txt
using System.Linq; 
using System.Threading; // Countdown

namespace Fotbollsregister

{
    class Program
    {
        public struct Spelare
        {
            public string spelarNamn;
            public int ålder;
            public double längd;
            public int tröjNummer;
            public string lag;

        }

        public static Spelare[] spelarArray;      


        static void Main(string[] args)       
            
        {
            Meny();
            while (true)
            {
                HämtaData();
                int menyVal;
                while (!int.TryParse(Console.ReadLine(), out menyVal))
                {
                    Console.WriteLine($"Välj ett av följande alternativ: ");
                }

                switch (menyVal)
                {
                    case 0:
                        Meny();
                        break;
                    case 1:
                        Console.Clear();
                        VisaAllaSpelare();
                        break;
                    case 2:
                        Console.Clear();
                        LäggTillSpelare();
                        break;
                    case 3:
                        Console.Clear();
                        TaBortSpelareViaIndex();
                        break;
                    case 4:
                        Console.Clear();
                        ÄndraTröjNummer();
                        break;
                    case 5:
                        Console.WriteLine("Avslutar program...");
                        return;
                    default:
                        Console.WriteLine("Ogiltigt! Ange ett tal mellan 1-6!");
                        break;

                        

                }
            }
            
            
        }
        public static void Meny()
        {
            Console.Clear(); // Rensa konsolfönstret vid varje nytt val för en renare konsol
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║ Välkommen till fotbollregistret!         ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ Välj en åtgärd:                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║ 1. Skriv ut alla spelare                 ║");
            Console.WriteLine("║ 2. Lägg till en spelare                  ║");
            Console.WriteLine("║ 3. Ta bort en spelare                    ║");
            Console.WriteLine("║ 4. Ändra lagnummer på spelare            ║");
            Console.WriteLine("║ 5. Avsluta programmet                    ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

        }
        public static void LäggTillSpelare()
        {
            using (StreamWriter inData = new StreamWriter("spelare.txt", true))
            {
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                    Spelarinformation                                       ║");
                Console.WriteLine("╟────────────────────────────────────────────────────────────────────────────────────────────╢");

                Console.Write("║ Vad heter spelaren (För och efternamn):     ");
                string spelarNamn = Console.ReadLine();

                int ålder;
                bool acceptabelÅlder = false;
                do
                {
                    Console.Write($"║ Hur gammal är {spelarNamn}:                    ");
                    if (int.TryParse(Console.ReadLine(), out ålder))
                    {
                        acceptabelÅlder = true;
                    }
                    else
                    {
                        Console.WriteLine("║ Ogiltig ålder! Ange en giltig ålder.");
                        continue;
                    }
                } while (!acceptabelÅlder);

                double längd;
                bool acceptabelLängd = false;
                do
                {
                    Console.Write($"║ Hur lång är {spelarNamn} (i cm):               ");
                    if (double.TryParse(Console.ReadLine(), out längd))
                    {
                        acceptabelLängd = true;
                    }
                    else
                    {
                        Console.WriteLine("║ Ogiltig längd! Ange en giltig längd.");
                        continue;
                    }
                } while (!acceptabelLängd);

                int tröjNummer;
                bool acceptabelTröjNummer = false;
                do
                {
                    Console.Write($"║ Vilket tröjnummer har {spelarNamn}):               ");
                    if (int.TryParse(Console.ReadLine(), out tröjNummer))
                    {
                        acceptabelTröjNummer = true;
                    }
                    else
                    {
                        Console.WriteLine("║ Ogiltig längd! Ange en giltig längd.");
                        continue;
                    }
                } while (!acceptabelTröjNummer);

                Console.Write($"║ Vilket lag spelar {spelarNamn} i:              ");
                string lag = Console.ReadLine();

                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════╝");


                inData.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t", spelarNamn, ålder, längd, tröjNummer, lag);
            }

        }
        public static void HämtaData()
        {
            // Hämta data från txt filen
            StreamReader inData = new StreamReader("spelare.txt");
            int antalRader = File.ReadLines("spelare.txt").Count(); // Räknar antal rader i txt filen
            spelarArray = new Spelare[antalRader];
            string rad;

            int i = 0;

            while ((rad = inData.ReadLine()) != null) // Loopa igenom varje rad i spelare.txt så länge det finns saker att läsa
            {
                Spelare spelare = new Spelare(); 
                string[] fält = rad.Split('\t'); // Gå igenom rad och splitta för varje tab 
                spelare.spelarNamn = fält[0];
                spelare.ålder = int.Parse(fält[1]);
                spelare.längd = double.Parse(fält[2]);
                spelare.tröjNummer = int.Parse(fält[3]);
                spelare.lag = fält[4];

                spelarArray[i] = spelare;
                i++;
            }
            inData.Close();
        } 

        public static void VisaAllaSpelare()
        {
            int index = 0;
            foreach (Spelare spelare in spelarArray)
            {
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ Index: {index} Spelarnamn: {spelare.spelarNamn,-20} ║ Ålder: {spelare.ålder,-5} ║ Längd: {spelare.längd,-5} ║ Tröjnummer: {spelare.tröjNummer,-5} ║ Lag: {spelare.lag,-16} ║"); // specificera viss utrymmesbredd
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

                index++;
            }

        }

        public static void SparaData()
        {
            using (StreamWriter data = new StreamWriter("spelare.txt"))
            {
                foreach (Spelare enspelare in spelarArray)
                {
                    data.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t", enspelare.spelarNamn, enspelare.ålder, enspelare.längd, enspelare.tröjNummer, enspelare.lag);
                }
            }
            Console.WriteLine("Informationen har sparats!");

        }

        public static void TaBortSpelareViaIndex()
        {
            Console.WriteLine("Mata in index på den spelare du vill ta bort: ");
            int spelarNummer;
            if (int.TryParse(Console.ReadLine(), out spelarNummer))
            {
                if (spelarNummer >= 0 && spelarNummer < spelarArray.Length) // Kontrollera om indexet är inom arrayens gränser
                {
                    Spelare[] nySpelarArray = new Spelare[spelarArray.Length - 1]; // Skapa en ny array med ett element mindre än den befintliga arrayen
                    int index = 0;
                    for (int i = 0; i < spelarArray.Length; i++)
                    {
                        if (i != spelarNummer)
                        {
                            nySpelarArray[index] = spelarArray[i];
                            index++;
                        }
                    }
                    spelarArray = nySpelarArray; // Uppdatera spelarArray till den nya arrayen utan den borttagna spelaren
                    SparaData(); // Spara den uppdaterade spelarlistan till filen
                    Console.WriteLine("Spelaren har tagits bort.");
                }
                else
                {
                    Console.WriteLine("Ogiltigt index! Försök igen.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt index! Försök igen.");
            }

            Thread.Sleep(3000); // Vänta 3 sekunder
            Meny(); // Återgå till huvudmenyn
        }


        public static void ÄndraTröjNummer()
        {
            Console.WriteLine("Vilken spelare vill du ändra tröjnummer på: ");
            string spelareAttBytaNummerPå = Console.ReadLine();

            for (int i = 0; i < spelarArray.Length; i++)
            {
                if (spelarArray[i].spelarNamn == spelareAttBytaNummerPå)
                {
                    Console.WriteLine("Vilket nummer vill du byta till: ");
                    int nyttTröjNummer;
                    if (int.TryParse(Console.ReadLine(), out nyttTröjNummer))
                    {
                        spelarArray[i].tröjNummer = nyttTröjNummer; // Konvertera nyttTröjNummer till sträng
                        break; // Avsluta loopen när spelaren hittas och tröjnumret ändras
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt tröjnummer! Försök igen.");
                        return; // Återgå till huvudmenyn om ett ogiltigt tröjnummer angavs
                    }
                }
            }

            SparaData(); // Spara uppdaterad data efter att tröjnumret har ändrats
            Thread.Sleep(3000); // Vänta 3 sekunder
            Meny(); // Återgå till huvudmenyn
        }

    }

}
