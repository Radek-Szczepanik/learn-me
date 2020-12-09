namespace LearnMe.Core.DTO.Config
{
    public class PaginationHeader
    {
        public int CurentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationHeader(int curentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurentPage = curentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}
