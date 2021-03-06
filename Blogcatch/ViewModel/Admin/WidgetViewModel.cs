﻿using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Admin
{
    public class WidgetViewModel
    {
        public int Id { get; set; }
        
        public short Sorting { get; set; }

        public bool Enabled { get; set; }
        
        public string Type { get; set; }

        public string Description { get; set; }

        public WidgetViewModel(Widget widget)
        {
            this.Id = widget.Id;
            this.Sorting = widget.Sorting;
            this.Enabled = widget.Enabled;
            this.Type = widget.Type;
            this.Description = widget.Description;
        }
    }
}