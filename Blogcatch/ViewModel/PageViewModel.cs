using Blogcatch.Areas.Admin.Controllers;
using Blogcatch.Areas.Admin.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Blogcatch.ViewModel
{
    public class PageViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        [MinLength(3)]
        public string Body { get; set; }
        public int Sorting { get; set; }

        public bool HasSidebar { get; set; }

        public string Heading
        {
            get { return (Id == 0) ? "Add a new page" : "Edit page"; }
        }

        public string Action
        {
            get
            {
                Expression<Func<PagesController, ActionResult>> AddPage = (pg => pg.AddPage(null));
                Expression<Func<PagesController, ActionResult>> EditPage = (pg => pg.EditPage(0));
                var action = (Id == 0) ? AddPage : EditPage;
                return (action.Body as MethodCallExpression).Method.Name;
            }

        }


        public PageViewModel(Page page)
        {
            this.Id = page.Id;
            this.Title = page.Title;
            this.Slug = page.Slug;
            this.Body = page.Body;
            this.Sorting = page.Sorting;
            this.HasSidebar = page.HasSidebar;
        }

        public PageViewModel()
        {
        }

    }
}