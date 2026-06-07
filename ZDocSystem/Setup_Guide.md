# 🏨 HỆ THỐNG QUẢN LÝ KHÁCH SẠN (HOTEL MANAGEMENT SYSTEM)
> **Tài liệu cấu hình môi trường và kết nối Cơ sở dữ liệu (Database Setup Guide)**
> **Công nghệ áp dụng:** .NET 8.0, ASP.NET Core MVC, Entity Framework Core (Code-First)

---

## 1. Danh Sách Tài Nguyên Thư Viện Đã Cài Đặt (NuGet Packages)
Để triển khai kiến trúc Code-First, hệ thống đã được tích hợp bộ 3 thư viện cốt lõi (khớp hoàn toàn với phiên bản .NET 8.0 của dự án):
* `Microsoft.EntityFrameworkCore.SqlServer` (v8.0.27): Trình cung cấp dữ liệu, giúp mã nguồn C# giao tiếp trực tiếp với hệ quản trị cơ sở dữ liệu MS SQL Server.
* `Microsoft.EntityFrameworkCore.Tools` (v8.0.27): Bộ công cụ tích hợp vào cửa sổ lệnh `Package Manager Console` để thực hiện các lệnh quản lý phiên bản database.
* `Microsoft.EntityFrameworkCore.Design` (v8.0.27): Thư viện hỗ trợ phân tích các lớp thực thể (Entities) để biên dịch sang cấu trúc bảng SQL.

---

## 2. Chuỗi Kết Nối An Toàn (Connection String)
Cấu hình kết nối được đặt tập trung tại file `appsettings.json` ở thư mục gốc của dự án:
* **Tên Cơ sở dữ liệu:** `HotelManage_CodeFirst`
* **Phương thức xác thực:** Windows Authentication (`Trusted_Connection=True;`). Không cần tài khoản/mật khẩu, sử dụng quyền trực tiếp của người dùng máy cục bộ để tối ưu hóa khả năng di động khi chạy code trên các máy tính khác nhau (phòng thực hành FPTU).

---

## 3. Kiến Trúc Lớp Bối Cảnh Trung Tâm (DbContext)
Lớp `HotelDbContext.cs` được khởi tạo tại thư mục `Models`, đóng vai trò là "Cầu nối" đại diện cho toàn bộ Database trong mã nguồn.
* Quản lý tập trung các bảng dữ liệu qua các thuộc tính `DbSet<T>`.
* Cấu hình các ràng buộc nâng cao (Fluent API) như: chỉ mục duy nhất (`IsUnique()`) cho `Username` của tài khoản và `RoleName` của phân quyền.

---

## 4. Nhật Ký Lệnh Triển Khai (Migration & Database Update)
Hệ thống đã thực hiện thành công chuỗi lệnh khởi tạo Database từ cửa sổ dòng lệnh:
1. `Add-Migration InitialCreate`: Quét toàn bộ các lớp C# để tự động sinh ra file kiến trúc bản vẽ database đầu tiên (nằm trong thư mục `Migrations`).
2. `Update-Database`: Thực thi bản vẽ, tự động gọi xuống SQL Server local để tạo Database và các bảng `Roles`, `Accounts`, kèm theo bảng lịch sử hệ thống `__EFMigrationsHistory`.