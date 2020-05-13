using BookStore.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BLL.Services
{
    public class DataService : IDataService
    {
        public IEnumerable<BookModel> BuildBookModelsFromBooks(IEnumerable<Book> books)
        {
            var orderedBookList = books.OrderBy(e => e.Price);

            var result = new List<BookModel>();
            var colors = GetColorBites(orderedBookList).ToList();

            foreach (var item in orderedBookList.Select((book, index) => new { index, book })) //this selection will help to get book's index and corresponding color
            {
                yield return new BookModel
                {
                    Author = item.book.Author,
                    Binding = item.book.Binding,
                    Price = item.book.Price,
                    Description = item.book.Description,
                    InStock = item.book.InStock,
                    ColorQuantityByPrice = colors[item.index],
                    Title = item.book.Title,
                    Year = item.book.Year
                };
            }
        }

        public void FixColorQuantityByPrice(IEnumerable<BookModel> models)
        {
            var colors = GetColorBites(models.OrderBy(e => e.Price)).ToList();

            foreach (var item in models.Select((value, index) => new { index, value }))
            {
                item.value.ColorQuantityByPrice = colors[item.index];
            }
        }

        public IEnumerable<string> GetBindings(IEnumerable<Book> books)
        {
            return books.Select(e => e.Binding).Distinct();
        }

        private IEnumerable<byte> GetColorBites<T>(IOrderedEnumerable<T> orderedBookList) where T:class
        {
            //this list will help to get corresponding color quantity for the current item
            var colorQuantity = Enumerable.Range(0, 255).Select(i => (byte)i).ToArray();

            var step = colorQuantity.Count() / orderedBookList.Count();

            var newList = colorQuantity.Where(e => e % step == 0).OrderBy(e => e);
            return newList;
        }

    }
}