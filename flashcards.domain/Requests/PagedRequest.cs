using System.ComponentModel.DataAnnotations;

namespace flashcards.domain.Requests
{
    public abstract class PagedRequest : Request
    {
        [MinLength(1, ErrorMessage = "Invalid page number")]
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = Configuration.DefaultPageSize;

        public PagedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int GetPageNumber() => PageNumber;
        public int GetPageSize() => PageSize;

        public override bool IsValid()
        {
            if(PageNumber <= 0) {
                Errors.Add("Invalid page");
            }

            if(PageSize <= 0) {
                Errors.Add("Invalid page size");
            }

            return Errors.Count <= 0;
        }
    }
}