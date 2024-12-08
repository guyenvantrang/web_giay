GO
CREATE DATABASE SHOPGIAY_2
GO
USE SHOPGIAY_2
GO
-- Bảng NHACUNGCAP
CREATE TABLE NHACUNGCAP (
    MANCC INT IDENTITY(1,1) NOT NULL,
    TENNCC NVARCHAR(100),
    DIACHI NVARCHAR(255),
	CONSTRAINT PK_NCC PRIMARY KEY (MANCC)
);
CREATE TABLE PHIEUNHAP (
    MAPN INT IDENTITY(1,1) NOT NULL,
    MANCC INT,
    NGAYNHAP DATE,
    TONGTIEN INT,
    TRANGTHAI NVARCHAR(50),
	CONSTRAINT PK_PN PRIMARY KEY (MAPN),
    CONSTRAINT FK_PN_NCC FOREIGN KEY (MANCC) REFERENCES NHACUNGCAP(MANCC)
);
CREATE TABLE KICHCO(
	MAKC NCHAR(30) NOT NULL,
	KICHCO INT ,
	CONSTRAINT PK_KC PRIMARY KEY (MAKC)
)
CREATE TABLE MAUSAC (
	MAMS NCHAR(30) NOT NULL,
	TENMAUSAC NVARCHAR(50)
	CONSTRAINT PK_MS PRIMARY KEY (MAMS)
)
CREATE TABLE NHANHANG(
	MANH NCHAR(30) NOT NULL,
	TENNH NVARCHAR(200),
	CONSTRAINT PK_NH PRIMARY KEY (MANH),
)
CREATE TABLE LOAISANPHAM (
    MALOAI INT IDENTITY(1,1) NOT NULL,
    TENLOAI NVARCHAR(100),
    SOLUONGTON INT,
	CONSTRAINT PK_LSP PRIMARY KEY (MALOAI)
);
CREATE TABLE HINHANH (
	MAHA NCHAR(30) NOT NULL,
	TENHINHANH NVARCHAR(200),
	DUONGDAN NVARCHAR(500),
	CONSTRAINT PK_HA PRIMARY KEY (MAHA)
)
CREATE TABLE SANPHAM (
    MASP INT IDENTITY(1,1) NOT NULL,
	MALOAI INT NOT NULL,
    TENSP NVARCHAR(100),
	GIAGOCSP INT,
	SOLUONGTON INT,
	MANH NCHAR(30),
	HINHANHBIA NVARCHAR(1000),
	MAHA  NCHAR(30)  NOT NULL,
	MOTA NVARCHAR(4000),
	GIACHINHTHUCSP INT,
	CONSTRAINT PK_SP PRIMARY KEY (MASP),
	CONSTRAINT FK_SP_LSP FOREIGN KEY (MALOAI) REFERENCES LOAISANPHAM(MALOAI),
	CONSTRAINT FK_SP_HA FOREIGN KEY (MAHA) REFERENCES HINHANH(MAHA),
	CONSTRAINT FK_SP_NH FOREIGN KEY (MANH) REFERENCES NHANHANG(MANH)
)
CREATE TABLE CHITIETSANPHAM(
	MASP INT  NOT NULL,
	MAKC NCHAR(30) NOT NULL,
	MAMS NCHAR(30) NOT NULL,
	MOTA NVARCHAR(4000),
	SOLUONG INT,
	CONSTRAINT PK_CTSP PRIMARY KEY (MASP,MAKC,MAMS),
	CONSTRAINT FK_CTSP_SP FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP),
	CONSTRAINT FK_CTSP_KC FOREIGN KEY (MAKC) REFERENCES KICHCO(MAKC),
	CONSTRAINT FK_CTSP_MS FOREIGN KEY (MAMS) REFERENCES MAUSAC(MAMS)
)
-- Bảng CHITIETPHIEUNHAP
CREATE TABLE CHITIETPHIEUNHAP(
    MAPN INT,
    MASP INT,
	MAKC NCHAR(30) ,
	MAMS NCHAR(30) ,
    DONGIA INT,
    SOLUONG INT,
    CONSTRAINT PK_CTPN PRIMARY KEY (MAPN,MASP,MAKC,MAMS),
    CONSTRAINT FK_CTPN_MAPN FOREIGN KEY (MAPN) REFERENCES PHIEUNHAP(MAPN),
	CONSTRAINT FK_CTPN_SP FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP),
	CONSTRAINT FK_CTPN_MAKC FOREIGN KEY (MAKC) REFERENCES KICHCO(MAKC),
	CONSTRAINT FK_CTPN_MAMS FOREIGN KEY (MAMS) REFERENCES MAUSAC(MAMS),
);
-- Bảng KHACHHANG
CREATE TABLE KHACHHANG (
    MAKH INT IDENTITY(1,1) NOT NULL,
    TENKH NVARCHAR(100),
    NGAYSINH DATE,
    GIOITINH NVARCHAR(10),
    DIACHI NVARCHAR(500),
    SODIENTHOAI VARCHAR(15),
    SOTHICH NVARCHAR(500),
    TENDANGNHAP NCHAR(50) UNIQUE,
    MATKHAU NCHAR(50),
	CONSTRAINT PK_KH PRIMARY KEY(MAKH)
);

