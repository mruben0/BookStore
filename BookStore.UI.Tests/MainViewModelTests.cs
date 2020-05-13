using BookStore.BLL.Models;
using BookStore.BLL.Services;
using BookStore.UI.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookStore.UI.Tests
{
    public class MainViewModelTests
    {
        private MainViewModel mainViewModel;
        private Mock<ICsvConverter> csvConverterMock = new Mock<ICsvConverter>();
        private Mock<IDataService> dataServiceMock = new Mock<IDataService>();
        private List<Book> _books;
        private List<BookModel> _bookModels;
        public MainViewModelTests()
        {
            mainViewModel = new MainViewModel(csvConverterMock.Object, dataServiceMock.Object);

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

            _bookModels = new List<BookModel>
            {
                new BookModel
                {
                    Author="Neil Gaiman",
                    Binding="Unknown",
                    Title = "Neverwhere",
                    InStock = "Yes",
                    Description = @"Neverwhere is the companion novelisation written by English author Neil Gaiman of the television serial Neverwhere, by Gaiman and Lenny Henry. The plot and characters are exactly the same as in the series, with the exception that the novel form allowed Gaiman to expand and elaborate on certain elements of the story and restore changes made in the televised version from his original plans.",
                    Price = 50,
                    Year =1996
                },
                new BookModel
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
        public void ShouldRemoceNotInStockItems()
        {
            //Arrange
            csvConverterMock.Setup(e => e.Convert<Book>(It.IsAny<string>(), It.IsAny<char>())).Returns(_books);
            dataServiceMock.Setup(e => e.BuildBookModelsFromBooks(It.IsAny<IEnumerable<Book>>())).Returns(_bookModels);
            dataServiceMock.Setup(e => e.GetBindings(It.IsAny<IEnumerable<Book>>())).Returns(new List<string>() { "Unknown" });

            //act
            mainViewModel.OpenPredefinedCSVCommand.Execute(null);
            mainViewModel.RemoveNotInStocksCommand.Execute(null);

            //assert
            Assert.DoesNotContain(mainViewModel.BookModels, e => e.InStock == "no");
            csvConverterMock.Verify(m=>m.Convert<Book>(It.IsAny<string>(), It.IsAny<char>()),Times.Once);
            dataServiceMock.Verify(e => e.BuildBookModelsFromBooks(It.IsAny<IEnumerable<Book>>()),Times.Once);
            dataServiceMock.Verify(e => e.GetBindings(It.IsAny<IEnumerable<Book>>()), Times.Once);
        }
    }
}