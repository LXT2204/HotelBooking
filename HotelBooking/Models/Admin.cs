using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    [Table("tbl_admin")]
    public class Admins
    {
        [Key]
        public int admin_id { get; set; }
        public string admin_email { get; set; }
        public string admin_password { get; set; }
        public string admin_name { get; set; }
        public string admin_phone { get; set; }
    }
}
