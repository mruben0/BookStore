using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BLL.Services
{
    public class CsvConverter : ICsvConverter
    {
        public IEnumerable<T> Convert<T>(string scvString, char seperator) where T : class, new()
        {
            var lines = scvString.Split(
                            new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.None
                        ).ToList();

            var properties = typeof(T).GetProperties();

            var headers = lines.FirstOrDefault().Split(seperator).AsEnumerable();

            //remove headers line
            lines.RemoveAt(0); 

            foreach (var line in lines)
            {
                if (line.IndexOf(seperator) >= 0)
                {
                    string[] scvValues = line.Split(seperator);
                    T obj = new T();

                    foreach (var item in properties)
                    {
                        var type = item.PropertyType;

                        //using replace and tolower methods to have unified values and get the corresponding field
                        var index = headers.Select((header, headerIndex) => new { header, headerIndex })
                                    .SingleOrDefault(e => e.header.Replace(" ", "").ToLower() == item.Name.ToLower())
                                    .headerIndex;

                        //using System.Convert to change type from string to the corresponding type
                        item.SetValue(obj, this.ChangeType(scvValues[index], item.PropertyType));

                    }
                    yield return obj;
                }
            }
        }

        private object ChangeType(string value, Type type)
        {
            return System.Convert.ChangeType(value, type);
        }
    }
}