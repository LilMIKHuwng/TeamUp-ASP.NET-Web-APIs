using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.SportsComplexModelViews
{
    public class CreateSportsComplexModelView
    {
        [Required(ErrorMessage = "Tên khu thể thao không được để trống.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng tải lên ít nhất một hình ảnh.")]
        [MinLength(1, ErrorMessage = "Cần ít nhất một hình ảnh.")]
        public List<IFormFile> ImageUrls { get; set; }

        [Required(ErrorMessage = "Id của chủ sân không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id của chủ sân phải là số hợp lệ.")]
        public int OwnerId { get; set; }
    }
}
