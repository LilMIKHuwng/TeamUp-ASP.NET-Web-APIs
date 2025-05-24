using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ModelViews.RatingModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.SportsComplexModelViews
{
    public class SportsComplexModelView
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> ImageUrls { get; set; }

        public double Latitude { get; set; }   // Vĩ độ
        public double Longitude { get; set; }  // Kinh độ
        public EmployeeResponseModel Owner { get; set; }
        public RatingSummaryModelView RatingSummaryModelView { get; set; }
    }
}
