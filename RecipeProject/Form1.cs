using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Функция, получаваща като аргумент списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ цена и връща списък с всички рецепти с обща цена по-малка от изискваната
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

        //Функция, получаваща като аргумент списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ цена и връща списък с всички рецепти с обща цена по-голяма от изискваната
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

        //Функция, получаваща като аргумент списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ цена и връща списък с всички рецепти с обща цена равна на изискваната
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

        //Функция, получаваща като аргумент списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ цена и връща списък с всички рецепти с обща цена по-малка или равна на изискваната
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

        //Функция, получаваща като аргумент списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ цена и връща списък с всички рецепти с обща цена по-голяма или равна на изискваната
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


        //Функция, получаваща като аргументи 1 списък с рецепти (в нашия случай ще получава всички рецепти)
        //+ списък с продукти, които искаме нашата рецепта да съдържа
        static List<Recipe> hasProducts(List<Recipe> list, List<string> products)
        {
            List<Recipe> hasProducts = new List<Recipe>(); //Създаваме си празен списък
            foreach (Recipe a in list) //За всяка рецепта, която имаме
            {   
                bool status = true; 
                foreach (string product in products) //За всеки продукт в нашия списък
                {
                    if (!a.containsProduct(product)) //Ако нямаме такъв продукт
                    {
                        status = false; //Статусът става false
                        break; //и прекратяваме търсенето на съответния продукт (принципно трябва да излезем от рецептата напълно, но трябва да видя как точно
                    }
                }
                if (status) //Ако статусът е true - съдържа всички продукти
                    hasProducts.Add(a); //Добавяме рецептата към нашия списък
            }
            return hasProducts; //Връщаме списъка, съдържащ всички продукти, които искаме
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
        




        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 40;
            List<Recipe> recipes = getAllRecipes();
        }

        private void button1_Click(object sender, EventArgs e)
        {   List<Recipe> recipes = getAllRecipes();
            dataGridView1.Rows.Clear();
            foreach (Recipe a in recipes)
                {
                    string products = "";
                foreach (KeyValuePair<Product, double> pair in a.getProducts()){
                    products += pair.Key.getName() + "-" + pair.Value.ToString() + " " + pair.Key.getUnit() +"\n";

                }
                string[] add = new string[] { a.getTitle(), a.getCategory(), products, a.getTotal().ToString() + " лв.", a.getTime() + " мин.", a.getDescription() };
                dataGridView1.Rows.Add(add);
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string category = "";
        if (radioButton1.Checked == true)
            category = "Салата";
        else if (radioButton2.Checked == true)
            category = "Основно рибно";
        else if (radioButton3.Checked == true)
            category = "Основно месно";
        else category = "Десерт";

        List<Recipe> recipes = getAllRecipes();
        CategoryRecipe categ = new CategoryRecipe(category, getAllRecipes());
        dataGridView1.Rows.Clear();
        foreach (Recipe a in categ.getRecipes())
        {
            string products = "";
            foreach (KeyValuePair<Product, double> pair in a.getProducts())
            {
                products += pair.Key.getName() + "-" + pair.Value.ToString() + " " + pair.Key.getUnit() + "\n";

            }
            string[] add = new string[] { a.getTitle(), a.getCategory(), products, a.getTotal().ToString() + " лв.", a.getTime() + " мин.", a.getDescription() };
            dataGridView1.Rows.Add(add);
        }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<Recipe> recipes = new List<Recipe>();
            double b = Double.Parse(textBox1.Text);
            if (radioButton6.Checked == true)
                recipes = moreTotal(getAllRecipes(), b);
            else if (radioButton5.Checked == true)
                recipes = lessTotal(getAllRecipes(), b);
            else if (radioButton7.Checked == true)
                recipes = equalTotal(getAllRecipes(), b);
            else if (radioButton8.Checked == true)
                recipes = lessOrEqualTotal(getAllRecipes(), b);
            else 
                recipes = moreOrEqualTotal(getAllRecipes(), b);
            dataGridView1.Rows.Clear();
            foreach (Recipe a in recipes)
            {
                string products = "";
                foreach (KeyValuePair<Product, double> pair in a.getProducts())
                {
                    products += pair.Key.getName() + "-" + pair.Value.ToString() + " " + pair.Key.getUnit() + "\n";

                }
                string[] add = new string[] { a.getTitle(), a.getCategory(), products, a.getTotal().ToString() + " лв.", a.getTime() + " мин.", a.getDescription() };
                dataGridView1.Rows.Add(add);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<string> products1 = new List<string>();
            foreach (Control c in panel3.Controls)
            {
                if ((c is CheckBox) && ((CheckBox)c).Checked)
                    products1.Add(c.Text);
            }
            List<Recipe> rec = hasProducts(getAllRecipes(), products1);

            dataGridView1.Rows.Clear();
            foreach (Recipe a in rec)
            {
                string products = "";
                foreach (KeyValuePair<Product, double> pair in a.getProducts())
                {
                    products += pair.Key.getName() + "-" + pair.Value.ToString() + " " + pair.Key.getUnit() + "\n";

                }
                string[] add = new string[] { a.getTitle(), a.getCategory(), products, a.getTotal().ToString() + " лв.", a.getTime() + " мин.", a.getDescription() };
                dataGridView1.Rows.Add(add);
            }



        }
    }
}
