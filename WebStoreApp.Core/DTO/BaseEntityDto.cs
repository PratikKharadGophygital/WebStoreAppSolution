using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTO
{
 

    public abstract class BaseEntityDto 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0.")]
        public Int64 CurrentPage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0.")]
        public Int64 PageSize { get; set; } = 10;
        public Int64 PageNumber { get; set; } = 1;

        public Int64 TotalCount { get; set; }
        public Int64 TotalPages => (Int64)Math.Ceiling((double)TotalCount / PageSize);

        [StringLength(100, ErrorMessage = "Search term cannot exceed 100 characters.")]
        public string SearchTerm { get; set; }

        public string SortColumn { get; set; } = "Id";

        public string SortDirection { get; set; } = "ASC";



    }
}
