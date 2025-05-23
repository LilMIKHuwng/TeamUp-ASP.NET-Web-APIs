﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.PackageModelViews
{
    public class PackageModelView
    {
        public int Id { get ; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int DurationDays { get; set; }
        public string Type { get; set; }
    }
}
