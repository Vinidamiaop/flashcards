using System.ComponentModel.DataAnnotations;

namespace flashcards.domain.Requests
{
    public class PagedRequest : Request
    {
        [MinLength(1, ErrorMessage = "Invalid page number")]
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}