-- Bảng ADMIN
CREATE TABLE ADMIN (
    MAAD INT IDENTITY(1,1) NOT NULL,
    TENAD VARCHAR(100),
    TENDANGNHAP VARCHAR(50) UNIQUE,
    MATKHAU VARCHAR(50),
    EMAIL VARCHAR(100),
	CONSTRAINT PK_AD PRIMARY KEY (MAAD)
);

-- Bảng DONHANG
CREATE TABLE DONHANG (
    MADH INT IDENTITY(1,1) NOT NULL,
    MAKH INT,
    NGAYDAT DATE,
	NGAYGIAO DATE,
	TRANGTHAI NVARCHAR(50),
	GHITRU NVARCHAR(4000),
	TINHTHANH NVARCHAR(200),
	QUAN NVARCHAR(200),
	PHUONG NVARCHAR(200),
	DIACHICUTHE NVARCHAR(4000),
	GHITRUHUYHANG NVARCHAR(4000),
    TONGTIEN INT,
	CONSTRAINT PK_DH PRIMARY KEY (MADH),
    CONSTRAINT FK_DH FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
);

-- Bảng CHITIETDONHANG
CREATE TABLE CHITIETDONHANG (
    MADH INT NOT NULL,
    MASP INT NOT NULL,
	MAKC NCHAR(30) ,
	MAMS NCHAR(30) ,
    THANHTIEN INT,
    SOLUONG INT,
    CONSTRAINT PK_CTDH PRIMARY KEY (MADH, MASP,MAKC,MAMS),
    CONSTRAINT FK_CTDH_MADH FOREIGN KEY (MADH) REFERENCES DONHANG(MADH),
    CONSTRAINT FK_CTDH_MASP FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP),
	CONSTRAINT FK_CTDH_MAKC FOREIGN KEY (MAKC) REFERENCES KICHCO(MAKC),
	CONSTRAINT FK_CTDH_MAMS FOREIGN KEY (MAMS) REFERENCES MAUSAC(MAMS),
);

CREATE TABLE KHUYENMAI(
MAKM NCHAR(30) NOT NULL,
TENKHUYENMAI NVARCHAR(300),
NGAYBATDAU DATE ,
NGAYKETTHUC DATE,
CONSTRAINT PK_KM PRIMARY KEY (MAKM),
)
CREATE TABLE CHITIETKHUYENMAI(
	MAKM NCHAR(30) NOT NULL,
	MASP INT NOT NULL,
	TILEGIAM FLOAT,
	CONSTRAINT PK_CTKM PRIMARY KEY (MAKM, MASP),
	CONSTRAINT FK_CTKM_KM FOREIGN KEY (MAKM) REFERENCES KHUYENMAI(MAKM),
    CONSTRAINT FK_CTKM_SP FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)

CREATE TABLE GIAODIENTRANGCHINH(
MA	NCHAR(10) NOT NULL,
TEN NVARCHAR(100),
MOTA NVARCHAR(500),
TIEUDE NVARCHAR(255),
NOIDUNG NVARCHAR(1000),
HINHANH NVARCHAR(200),
DUONGDAN NVARCHAR(200),
CONSTRAINT PK_GDTC PRIMARY KEY (MA),
)
-- Thêm dữ liệu vào bảng NHACUNGCAP
INSERT INTO NHACUNGCAP (TENNCC, DIACHI) VALUES 
(N'Nhà Cung Cấp 1', N'Địa chỉ 1'),
(N'Nhà Cung Cấp 2', N'Địa chỉ 2'),
(N'Nhà Cung Cấp 3', N'Địa chỉ 3');

