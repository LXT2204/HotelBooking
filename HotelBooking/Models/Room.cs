using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    [Table("tbl_room")]
    public class Room
    {
        [Key]
        public int room_id { get; set; }
        public CategoryRoom? category { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên phòng")]
        public string? room_name { get; set; }
        public int? category_id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả phòng")]
        public string? room_desc { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiện ích của phòng")]
        public string? room_content { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập giá phòng")]
        public int? room_price { get; set; }
        public string? room_image { get; set; }
        [Required]
        public int room_status { get; set; }
    }
}
