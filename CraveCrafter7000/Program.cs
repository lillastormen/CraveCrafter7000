using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraveCrafter7000
{
    class User
    {
        private Wallet wallet;
        private Inventory inventory;
        private String name;

        public User(string Name, int Balance=100)
        {
            name = Name;
            wallet = new Wallet(Balance);
            inventory = new Inventory();
        }

        public bool Purchase(Item item)
        {
            if (Payment(item.Price))
            {
                inventory.AddItem(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Sell(Item item)
        {
            inventory.RemoveItem(item);
            wallet.Deposit(item.Price);
        }
        
        public bool Payment(int price)
        {
            if (wallet.Withdraw(price))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Restock(Item item)
        {
            inventory.AddItem(item, true);
        }

        public void RemoveFromInventory(Item item)
        {
            inventory.RemoveItem(item);
        }
        public void CheckInventory()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{name}'s current inventory is:");
            Console.ResetColor();
            inventory.CheckInventory();
        }

        public Dictionary<string, Item> GetInventory()
        {
            return inventory.GetInventory();
        }

        public void CheckBalance()
        {

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{name}'s current balance is:");
            Console.ResetColor();
            wallet.CheckBalance();
        }
    }

    class Wallet
    {
        private int balance;

        public Wallet(int Balance=0)
        {
            balance = Balance;
        }

        public void CheckBalance()
        {
            Console.WriteLine(balance);
        }

        public bool Withdraw(int amount)
        {
            if (amount > balance)
            {
                return false;
            }
            else
            {
                balance -= amount;
                return true;
            }
        }

        public int Deposit(int amount)
        {
            balance += amount;
            return balance;
        }
    }

    class Inventory
    {
        private Dictionary<string, Item> items = new Dictionary<string, Item>();

        public void AddItem(Item item, bool restock = false)
        {
            if (items.ContainsKey(item.Name))
            {
                items[item.Name].Quantity++;
            }
            else
            {
                if (!restock)
                {
                    item.Quantity = 1;
                }
                items.Add(item.Name, item);
            }
        }

        public void RemoveItem(Item item)
        {
            if (items.ContainsKey(item.Name))
            {
                items[item.Name].Quantity--;
            }
            else
            {
                items.Remove(item.Name);
            }
        }

        public void CheckInventory()
        {
            foreach (KeyValuePair<string, Item> item in items)
            {
                Console.WriteLine($"Product: {item.Value.Name}, Amount: {item.Value.Quantity}");
            }
        }

        public Dictionary<string, Item> GetInventory()
        {
            return items;
        }
    }

    class Item
    {
        public string Name;
        public int Price;
        public int Quantity; 

        public Item(string name, int price, int quantity=1)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
    
    internal class Program
    {

        public static void Main(string[] args)
        {

            User Karo = new User("Your", 100);
            User VendingMachine = new User("CraveCrafter7000", 0);

            VendingMachine.Restock(new Item("Nocco", 20, quantity: 15));
            VendingMachine.Restock(new Item("Fanta", 15, quantity: 10));
            VendingMachine.Restock(new Item("Cola", 15, quantity: 10));
            VendingMachine.Restock(new Item("Water", 15, quantity: 10));
            VendingMachine.Restock(new Item("Snickers", 12, quantity:25));
            VendingMachine.Restock(new Item("Sport Lunch", 12, quantity: 25));
            VendingMachine.Restock(new Item("Marabou", 20, quantity: 15));
            VendingMachine.Restock(new Item("Lays", 15, quantity: 15));
            VendingMachine.Restock(new Item("Salty sticks", 10, quantity: 10));
            VendingMachine.Restock(new Item("Peanuts", 10, quantity:30));
            VendingMachine.Restock(new Item("Almonds", 10, quantity:30));
            VendingMachine.Restock(new Item("Sandwich", 25, quantity: 10));
            VendingMachine.Restock(new Item("Licorice", 20, quantity: 20));
            
            int currentIndex = 0;
            Item selectedItem = null;
            Item latestPurchase = null;
            bool error = false;

            Dictionary<string, Item> inventory = VendingMachine.GetInventory();
            ConsoleKey key;
            do
            {

                Console.Clear();
                Console.WriteLine("WELCOME TO CraveCarfter 7000 - Satisfy your cravings with us!");

                Karo.CheckInventory();
                Karo.CheckBalance();

                if (latestPurchase != null && !error)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    String message = $"| You purchased {latestPurchase.Name} |";
                    String dashes = new string('-', message.Length-2);
                    Console.WriteLine($"\n*{dashes}*");
                    Console.WriteLine(message);
                    Console.WriteLine($"*{dashes}*");
                }

                if (error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    String message = $"|   You didnt have enough money to purchase {latestPurchase.Name} =(   |";
                    String dashes = new string('-', message.Length - 2);
                    Console.WriteLine($"\n*{dashes}*");
                    Console.WriteLine(message);
                    Console.WriteLine($"*{dashes}*");
                }

                Console.ResetColor();

                Console.WriteLine("\n\nPlease navigate to your option and press Enter to purchase:\n--------------");
                int menuItem = 0;

                foreach (KeyValuePair<string, Item> item in inventory)
                {
                    if (menuItem == currentIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        selectedItem = new Item(item.Value.Name, item.Value.Price);
                        
                    }
                    
                    Console.WriteLine($"Product: {item.Value.Name}, Price: {item.Value.Price}, Amount: {item.Value.Quantity}");
                    Console.ResetColor();
                    menuItem++;
                }

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex = (currentIndex > 0) ? currentIndex - 1 : inventory.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentIndex = (currentIndex + 1) % inventory.Count;
                        break;
                }
                if (key == ConsoleKey.Enter)
                {
                    if (Karo.Purchase(selectedItem)) { 
                        VendingMachine.RemoveFromInventory(selectedItem);
                        latestPurchase = selectedItem;
                        error = false; 
                    } 
                    else 
                    { 
                        error = true; 
                        latestPurchase = selectedItem; 
                    };
                    
                }               

            } while (true); // (key != ConsoleKey.Enter);
           
        }
    }
}
