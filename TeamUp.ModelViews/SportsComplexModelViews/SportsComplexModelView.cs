using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public EmployeeResponseModel Owner { get; set; }
    }
}
