using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TeamUp.ModelViews.CourtModelViews
{
    public class CreateCourtModelView
    {
        public int SportsComplexId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal PricePerHour { get; set; }

        public List<IFormFile> ImageUrls { get; set; }
    }
}
