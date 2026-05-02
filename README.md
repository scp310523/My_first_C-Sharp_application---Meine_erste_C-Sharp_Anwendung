# Meine erste C#-Anwendung — Car Sales App

A personal first C# project built from scratch as a learning exercise.
Consists of two versions of the same car dealership management system —
a console application and a WPF desktop GUI — sharing the same CSV-based
data layer.

---

## What it does

The app simulates a small car dealership. It supports two sides of the
business: buying a car from the catalogue, and selling your own car to
the dealership.

### Buying a car
- Displays a catalogue of available cars loaded from CarsList.csv
- Each car shows: ID, make, model, color, doors, min/max horsepower,
  condition, year, mileage, and price (formatted in Euro)
- The user selects a car by ID (console) or by clicking a row (WPF)
- On confirmation, the car is marked as sold (IsSold = true) and
  disappears from the catalogue
- The purchase is appended to receipt.csv as a permanent record

### Selling a car
- The user enters their car's details: make, model, condition, year,
  mileage, and asking price
- The car is added to CarsList.csv and becomes available in the catalogue

### Admin functions (console only, hidden codes)
- **303** — Edit any existing car's full details
- **404** — Add a new car to the catalogue manually, with option to
  hide it from the public listing (IsSold flag)
- **505** — Delete a car from the catalogue; deleted entries are
  archived to DeletedCarsList.csv so nothing is permanently lost

---

## Data storage

All data lives in CSV files, read and written with CsvHelper:

| File | Purpose |
|---|---|
| CarsList.csv | Master car inventory (all cars, sold and unsold) |
| customersCars.csv | Cars submitted by customers for sale |
| receipt.csv | Record of every confirmed purchase |
| DeletedCarsList.csv | Archive of admin-deleted cars |

---

## Project structure

The solution contains two projects:

**Car-Sale** — Console application (.NET 4.8)
Full feature set: buy, sell, admin edit/add/delete.
Entry point: `Program.cs`
Models: `Car.cs`, `TradeType.cs`

**CarSales** — WPF desktop application (.NET 4.7.2)
Visual version of the catalogue with a ListView.
Buy button with confirmation dialog, live list refresh after purchase,
receipt CSV write. Currency displayed in German Euro format (de-DE).
Entry point: `MainWindow.xaml` / `MainWindow.xaml.cs`
Model: `CarWPF.cs`
Converter: `CurrencyEuroConverter` (IValueConverter, formats decimal as €)

---

## Tech stack

- C# / .NET Framework 4.8 (console), 4.7.2 (WPF)
- WPF with XAML for the GUI version
- CsvHelper 32.0.3 for CSV read/write
- Visual Studio, Git for version control

---

## What I learned building this

- Reading and writing CSV files with CsvHelper (headers, append mode,
  record serialization)
- OOP basics: classes, properties, constructors, static vs instance
- Console I/O, input parsing, loop control
- WPF: XAML layout, ListView with GridView, data binding,
  IValueConverter for currency formatting, event handlers
- The difference between static and instance fields (CS0414 bug)
- How IsSold flags work as a soft-delete pattern
- Git commits, branching, version control workflow
