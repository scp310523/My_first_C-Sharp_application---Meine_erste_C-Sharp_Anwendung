using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace meine_erste_C_Sharp_anwendung
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // while (key press)
            string key = "";
            char Euro = '€';
            int zero = 0;
            string nous = "Claude Monet";
            string selectedItems = "";
            decimal selectedPrice = 0m;

            ///Hauptspeise
            ///List<FoodType> list = new List<FoodType>();
            List<Food> listStarters = new List<Food>();
            List<Food> listMainCourse = new List<Food>();
            List<Food> listDesserts = new List<Food>();
            List<Food> listDrinks = new List<Food>();
            List<Food> listAlkohol = new List<Food>();

            listStarters.Add(new Food(1, "Vanille-Spargel mit Kerbelbutter und Fougasse", "Tolles Aromenspiel! Der Spargel wird mit Vanille und Zitrone gekocht – darüber kommt Kerbel-Butter. \nUnd das provenzalische Hefebrot ist super fluffig.", 14.50m));
            listStarters.Add(new Food(2, "Gefüllte Fleischtomaten à la Niçoise", "Verführerisch: Je größer der Paradiesapfel, desto mehr geht rein – Thunfisch, Rauke, Petersilie, Kapern ...", 13.80m));
            listStarters.Add(new Food(3, "Apfeltarte mit Brie und Walnüssen", "Kombiniert mit Brie und Karamell-Nüssen ist diese Apfeltarte aus der Normandie ein echtes \"Ich will noch was!\"-Dessert.", 12.10m));
            listStarters.Add(new Food(4, "Blauschimmel-Obatzter", "Blauschimmel-Obatzter ist zum Verlieben!", 9.70m));
            listStarters.Add(new Food(5, "Pissaladière", "Mit Zwiebel, Oliven und Sardellen", 7.40m));

            listMainCourse.Add(new Food(6, "Tarte Normande au Calvados", "", 28.50m));
            listMainCourse.Add(new Food(7, "Oeuf-Poche-Aux-Giroles", 22.60m));
            listMainCourse.Add(new Food(8, "Quiche aux légumes", 15.20m));
            listMainCourse.Add(new Food(9, "Coupe Nymphéas", 10.80m));
            listMainCourse.Add(new Food(10, "Salat Monet", 9.80m));

            listDesserts.Add(new Food(11, "Tarte Tatin mit Blätterteig", 5.10m));
            listDesserts.Add(new Food(12, "Französische Crepes", 4.40m));
            listDesserts.Add(new Food(13, "Mousse Au Chocolat", 3.90m));
            listDesserts.Add(new Food(14, "Millefeuille", 3.40m));
            listDesserts.Add(new Food(15, "Flan Caramel", 2.90m));

            listDrinks.Add(new Food(16, "Mosquito", 4.10m));
            listDrinks.Add(new Food(17, "Provence Cocktail", 3.50m));
            listDrinks.Add(new Food(18, "Erdbeerbowle mit dem sagenhaften Aroma", 3.20m));
            listDrinks.Add(new Food(19, "Säfte (Mango/Trauben/Erdbeere/Granatapfel)", 2.80m));
            listDrinks.Add(new Food(20, "Wasser (Still/Sprudel/Minerall/aromatisiert)", 0.50m));

            listAlkohol.Add(new Food(21, "Weine(Bordeaux/Chablis/Chardonnay/Châteauneuf-du-Pape)", 7.90m));
            listAlkohol.Add(new Food(22, "Champagner(Cattier/Armand de Brignac/Louis Roederer)", 6.10m));
            listAlkohol.Add(new Food(23, "Apfelwein / Cidre", 5.60m));
            listAlkohol.Add(new Food(24, "Prosecco", 5.20m));
            listAlkohol.Add(new Food(25, "Cognac", 4.50m));

            Dictionary<FoodType, List<Food>> foods = new Dictionary<FoodType, List<Food>>();
            foods.Add(new FoodType(1, "Vorspeisen"), listStarters);
            foods.Add(new FoodType(2, "Hauptspeisen"), listMainCourse);
            foods.Add(new FoodType(3, "Nachspeisen"), listDesserts);
            foods.Add(new FoodType(4, "Getränke"), listDrinks);
            foods.Add(new FoodType(5, "Alkohol"), listAlkohol);

            List<Food> order = new List<Food>();

            do
            {
                do
                {
                    //Menü
                    Console.WriteLine();
                    Console.WriteLine($"{nous} Menu: ");
                    Console.WriteLine();
                    Console.WriteLine($"Was wollen Sie bestellen? ");
                    
                    // Kathegorie Ausgabe
                    foreach (var item in foods)
                        Console.WriteLine($"drücken Sie {item.Key.ID} um {item.Key.Name} zu wählen ");

                    //Personen Eingabe
                    Console.Write("Nummer: ");
                    string number = (Console.ReadLine());
                    int numberInt;
                    bool isnumber = int.TryParse(number, out numberInt);

                    if (isnumber)
                    {
                        if (foods.FirstOrDefault(x => x.Key.ID == numberInt).Key != null)
                        {
                            // Liste Essen
                            List<Food> foodItem = new List<Food>();

                            try
                            {
                                //Liste mit dem Essen
                                foreach (var item in foods)
                                {
                                    if (item.Key.ID == numberInt)
                                    {
                                        foodItem = item.Value;
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }

                            //Ausgabe der Liste
                            Console.WriteLine($"Bitte wählen sie einen menupunkt aus");
                            Console.WriteLine();

                            //foreach (Food item in foodItem)
                            for (int i = 0; i < foodItem.Count; i++)
                            {
                                Food item = foodItem[i];
                                int nr = i + 1;
                                Console.WriteLine($"{nr} | Nr. " + item.ID + ": " + item.Name + " - " + item.Price + "€");
                            }

                            Console.Write("Nummer: ");
                            string number1 = Console.ReadLine();
                            int number2;
                            bool Boolean = int.TryParse(number1, out number2);

                            if (Boolean)
                            {
                                //if (number2 > 0 && number2 <= foodItem.Count)
                                if (foodItem.FirstOrDefault(x => x.ID == number2) != null)
                                {
                                    Console.Write("");

                                    foreach (Food item in foodItem)
                                    {
                                        if (item.ID == number2)
                                        {
                                            //Essen Ausgabe
                                            Console.WriteLine(item.Name + " " + item.Price + "€");
                                            order.Add(item);
                                            break;
                                        }
                                    }

                                    Console.WriteLine();

                                }
                                else
                                {
                                    Console.WriteLine("Bitte ein vorhandenes Element auswählen");
                                }
                            }
                        }
                        else 
                        {
                            Console.WriteLine("Bitte eine vorhandene Kategorie auswählen...");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Bitte eine gültige Zahl eingeben...");
                        Console.WriteLine();
                    }

                    //Personen Eingabe
                    Console.WriteLine("Möchten sie noch etwas anderes bestellen? 1 ja, 2 nein");
                    key = Console.ReadLine();
                } while (key == "1") ;

                // Schleife (order)
                for (int i = 0; i < order.Count; i++)
                {
                    Console.WriteLine(order[i].Name + " - " + order[i].Price);
                    selectedPrice += order[i].Price;
                }


                // gesamt betrag €
                // Console.WriteLine(selectedPrice.ToString());
                Console.WriteLine();
                Console.WriteLine($"Der Gesamtpreis: {selectedPrice}");
                Console.WriteLine();


                Console.WriteLine();
                Console.WriteLine("Um das programm zu beenden drücken Sie #.");
                key = Console.ReadLine();
            }while (key != "#");
        }
    }
}
