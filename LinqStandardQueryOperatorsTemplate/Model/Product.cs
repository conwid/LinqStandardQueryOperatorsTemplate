using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Model
{
    [DataContract(Namespace = "CSharpHalado")]
    public class Product
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public Category Category { get; set; }

        [DataMember]
        public string QuantityPerUnit { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short UnitsInStock { get; set; }

        [DataMember]
        public short UnitsOnOrder { get; set; }

        [DataMember]
        public bool Discontinued { get; set; }        
        
        public override string ToString()
        {
            return Name;
        }

   }
}
