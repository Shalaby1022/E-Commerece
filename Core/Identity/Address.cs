using System.ComponentModel.DataAnnotations;

namespace Core.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; } = string.Empty;
        public string LName {  get; set; } = string.Empty;
        public string Street { get; set; }= string.Empty;
        public string City { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;


        // Navigation Properitites
        public ApplicationUser User { get; set; }
        [Required]
        public string AppUserId { get; set; }

    }
}