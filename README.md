# **Project 1— Batch Rename**
* 1712774 Nguyễn Chí Thành
* 1712800 Mai Huy Thông
> Chương trình được thiết kế bằng **ngôn ngữ lập trình C#** dựa trên nền tảng **WPF** cho phép người dùng xử lý tên của các file và folder
<br> **Reference ** [Click here](https://github.com/hyperion0201/batch-rename) 
<br> **Reference ** [Click here](https://github.com/ErodNelps/BatchRename-WPF)

## **Những chức năng đã làm được**

### **I. Nhóm chức năng chính** 
Các chức năng theo yêu cầu của project:
----
1. Chọn một thư mục để thêm vào danh sách các tập tin của thư mục được chọn. (0.5 điểm)
2. Chọn một thư mục để thêm vào danh sách các thư mục con của thư mục được chọn. (0.5 điểm)
3. Hiển thị danh sách các hành động có thể có để người dùng lựa chọn thêm vào danh sách. (1 điểm)
    * Các màn hình dialog
    * Danh sách các hành động thao tác với tên tập tin 
4. Khi bấm **Start Batch** thì sẽ lần lượt áp các hành động vào từng tên tập tin và thực hiện đổi tên. (1 điểm)
    * Trùng sẽ tự động đổi tên cho khỏi trùng (thêm hậu tố là số)
    * Nếu trùng thì sẽ tự động bỏ qua giữ lại tên cũ
5. Tập các hành động đã được tạo ra có thể được lưu lại dưới dạng preset và được nạp lên để người dùng lựa chọn. (2 điểm)
    * Đổi phần mở rộng
    * Thay thế chuỗi 
    * Tìm và xóa chuỗi trong tên tập tin
6. Giao diện gọn gàng, dễ sử dụng, tươm tất, có bắt lỗi nhập dữ liệu đầy đủ (1 điểm)
7. Cài đặt các hành động thao tác với tên tập tin được trình bày ở **phần II** (4 điểm)
8. Những đặc điểm dặc biệt ở **phần III**
### **II. Danh sách các hành động thao tác với tên tập tin**
1. 	**Thay thế - Replace**
    * Dành cho nhu cầu tải hàng loạt file, ví dụ khi tải ở youtube về luôn bắt đầu bằng `youtube.com`, bạn sẽ muốn xóa bỏ đi các từ này.
2.  **Thay đổi kiểu chữ - New case**
    * Tên tập tin/thư mục toàn bộ bằng chữ hoa (Upper Case)
    * Tên tập tin/thư mục toàn bộ chữ thường (Lower Case)
    * Chỉ có ký tự đầu tiên mỗi từ viết hoa, còn lại viết thường (Title case)
3.  **Chuẩn hóa họ tên – Fullname normalize** 
    * Bắt đầu và kết thúc không có khoảng trắng
    * Mỗi từ bắt đầu bằng kí tự viết hoa, các kí tự còn lại viết thường
    * Không có quá 1 khoảng trắng giữa các từ
4.  **Thay đổi vị trí (Số ISBN- Tên file)– Move**
    * Ví dụ `99921-58-10-7 A new way to tackle stress, John Smith.pdf` thành `A new way to tackle stress, John Smith 99921-58-10-7.pdf`
    * Số ISBN luôn có độ dài cố định là 13, có thể cắt nó đưa ra sau hoặc trước bằng việc cài đặt chức năng.
5.  **Đặt tên duy nhất – Unique name**
    * Biến tên của mỗi tập tin tải về thành GUID để nó có tên là duy nhất.

### **III Các đặc điểm và chức năng khác**
* Có thể **Start Batch** nhiều lần, nếu bị trùng tên sau khi Start Batch nhiều lần, phần mềm sẽ hiện thông báo cho người dùng các file đã tồn tại
* Ở phần Đổi phần mở rộng, nếu người dùng nhập các ký tự đặc biệt ở phần mở rộng thì sẽ tự động loại bỏ các ký tự đó 
<br>    Ví dụ `co...m@@` sẽ được chuẩn hóa lại thành `com` S

* Có thêm chức năng **Clear**, **Refresh**, Các nút để thay đổi vị trí, thứ tự các hành động và tập tin
