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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Runtime.ConstrainedExecution;



namespace CarSales
{
    public class CurrencyEuroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return string.Format(CultureInfo.GetCultureInfo("de-DE"), "{0:C}", decimalValue);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Car> list = new List<Car>();
        private string pathCarsListCSV = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSV Files", "CarsList.csv");
        private string pathReceiptCSV = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSV Files", "receipt.csv");

        public MainWindow()
        {
            InitializeComponent();
            showTheListBTN.Content = "List laden";
            buyCarBTN.Content = "Die Auto Kaufen";
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
            public void Button0_Click(object sender, RoutedEventArgs e)
            {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                MissingFieldFound = null
            };

            using (StreamReader reader = new StreamReader(pathCarsListCSV))
            using (CsvReader csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                list = csv.GetRecords<Car>().ToList();
            }
            MessageBoxResult result = MessageBox.Show("Wollen Sie die Liste Anschauen?", "Bestätigung", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Du hast 'Ja' ausgewählt!");
                    using (var reader = new StreamReader(pathCarsListCSV))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        list = csv.GetRecords<Car>().ToList();
                    }
                var availableCars = list.Where(car => car.IsSold == false).ToList();

                myListView.ItemsSource = availableCars;
            }
                else
                {
                    MessageBox.Show("Du hast 'Nein' ausgewählt! =(");
                }
            }
        public void buyCarClick(object sender, RoutedEventArgs e)
        {
            Car selectedCar = myListView.SelectedItem as Car;

            if (selectedCar == null)
            {
                MessageBox.Show("Bitte wählen Sie zuerst ein Auto aus der Liste aus.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Wollen Sie {selectedCar.Make} {selectedCar.Model} kaufen?",
                "Bestätigung",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                selectedCar.IsSold = true;

                using (var writer = new StreamWriter(pathCarsListCSV))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    csv.WriteRecords(list);

                // ← this is what makes pathReceiptCSV "used"
                using (var writer = new StreamWriter(pathReceiptCSV, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(selectedCar);
                    csv.NextRecord();
                }

                myListView.ItemsSource = list.Where(car => !car.IsSold).ToList();
                MessageBox.Show($"Sie haben erfolgreich {selectedCar.Make} {selectedCar.Model} gekauft!");
            }
            else
            {
                MessageBox.Show("Die Auto wurde nicht gekauft.");
            }
        }



    }
    }


