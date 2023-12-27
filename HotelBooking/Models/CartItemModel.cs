namespace HotelBooking.Models
{
    public class CartItemModel
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get {
                DateTime ngaymuon = Convert.ToDateTime("Checkin");
                DateTime ngaytra = Convert.ToDateTime("Chekout");
                TimeSpan Time = ngaytra - ngaymuon;
                return Time.Days;
            } }
        public decimal Total { get
            {
                return Quantity * Price;
            } }
        public string image { get; set; }
        public CartItemModel() { }
        public CartItemModel(Room room,DateTime qty_Checkin,DateTime qty_Checkout)
        {
            RoomId = room.room_id;
            RoomName = room.room_name;
            Price = Convert.ToDecimal(room.room_price);
            image = room.room_image;
            Checkin = qty_Checkin;
            Checkout = qty_Checkout;

        }

    }
}
