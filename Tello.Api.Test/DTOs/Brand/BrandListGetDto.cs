namespace Tello.Api.Test.DTOs.Brand
{
    public class BrandListGetDto<T>
    {
        //public BrandListGetDto(List<T> items)
        //{
        //    this.AddRange(items);
        //}
        public List<T> Items { get; set; }
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public bool HasPrev { get => PageIndex > 1; }
        public bool HasNext { get => PageIndex < TotalPage; }
    }
}
