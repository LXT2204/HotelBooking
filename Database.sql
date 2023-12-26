SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

-- Bảng `tbl_admin`

CREATE TABLE `tbl_admin` (
  `admin_id` int NOT NULL,
  `admin_email` varchar(100) NOT NULL,
  `admin_password` varchar(255) NOT NULL,
  `admin_name` varchar(255) NOT NULL,
  `admin_phone` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


INSERT INTO `tbl_admin` (`admin_id`, `admin_email`, `admin_password`, `admin_name`, `admin_phone`) VALUES
(1, 'admin@gmail.com', 'admin', 'Admin', '111');

-- Bảng `tbl_category_room`

CREATE TABLE `tbl_category_room` (
  `category_id` int NOT NULL,
  `category_name` varchar(255) NOT NULL,
  `category_desc` text NOT NULL,
  `category_status` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `tbl_category_room` (`category_id`, `category_name`, `category_desc`, `category_status`) VALUES
(6, 'Phòng Đôi', 'a', 0),
(7, 'Phòng Đơn', 'a', 0),
(8, 'Phòng Vip', 'b', 0),
(9, 'Phòng Tổng Thống', 'c', 0),
(10, 'Phòng Tình Yêu', 'd', 0),
(11, 'Phòng Gia Đình', 'aaaa', 0);

-- Bảng `tbl_room`

CREATE TABLE `tbl_room` (
  `room_id` int NOT NULL,
  `room_name` varchar(255) NOT NULL,
  `category_id` int NOT NULL,
  `room_desc` text NOT NULL,
  `room_content` text NOT NULL,
  `room_price` int NOT NULL,
  `room_image` varchar(255) NOT NULL,
  `room_status` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `tbl_room` (`room_id`, `room_name`, `category_id`, `room_desc`, `room_content`, `room_price`, `room_image`, `room_status`) VALUES
(11, 'Phòng 101', 7, 'Phòng Đơn tại Khách sạn Chúng tôi là một không gian thoáng đãng và tiện nghi được thiết kế đặc biệt để đáp ứng nhu cầu của khách du lịch độc thân hoặc những người có nhu cầu ở một mình. Với diện tích rộng rãi và sự bố trí hợp lý, phòng đơn mang lại sự thoải mái và tiện lợi cho quý khách.', 'Các tiện nghi trong phòng đơn bao gồm:\r\n\r\nMột giường đơn thoải mái với trang thiết bị giường cao cấp để đảm bảo giấc ngủ ngon.\r\nKhu vực làm việc với bàn và ghế tiện lợi để quý khách có thể làm việc hoặc sử dụng máy tính.\r\nTủ quần áo và kệ để cất đồ cá nhân của quý khách.\r\nPhòng tắm riêng với vòi sen và các tiện nghi vệ sinh cá nhân cơ bản.\r\nTruyền hình cáp và màn hình TV phẳng để quý khách có thể thư giãn và giải trí trong thời gian nghỉ ngơi.\r\nNgoài ra, phòng đơn của chúng tôi còn có đèn chiếu sáng mềm mại, điều hòa nhiệt độ và ổ cắm điện tiêu chuẩn để đảm bảo sự tiện nghi tối ưu. Chúng tôi cũng cung cấp dịch vụ phòng hàng ngày để giữ cho không gian của quý khách luôn sạch sẽ và thoáng đãng.', '500000', '15.png', 0);

-- Cấu trúc bảng cho bảng `users`

CREATE TABLE `users` (
  `id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `phone` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL,
  `dob` datetime
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE `tbl_admin` ADD PRIMARY KEY (`admin_id`);
ALTER TABLE `tbl_category_room` ADD PRIMARY KEY (`category_id`);
ALTER TABLE `tbl_room` ADD PRIMARY KEY (`room_id`);
ALTER TABLE `users` ADD PRIMARY KEY (`id`);


ALTER TABLE `tbl_admin` MODIFY `admin_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
ALTER TABLE `tbl_category_room` MODIFY `category_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
ALTER TABLE `tbl_room` MODIFY `room_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
ALTER TABLE `users` MODIFY `id` bigint NOT NULL AUTO_INCREMENT;

DELIMITER //
create trigger delete_room_of_category
before delete on tbl_category_room
for each row
begin
	delete from tbl_room where category_id = old.category_id;
end;
//
DELIMITER ;

drop table tbl_admin;
drop table tbl_category_room;
drop table tbl_room;
drop table users;