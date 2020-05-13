using BookStore.BLL.Models;
using System.Collections.Generic;

namespace BookStore.BLL.Services
{
    public interface IDataService
    {
        IEnumerable<BookModel> BuildBookModelsFromBooks(IEnumerable<Book> books);

        void FixColorQuantityByPrice(IEnumerable<BookModel> models);

        IEnumerable<string> GetBindings(IEnumerable<Book> books);
    }
}