using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	[Table("tbl_order")]

	public class Order
	{
         [Key]
		 public int order_id { get; set; }
		public int customer_id {  get; set; }
		public double order_total { get; set; }
		public int order_status {  get; set; }

	}
}
