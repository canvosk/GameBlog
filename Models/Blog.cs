using System;
using System.ComponentModel.DataAnnotations;

namespace GameBlog.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string BlogHeader { get; set; }
        public string BlogContent { get; set; }
        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime BlogDate { get; set; }
        public int Counter { get; set; }
        public bool IsPublish { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}