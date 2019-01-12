using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Projekt
{
    [Serializable]
    public class StationList : ICloneable
    {
        string name;
        [Key]
        public int StationListID { get; set; }
        //public List<WeatherStation> list;
        public virtual List<WeatherStation> list { get; set; }


        public void Add(WeatherStation w)
        {
            list.Add(w);
        }
  
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public StationList()
        {
            list = new List<WeatherStation>();
            Name = "";
        }
        public StationList(string s)
        {
            list = new List<WeatherStation>();
            Name = s;
        }
        public void SaveXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(StationList));
            StreamWriter sw = new StreamWriter(filename);
            serializer.Serialize(sw, this);
            sw.Close();
        }
        public static Object LoadXML(string filename)
        {
            StationList readList;
            try
            {
                TextReader tr = new StreamReader(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(StationList));
                readList = (StationList)serializer.Deserialize(tr);
                tr.Close();
                return readList;
            }
            catch (FileNotFoundException)
            {
                
                Console.WriteLine("Plik {0} nie istnieje!!!", filename);
            }
            return null;
        }
        public object Clone()
        {
            StationList copy = new StationList(name);
            foreach(WeatherStation x in list)
            {
                copy.list.Add((WeatherStation)x.Clone());
            }

            return copy;
        }

       public void Sortuj()
        {
            list.Sort();
        }

        public void ZapiszDoBazy()
        {
            using (var db = new Model1())
            {
                db.StationListBaza.Add(this);
                db.SaveChanges();


            }
        }

        public static void WypiszListe()
        {
            using (var db = new Model1())
            {
                Console.WriteLine(db.StationListBaza.FirstOrDefault());

            }
        }

        public void MeasureAll()
        {
            Console.WriteLine("==================");
            foreach (WeatherStation x in list)
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.MeasureAll());
                Console.WriteLine("------------------");

            }
            Console.WriteLine("==================");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(WeatherStation x in list)
            {
                sb.AppendLine(x.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
