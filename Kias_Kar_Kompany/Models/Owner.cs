using System.ComponentModel.DataAnnotations;

namespace Kias_Kar_Kompany.Models
{
    public class Owner
    {
        [Key]
        public required int OwnerId { get; set; }
        public required string OwnerName { get; set; }
        public required string OwnerSurname { get; set; }
        public required string Cellnumber { get; set; }
        public string? OwnerEmail { get; set; }

        public List<Vehicle>? Vehicles { get; set; }
    }
}
