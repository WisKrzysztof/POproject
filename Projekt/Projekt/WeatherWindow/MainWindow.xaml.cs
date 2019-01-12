using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt;
using System.Collections.ObjectModel;

namespace WeatherWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StationList sl;
        ObservableCollection<WeatherStation> lista;
        WeatherStation current;
        
        public MainWindow()
        {


            InitializeComponent();
            sl = (StationList)StationList.LoadXML("saved.xml");
            sl.Sortuj();                    
            lista = new ObservableCollection<WeatherStation>(sl.list);
            foreach(WeatherStation x in lista)
            {
                if (x.Hygro != null) x.loc = x.Hygro.location;
                if (x.Anemo != null) x.loc = x.Anemo.location;
                if (x.Bar != null) x.loc = x.Bar.location;
                if (x.Thermo != null) x.loc = x.Thermo.location;

                if (x.Hygro != null) x.Hygro.location=x.loc;
                if (x.Anemo != null) x.Anemo.location=x.loc;
                if (x.Bar != null)   x.Bar.location= x.loc;
                if (x.Thermo != null) x.Thermo.location = x.loc;
            }
            
            current = new WeatherStation("Empty", new Location(0, 0, 0, "Empty"));
            listBox.ItemsSource = lista;

            TemperatureBox.Text = "---";
            HumidityBox.Text = "---";
            WindStrengthBox.Text = "---";
            WindDirectionBox.Text = "---";
            PressureBox.Text = "---";
            NameBox.Text = "---";
            LongitudeBox.Text = "---";
            LatitudeBox.Text = "---";
            AltitudeBox.Text = "---";





            // if (lista == null) MessageBox.Show("IS NULL");


            //textBox_nazwa.Text = zespol.Nazwa;
            //textBox_kierownik.Text = zespol.Kierownik.ToString();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            current = (WeatherStation)listBox.SelectedItem;
            NameBox.Text = current.ToString();
            if(current.loc.Latitude<0)
            {
                double tmp = current.loc.Latitude * (-1);
                LatitudeBox.Text = tmp.ToString() + "°S";
            }
            else LatitudeBox.Text = current.loc.Latitude.ToString() + "°N";

            if (current.loc.Longitude < 0)
            {
                double tmp = current.loc.Longitude * (-1);
                LongitudeBox.Text = tmp.ToString() + "°W";
            }
            else LongitudeBox.Text = current.loc.Longitude.ToString() + "°E";

            AltitudeBox.Text = current.loc.Altitude.ToString()+"m";

            TemperatureBox.Text = "Click \"measure\"";
            HumidityBox.Text = "Click \"measure\"";
            WindStrengthBox.Text = "Click \"measure\"";
            WindDirectionBox.Text = "Click \"measure\"";
            PressureBox.Text = "Click \"measure\"";


        }

        private void MeasureButton_Click(object sender, RoutedEventArgs e)
        {
            if (current.Thermo == null) TemperatureBox.Text = "No measurer";
            else TemperatureBox.Text = current.Thermo.Measure().ToString() + "°C";
            if (current.Hygro == null) HumidityBox.Text = "No measurer";
            else {
                double tm = current.Hygro.Measure() * 100;
                HumidityBox.Text = tm.ToString() + "%";
            }
            if (current.Anemo == null) WindStrengthBox.Text = "No measurer";
            else WindStrengthBox.Text = current.Anemo.Measure().strength.ToString() + " km/h";
            if (current.Anemo == null) WindDirectionBox.Text = "No measurer";
            else WindDirectionBox.Text = current.Anemo.Measure().direction.ToString() + "°";
            if (current.Bar == null) PressureBox.Text = "No measurer";
            else PressureBox.Text = current.Bar.Measure().ToString() + "hPa";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            List<Location> lis = new List<Location>();
            foreach(WeatherStation x in lista)
            {
                if (lis.Contains(x.loc) == false) lis.Add(x.loc);
            }
            foreach(Location x in lis)
            {
                x.SimulateTimePassing();
            }
        }
    }
}
