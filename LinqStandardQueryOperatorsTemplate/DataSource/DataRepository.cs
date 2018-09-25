using $safeprojectname$.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace $safeprojectname$.DataSource
{
    public static class DataRepository
    {
        public static IEnumerable<Category> Categories { get; private set; }
        public static IEnumerable<Product> Products { get; private set; }
        static DataRepository()
        {
            using (var catFile = XmlReader.Create(@"..\..\DataSource\categories.xml"))
            {
                var dcs = new DataContractSerializer(typeof(List<Category>));
                Categories = (List<Category>)dcs.ReadObject(catFile);
                foreach (var c in Categories)
                {
                    c.Products = new List<Product>();
                }
            }

            using (var prodFile = XmlReader.Create(@"..\..\DataSource\products.xml"))
            {
                var dcs = new DataContractSerializer(typeof(List<Product>));
                Products = (List<Product>)dcs.ReadObject(prodFile);
                foreach (var prod in Products)
                {
                    prod.Category = Categories.Single(c => c.CategoryID == prod.CategoryID);
                    prod.Category.Products.Add(prod);
                }
            }
        }
    }
}
