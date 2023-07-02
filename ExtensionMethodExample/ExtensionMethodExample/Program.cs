using ClassLibrary1;


namespace ExtensionMethodExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new() { ProductCost = 100, DiscountPersentage = 10};

            Console.WriteLine(product.GetDiscount());
        }
    }
}