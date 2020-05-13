namespace BookStore.BLL.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string InStock { get; set; } // this isn't bool value, because I would like to use Value Converter in WPF
        public string Binding { get; set; }
        public string Description { get; set; }
        public byte ColorQuantityByPrice { get; set; }

    }
}
