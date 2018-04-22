using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class ProductCategory //Категория продукт - ВСЕ ОЩЕ НЕ СЕ ПОЛЗВА НИКЪДЕ
    {
        private String categoryName; //Име на категория продукти
        private List<Product> products; //Списък с продукти към съответната категория

        public ProductCategory() { } //Default constructor

        //Explicit constructor
        public ProductCategory(String catName, List<Product> prod){
            categoryName=catName;
            products=prod;
        }

        /////////////////
        //GETS AND SETS//
        /////////////////
        public String getCategoryName(){
            return categoryName;
        }
        public List<Product> getProducts(){
            return products;
        }
        public void setCategoryName(String catName){
            categoryName=catName;
        }
        public void setProducts(List<Product> prod){
            products=prod;
        }
    }
}
