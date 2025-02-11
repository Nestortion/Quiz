﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OnlineShopping
{
    public class ConsoleUI
    {

        private bool running = true;
        private StringBuilder productName = new StringBuilder("");
        private string category;
        private string brand;
        private int categChoice;
        

        public string getCategory()
        {
            return this.category;
        }
        public string getBrand()
        {
            return this.brand;
        }
        public ConsoleUI()
        {
            StartMenu();
        }
        public void StartMenu()
        {
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Pre Create PC Online Shop");
                Console.WriteLine("Here are the list of products\n");
                Console.WriteLine("#     Category");
                foreach (BusinessLayer.AvailableProducts s in Enum.GetValues(typeof(BusinessLayer.AvailableProducts)))
                {
                    Console.WriteLine($"{(int)s}     {s}");
                }


                Console.Write($"\nEnter 1-5 to choose: ");

                switch (Console.ReadLine().ToLower())
                {
                    case "1":
                        SecondMenu(1);
                        running = false;
                        break;
                    case "2":
                        SecondMenu(2);
                        running = false;
                        break;
                    case "3":
                        SecondMenu(3);
                        running = false;
                        break;
                    case "4":
                        SecondMenu(4);
                        running = false;
                        break;
                    case "5":
                        SecondMenu(5);
                        running = false;
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice");
                        Console.Write("Returning in");
                        for (int i = 3; i > 0; i--)
                        {
                            Console.Write(" .");
                            Thread.Sleep(1000);
                        }
                        break;
                }

            }
        }
        public void SecondMenu(int choice)
        {
            categChoice = choice;
            Console.Clear();
            string categ= Enum.GetName(typeof(BusinessLayer.AvailableProducts), choice);
            Console.WriteLine($"Here are the list of {categ} brands\n");
            Console.WriteLine("     Price  Brand");
            switch (choice)
            {
                case 1:
                    foreach (BusinessLayer.PowerSupplyBrands p in Enum.GetValues(typeof(BusinessLayer.PowerSupplyBrands)))
                    {
                        Console.WriteLine($"     ${(int)p}   {p}");
                        category = categ;
                        brand = p.ToString();
                    }
                    break;
                case 2:
                    foreach (BusinessLayer.MotherboardBrands p in Enum.GetValues(typeof(BusinessLayer.MotherboardBrands)))
                    {
                        Console.WriteLine($"     ${(int)p}   {p}");
                        category = categ;
                        brand = p.ToString();
                    }
                    break;
                case 3:
                    foreach (BusinessLayer.ProcessorBrands p in Enum.GetValues(typeof(BusinessLayer.ProcessorBrands)))
                    {
                        Console.WriteLine($"     ${(int)p}   {p}");
                        category = categ;
                        brand = p.ToString();
                    }
                    break;
                case 4:
                    foreach (BusinessLayer.RamBrands p in Enum.GetValues(typeof(BusinessLayer.RamBrands)))
                    {
                        Console.WriteLine($"     ${(int)p}   {p}");
                        category = categ;
                        brand = p.ToString();
                    }
                    break;
                case 5:
                    foreach (BusinessLayer.StorageBrands p in Enum.GetValues(typeof(BusinessLayer.StorageBrands)))
                    {
                        Console.WriteLine($"     ${(int)p}   {p}");
                        category = categ;
                        brand = p.ToString();
                        
                    }
                    break;
                default:
                    break;
            }
            Console.Write("\nEnter 1 or 2 to select a brand and proceed to order o 0 to go back:");
            switch (Console.ReadLine().ToLower())
            {
                case "1":
                    OrderMenu(1);
                    break;
                case "2":
                    OrderMenu(2);
                    break;
                case "0":
                    StartMenu();
                    break;
                default:
                    break;
            }
        }
        public void OrderMenu(int choice)
        {
            productName.Clear();
            productName.Append($"{brand} {category}");
            
            Console.Clear();
            Console.WriteLine("Here are the details of this item:\n");
            Console.WriteLine($"Name: {productName}");
            Console.WriteLine($"Price: {DataLayer.SqlData.DisplayPrice(category,brand)}");
            Console.Write("\nEnter 1 to add to cart or 0 to go back: ");
            switch (Console.ReadLine().ToLower())
            {
                case "1":
                    AddToCart();
                    break;
                case "0":
                    SecondMenu(categChoice);
                    break;
                default:
                    break;
            }
        }
        public void AddToCart()
        {
            bool running = true;
            Console.WriteLine("Enter Product Quantity: ");
            var quantity = Console.ReadLine();
            BusinessLayer.CheckOut.order.AddOrderItem(new BusinessLayer.Product(productName.ToString(), DataLayer.SqlData.DisplayPrice(category, brand), Convert.ToInt32(quantity)));
            do
            {
                Console.Clear();
                BusinessLayer.CheckOut.order.DisplayCart();


                Console.Write("Enter 1 to check out or 2 to add another product or 3 to delete a product: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        if (BusinessLayer.CheckOut.IfCanCheckOut(BusinessLayer.CheckOut.order.orderTotal) == false)
                        {
                            running = true;
                            Console.Write("Cart is Empty");
                            for (int i = 3; i > 0; i--)
                            {
                                Console.Write(" .");
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            running = false;
                            BusinessLayer.User.Login();
                        }
                        
                        break;
                    case "2":
                        running = false;
                        StartMenu();
                        break;
                    case "3":
                        DeleteItem();
                        break;

                    default:
                        Console.Write("Error Try again in ");
                        for (int i = 3; i > 0; i--)
                        {
                            Console.Write(" .");
                            Thread.Sleep(1000);
                        }
                        break;
                }
            } while (running);
            
        }
        public static void DeleteItem()
        {
            Console.Write("Enter the product name to delete: ");
            string name = Console.ReadLine().ToLower();
            BusinessLayer.CheckOut.order.DeleteOrderItem(name);
        }
        
        
        
    }
    
}
