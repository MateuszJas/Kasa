using System;
using System.Collections.Generic;

namespace CashRegister
{
    class Item
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double? UnitWeight { get; set; }

        public Item(string name, double unitPrice, double? unitWeight = null)
        {
            Name = name;
            UnitPrice = unitPrice;
            UnitWeight = unitWeight;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //  Tworzenie listy produktów
            var items = new List<Item>
            {
                new Item("Jabłko", 0.99),
                new Item("Pomaranćza", 0.50),
                new Item("Banan", 0.25),
                new Item("Wołownia", 4.99, 0.45),
                new Item("Kurczak", 3.99, 0.30),
            };

            while (true)
            {
                // Tworzenie pustego wózka
                var cart = new List<(Item, double)>();

                // poproś użytkownika o dodanie pozycji do koszyka
                while (true)
                {
                    // wyczyść 
                    Console.Clear();

                    // wydrukuj dostępne pozycje
                    Console.WriteLine("Witam w sklepie! To jest nasz asortyment:");
                    for (var i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        Console.WriteLine($"{i + 1}. {item.Name}: {item.UnitPrice:C}");
                    }
                    // poproś użytkownika o wybranie elementu
                    Console.Write("Proszę wybrać po numerze ");
                    var itemIndex = int.Parse(Console.ReadLine());
                    var chosenItem = items[itemIndex - 1];


                    // zapytaj użytkownika o ilość lub wagę towaru
                    double quantity;
                    if (chosenItem.UnitWeight.HasValue)
                    {
                        // jeśli przedmiot jest sprzedawany na wagę, zapytaj o wagę
                        Console.Write("Proszę podać wagę w kg: ");
                        quantity = double.Parse(Console.ReadLine());
                    }
                    else
                    {
                        // jeśli przedmiot jest sprzedawany na sztuki, zapytaj o ilość
                        Console.Write("Proszę podać ilość: ");
                        quantity = int.Parse(Console.ReadLine());
                    }

                    // dodaj przedmiot do koszyka
                    cart.Add((chosenItem, quantity));

                    // zapytaj użytkownika, czy chce dodać więcej pozycji lub wydrukować paragon
                    Console.Write("zapytaj użytkownika, czy chce dodać więcej pozycji lub wydrukuj paragonCzy chcesz dodać więcej pozycji? [T/n] ");
                    var addMore = Console.ReadLine();
                    if (addMore.ToLower() != "t")
                    {
                        break;
                    }
                }

                // print the receipt
                Console.Clear();
                Console.WriteLine("Oto twój rachunek:");
                foreach (var (item, quantity) in cart)
                {
                    // obliczyć cenę przedmiotu na podstawie ilości lub wagi
                    var price = item.UnitPrice * (item.UnitWeight.HasValue ? quantity : quantity);
                    Console.WriteLine($"{item.Name}: {quantity} x {item.UnitPrice:C} = {price:C}");
                }

                // wydrukować całkowity koszt
                var total = 0.0;
                foreach (var (item, quantity) in cart)
                {
                    total += item.UnitPrice * (item.UnitWeight.HasValue ? quantity : quantity);
                }
                Console.WriteLine($"Całkowity koszt: {total:C}");


                // zapytaj użytkownika, czy chce rozpocząć nową transakcję, czy wyjść z programu
                Console.Write("Czy chcesz rozpocząć nową transakcję? [T/n] ");
                var newTransaction = Console.ReadLine();
                if (newTransaction.ToLower() != "t")
                {
                    break;
                }
            }
        }
    }
}

