using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.SportsComplexModelViews
{
    public class UpdateSportsComplexModelView
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<IFormFile>? ImageUrls { get; set; }
        public int? OwnerId { get; set; }
    }
}
