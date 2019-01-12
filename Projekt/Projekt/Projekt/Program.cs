//System pogoda - rejestrujący zjawiska pogodowe w wybranych stacjach pomiarowych
//Jadwiga Szkatuła, Anna Światłoń, Krzysztof Wis, Gabriela Wojtas

using System;
using System.Text;

namespace Projekt
{
   class Program
    {
        public Random rnd = new Random(Guid.NewGuid().GetHashCode());
        
        static void Main(string[] args)
        {
            // tworzenie lokalizacji stacji
            Location Kraków = new Location(14.6780, -56.8003, 456, "Kraków");
            Location Warszawa = new Location(20, 30, 300, "Warszawa");
            // sprawdzenie poprawnego wypisywania danych lokalizacyjnych 
            Console.WriteLine(Kraków);

            // tworzenie urządzeń pomiarowych
            Thermometer t1 = new Thermometer("Thermo 3000", 0.01);
            Hygrometer h1 = new Hygrometer("Superhygro", 0.001);
            WindMeter w1 = new WindMeter("Windy 3XXX", 0.01, 2);
           

            // tworzenie stacji, w której zostaną umieszczone urządzenia pomiarowe
            WeatherStation stacjaKrk = new WeatherStation("Pomiarowa 1", Kraków);
            stacjaKrk.Insert(t1);
            stacjaKrk.Insert(h1);
            stacjaKrk.Insert(w1);
            
            // sprawdzenie czy wartości temperatury w danej lokalizacji odpowiadają tym podawanym przez termometr
            Console.WriteLine(Kraków.Temperature);
            // urządzenie pomiarowe zaokrągla wartość temperatury
            Console.WriteLine(stacjaKrk.MeasureAll());

           // tworzenie kopii stacji
            WeatherStation kopia = (WeatherStation)stacjaKrk.Clone();
                  

            Thermometer t2 = new Thermometer("Thermo 4000", 0.001);
            Hygrometer h2 = new Hygrometer("Uberhygro", 0.0001);
            WindMeter w2 = new WindMeter("Windy 5XXX", 0.001, 3);
            
            kopia.Insert(t2);
            // sprawdzenie poprawności wypisywanych danych (w stosunku do stacji stacjaKrk)
            Console.WriteLine(kopia.MeasureAll());

            WeatherStation stacjaWr = new WeatherStation("Warszawska stacja", Warszawa);
            stacjaWr.Insert(h2);
            stacjaWr.Insert(w2);

            // sprawdzanie wyświetlania danych w momencie, gdy w stacji nie ma wszystkich urządzeń pomiarowych
            Console.WriteLine(stacjaWr.MeasureAll());

            // dodawanie stacji do listy stacji

            StationList KompaniaMeteo = new StationList("TVN meteo");
            KompaniaMeteo.Add(stacjaKrk);
            kopia.Insert(new Barometer("Bad Baro", 5));
            kopia.Name = "Bliźniak 1";
            KompaniaMeteo.Add(kopia);
            KompaniaMeteo.Add(stacjaWr);

            // wyświetlanie listy stacji
            KompaniaMeteo.MeasureAll();
            KompaniaMeteo.MeasureAll();

            // sprawdzanie poprawności sortowania stacji
            KompaniaMeteo.Sortuj();
            KompaniaMeteo.MeasureAll();

            // zapisywanie danych stacji do pliku XML
            KompaniaMeteo.SaveXML("saved.xml");
            StationList Odczytana = (StationList)StationList.LoadXML("saved.xml");
            // odczytywanie danych z pliku XML
            Odczytana.MeasureAll();

            ///////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////
            StationList FauxList = new StationList("Has to be full");
            WeatherStation a = new WeatherStation("Pomiarowa 4", Kraków);
            WeatherStation b = new WeatherStation("Pomiarowa 5", Warszawa);
            Thermometer t4 = new Thermometer("Thermo 6000", 0.01);
            Hygrometer h4 = new Hygrometer("Megahygro", 0.001);
            WindMeter w4 = new WindMeter("Windy 3XXX", 0.01, 2);
            Barometer b4 = new Barometer("Deep Pressure", 1);
            Thermometer t5 = new Thermometer("Thermo 3000", 0.01);
            Hygrometer h5 = new Hygrometer("uberhygro", 0.001);
            WindMeter w5 = new WindMeter("Windy 5XXX", 0.01, 2);
            Barometer b5 = new Barometer("Deepest Pressure", 0.6);
            a.Insert(t4);
            a.Insert(h4);
            a.Insert(w4);
            a.Insert(b4);
            b.Insert(t5);
            b.Insert(h5);
            b.Insert(w5);
            b.Insert(b5);
            FauxList.Add(a);
            FauxList.Add(b);
            ///////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////
            int x = 0;


            for (; ; )
            {
                System.Console.Clear();
                Console.WriteLine("Wpisz odpowiednią liczbę by wyświetlić:\n1.Dane stacji Kraków\n2.Dane stacji Warszawa\n3.Wartości urządzeń pomiarowych w stacji Kraków\n4.Wartości urządzeń pomiarowych w stacji Warszawa\n5.Dane ze wszystkich stacji\n6. Prezentacja zapisu do XML. \nAby zakończyć działanie programu wpisz 10.");
                Console.Write("Wybierz odpowiedni numer stacji, by wyświetlić dane pomiarowe: ");
               
               try
                {
                    x = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Brak opcji");
                    continue;
                }

                // lub while (Int32.TryParse(Console.ReadLine(), out x))

                switch (x)
                {
                    case 1:
                        Console.WriteLine(Kraków);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine(Warszawa);
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine(stacjaKrk.Name + " \n" + stacjaKrk.MeasureAll());
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine(stacjaWr.Name + " \n" + stacjaWr.MeasureAll());
                        Console.ReadKey();
                        break;
                    case 5:
                        Odczytana.MeasureAll();
                        Console.ReadKey();
                        break;
                    case 6:
                        FauxList.ZapiszDoBazy();
                        StationList.WypiszListe();
                        Console.ReadKey();
                        break;
                    case 10:
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        break;

                    default:
                        break;
                }

            }
            
        }
    }
}