-- Thêm dữ liệu vào bảng KICHCO
INSERT INTO KICHCO (MAKC,KICHCO) VALUES 
('KC_38',38), ('KC_39',39), ('KC_40',40), ('KC_41',41), ('KC_42',42);

-- Thêm dữ liệu vào bảng MAUSAC
INSERT INTO MAUSAC (MAMS,TENMAUSAC) VALUES 
('MS_DO',N'Đỏ'), ('MS_XANH',N'Xanh'), ('MS_VANG',N'Vàng'), ('MS_DEN',N'Đen'), ('MS_TRANG',N'Trắng');

-- Thêm dữ liệu vào bảng NHANHANG
INSERT INTO NHANHANG (MANH,TENNH) VALUES 
('NH_1',N'Nhãn Hàng 1'), ('NH_2',N'Nhãn Hàng 2'), ('NH_3',N'Nhãn Hàng 3');

-- Thêm dữ liệu vào bảng LOAISANPHAM
INSERT INTO LOAISANPHAM (TENLOAI, SOLUONGTON) VALUES 
(N'Loại 1', 100), (N'Loại 2', 200), (N'Loại 3', 150);

-- Thêm dữ liệu vào bảng HINHANH
INSERT INTO HINHANH (MAHA, TENHINHANH, DUONGDAN) VALUES 
-- Hình 1
('hinh_1', 'Hình 1', 'hinh_1_1.jpg,hinh_1_2.jpg,hinh_1_3.jpg,hinh_1_4.jpg,hinh_1_5.jpg,hinh_1.jpg'),
-- Hình 2
('hinh_2', 'Hình 2', 'hinh_2_1.jpg,hinh_2_2.jpg,hinh_2_3.jpg,hinh_2.jpg'),
-- Hình 3
('hinh_3', 'Hình 3', 'hinh_3_1.jpg,hinh_3_2.jpg,hinh_3_3.jpg,hinh_3.jpg'),
-- Hình 4
('hinh_4', 'Hình 4', 'hinh_4_1.jpg,hinh_4_2.jpg,hinh_4.jpg'),
-- Hình 5
('hinh_5', 'Hình 5', 'hinh_5_1.jpg,hinh_5_2.jpg,hinh_5_3.jpg,hinh_5_4.jpg,hinh_5.jpg'),
-- Hình 6
('hinh_6', 'Hình 6', 'hinh_6_1.jpg,hinh_6_2.jpg,hinh_6_3.jpg,hinh_6_4.jpg,hinh_6.jpg'),
-- Hình 7
('hinh_7', 'Hình 7', 'hinh_7_1.jpg,hinh_7_2.jpg,hinh_7_3.jpg,hinh_7_4.jpg,hinh_7.jpg'),
-- Hình 8
('hinh_8', 'Hình 8', 'hinh_8_1.jpg,hinh_8_2.jpg,hinh_8.jpg'),
-- Hình 9
('hinh_9', 'Hình 9', 'hinh_9_1.jpg,hinh_9_2.jpg,hinh_9.jpg'),
-- Hình 10
('hinh_10', 'Hình 10', 'hinh_10_1.jpg,hinh_10_2.jpg,hinh_10_3.jpg,hinh_10_4.jpg,hinh_10.jpg'),
-- Hình 11
('hinh_11', 'Hình 11', 'hinh_11_1.jpg,hinh_11_2.jpg,hinh_11.jpg'),
-- Hình 12
('hinh_12', 'Hình 12', 'hinh_12_1.jpg,hinh_12_2.jpg,hinh_12_3.jpg,hinh_12.jpg'),
-- Hình 13
('hinh_13', 'Hình 13', 'hinh_13_1.png,hinh_13_2.png,hinh_13.png'),
-- Hình 14
('hinh_14', 'Hình 14', 'hinh_14_1.png,hinh_14_2.png,hinh_14_3.png,hinh_14.png'),
-- Hình 15
('hinh_15', 'Hình 15', 'hinh_15_1.png,hinh_15_2.png,hinh_15_3.png,hinh_15_4.png,hinh_15_5.png,hinh_15_6.png,hinh_15.png'),
-- Hình 16
('hinh_16', 'Hình 16', 'hinh_16_1.png,hinh_16_2.png,hinh_16_3.png,hinh_16.png'),
-- Hình 17
('hinh_17', 'Hình 17', 'hinh_17_1.png,hinh_17_2.png,hinh_17_3.png,hinh_17_4.png,hinh_17.png'),
-- Hình 18
('hinh_18', 'Hình 18', 'hinh_18_1.png,hinh_18_2.png,hinh_18_3.png,hinh_18_4.png,hinh_18_5.png,hinh_18_6.png,hinh_18_7.png,hinh_18.png'),
-- Hình 19
('hinh_19', 'Hình 19', 'hinh_19_1.png,hinh_19_2.png,hinh_19_3.png,hinh_19.png'),
-- Hình 20
('hinh_20', 'Hình 20', 'hinh_20_1.png,hinh_20_2.png,hinh_20_3.png,hinh_20.png'),
-- Hình 21
('hinh_21', 'Hình 21', 'hinh_21_1.png,hinh_21_2.png,hinh_21_3.png,hinh_21.png'),
-- Hình 22
('hinh_22', 'Hình 22', 'hinh_22_1.png,hinh_22_2.png,hinh_22_3.png,hinh_22.png'),
-- Hình 23
('hinh_23', 'Hình 23', 'hinh_23_1.png,hinh_23_2.png,hinh_23_3.png,hinh_23.png'),
-- Hình 24
('hinh_24', 'Hình 24', 'hinh_24_1.png,hinh_24_2.png,hinh_24_3.png,hinh_24.png'),
-- Hình 25
('hinh_25', 'Hình 25', 'hinh_25_1.png,hinh_25_2.png,hinh_25.png'),
-- Hình 26
('hinh_26', 'Hình 26', 'hinh_26_1.png,hinh_26_2.png,hinh_26_3.png,hinh_26_4.png,hinh_26.png'),
-- Hình 27
('hinh_27', 'Hình 27', 'hinh_27_1.png,hinh_27_2.png,hinh_27.png');

