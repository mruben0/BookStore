using System.Collections.Generic;

namespace BookStore.BLL.Services
{
    public interface ICsvConverter
    {
        IEnumerable<T> Convert<T>(string scvString, char seperator) where T : class, new();
    }
}