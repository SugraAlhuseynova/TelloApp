using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.DTOs
{
    public class PaginatedListDto2<T>:List<T>
    {
        public PaginatedListDto2(List<T> items, int count, int pageIndex, int pagesize)
        {
            TotalPage = (int)Math.Ceiling(count / (double)pagesize);
            this.AddRange(items);
            PageIndex = pageIndex;
        }
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public bool HasPrev { get => PageIndex > 1; }
        public bool HasNext { get => PageIndex < TotalPage; }
    }
}