using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
   class CategoryRecipe // ВСЕ ОЩЕ НЕ ГО ПОЛЗВАМЕ НИКЪДЕ
    {
        private String categoryName; //Име на категория на рецепти
        private List<Recipe> recipes = new List<Recipe>(); //Списък с рецепти към съответната категория

        public CategoryRecipe() { } //Default конструктор

       //Експлицитен констуктор
        public CategoryRecipe(String catName, List<Recipe> recipe){

            categoryName=catName;

            foreach (Recipe a in recipe)
            {
                if (a.getCategory() == catName)
                    recipes.Add(a);
            }
        }

        /////////////////
        //GETS AND SETS//
        /////////////////
        public String getCategoryName(){
            return categoryName;
        }
        public List<Recipe> getRecipes(){
            return recipes;
        }
        public void setCategoryName(String catName){
            categoryName=catName;
        }
        public void setRecipes(List<Recipe> recipe){
            recipes=recipe;
        }
    }
}
