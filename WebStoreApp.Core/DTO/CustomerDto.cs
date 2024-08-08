using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Application.DTO
{
    public class CustomerDto 
    {

        public IEnumerable<Customer> Users { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchTerm { get; set; }
        public int TotalRecords { get; set; }
    }
}
