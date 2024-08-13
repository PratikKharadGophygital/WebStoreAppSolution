using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTO
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; }
        public SortOrder SortOrder { get; set; }
        public string SearchTerm { get; set; }
        
    }
    public enum SortOrder
    {
        Ascending,
        Descending
    }


}
