using System;
using System.Collections.Generic;
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
                Console.WriteLine($"{name} purchased {item.Name}!");
                return true;
            }
            else
            {
                Console.WriteLine($"{name} Not able to purchase {item.Name}!");
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
                Console.WriteLine($"{name} had insufficient founds, get rich bro!");
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
            Console.WriteLine($"{name}'s current iventory is:");
            inventory.CheckInventory();
        }

        public Dictionary<string, Item> GetInventory()
        {
            return inventory.GetInventory();
        }

        public void CheckBalance()
        {
            Console.WriteLine($"{name}'s current balance is:");
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
            User Karo = new User("Karo", 100);
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


           Dictionary<string, Item> inventory = VendingMachine.GetInventory();
            int menuItem = 0;
            int currentIndex = 0;

            ConsoleKey key;
            do {
                Console.Clear();
                Console.WriteLine("Please navigate to your option and press Enter to select:\n");

                foreach (KeyValuePair<string, Item> item in inventory)
                {
                    menuItem++;
                    if (menuItem == currentIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"Product: {item.Value.Name}, Amount: {item.Value.Quantity}");
                    Console.ResetColor();
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
            } while (key != ConsoleKey.Enter);

            Console.Clear();
            Console.WriteLine($"You have selected: {currentIndex}");*/
           
           string[] options = { "Option 1", "Option 2", "Option 3", "Option 4" }; // This should be our inventory
           int currentIndex = 0;

           ConsoleKey key;
           do
           {
               Console.Clear();
               Console.WriteLine("Please navigate to your option and press Enter to select:\n");

               for (int i = 0; i < options.Length; i++) // here we do foreach instead and loop thorugh our invientory
               {
                   if (i == currentIndex)
                   {
                       Console.BackgroundColor = ConsoleColor.Gray;
                       Console.ForegroundColor = ConsoleColor.Black;
                   }

                   Console.WriteLine(options[i]); // Here we write our inventory 
                   Console.ResetColor();
               }

               key = Console.ReadKey(true).Key;

               switch (key)
               {
                   case ConsoleKey.UpArrow:
                       currentIndex = (currentIndex > 0) ? currentIndex - 1 : options.Length - 1;
                       break;
                   case ConsoleKey.DownArrow:
                       currentIndex = (currentIndex + 1) % options.Length;
                       break;
               }
           }
           while (key != ConsoleKey.Enter);

           Console.Clear();
           Console.WriteLine($"You have selected: {options[currentIndex]}");

        }
    }
}