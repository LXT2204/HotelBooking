using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    [Table("tbl_category_room")]
    public class CategoryRoom
    {
        [Key]
        public int category_id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên loại phòng" )]
        public string category_name { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả loại phòng")]
        public string category_desc { get; set; }
        [Required]
        public int category_status { get; set; }
    }
}
