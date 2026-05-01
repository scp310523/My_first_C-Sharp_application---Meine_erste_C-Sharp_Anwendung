using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Reflection;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using Microsoft.Build.Tasks;
using System.Runtime.Remoting.Lifetime;
using CsvHelper;
using CsvHelper.Configuration;
using System.Security.Cryptography;
using System.Collections;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization.Formatters;

namespace Car_Sale
{
    internal class Program
    {
        private static List<Car> cars = new List<Car>();
        private static List<Car> customersCars = new List<Car>();
        private static string pathCarsListCSV = @".\CSharpFolder\meine_erste_C_Sharp_Anwendung\CarsList.csv";
        private static string pathCustomersCarsCSV = @".\CSharpFolder\meine_erste_C_Sharp_Anwendung\customersCars.csv";
        private static string pathReceiptCSV = @".\CSharpFolder\meine_erste_C_Sharp_Anwendung\receipt.csv";
        private static string pathDeletedCarsList = @".\CSharpFolder\meine_erste_C_Sharp_Anwendung\DeletedCarsList.csv";
        private static string pathTemporalCarsListCSV = @".\CSharpFolder\meine_erste_C_Sharp_Anwendung\TemporalCarsList.csv";
        private static string priceStr;

        static void Main(string[] args1)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            String key;
            String color = "Farblose";
            string make = "Brand";
            String model = "Formula A";
            int doors = 4;
            Int32 minPs = 100;
            int maxPs = 101;
            String condition = "Gute";
            int age = 2020;
            Int32 mileage = 3003;
            decimal price = 0;
            //Decimal priceTotal = 0m;
            bool isSold = false;

            int e = 0;
            

            List<Car> receipt = new List<Car>();
            List<TradeType> tradeType = new List<TradeType>();
            tradeType.Add(new TradeType(1, "Brandneue Auto zu Kaufen"));
            tradeType.Add(new TradeType(2, "Ihr Auto zu verkaufen"));

            //CSV Reader

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
                cars = csv.GetRecords<Car>().ToList<Car>();
            }


            do
            {
                int maxId = cars.Max(car => car.ID);
                Int32 idNext = ++maxId;
                int id = idNext;
                //Anfang
                Console.WriteLine("\n\nWollen Sie ein Auto kaufen oder möchten Sie Ihr Auto verkaufen?\n");

                //Wenn Kaufen
                Console.WriteLine("Bitte drücken Sie:");
                foreach (TradeType item in tradeType)
                    Console.WriteLine($"{item.ID} um {item.Name}");
                Console.Write("\nNummer: ");
                string userInputS = (Console.ReadLine());
                int userInputInt;
                bool isnumber1 = int.TryParse(userInputS, out userInputInt);
                TradeType selectedTradeType = null;

                foreach (TradeType item in tradeType)
                {
                    if (item.ID == userInputInt)
                    {
                        selectedTradeType = item;
                        break;
                    }

                }


                //Auto Kaufen
                if (userInputInt == 1)
                {

                    //Autokatalog
                    Console.WriteLine("\n\nAutokatalog\n" +
                                      "Welche Auto wollen Sie denn kaufen?\n" +
                                      "Bitte drücken Sie:");
                    //Katalog Ausgabe
                    List<Car> availableCars = cars.Where(car => car.IsSold == false).ToList();
                    foreach (Car item in availableCars)
                    {
                        Console.WriteLine($"{item.ID} um {item.Make} {item.Model} " +
                                          $"für {item.Price.ToString("C", CultureInfo.CurrentCulture)}");
                    }
                    Console.WriteLine("zu kaufen");

                    //Personen Eingabe
                    Console.Write("Nummer: ");
                    string customerInput = (Console.ReadLine());
                    int customerInputInt;
                    bool isCustomerInput = int.TryParse(customerInput, out customerInputInt);
                    Car selectedCar = null;

                    // Liste Autos
                    List<Car> soldCars = new List<Car>();

                    for (int i = 0; i < cars.Count; i++)

                    {
                        if (customerInputInt == cars[i].ID && !cars[i].IsSold)
                        {
                            cars[i].IsSold = true;
                            soldCars.Add(cars[i]);
                            selectedCar = cars[i];
                            receipt.Add(selectedCar);
                            Console.WriteLine($"\nSie haben erfolgreich eine Brandneue Auto\n" +
                                              $"Mit folgenden Eigenschaften:" +
                                              $"  Farbe: {cars[i].Color}\n" +
                                              $"  Modell: {cars[i].Model}\n" +
                                              $"  Türenanzahl - {cars[i].Doors}\n" +
                                              $"  Minimale PS - {cars[i].MinPS}\n" +
                                              $"  Maximale PS - {cars[i].MaxPS}\n" +
                                              $"  Für: {cars[i].Price.ToString("C", CultureInfo.CurrentCulture)}\n" +
                                              $" Bei uns gekauft\n" +
                                              $"Vielen Dank für Ihren Kauf bei uns!");
                            break;
                        }
                    }
                    using (var writer = new StreamWriter(pathCarsListCSV))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(cars);
                    }
                }

