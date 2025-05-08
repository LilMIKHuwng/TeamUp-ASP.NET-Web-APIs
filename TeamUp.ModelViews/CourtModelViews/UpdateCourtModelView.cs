using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.CourtModelViews
{
    public class UpdateCourtModelView
    {
        public int? SportsComplexId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? PricePerHour { get; set; }

        public List<IFormFile>? ImageUrls { get; set; }
    }
}
