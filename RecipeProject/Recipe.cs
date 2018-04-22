using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    //Клас рецепта
    class Recipe
    {
        private String title; //Име на рецепта
        private String category; //Категория на рецепта
        private Dictionary<Product, double> products; //Списък с необходими продукти и количество
        private double total; //Цена за всички продукти
        private string rtime; //Време за приготвяне (в минути)
        private String description; //Описание (стъпки на приготвяне)
        private String photoURL; //Линк към снимка, за съответната рецепта - НЕ СЕ ПОЛЗВА В МОМЕНТА

        public Recipe() { } //Default конструктор

        //Explicit конструктор, включващ photoURL - НЕ СЕ ПОЛЗВА В МОМЕНТА
        public Recipe(String t, String c, Dictionary<Product, double> p, double tot, string rt, String desc, String URL){
            title=t;
            category=c;
            products=p;
            total=tot;
            rtime=rt;
            description=desc;
            photoURL=URL;
        }

        //Explicit конструктор без photoURL
        public Recipe(String t, String c, Dictionary<Product, double> p, double tot, string rt, String desc)
        {
            title = t;
            category = c;
            products = p;
            total = tot;
            rtime = rt;
            description = desc;
        }

        ///////////////
        //GETS + SETS//
        ///////////////
        public String getTitle(){
            return title;
        }
        public String getCategory(){
            return category;
        }
        public Dictionary<Product, double> getProducts(){
            return products;
        }
        public double getTotal(){
            return total;
        }
        public string getTime(){
            return rtime;
        }
        public String getDescription(){
            return description;
        }
        public String gePhotoURL(){
            return photoURL;
        }

        public void setTitle(string t)
        {
            title = t;
        }
        public void setCategory(string c)
        {
            category = c;
        }
        public void setProducts(Dictionary<Product, double> pr)
        {
            products = pr;
        }
        public void setTotal(double tot)
        {
            total = tot;
        }
        public void setTime(string rt)
        {
            rtime = rt;
        }
        public void setDescription(String desc)
        {
            description = desc;
        }
        public void setPhotoURL(String URL)
        {
            photoURL = URL;
        }


        //Функция, връщаща true, ако рецептата съдържа даден продукт
        public bool containsProduct(string productName) //продукт, който търсим
        {
            bool contains = false; // false по default 
            foreach (KeyValuePair<Product, double> a in products) // Обхождаме целия Dictionary с продуктите
            {
                if (a.Key.getName() == productName) // Ако името на продукт съответства на търсения
                {
                    contains = true; //contains става true
                    break; // и приключваме търсенето
                }
            }
            return contains; //връщаме true/false
        }


        //toString метод за Рецепта
        public string toString()
        {
            string recipe = "";
            recipe += "Заглавие: " + title + Environment.NewLine;
            recipe += "Категория: " + category + Environment.NewLine;
            recipe += "Цена(общо): " + total + " лв. " + Environment.NewLine;
            recipe += "Време на приготвяне: " + rtime + Environment.NewLine;
            recipe += "Необходими продукти: " + Environment.NewLine + Environment.NewLine;


            //Обхождаме всички продукти - Взимаме име + unit + количество за всеки продукт
            foreach (KeyValuePair<Product, double> a in products)
            {
                recipe += a.Key.getName() + " - " + a.Value.ToString() + " " + a.Key.getUnit() + Environment.NewLine;
            }

            recipe += Environment.NewLine + "Описание: " + Environment.NewLine + description + Environment.NewLine;
            return recipe; // Връщаме string
        }
    }
}
