using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress]
        public string? email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}
