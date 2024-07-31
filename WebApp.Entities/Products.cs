﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }

        public Product(string name, int quantity, float price, decimal discount, string description, string category, string manufacturer)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Discount = discount;
            Description = description;
            Category = category;
            Manufacturer = manufacturer;
        }
    }
}
