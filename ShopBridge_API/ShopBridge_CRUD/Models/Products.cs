using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge_CRUD.Models
{
    public class Products
    {
        public string PRODUCT_ID { get; set;  }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public int PRODUCT_MRP { get; set; }
        public string PRODUCT_SELLER { get; set; }
    }
}
