using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class SportsComplex : BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // This will be the column saved in the database
        public string ImageUrlsJson { get; set; } = "[]";

        [NotMapped]
        public List<string> ImageUrls
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImageUrlsJson))
                    return new List<string>();

                // Deserialize the JSON to a list of URLs without any decoding
                return JsonSerializer.Deserialize<List<string>>(ImageUrlsJson) ?? new List<string>();
            }
            set => ImageUrlsJson = JsonSerializer.Serialize(value);
        }

        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public string Status { get; set; }

        // Thêm tọa độ
        public double Latitude { get; set; }   // Vĩ độ
        public double Longitude { get; set; }  // Kinh độ

        public virtual ICollection<Court> Courts { get; set; }


    }
}
