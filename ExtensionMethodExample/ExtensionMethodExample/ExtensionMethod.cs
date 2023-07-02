using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ExtensionMethodExample
{
    public static class ExtensionMethod
    {
        public static int GetDiscount(this Product product)
        {
            return (product.DiscountPersentage * product.ProductCost) / 100;
        }
    }
}
