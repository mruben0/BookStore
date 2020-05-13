using BookStore.BLL.Models;
using BookStore.BLL.Services;
using System;
using System.Linq;
using Xunit;

namespace BookStore.BLL.Tests
{
    public class CsvConverterTest
    {
        private ICsvConverter _csvConverter;

        public CsvConverterTest()
        {
            _csvConverter = new CsvConverter();
        }

        [Fact]
        public void ShouldConvert()
        {
            //Arrange
            var csvString = "Title; Author; Year; Price; In Stock; Binding; Description \nHitchhiker's guide to the galaxy;Douglas Adams;1978;42;no;Hardcover;The Hitchhiker's Guide to the Galaxy is a fictional electronic guide book in the multimedia scifi/comedy series of the same name by Douglas Adams";
            var seperator = ';';
            //Act
            var books = _csvConverter.Convert<Book>(csvString, seperator).ToList();
            //Assert
            Assert.Equal("Douglas Adams", books.First().Author);
        }

        [Fact]
        public void ShouldThrowException()
        {
            //Arrange
            var csvString = "Title; Author; Year; Price; In Stock; Binding; Description \nHitchhiker's guide to the galaxy;Douglas Adams;1978;20;no;Hardcover;The Hitchhiker's Guide to the Galaxy is a fictional electronic guide book in the multimedia scifi/comedy series of the same name by Douglas Adams";
            var seperator = 'A';
            // Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                var books = _csvConverter.Convert<Book>(csvString, seperator).ToList();
            });
        }
    }
}