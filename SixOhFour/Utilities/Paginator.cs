using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.Utilities
{
    public class Paginator
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageLength { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Paginator(int items, int? pageNumber, int pageLength)
        {
            //Get the number of pages based on the number of items and the number of items per page
            int pageCount = (int)Math.Ceiling((double)items / (double)pageLength);

            var currentPage = pageNumber != null ? (int)pageNumber : 1;

            var startPage = currentPage - 4;
            var endPage = currentPage + 4;

            //If the beginning page is less than 0...
            if (startPage <= 0)
            {
                //...let the end page equal the positive value of the start page -1 (which adds one to the actual end page)...
                endPage -= (startPage);
                //...and set the start page to 1 as the minimum page that can be started from.
                startPage = 1;
            }

            // if the calculated end page is greater than the total page count...
            if (endPage > pageCount)
            {
                //...set the end page to the total number of pages...
                endPage = pageCount;
                //...and check if the 
                if (endPage > 9)
                {
                    startPage = endPage - 8;
                }
            }
            TotalItems = items;
            CurrentPage = currentPage;
            PageLength = pageLength;
            TotalPages = pageCount;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}