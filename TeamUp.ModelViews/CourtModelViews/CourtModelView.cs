using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.RatingModelViews;
using TeamUp.ModelViews.SportsComplexModelViews;

namespace TeamUp.ModelViews.CourtModelViews
{
    public class CourtModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal PricePerHour { get; set; }

        public List<string> ImageUrls { get; set; }

        public SportsComplexModelView SportsComplexModelView { get; set; }

        public RatingSummaryModelView RatingSummaryModelView { get; set; }
    }
}
