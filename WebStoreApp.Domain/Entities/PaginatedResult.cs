using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Domain.Entities
{
    /// <summary>
    /// This is generic type 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalRecords { get; set; }
    }
}
