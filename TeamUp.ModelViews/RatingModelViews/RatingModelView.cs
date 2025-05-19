using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RatingModelViews
{
    public class RatingModelView
    {
        public int Id { get; set; }
        public UserResponseModel Reviewer { get; set; }
        public UserResponseModel Reviewee { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }
    }
}
