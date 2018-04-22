using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
namespace WindowsFormsApplication1
{
    class Sorting
    {

        static List<Recipe> hasProducts(List<Recipe> list, List<string> products)
        {
            List<Recipe> hasProducts = new List<Recipe>();
            foreach (Recipe a in list)
            {
                bool status = true;
                foreach (string product in products)
                {
                    if (!a.containsProduct(product))
                    {
                        status = false;
                        break;
                    }
                }
                if (status)
                    hasProducts.Add(a);
            }
            return hasProducts;
        }




        static List<Recipe> lessTotal(List<Recipe> list, double total)
        {
            List<Recipe> lessTotal = new List<Recipe>();
            foreach (Recipe a in list)
            {
                if (a.getTotal() < total)
                    lessTotal.Add(a);
            }
            return lessTotal;
        }

        static List<Recipe> moreTotal(List<Recipe> list, double total)
        {
            List<Recipe> moreTotal = new List<Recipe>();
            foreach (Recipe a in list)
            {
                if (a.getTotal() > total)
                    moreTotal.Add(a);
            }
            return moreTotal;
        }

        static List<Recipe> equalTotal(List<Recipe> list, double total)
        {
            List<Recipe> equalTotal = new List<Recipe>();
            foreach (Recipe a in list)
            {
                if (a.getTotal() == total)
                    equalTotal.Add(a);
            }
            return equalTotal;
        }

        static List<Recipe> lessOrEqualTotal(List<Recipe> list, double total)
        {
            List<Recipe> lessOrEqualTotal = new List<Recipe>();
            foreach (Recipe a in list)
            {
                if (a.getTotal() <= total)
                    lessOrEqualTotal.Add(a);
            }
            return lessOrEqualTotal;
        }

        static List<Recipe> moreOrEqualTotal(List<Recipe> list, double total)
        {
            List<Recipe> moreOrEqualTotal = new List<Recipe>();
            foreach (Recipe a in list)
            {
                if (a.getTotal() >= total)
                    moreOrEqualTotal.Add(a);
            }
            return moreOrEqualTotal;
        }


        //Функция връщаща абсолютно всички рецепти от базата данни
        static List<Recipe> getAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>(); //Създаваме празен списък с Рецепти
            Database databaseObject = new Database(); //Създаваме си нов Database Object

            //Създаваме си query
            string select = "Select * from Recipe join recipeCategory on Recipe.IDrecipeCategory = recipeCategory.IDrecipeCategory;";

            //Отваряме connection към базата
            databaseObject.OpenConnection();

            //Създаваме си SQLite команда
            SQLiteCommand select1 = new SQLiteCommand(select, databaseObject.myConnection);

            //И я execute-ваме
            SQLiteDataReader resultt = select1.ExecuteReader();
            if (resultt.HasRows) //Ако има резултат
            {
                while (resultt.Read()) //Четем до край
                {
                    //Създаваме си необходимите променливи за обект от клас Рецепта
                    string title;
                    string category;
                    Dictionary<Product, double> productsDic = new Dictionary<Product, double>();
                    double total = 0;
                    string rtime;
                    String description;

                    //Четем полета от резултата от query-то
                    title = resultt["recipeTitle"].ToString();
                    category = resultt["recipeCategoryName"].ToString();
                    rtime = resultt["time"].ToString();
                    description = resultt["description"].ToString();

                    //Малко тъпо направено query за изличане на продуктите за съответната рецепта
                    string products = "Select * from recipeProducts join Products on Products.IDproduct = recipeProducts.IDproduct join Unit on Unit.IDunit = recipeProducts.IDunit join productCategory on Products.IDproductCategory = productCategory.IDCategory  where IDrecipe = " + resultt[("IDrecipe")].ToString() + ";";
                    SQLiteCommand products1 = new SQLiteCommand(products, databaseObject.myConnection);
                    SQLiteDataReader productss = products1.ExecuteReader();

                    while (productss.Read()) //Четем всичко
                    {
                        Product prod = new Product(); //Създаваме си нов продукт
                        prod.setName(productss["productName"].ToString());
                        prod.setCategory(productss["categoryName"].ToString());
                        prod.setUnit(productss["unitName"].ToString());
                        prod.setPrice((Double)productss["price"]);
                        productsDic.Add(prod, (Double)productss["quantity"]); //И го добавяме към продуктите за нашата рецепта
                        total += prod.getPrice() * (Double)productss["quantity"]; //Увеличаваме общата цена - единична цена * количество
                    }
                    recipes.Add(new Recipe(title, category, productsDic, total, rtime, description)); //И добавяме рецептата към списъка
                }
            }

            databaseObject.CloseConnection(); //Затваряме connection
            return recipes; //Връщаме списъка с всички рецепти
        }
    }
}