INSERT INTO SANPHAM (TENSP, MALOAI, GIAGOCSP, SOLUONGTON, HINHANHBIA, GIACHINHTHUCSP, MAHA, MANH, MOTA) VALUES
(N'Sản phẩm 1', 1, 100000, 10, N'hinh_1.jpg', 120000, N'hinh_1', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 2', 2, 150000, 15, N'hinh_2.jpg', 170000, N'hinh_2', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 3', 3, 200000, 20, N'hinh_3.jpg', 220000, N'hinh_3', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 4', 1, 250000, 25, N'hinh_4.jpg', 270000, N'hinh_4', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 5', 2, 300000, 30, N'hinh_5.jpg', 320000, N'hinh_5', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 6', 3, 350000, 35, N'hinh_6.jpg', 370000, N'hinh_6', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 7', 1, 400000, 40, N'hinh_7.jpg', 420000, N'hinh_7', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 8', 2, 450000, 45, N'hinh_8.jpg', 470000, N'hinh_8', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 9', 3, 500000, 50, N'hinh_9.jpg', 520000, N'hinh_9', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 10', 1, 550000, 55, N'hinh_10.jpg', 570000, N'hinh_10', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 11', 2, 600000, 60, N'hinh_11.jpg', 620000, N'hinh_11', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 12', 3, 650000, 65, N'hinh_12.jpg', 670000, N'hinh_12', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 13', 1, 700000, 70, N'hinh_13.png', 720000, N'hinh_13', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 14', 2, 750000, 75, N'hinh_14.png', 770000, N'hinh_14', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 15', 3, 800000, 80, N'hinh_15.png', 820000, N'hinh_15', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 16', 1, 850000, 85, N'hinh_16.png', 870000, N'hinh_16', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 17', 2, 900000, 90, N'hinh_17.png', 920000, N'hinh_17', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 18', 3, 950000, 95, N'hinh_18.png', 970000, N'hinh_18', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 19', 1, 1000000, 100, N'hinh_19.png', 1020000, N'hinh_19', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 20', 2, 1050000, 105, N'hinh_20.png', 1070000, N'hinh_20', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 21', 3, 1100000, 110, N'hinh_21.png', 1120000, N'hinh_21', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 22', 1, 1150000, 115, N'hinh_22.png', 1170000, N'hinh_22', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 23', 2, 1200000, 120, N'hinh_23.png', 1220000, N'hinh_23', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 24', 3, 1250000, 125, N'hinh_24.png', 1270000, N'hinh_24', 'NH_3', N'Mô tả 1'),
(N'Sản phẩm 25', 1, 1300000, 130, N'hinh_25.png', 1320000, N'hinh_25', 'NH_1', N'Mô tả 1'),
(N'Sản phẩm 26', 2, 1350000, 135, N'hinh_26.png', 1370000, N'hinh_26', 'NH_2', N'Mô tả 1'),
(N'Sản phẩm 27', 3, 1400000, 140, N'hinh_27.png', 1420000, N'hinh_27', 'NH_3', N'Mô tả 1');

-- Thêm dữ liệu vào bảng CHITIETSANPHAM
-- Thêm dữ liệu vào bảng CHITIETSANPHAM
INSERT INTO CHITIETSANPHAM (MASP, MAKC, MAMS, SOLUONG, MOTA) VALUES 
(1,  'KC_38', 'MS_DO', 10, N'Mô tả sản phẩm 1'),
(2,  'KC_39', 'MS_XANH', 15, N'Mô tả sản phẩm 2'),
(3,  'KC_40', 'MS_VANG', 20, N'Mô tả sản phẩm 3'),
(4, 'KC_41', 'MS_DEN', 25, N'Mô tả sản phẩm 4'),
(5, 'KC_42', 'MS_TRANG', 30, N'Mô tả sản phẩm 5'),
(6, 'KC_38', 'MS_DO', 35, N'Mô tả sản phẩm 6'),
(7,  'KC_39', 'MS_XANH', 40, N'Mô tả sản phẩm 7'),
(8,  'KC_40', 'MS_VANG', 45, N'Mô tả sản phẩm 8'),
(9,  'KC_41', 'MS_DEN', 50, N'Mô tả sản phẩm 9'),
(10,  'KC_42', 'MS_TRANG', 55, N'Mô tả sản phẩm 10'),
(11,  'KC_38', 'MS_DO', 60, N'Mô tả sản phẩm 11'),
(12,  'KC_39', 'MS_XANH', 65, N'Mô tả sản phẩm 12'),
(13,  'KC_40', 'MS_VANG', 70, N'Mô tả sản phẩm 13'),
(14, 'KC_41', 'MS_DEN', 75, N'Mô tả sản phẩm 14'),
(15,  'KC_42', 'MS_TRANG', 80, N'Mô tả sản phẩm 15'),
(16,  'KC_38', 'MS_DO', 85, N'Mô tả sản phẩm 16'),
(17,  'KC_39', 'MS_XANH', 90, N'Mô tả sản phẩm 17'),
(18,  'KC_40', 'MS_VANG', 95, N'Mô tả sản phẩm 18'),
(19,  'KC_41', 'MS_DEN', 100, N'Mô tả sản phẩm 19'),
(20,  'KC_42', 'MS_TRANG', 105, N'Mô tả sản phẩm 20'),
(21,  'KC_38', 'MS_DO', 110, N'Mô tả sản phẩm 21'),
(22,  'KC_39', 'MS_XANH', 115, N'Mô tả sản phẩm 22'),
(23,  'KC_40', 'MS_VANG', 120, N'Mô tả sản phẩm 23'),
(24,  'KC_41', 'MS_DEN', 125, N'Mô tả sản phẩm 24'),
(25,  'KC_42', 'MS_TRANG', 130, N'Mô tả sản phẩm 25'),
(26,  'KC_38', 'MS_DO', 135, N'Mô tả sản phẩm 26'),
(27,  'KC_39', 'MS_XANH', 140, N'Mô tả sản phẩm 27');

-- Thêm dữ liệu vào bảng KHACHHANG
INSERT INTO KHACHHANG (TENKH, NGAYSINH, GIOITINH, DIACHI, SODIENTHOAI, SOTHICH, TENDANGNHAP, MATKHAU) VALUES 
(N'Khách hàng 1', '1990-01-01', N'Nam', N'Địa chỉ KH 1', '0912345678', N'Sở thích KH 1', 'nv_a', '123'),
(N'Khách hàng 2', '1991-02-01', N'Nữ', N'Địa chỉ KH 2', '0912345679', N'Sở thích KH 2', 'kh2', 'matkhau2'),
(N'Khách hàng 3', '1992-03-01', N'Nam', N'Địa chỉ KH 3', '0912345680', N'Sở thích KH 3', 'kh3', 'matkhau3'),
(N'Khách hàng 4', '1993-04-01', N'Nữ', N'Địa chỉ KH 4', '0912345681', N'Sở thích KH 4', 'kh4', 'matkhau4'),
(N'Khách hàng 5', '1994-05-01', N'Nam', N'Địa chỉ KH 5', '0912345682', N'Sở thích KH 5', 'kh5', 'matkhau5');

---- Thêm dữ liệu vào bảng DONHANG
--INSERT INTO DONHANG (MAKH, NGAYDAT, NGAYGIAO, TONGTIEN, TRANGTHAI, GHITRU) VALUES 
--(1, '2024-01-01', '2024-01-05', 240000, N'Chưa giao', N'Ghi chú đơn hàng 1'),
--(2, '2024-02-01', '2024-02-06', 510000, N'Chưa giao', N'Ghi chú đơn hàng 2'),
--(3, '2024-03-01', '2024-03-07', 220000, N'Chưa giao', N'Ghi chú đơn hàng 3'),
--(4, '2024-04-01', '2024-04-08', 1350000, N'Chưa giao', N'Ghi chú đơn hàng 4'),
--(5, '2024-05-01', '2024-05-09', 1280000, N'Chưa giao', N'Ghi chú đơn hàng 5');


---- Thêm dữ liệu vào bảng CTHD (Chi Tiết Hóa Đơn)
--INSERT INTO CHITIETDONHANG(MADH, MASP, SOLUONG, THANHTIEN,MAKC,MAMS) VALUES 
--(1, 1, 2, 240000),
--(2, 2, 3, 510000),
--(3, 3, 1, 220000),
--(4, 4, 5, 1350000),
--(5, 5, 4, 1280000);

-----=========================================================================================================

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD01_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null, N'hinh9.jpg',N'GD01_1'),
('GD01_2',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null, N'hinh10.png',N'GD01_2'),
('GD01_3',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null, N'hinh11.jpg',N'GD01_3');
INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD02_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Sneacker', null,null,null),
('GD02_2',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Sản phẩm đặc trưng', null,null,null);

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD04_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Về chúng tôi', null,null,null)
INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD05_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Về chúng tôi',N'Lịch sử hình thành nên thương hiệu đi từ 
chất liệu thượng hạng tạo nên tên tuổi .Sứ mệnh mang đến giá trị bền vững tới khách hàng .',N'hinh_gioithieu_4.jpg',N'GD05_1'),
('GD05_2',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Về chúng tôi',N'Lịch sử hình thành nên thương hiệu đi từ
chất liệu thượng hạng tạo nên tên tuổi .Sứ mệnh mang đến giá trị bền vững tới khách hàng .',N'hinh_gioithieu_5.png',N'GD05_2'),
('GD05_3',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Về chúng tôi',N'Lịch sử hình thành nên thương hiệu đi từ 
chất liệu thượng hạng tạo nên tên tuổi .Sứ mệnh mang đến giá trị bền vững tới khách hàng .',N'hinh_gioithieu_6.png',N'GD05_3')

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD06_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Features', N'Chúng tôi luôn hướng đến tương lai đổi mới kiểu dáng chất
lượng sản phẩm nhằm mục đích mang đến khách hàng trải nghiệm tốt đẹp nhất',N'videoquangcao.mp4',N'GD06_1')

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD07_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null,N'hang_adidas.png',N'GD07_1'),
('GD07_2',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null,N'nike.png',N'GD07_2'),
('GD07_3',N'Trang chính giao diện người dùng', N'phần thứ nhất',null, null,N'vans.png',N'GD07_3');

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD08_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Thương hiệu nỗi bật', null,null,null)

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD09_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Chăm sóc khách hàng', null,null,null)

INSERT INTO GIAODIENTRANGCHINH (MA,TEN, MOTA, TIEUDE, NOIDUNG, HINHANH,DUONGDAN)
VALUES 
('GD10_1',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Chăm sóc khách hàng', null,N'hinh3.jpg','GD10_1'),
('GD10_2',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Chăm sóc khách hàng', null,N'hinh3.jpg','GD10_2'),
('GD10_3',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Chăm sóc khách hàng', null,N'hinh3.jpg','GD10_3'),
('GD10_4',N'Trang chính giao diện người dùng', N'phần thứ nhất',N'Chăm sóc khách hàng', null,N'hinh3.jpg','GD10_4');


--==================================================================================================================
GO
CREATE TRIGGER chaykhuyenmai
ON CHITIETKHUYENMAI
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Kiểm tra nếu có hành động INSERT (thêm chi tiết khuyến mãi)
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        -- Kiểm tra xem khuyến mãi có hết hạn không
        DECLARE @NgayKetThuc DATE;
        SELECT @NgayKetThuc = KHUYENMAI.NGAYKETTHUC
        FROM KHUYENMAI
        INNER JOIN inserted ON KHUYENMAI.MAKM = inserted.MAKM
        WHERE KHUYENMAI.NGAYKETTHUC >= GETDATE();

        -- Nếu khuyến mãi chưa hết hạn, cập nhật giá sản phẩm
        IF @NgayKetThuc >= GETDATE()
        BEGIN
            UPDATE SANPHAM
            SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP * (1 - inserted.TILEGIAM / 100)
            FROM SANPHAM
            INNER JOIN inserted ON SANPHAM.MASP = inserted.MASP;
        END
        ELSE
        BEGIN
            -- Nếu khuyến mãi đã hết hạn, không thực hiện cập nhật giá
            PRINT 'Khuyến mãi đã hết hạn, không áp dụng giá mới cho sản phẩm.';
        END
    END

    -- Kiểm tra nếu có hành động DELETE (xóa chi tiết khuyến mãi)
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        DECLARE @NgayKetThuc__ DATE;
        -- Lấy ngày kết thúc của khuyến mãi liên quan đến sản phẩm
        SELECT @NgayKetThuc__ = KHUYENMAI.NGAYKETTHUC
        FROM KHUYENMAI
        INNER JOIN deleted ON KHUYENMAI.MAKM = deleted.MAKM;

        -- Kiểm tra nếu ngày kết thúc khuyến mãi đã hết hạn
        IF @NgayKetThuc__ >= GETDATE()
        BEGIN
            -- Nếu khuyến mãi chưa hết hạn, cập nhật lại giá sản phẩm về giá gốc
            UPDATE SANPHAM
            SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP
            FROM SANPHAM
            INNER JOIN deleted ON SANPHAM.MASP = deleted.MASP;
        END
        ELSE
        BEGIN
            -- Nếu khuyến mãi đã hết hạn, không cập nhật lại giá sản phẩm
            PRINT 'Khuyến mãi đã hết hạn, không cần thay đổi giá sản phẩm.';
        END
    END

    -- Kiểm tra nếu có hành động UPDATE (cập nhật chi tiết khuyến mãi)
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
    BEGIN
        DECLARE @NgayKetThuc_ DATE;
        SELECT @NgayKetThuc_ = KHUYENMAI.NGAYKETTHUC
        FROM KHUYENMAI
        INNER JOIN inserted ON KHUYENMAI.MAKM = inserted.MAKM
        WHERE KHUYENMAI.NGAYKETTHUC >= GETDATE();

        -- Nếu khuyến mãi chưa hết hạn, cập nhật lại giá sản phẩm
        IF @NgayKetThuc_ >= GETDATE()
        BEGIN
            UPDATE SANPHAM
            SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP * (1 - inserted.TILEGIAM / 100)
            FROM SANPHAM
            INNER JOIN inserted ON SANPHAM.MASP = inserted.MASP;
        END
        ELSE
        BEGIN
            -- Nếu khuyến mãi đã hết hạn, không thực hiện cập nhật giá
            PRINT 'Khuyến mãi đã hết hạn, không áp dụng giá mới cho sản phẩm.';
        END
    END
END
GO
CREATE TRIGGER ketthuckhuyenmai
ON KHUYENMAI
AFTER UPDATE, INSERT
AS
BEGIN
    -- Kiểm tra khuyến mãi đã hết hạn và cập nhật lại giá sản phẩm
    UPDATE SANPHAM
    SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP
    FROM SANPHAM
    INNER JOIN CHITIETKHUYENMAI ON SANPHAM.MASP = CHITIETKHUYENMAI.MASP
    INNER JOIN KHUYENMAI ON CHITIETKHUYENMAI.MAKM = KHUYENMAI.MAKM
    WHERE KHUYENMAI.NGAYKETTHUC < GETDATE();

    -- Áp dụng lại khuyến mãi cho những sản phẩm thuộc khuyến mãi còn hiệu lực
    UPDATE SANPHAM
    SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP * (1 - CHITIETKHUYENMAI.TILEGIAM / 100)
    FROM SANPHAM
    INNER JOIN CHITIETKHUYENMAI ON SANPHAM.MASP = CHITIETKHUYENMAI.MASP
    INNER JOIN KHUYENMAI ON CHITIETKHUYENMAI.MAKM = KHUYENMAI.MAKM
    WHERE KHUYENMAI.NGAYKETTHUC >= GETDATE();
END
GO
CREATE TRIGGER capnhatkhisualaingaykm
ON KHUYENMAI
AFTER UPDATE
AS
BEGIN
    -- Kiểm tra nếu ngày kết thúc khuyến mãi đã thay đổi
    IF UPDATE(NGAYKETTHUC)
    BEGIN
        -- Tắt khuyến mãi cho những sản phẩm thuộc khuyến mãi đã hết hạn
        UPDATE SANPHAM
        SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP
        FROM SANPHAM
        INNER JOIN CHITIETKHUYENMAI ON SANPHAM.MASP = CHITIETKHUYENMAI.MASP
        INNER JOIN KHUYENMAI ON CHITIETKHUYENMAI.MAKM = KHUYENMAI.MAKM
        WHERE KHUYENMAI.NGAYKETTHUC < GETDATE();

        -- Áp dụng lại khuyến mãi cho những sản phẩm thuộc khuyến mãi còn hiệu lực
        -- Kiểm tra lại điều kiện ngày kết thúc mới (sau khi cập nhật)
        UPDATE SANPHAM
        SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP * (1 - CHITIETKHUYENMAI.TILEGIAM / 100)
        FROM SANPHAM
        INNER JOIN CHITIETKHUYENMAI ON SANPHAM.MASP = CHITIETKHUYENMAI.MASP
        INNER JOIN KHUYENMAI ON CHITIETKHUYENMAI.MAKM = KHUYENMAI.MAKM
        WHERE KHUYENMAI.NGAYKETTHUC >= GETDATE();

        -- Xử lý trường hợp khi ngày kết thúc khuyến mãi được kéo dài
        -- Không áp dụng lại khuyến mãi cho các sản phẩm đã có khuyến mãi khác còn hiệu lực
        UPDATE SANPHAM
        SET GIACHINHTHUCSP = SANPHAM.GIAGOCSP * (1 - CHITIETKHUYENMAI.TILEGIAM / 100)
        FROM SANPHAM
        INNER JOIN CHITIETKHUYENMAI ON SANPHAM.MASP = CHITIETKHUYENMAI.MASP
        INNER JOIN KHUYENMAI ON CHITIETKHUYENMAI.MAKM = KHUYENMAI.MAKM
        WHERE KHUYENMAI.NGAYKETTHUC >= GETDATE()
        AND NOT EXISTS (
            SELECT 1
            FROM CHITIETKHUYENMAI AS c2
            INNER JOIN KHUYENMAI AS k2 ON c2.MAKM = k2.MAKM
            WHERE c2.MASP = SANPHAM.MASP
            AND k2.NGAYKETTHUC >= GETDATE()
            AND c2.MAKM != KHUYENMAI.MAKM
        );
    END
END
GO


--select MASP,HINHANH.MAHA,TENHINHANH,DUONGDAN from SANPHAM,HINHANH where SANPHAM.MAHA=HINHANH.MAHA
--select MASP,MAUSAC.MAMS,TENMAUSAC from MAUSAC,SANPHAM where MAUSAC.MAMS=SANPHAM.MAMS and MASP=1

--SELECT SANPHAM.* FROM SANPHAM,CHITIETSANPHAM WHERE SANPHAM.MASP=CHITIETSANPHAM.MASP AND MASP=1

--select SANPHAM.MASP, KICHCO.MAKC,MAUSAC.MAMS
--from SANPHAM,KICHCO,MAUSAC,CHITIETSANPHAM 
--where SANPHAM.MASP=CHITIETSANPHAM.MASP 
--	and CHITIETSANPHAM.MAKC=KICHCO.MAKC 
--	and CHITIETSANPHAM.MAMS=MAUSAC.MAMS
--	and SANPHAM.MASP=1
--select * from DONHANG
--select *from CHITIETDONHANG

--select *from CHITIETDONHANG where CHITIETDONHANG.MADH = 2 

--declare capnhatgia cursor 
--for select MASP , GIAGOCSP , GIACHINHTHUCSP from SANPHAM

--open capnhatgia
--declare @ma int, @giagoc int , @giachinhthuc int
--fetch from capnhatgia into @ma, @giagoc , @giachinhthuc
--while @@FETCH_STATUS=0
--begin
--update SANPHAM
--set GIACHINHTHUCSP=@giagoc
--where MASP=@ma
--fetch from capnhatgia into @ma, @giagoc , @giachinhthuc
--end
--close capnhatgia
--deallocate capnhatgia
