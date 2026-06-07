

# TÀI LIỆU CẤU TRÚC CƠ SỞ DỮ LIỆU (DATABASE SCHEMA)

> **Dự án:** Hệ thống quản lý khách sạn (Hotel Management System)
> **Hệ quản trị DB:** Microsoft SQL Server

---

## Sơ đồ tổng quan các thực thể (14 Bảng)

### 1. Bảng: `Role` (Phân quyền hệ thống)

*Quản lý danh sách các vai trò/quyền hạn truy cập trong hệ thống.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `RoleID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã phân quyền (Tự tăng) |
| `RoleName` | `NVARCHAR(50)` | `NOT NULL`, `UNIQUE` | Tên quyền (`Admin`, `Hotel Manager`,...) |
| `Description` | `NVARCHAR(255)` | `NULL` | Mô tả chi tiết về quyền |

---

### 2. Bảng: `Account` (Tài khoản người dùng)

*Lưu trữ thông tin tài khoản đăng nhập của nhân viên và quản lý.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `AccountID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã tài khoản (Tự tăng) |
| `Username` | `NVARCHAR(50)` | `NOT NULL`, `UNIQUE` | Tên đăng nhập |
| `PasswordHash` | `NVARCHAR(255)` | `NOT NULL` | Mật khẩu đã mã hóa |
| `Email` | `NVARCHAR(100)` | `NULL` | Thư điện tử |
| `Phone` | `NVARCHAR(20)` | `NULL` | Số điện thoại |
| `Status` | `NVARCHAR(20)` | `NOT NULL`, `DEFAULT 'Active'`, `CHECK ('Active', 'Inactive')` | Trạng thái hoạt động |
| `CreatedAt` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian tạo tài khoản |
| `UpdatedAt` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian cập nhật tài khoản |
| `RoleID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Liên kết tới bảng `Role(RoleID)` |

---

### 3. Bảng: `Hotel` (Thông tin khách sạn)

*Cấu hình thông tin cơ bản của thực thể khách sạn.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `HotelID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã khách sạn (Tự tăng) |
| `HotelName` | `NVARCHAR(150)` | `NOT NULL` | Tên khách sạn |
| `Address` | `NVARCHAR(255)` | `NULL` | Địa chỉ |
| `Phone` | `NVARCHAR(20)` | `NULL` | Số điện thoại bàn/hotline |
| `Email` | `NVARCHAR(100)` | `NULL` | Email liên hệ |
| `Description` | `NVARCHAR(MAX)` | `NULL` | Giới thiệu khách sạn |
| `Logo` | `NVARCHAR(255)` | `NULL` | Đường dẫn ảnh logo |
| `CheckInTime` | `TIME` | `NULL` | Giờ nhận phòng quy định |
| `CheckOutTime` | `TIME` | `NULL` | Giờ trả phòng quy định |

---

### 4. Bảng: `RoomType` (Loại phòng)

*Định nghĩa các loại phòng và chính sách giá tương ứng.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `RoomTypeID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã loại phòng (Tự tăng) |
| `TypeName` | `NVARCHAR(100)` | `NOT NULL` | Tên loại phòng (Single, Double, VIP...) |
| `Description` | `NVARCHAR(MAX)` | `NULL` | Mô tả tiện ích loại phòng |
| `Capacity` | `INT` | `NOT NULL` | Sức chứa tối đa (Số người) |
| `BasePrice` | `DECIMAL(18,2)` | `NOT NULL` | Giá thuê tiêu chuẩn theo đêm |
| `Size` | `DECIMAL(10,2)` | `NULL` | Diện tích phòng ($m^2$) |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Active'`, `CHECK ('Active', 'Inactive')` | Trạng thái áp dụng kinh doanh |

---

### 5. Bảng: `Room` (Phòng)

*Danh sách các phòng cụ thể trong khách sạn.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `RoomID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã phòng (Tự tăng) |
| `RoomNumber` | `NVARCHAR(20)` | `NOT NULL`, `UNIQUE` | Số phòng (Ví dụ: P101, P202) |
| `FloorNumber` | `INT` | `NULL` | Tầng |
| `Status` | `NVARCHAR(30)` | `DEFAULT 'Available'`, `CHECK ('Available', 'Occupied', 'Dirty', 'Cleaning', 'Maintenance')` | Tình trạng thực tế của phòng |
| `Description` | `NVARCHAR(MAX)` | `NULL` | Ghi chú thêm về phòng |
| `RoomTypeID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Liên kết tới bảng `RoomType(RoomTypeID)` |

---

### 6. Bảng: `Guest` (Khách hàng)

