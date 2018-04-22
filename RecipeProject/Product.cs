using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Product
    {
        private String name; //Име на продукт
        private String category; //Категория на продукт
        private double price; // Цена на продукт за съответния unit
        private String unit; //Мерна единица

        public Product() { } //Default конструктор

        //Експлицитен конструктор
        public Product(String a, String cat, double b, String c){
            name=a;
            category=cat;
            price=b;
            unit=c;
        }

        /////////////////
        //GETS AND SETS//
        /////////////////
        public String getName(){
            return name;
        }
        public String getCategory(){
            return category;
        }
        public double getPrice(){
            return price;
        }
        public String getUnit(){
            return unit;
        }
        public void setName(String a){
            name=a;
        }
        public void setPrice(double b){
            price=b;
        }
        public void setUnit(String c){
            unit=c;
        }
        public void setCategory(String cat){
            category=cat;
        }
    }
}
