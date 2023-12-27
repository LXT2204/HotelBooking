using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	[Table("tbl_order_details")]
	public class OrderDetails
	{
		[Key]
		public int order_id { get; set; }
		public int room_id { get; set; }
		public string room_name { get; set; }
		public double room_price { get; set; }
		public string room_image { get; set; }
		public DateTime checkin {  get; set; }
		public DateTime checkout { get; set; }


	}
}
