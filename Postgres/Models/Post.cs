﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Postgres.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
