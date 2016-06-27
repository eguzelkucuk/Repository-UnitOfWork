﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepveUOW.Data.Model
{
    public class Article : ModelBase
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        // Relations
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
