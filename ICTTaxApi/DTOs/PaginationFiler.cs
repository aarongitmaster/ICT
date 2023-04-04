namespace ICTTaxApi.DTOs
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 15;
        }
        public PaginationFilter(int pageNumber,
            int pageSize,
            int firstPage, 
            int lastPage,
            int totalPages)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.FirstPage = firstPage;
            this.LastPage = lastPage;
            this.TotalPages = totalPages;
        }
    }
}
