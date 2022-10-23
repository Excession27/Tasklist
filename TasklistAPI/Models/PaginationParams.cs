namespace TasklistAPI.Models
{
    public class PaginationParams
    {
        private const int MaxItems = 36;
        private int itemsPerPage;

        public int Page { get; set; } = 1;
        public int ItemsPerPage
        {
            get => itemsPerPage;
            set => itemsPerPage = value > MaxItems ? MaxItems : value;
        }
    }
}