                //Auto Verkaufen
                else if (userInputInt == 2)
                {
                    Car car = cars[e];

                    e++;

                    Console.WriteLine($"\n\nAutoverkauf\n");
                    Console.WriteLine("Welche Automarke ist Ihr Auto");
                    make = Console.ReadLine();
                    Console.WriteLine("\nIn welchem Zustand ist Ihr Auto");
                    condition = Console.ReadLine();
                    Console.WriteLine("\nWelches Baujahr ist Ihr Auto");
                    age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nWelche Modell hat Ihr Auto");
                    model = Console.ReadLine();
                    Console.WriteLine("\nWas ist die Laufleistung");
                    mileage = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nFür wie viel Euro wollen Sie Ihr Auto verkaufen");
                    price = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine($"\n\nIhr Auto mit folgenden Eigenschaften:\n" +
                                      $" Automarke: {make}\n" +
                                      $" Zustand: {condition}\n" +
                                      $" Model: {model}\n" +
                                      $" Baujahr - {age}\n" +
                                      $" Laufleistung - {mileage}\n" +
                                      $"Wurde erfolgreich\n" +
                                      $"für: {Convert.ToDecimal(price).ToString("C", CultureInfo.CurrentCulture)}\n" +
                                      $"Verkauft!");



                    AddNewCar(color, make, model, doors, minPs, maxPs, condition, age, mileage, price, isSold, pathCarsListCSV);
                }

                else if (userInputInt == 303)
                {
                    Car car = cars[e];
                    e++;

                    Console.WriteLine("\n\nAdministratoreinstellungen\n");
                    Console.WriteLine($"Auto Bearbeiten\n");


                    using (StreamReader reader = new StreamReader(pathCarsListCSV))
                    using (CsvReader csv = new CsvReader(reader, config))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        cars = csv.GetRecords<Car>().ToList();
                    }

                    foreach (Car item in cars)
                    {
                        Console.WriteLine($"Bitte drücken Sie:\n" +
                                          $" {item.ID} um " +
                                          $"{item.Color} {item.Make} {item.Model};\n" +
                                          $"Türenanzahl - {item.Doors}; PS Min/Max - {item.MinPS}/{item.MaxPS};\n" +
                                          $"Zustand: {item.Condition}; Baujahr: {item.Age};\n" +
                                          $"Kilometerstand - {item.Mileage.ToString("N", CultureInfo.CurrentCulture) + " Km"};\n" +
                                          $"Preis: {item.Price.ToString("C", CultureInfo.CurrentCulture)}\n" +
                                          $"Is Sold - {item.IsSold};\n" +
                                          $"zu ändern\n");
                    }

                    //Personen Eingabe
                    Console.Write("Nummer: ");
                    string number = (Console.ReadLine());
                    int numberInt;
                    bool isnumber = int.TryParse(number, out numberInt);


