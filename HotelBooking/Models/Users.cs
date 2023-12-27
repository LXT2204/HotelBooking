using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage  ="Vui lòng không để trống")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]

        public string? password { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]

        public string? phone { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]

        public string? address { get; set; }
        public int user_role { get; set; }
    }
}
