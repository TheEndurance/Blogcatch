using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogcatch.Models
{
    public class Pager
    {

        public int TotalItems { get; set; }
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager(int totalItems,int? page,int numItemsPerPage = 10)
        {
            var totalPages = (int) Math.Ceiling((decimal) totalItems / (decimal) numItemsPerPage);
            var currentPage = page ?? 1; //if page is null, set the value of current page to 1
            var startPage = currentPage - 5; 
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1); //add 5 to endPage
                startPage = 1; //set firstPage to 1
            }

            if (endPage > totalPages) //If the endPage is more than the number of pages
            {
                endPage = totalPages; //set the endPage to the maximum number of pages
                if (endPage > 10) //once the endpage moves past 10 pages  
                {
                    startPage = endPage - 9;
                }
            }

            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.NumItemsPerPage = numItemsPerPage;
            this.CurrentPage = currentPage;
            this.StartPage = startPage;
            this.EndPage = endPage;
        }
    }
}