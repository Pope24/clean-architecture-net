using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Bases
{
    public class BasePaging<T>
    {
        public int CurrentPage { get; private set; } = 1;
        public List<T> Items { get; set; } = new List<T>();
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; } = 9;
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public BasePaging(List<T> _items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Items.AddRange(_items);
        }

        public static BasePaging<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new BasePaging<T>(items, count, pageNumber, pageSize);
        }
    }
}