*Quản lý hồ sơ thông tin của khách lưu trú.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `GuestID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã khách hàng (Tự tăng) |
| `FullName` | `NVARCHAR(150)` | `NOT NULL` | Họ và tên |
| `Gender` | `NVARCHAR(20)` | `CHECK ('Male', 'Female', 'Other')` | Giới tính |
| `DateOfBirth` | `DATE` | `NULL` | Ngày tháng năm sinh |
| `Nationality` | `NVARCHAR(100)` | `NULL` | Quốc tịch |
| `IdentityNumber` | `NVARCHAR(50)` | `NULL` | Số CMND/CCCD/Passport |
| `Phone` | `NVARCHAR(20)` | `NULL` | Số điện thoại liên lạc |
| `Email` | `NVARCHAR(100)` | `NULL` | Địa chỉ email |
| `Address` | `NVARCHAR(255)` | `NULL` | Địa chỉ thường trú |
| `CreatedAt` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian tạo hồ sơ khách |

---

### 7. Bảng: `Reservation` (Đơn đặt phòng)

*Thông tin tổng quát của một lượt đặt phòng.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `ReservationID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã đơn đặt (Tự tăng) |
| `ReservationCode` | `NVARCHAR(50)` | `UNIQUE` | Mã code tra cứu đơn đặt phòng |
| `CheckInDate` | `DATE` | `NOT NULL` | Ngày dự kiến nhận phòng |
| `CheckOutDate` | `DATE` | `NOT NULL` | Ngày dự kiến trả phòng |
| `NumberOfGuests` | `INT` | `DEFAULT 1` | Số lượng khách ở thực tế |
| `BookingDate` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian làm đơn đặt |
| `Status` | `NVARCHAR(30)` | `DEFAULT 'Pending'`, `CHECK ('Pending', 'Confirmed', 'CheckedIn', 'Completed', 'Cancelled')` | Trạng thái của đơn đặt phòng |
| `Notes` | `NVARCHAR(MAX)` | `NULL` | Yêu cầu đặc biệt của khách |
| `GuestID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Liên kết tới bảng `Guest(GuestID)` |
| `CreatedBy` | `INT` | `NOT NULL`, `FOREIGN KEY` | Nhân viên tiếp nhận `Account(AccountID)` |

---

### 8. Bảng: `ReservationRoom` (Chi tiết phòng đặt)

*Bảng trung gian thể hiện mối quan hệ nhiều-nhiều giữa đơn đặt và các phòng (Một đơn có thể đặt nhiều phòng).*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `ReservationRoomID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã chi tiết (Tự tăng) |
| `ReservationID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Thuộc về đơn đặt `Reservation(ReservationID)` |
| `RoomID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Phòng được chọn `Room(RoomID)` |
| `PricePerNight` | `DECIMAL(18,2)` | `NOT NULL` | Giá chốt thuê tính theo đêm |

---

### 9. Bảng: `Service` (Dịch vụ khách sạn)

*Danh mục sản phẩm/dịch vụ bổ trợ (Nhà hàng, Giặt ủi, Spa, MiniBar...).*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `ServiceID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã dịch vụ (Tự tăng) |
| `ServiceName` | `NVARCHAR(150)` | `NOT NULL` | Tên gọi dịch vụ |
| `Description` | `NVARCHAR(MAX)` | `NULL` | Mô tả dịch vụ |
| `Price` | `DECIMAL(18,2)` | `NOT NULL` | Đơn giá dịch vụ |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Active'`, `CHECK ('Active', 'Inactive')` | Trạng thái cung cấp dịch vụ |

---

### 10. Bảng: `ServiceUsage` (Chi tiết sử dụng dịch vụ)

*Ghi nhận nhật ký gọi đồ/dùng dịch vụ của phòng khách.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `UsageID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã lượt dùng (Tự tăng) |
| `ReservationID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Tính vào lượt đặt phòng nào |
| `ServiceID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Dịch vụ được sử dụng |
| `Quantity` | `INT` | `NOT NULL DEFAULT 1` | Số lượng gọi |
| `UnitPrice` | `DECIMAL(18,2)` | `NOT NULL` | Giá dịch vụ tại thời điểm gọi |
| `UsageDate` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian sử dụng |
| `RecordedBy` | `INT` | `NOT NULL`, `FOREIGN KEY` | Nhân viên ghi nhận `Account(AccountID)` |

---

### 11. Bảng: `Invoice` (Hóa đơn tổng)

*Bảng tính tiền thanh toán tổng hợp toàn bộ chi phí khi checkout.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `InvoiceID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã hóa đơn (Tự tăng) |
| `ReservationID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Xuất cho lượt đặt phòng nào |
| `InvoiceDate` | `DATETIME2` | `DEFAULT GETDATE()` | Ngày xuất hóa đơn |
| `RoomCharge` | `DECIMAL(18,2)` | `DEFAULT 0` | Tổng tiền phòng tích lũy |
| `ServiceCharge` | `DECIMAL(18,2)` | `DEFAULT 0` | Tổng tiền dịch vụ tích lũy |
| `Tax` | `DECIMAL(18,2)` | `DEFAULT 0` | Tiền thuế |
| `Discount` | `DECIMAL(18,2)` | `DEFAULT 0` | Số tiền giảm giá |
| `TotalAmount` | `DECIMAL(18,2)` | `NOT NULL` | Tổng số tiền thực tế khách phải trả |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Pending'`, `CHECK ('Pending', 'Paid', 'Cancelled')` | Trạng thái hóa đơn |

