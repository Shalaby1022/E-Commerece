namespace E_Commerece.API.ResourcceParameters
{
    public class ProductResourceParameters
    {

        private const int maxPageSize = 30;
        public int PageIndex { get; set; } = 1;
        
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        private string? _search;
        public string Seacrh
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}
