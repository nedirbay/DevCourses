namespace DevCourses.DataAccess.Helper
{
    public class QueryParameters
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // Sıralama için
        public string? SortBy { get; set; } // Hangi alana göre sıralanacağı (örn: "experiencePoints")
        public string? SortDirection { get; set; } = "asc"; // "asc" veya "desc"

        // Filtreleme için
        public string? FilterBy { get; set; } // Hangi alana göre filtreleneceği (örn: "league")
        public string? FilterValue { get; set; } // Filtre değeri (örn: "Gold")

        // Arama için
        public string? SearchQuery { get; set; } // Genel arama sorgusu
    }
}
