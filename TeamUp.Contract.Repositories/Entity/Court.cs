using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using TeamUp.Core.Base;


namespace TeamUp.Contract.Repositories.Entity
{
    public class Court : BaseEntity
    {
        public int SportsComplexId { get; set; }
        public virtual SportsComplex SportsComplex { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal PricePerHour { get; set; }

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

        public string Status { get; set; }

        public virtual ICollection<CourtBooking> CourtBookings { get; set; }
        public virtual ICollection<CoachBooking> CoachBookings { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }



    }

}
