using System.Web.Mvc;
using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Admin
{
    public class SidebarViewModel
    {
        public SidebarViewModel()
        {
        }

        public SidebarViewModel(Sidebar sidebar)
        {
            this.Id = sidebar.Id;
            this.Body = sidebar.Body;
        }
        public int Id { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }
}