---

### 12. Bảng: `Payment` (Giao dịch thanh toán)

*Chi tiết các lượt thanh toán tiền của khách hàng.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `PaymentID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã lượt thanh toán (Tự tăng) |
| `InvoiceID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Áp vào hóa đơn nào |
| `Amount` | `DECIMAL(18,2)` | `NOT NULL` | Số tiền đóng |
| `PaymentMethod` | `NVARCHAR(30)` | `CHECK ('Cash', 'CreditCard', 'BankTransfer', 'EWallet')` | Phương thức giao dịch |
| `PaymentDate` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian thanh toán |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Completed'`, `CHECK ('Pending', 'Completed', 'Failed')` | Trạng thái giao dịch |

---

### 13. Bảng: `HousekeepingTask` (Nhiệm vụ dọn phòng)

*Phân công việc buồng phòng cho nhân viên vệ sinh.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `TaskID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã công việc (Tự tăng) |
| `RoomID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Phòng cần dọn |
| `AssignedTo` | `INT` | `NOT NULL`, `FOREIGN KEY` | Nhân viên buồng phòng xử lý `Account(AccountID)` |
| `TaskDate` | `DATE` | `NOT NULL` | Ngày thực hiện nhiệm vụ |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Pending'`, `CHECK ('Pending', 'InProgress', 'Completed')` | Trạng thái hoàn thành việc |
| `Notes` | `NVARCHAR(MAX)` | `NULL` | Ghi chú nhắc nhở dọn dẹp |

---

### 14. Bảng: `MaintenanceReport` (Báo cáo bảo trì thiết bị)

*Theo dõi thông tin hỏng hóc, sửa chữa hạ tầng kỹ thuật trong phòng.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `ReportID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã phiếu báo cáo (Tự tăng) |
| `RoomID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Phòng gặp sự cố |
| `ReportedBy` | `INT` | `NOT NULL`, `FOREIGN KEY` | Người phát hiện/báo cáo `Account(AccountID)` |
| `IssueDescription` | `NVARCHAR(MAX)` | `NOT NULL` | Nội dung chi tiết thiết bị hỏng |
| `Priority` | `NVARCHAR(20)` | `DEFAULT 'Medium'`, `CHECK ('Low', 'Medium', 'High', 'Critical')` | Mức độ khẩn cấp |
| `Status` | `NVARCHAR(20)` | `DEFAULT 'Open'`, `CHECK ('Open', 'InProgress', 'Resolved')` | Tiến độ xử lý sự cố |
| `ReportDate` | `DATETIME2` | `DEFAULT GETDATE()` | Ngày ghi nhận lỗi |
| `ResolvedDate` | `DATETIME2` | `NULL` | Ngày hoàn tất sửa chữa xong |

---

### 15. Bảng: `AuditLog` (Nhật ký hệ thống)

*Lưu vết mọi hành động sửa đổi dữ liệu quan trọng để phục vụ bảo mật.*

| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Mô Tả |
| --- | --- | --- | --- |
| `LogID` | `INT` | `PRIMARY KEY`, `IDENTITY(1,1)` | Mã dòng log (Tự tăng) |
| `AccountID` | `INT` | `NOT NULL`, `FOREIGN KEY` | Người thực hiện hành động |
| `Action` | `NVARCHAR(255)` | `NOT NULL` | Tên thao tác (Ví dụ: `DELETE_ROOM`, `UPDATE_INVOICE`) |
| `EntityName` | `NVARCHAR(100)` | `NULL` | Tên bảng chịu tác động |
| `EntityID` | `INT` | `NULL` | ID của bản ghi bị tác động |
| `CreatedAt` | `DATETIME2` | `DEFAULT GETDATE()` | Thời gian chính xác diễn ra |

---

## ⚡ Dữ liệu khởi tạo mặc định (Data Seed)

Hệ thống khởi tạo sẵn các phân quyền nền tảng cố định ngay khi khởi chạy:

* `Admin`: System Administrator (Quản trị viên tối cao)
* `Hotel Manager`: Hotel Manager (Quản lý khách sạn)
* `Receptionist`: Receptionist (Nhân viên lễ tân)
* `Room Staff`: Room Staff (Nhân viên buồng phòng)