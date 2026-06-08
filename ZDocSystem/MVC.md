
---

## 📑 HƯỚNG DẪN VIẾT CODE TRONG PROJECT (MÔ HÌNH MVC)

Dự án này chạy theo mô hình **MVC (Model - View - Controller)** kết hợp với **Entity Framework Core (Code-First)**. Khi viết code, mọi người chú ý làm đúng theo các phân vùng sau:

### 📁 1. Quy định vị trí các thư mục

* **Models**: Nơi tạo các file thuộc tính cấu trúc bảng dữ liệu (Ví dụ: `Room.cs`, `Bill.cs`). Nếu có thay đổi cấu trúc bảng, phải mở console chạy lệnh `Add-Migration` và `Update-Database`.
* **Controllers**: Nơi viết code xử lý logic chính, lấy dữ liệu từ database và điều hướng. Tên file bắt buộc phải có chữ Controller ở đuôi (Ví dụ: `RoomController.cs`, `CustomerController.cs`).
* **Views**: Nơi làm giao diện HTML hiển thị (`.cshtml`). Thư mục này chia theo tên của Controller (Ví dụ: Code giao diện của `RoomController` thì phải nằm trong thư mục `Views/Room/`).

---

### 🔄 2. Quy trình 3 bước khi làm một chức năng mới (Ví dụ: Quản lý Phòng)

* **Bước 1 (Làm Model):** Vào thư mục `Models` tạo file `Room.cs` để định nghĩa các cột dữ liệu (Id, Số phòng, Loại phòng, Giá...). Sau đó đăng ký bảng này vào file `HotelDbContext.cs`.
* **Bước 2 (Làm Controller):** Vào thư mục `Controllers` tạo file `RoomController.cs`. Viết các hàm (Action) xử lý như: `Index()` (Xem danh sách), `Create()` (Thêm mới), `Edit()` (Sửa), `Delete()` (Xóa).
* **Bước 3 (Làm View):** Click chuột phải vào các hàm Action trong Controller -> Chọn **Add View** để hệ thống tự tạo file giao diện nằm đúng thư mục `Views/Room/`. Vào đó code giao diện form, bảng biểu.

---

### 🚀 3. Cách Push code lên GitHub sau khi làm xong

1. Mở tab **Git Changes** ở cột bên phải Visual Studio.
2. Nhập lời nhắn ngắn gọn vào ô trống (Ví dụ: *Vừa làm xong chức năng thêm phòng*).
3. Bấm nút **Commit All**.
4. Bấm vào nút **Mũi tên hướng lên $\uparrow$ (Push)** ở thanh công cụ nhỏ phía trên để đẩy code lên GitHub cho cả team kéo về.

---

