using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
    }
}