                    for (int i = 0; i < cars.Count; i++)
                    {
                        if (numberInt == cars[i].ID)
                        {
                            //string colorS, makeS, modelS;
                            //decimal priceD;
                            //int doorsI, mnPsI, mxPsI;
                            CarEdit(numberInt, color, make, model, doors, minPs, maxPs, priceStr);
                            Console.WriteLine($"\nErfolgreich aktualisiert!\n");
                            Console.WriteLine($"Nummer - {cars[i].ID}, Farbe: {color},\n" +
                                              $"Marke: {make}, Modell: {model},\n" +
                                              $"Türenanzahl - {doors}, PS: {minPs.ToString("F2", CultureInfo.CurrentCulture)}/{maxPs.ToString("F2", CultureInfo.CurrentCulture) + " PS"}\n" +
                                              $"Preis: {Convert.ToDecimal(price).ToString("C", CultureInfo.CurrentCulture)}");
                            break;
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(pathCarsListCSV))
                    using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(cars);
                    }
                }



                else if (userInputInt == 404)
                {
                    Car car = cars[e];
                    e++;

                    Console.WriteLine("\n\nAdministratoreinstellungen\n");
                    Console.WriteLine($"Auto Hinzufügen\n");

                    Console.Write("Gib eine Farbe ein: ");
                    color = Console.ReadLine();
                    Console.Write("Marke eingeben: ");
                    make = Console.ReadLine();
                    Console.Write("Modell: ");
                    model = Console.ReadLine();
                    Console.Write("Türenanzahl eingeben - ");
                    doors = int.Parse(Console.ReadLine());
                    Console.Write("Minimale Leistung - ");
                    minPs = int.Parse(Console.ReadLine());
                    Console.Write("Maximale Leistung - ");
                    maxPs = int.Parse(Console.ReadLine());
                    Console.Write("Zustand: ");
                    condition = Console.ReadLine();
                    Console.Write("Gib bitte das Baujahr ein - ");
                    age = int.Parse(Console.ReadLine());
                    Console.Write("Laufleistung eigeben: ");
                    mileage = int.Parse(Console.ReadLine());
                    Console.Write("Neuer Preis: ");
                    price = decimal.Parse(Console.ReadLine());
                    Console.Write("Im Autokatalog anzeigen? (Ja/Nein): ");
                    string soldS = Console.ReadLine().ToLower();
                    string soldStr = "Wird in Katalog nicht angezeigt";
                    string NotSoldStr = "Wird in Katalog angezeigt";
                    if (soldS == "nein" || soldS == "no" || soldS == "ni" || soldS == "nö")
                    {
                        Console.WriteLine($"\n\n{soldStr}\n\n");
                        isSold = true;
                    }
                    else
                    {
                        isSold = false;
                        Console.WriteLine($"\n{NotSoldStr}\n");
                    }
                    Console.WriteLine($"Erfolgreich Hinzugefügt!\n              " +
                                      $" Nummer - {cars[e].ID}; Farbe: {car.Color};\n" +
                                      $"Marke: {cars[e].Make}; Modell: {cars[e].Model};\n" +
                                      $"Türenanzahl - {cars[e].Doors}; PS: {cars[e].MinPS.ToString("F2", CultureInfo.CurrentCulture)}/{cars[e].MinPS.ToString("F2", CultureInfo.CurrentCulture) + " PS"}\n" +
                                      $"Preis: {cars[e].Price.ToString("C", CultureInfo.CurrentCulture)}");

                    AddNewCar(color, make, model, doors, minPs, maxPs, condition, age, mileage, price, isSold, pathCarsListCSV);
                }



                else if (userInputInt == 505)
                {
                    using (StreamReader reader = new StreamReader(pathCarsListCSV))
                    using (CsvReader csv = new CsvReader(reader, config))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        cars = csv.GetRecords<Car>().ToList();
                    }

                    foreach (Car item in cars)
                    {
                        Console.WriteLine($"Bitte drücken Sie:\n" +
                                          $" {item.ID} um " +
                                          $"{item.Color} {item.Make} {item.Model};\n" +
                                          $"Türenanzahl - {item.Doors}; PS Min/Max - {item.MinPS}/{item.MaxPS};\n" +
                                          $"Zustand: {item.Condition}; Baujahr: {item.Age};\n" +
                                          $"Kilometerstand - {item.Mileage.ToString("N", CultureInfo.CurrentCulture) + " Km"};\n" +
                                          $"Preis: {item.Price.ToString("C", CultureInfo.CurrentCulture)}\n" +
                                          $"Is Sold - {item.IsSold};\n" +
                                          $"zu löschen\n");
                    }
                    //Personen Eingabe
                    Console.Write("Nummer: ");
                    string number = (Console.ReadLine());
                    int numberInt;
                    bool isnumber = int.TryParse(number, out numberInt);
                    DeleteCar(numberInt);
                }



                Console.WriteLine("\n\nMöchten Sie vielleicht noch etwas?\n\n" +
                                  "Bitte drücken Sie:\n" +
                                  "1 um fortzusetzen\n" +
                                  "      oder\n" +
                                  "2 um diese Anwendung zu schließen");
                Console.Write("\nNummer: ");
                key = Console.ReadLine();

            } while (key == "1");

            using (StreamWriter writer = new StreamWriter(pathCustomersCarsCSV))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(customersCars);
            }

            using (StreamWriter writer = new StreamWriter(pathReceiptCSV))
            using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(receipt);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="colorS"></param>
        /// <param name="makeS"></param>
        /// <param name="modelS"></param>
        /// <param name="doorsI"></param>
        /// <param name="mnPsI"></param>
        /// <param name="mxPsI"></param>
        /// <param name="priceS"></param>
        private static void CarEdit(int id, string colorS, string makeS, string modelS, int doorsI, int mnPsI, int mxPsI, string priceS)
        {
            Car EditedCar = cars.FirstOrDefault(car => car.ID == id);


            if (EditedCar != null) {
                Console.Write("Neue Farbe: ");
                colorS = (Console.ReadLine());
                EditedCar.Color = colorS;
                Console.Write("Neue Marke: ");
                makeS = (Console.ReadLine());
                EditedCar.Make = makeS;
                Console.Write("Neues Modell: ");
                modelS = (Console.ReadLine());
                EditedCar.Model = modelS;
                Console.Write("Türenanzahl - ");
                string doorsS = (Console.ReadLine());
                bool isDoors = int.TryParse(doorsS, out doorsI);
                EditedCar.Doors = doorsI;
                Console.Write("Minimale Leistung - ");
                string mnPsS = (Console.ReadLine());
                bool isMnPs = int.TryParse(mnPsS, out mnPsI);
                EditedCar.MinPS = mnPsI;
                Console.Write("Maximale Leistung - ");
                string mxPsS = (Console.ReadLine());
                bool isMxPs = int.TryParse(mxPsS, out mxPsI);
                EditedCar.MaxPS = mxPsI;
                Console.Write("Zustand eingeben: ");
                EditedCar.Condition = (Console.ReadLine());
                Console.Write("Baujahr eingeben - ");
                string ageS = (Console.ReadLine());
                int ageI;
                bool isAge = int.TryParse(ageS, out ageI);
                EditedCar.Age = ageI;
                Console.Write("Kilometerstand - ");
                string mileageS = (Console.ReadLine());
                int mileageI;
                bool isMileage = int.TryParse(mileageS, out mileageI);
                EditedCar.Mileage = mileageI;
                Console.Write("Neuer Preis: ");
                priceS = (Console.ReadLine());
                decimal priceD;
                bool isPrice = decimal.TryParse(priceS, out priceD);
                EditedCar.Price = priceD;
                Console.Write("Im Autokatalog anzeigen? (Ja/Nein): ");
                string soldS = (Console.ReadLine().ToLower());
                string soldStr = "Wird in Katalog nicht angezeigt";
                string NotSoldStr = "Wird in Katalog angezeigt";
                if (soldS == "nein" || soldS == "no" || soldS == "ni" || soldS == "nö")
                {

                    EditedCar.IsSold = true;
                    Console.WriteLine($"\n\n{soldStr}\n");
                }
                else
                {
                    EditedCar.IsSold = false;
                    Console.WriteLine($"\n{NotSoldStr}");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathCarsListCSV"></param>
        /// <param name="pathTemporalCarsListCSV"></param>
        /// <param name="pathDeletedCarsList"></param>
        /// <param name="numberInt"></param>
        private static void DeleteCar(int ID)
        {
            Car carToDelete = cars.FirstOrDefault(car => car.ID == ID);

            if (carToDelete != null)
            {
                cars.Remove(carToDelete);
                using (var writer = new StreamWriter(pathTemporalCarsListCSV))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(cars);
                }
                System.IO.File.Delete(pathCarsListCSV);
                System.IO.File.Move(pathTemporalCarsListCSV, pathCarsListCSV);
                using (var writer = new StreamWriter(pathDeletedCarsList, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(carToDelete);
                    csv.NextRecord();
                }
                Console.WriteLine($"\nDie Auto\n" +
                                  $" Mit folgenden Eigenschaften:\n" +
                                  $"  Farbe: {carToDelete.Color}\n" +
                                  $"  Modell: {carToDelete.Model}\n" +
                                  $"  Türenanzahl - {carToDelete.Doors}\n" +
                                  $"  PS: {carToDelete.MinPS.ToString("F2", CultureInfo.CurrentCulture)}/{carToDelete.MaxPS.ToString("F2", CultureInfo.CurrentCulture) + " PS"}\n" +
                                  $"  Für: {carToDelete.Price.ToString("C", CultureInfo.CurrentCulture)}\n" +
                                  $" Wurde entfernt\n" +
                                  $");");
            }
            else
            {
                Console.WriteLine("Kein Auto mit dieser ID gefunden.");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param><
        /// <param name="color"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="color"></param>
        /// <param name="color"></param>
        /// <param name="color"></param>
        /// <param name="condition"></param>
        /// <param name="age"></param>
        /// <param name="mileage"></param>
        /// <param name="price"></param>
        ///// <param name="totalPrice"></param>
        ///<param name="isSold"></param>
        /// <param name="pathUCarsListCSV"></param>
        private static void AddNewCar(string color, string make, string model,int doors, int minPs, int maxPs, string condition, int age, int mileage, decimal price, /*decimal totalPrice, */bool isSold, string pathUCarsListCSV)
        {
            Car customersCarsSale = new Car(make, condition, model, age, mileage, price);
            customersCars.Add(customersCarsSale);

            using (var writer = new StreamWriter(pathUCarsListCSV, append: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                int maxId = cars.Max(car => car.ID);
                Int32 idNext = ++maxId;
                var record = new
                {
                    ID = idNext,
                    Color = color,
                    Make = make,
                    Model = model,
                    Doors = doors,
                    MinPS = minPs,
                    MaxPS = maxPs,
                    Condition = condition,
                    Age = age,
                    Mileage = mileage,
                    Price = price,
                    IsSold = isSold
                };
                csv.WriteRecord(record);
                csv.NextRecord();
            }
        }
    }
}
