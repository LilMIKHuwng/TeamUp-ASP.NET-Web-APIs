using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RatingModelViews
{
    public class CreateRatingModelView
    {
        [Required(ErrorMessage = "Người đánh giá (ReviewerId) là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "ReviewerId phải là số nguyên dương.")]
        public int ReviewerId { get; set; }

        [Required(ErrorMessage = "Người được đánh giá (RevieweeId) là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "RevieweeId phải là số nguyên dương.")]
        public int RevieweeId { get; set; }

        [Required(ErrorMessage = "Giá trị đánh giá là bắt buộc.")]
        [Range(1, 5, ErrorMessage = "Giá trị đánh giá phải nằm trong khoảng từ 1 đến 5.")]
        public int RatingValue { get; set; }

        [StringLength(500, ErrorMessage = "Bình luận không được vượt quá 500 ký tự.")]
        public string? Comment { get; set; }
    }
}
