using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Client.Member.DTOs
{
    public class PaginationListDto<T>
    {
        public PaginationListDto(List<T> items, int count, int pageIndex, int pagesize)
        {
            Items = items;
            TotalPage = (int)Math.Ceiling(count / (double)pagesize);
            PageIndex = pageIndex;
        }
        public List<T> Items { get; set; }
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public bool HasPrev { get => PageIndex > 1; }
        public bool HasNext { get => PageIndex < TotalPage; }
    }
}
