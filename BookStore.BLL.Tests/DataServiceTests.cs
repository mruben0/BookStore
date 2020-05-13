using BookStore.BLL.Models;
using BookStore.BLL.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookStore.BLL.Tests
{
    public class DataServiceTests
    {
        private readonly IDataService _dataService;
        private List<Book> _books;

        public DataServiceTests()
        {
            _dataService = new DataService();
            _books = new List<Book>
            {
                new Book
                {
                    Author="Neil Gaiman",
                    Binding="Unknown",
                    Title = "Neverwhere",
                    InStock = "Yes",
                    Description = @"Neverwhere is the companion novelisation written by English author Neil Gaiman of the television serial Neverwhere, by Gaiman and Lenny Henry. The plot and characters are exactly the same as in the series, with the exception that the novel form allowed Gaiman to expand and elaborate on certain elements of the story and restore changes made in the televised version from his original plans.",
                    Price = 50,
                    Year =1996
                },
                new Book
                {
                    Author = "Douglas Adams",
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Binding = "Unknown",
                    InStock = "no",
                    Description = "The Hitchhiker's Guide to the Galaxy is a fictional electronic guide book in the multimedia scifi/comedy series of the same name by Douglas Adams.",
                    Year = 1978,
                    Price = 42
                }
            };
        }

        [Fact]
        public void HigherPriceShouldHaveHigherGradient()
        {
            //Act
            var bookModels = _dataService.BuildBookModelsFromBooks(_books);
            _dataService.FixColorQuantityByPrice(bookModels);
            //Assert
            Assert.True(bookModels.Single(e => e.Price == 50).ColorQuantityByPrice > bookModels.Single(e => e.Price == 42).ColorQuantityByPrice);
        }

        [Fact]
        public void ShouldGetDistinctBindings()
        {
            //Act
            var bindings = _dataService.GetBindings(_books);

            //Assert
            Assert.True(bindings.Count() == 1);
            Assert.Contains(_books.First().Binding, bindings);
        }        
    }
}