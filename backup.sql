
ALTER DATABASE [TestDuHoc] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestDuHoc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestDuHoc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestDuHoc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestDuHoc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestDuHoc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestDuHoc] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestDuHoc] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestDuHoc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestDuHoc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestDuHoc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestDuHoc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestDuHoc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestDuHoc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestDuHoc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestDuHoc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestDuHoc] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TestDuHoc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestDuHoc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestDuHoc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestDuHoc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestDuHoc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestDuHoc] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TestDuHoc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestDuHoc] SET RECOVERY FULL 
GO
ALTER DATABASE [TestDuHoc] SET  MULTI_USER 
GO
ALTER DATABASE [TestDuHoc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestDuHoc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestDuHoc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestDuHoc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestDuHoc] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestDuHoc] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TestDuHoc] SET QUERY_STORE = OFF
GO
USE [TestDuHoc]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonDangKy]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonDangKy](
	[MaDangKy] [int] IDENTITY(1,1) NOT NULL,
	[MaSinhVien] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NOT NULL,
	[TrangThai] [int] NOT NULL,
	[GhiChuAdmin] [nvarchar](max) NOT NULL,
	[NguoiDuyet] [nvarchar](500) NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	[NgayCapNhat] [datetime] NOT NULL,
	[MaKhoaDuHoc] [int] NOT NULL,
 CONSTRAINT [PK_DonDangKy] PRIMARY KEY CLUSTERED 
(
	[MaDangKy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DuHoc]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DuHoc](
	[MaKhoaDuHoc] [int] IDENTITY(1,1) NOT NULL,
	[TenTruong] [nvarchar](max) NOT NULL,
	[QuocGia] [nvarchar](max) NOT NULL,
	[Website] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[SoDienThoai] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[HocPhi] [int] NOT NULL,
	[ThoiGian] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_DuHoc] PRIMARY KEY CLUSTERED 
(
	[MaKhoaDuHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaDinhSinhVien]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaDinhSinhVien](
	[IdSinhVien] [int] NOT NULL,
	[HoTenBo] [nvarchar](max) NOT NULL,
	[NgheNghiepBo] [nvarchar](max) NOT NULL,
	[SoDienThoaiBo] [nvarchar](max) NOT NULL,
	[HoTenMe] [nvarchar](max) NOT NULL,
	[NgheNghiepMe] [nvarchar](max) NOT NULL,
	[SoDienThoaiMe] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_GiaDinhSinhVien] PRIMARY KEY CLUSTERED 
(
	[IdSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGiangVien] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[SoDienThoai] [nvarchar](max) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[ChucVu] [nvarchar](max) NOT NULL,
	[HocVi] [nvarchar](max) NOT NULL,
	[SoCMND] [nvarchar](max) NOT NULL,
	[AnhDaiDien] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](max) NOT NULL,
	[NgaySinh] [datetime2](7) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_GiangVien] PRIMARY KEY CLUSTERED 
(
	[MaGiangVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiTaiLieu]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiTaiLieu](
	[LoaiTaiLieu] [nvarchar](50) NOT NULL,
	[TenTaiLieu] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LoaiTaiLieu] PRIMARY KEY CLUSTERED 
(
	[LoaiTaiLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [int] NOT NULL,
	[HoTen] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[SoDienThoai] [nvarchar](max) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](max) NOT NULL,
	[NgaySinh] [datetime2](7) NOT NULL,
	[CMND] [nvarchar](max) NOT NULL,
	[HinhAnh] [nvarchar](max) NOT NULL,
	[MaNganh] [nvarchar](max) NOT NULL,
	[MaLop] [int] NOT NULL,
	[MaHeDaoTao] [nvarchar](max) NOT NULL,
	[MaChuongTrinhHoc] [nvarchar](max) NOT NULL,
	[NienKhoa] [int] NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiLieu]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiLieu](
	[MaTaiLieu] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[LoaiTaiLieu] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](max) NOT NULL,
	[MaSinhVien] [int] NOT NULL,
	[isBanGoc] [bit] NOT NULL,
	[TenTaiLieu] [nvarchar](100) NULL,
 CONSTRAINT [PK_TaiLieu] PRIMARY KEY CLUSTERED 
(
	[MaTaiLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TinNhan]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinNhan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[NgayGui] [datetime2](7) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[DaDoc] [bit] NOT NULL,
 CONSTRAINT [PK_TinNhan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YeuCauDuHoc]    Script Date: 16/05/2024 5:20:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YeuCauDuHoc](
	[MaKhoaDuHoc] [int] NOT NULL,
	[LoaiTaiLieu] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](max) NOT NULL,
	[TenTaiLieu] [nvarchar](100) NOT NULL,
	[MaYeuCau] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_YeuCauDuHoc] PRIMARY KEY CLUSTERED 
(
	[MaYeuCau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Username], [Password], [isAdmin], [Email]) VALUES (N'admin', N'1', 1, N'admin@gmail.com')
INSERT [dbo].[Account] ([Username], [Password], [isAdmin], [Email]) VALUES (N'guest', N'1', 0, N'guest@gmail.com')
INSERT [dbo].[Account] ([Username], [Password], [isAdmin], [Email]) VALUES (N'vip', N'123', 0, N'1@gmail.com')
INSERT [dbo].[Account] ([Username], [Password], [isAdmin], [Email]) VALUES (N'zzz', N'123', 0, N'nguyenvana@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[DonDangKy] ON 

INSERT [dbo].[DonDangKy] ([MaDangKy], [MaSinhVien], [GhiChu], [TrangThai], [GhiChuAdmin], [NguoiDuyet], [NgayTao], [NgayCapNhat], [MaKhoaDuHoc]) VALUES (1, 1471020211, N'cái gì v b?', 0, N'Thiếu bài TOCFLA2', N'admin', CAST(N'2024-05-15T00:00:00.000' AS DateTime), CAST(N'2024-05-16T02:02:56.587' AS DateTime), 1)
INSERT [dbo].[DonDangKy] ([MaDangKy], [MaSinhVien], [GhiChu], [TrangThai], [GhiChuAdmin], [NguoiDuyet], [NgayTao], [NgayCapNhat], [MaKhoaDuHoc]) VALUES (2, 1471020211, N'OKE', 1, N'OKE !', N'admin', CAST(N'2024-05-15T19:57:50.073' AS DateTime), CAST(N'2024-05-16T01:54:50.117' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[DonDangKy] OFF
GO
SET IDENTITY_INSERT [dbo].[DuHoc] ON 

INSERT [dbo].[DuHoc] ([MaKhoaDuHoc], [TenTruong], [QuocGia], [Website], [Email], [SoDienThoai], [Image], [Ten], [HocPhi], [ThoiGian], [MoTa]) VALUES (1, N'Đại học Cao Quốc', N'Đài Loan', N'caoquoc.com', N'1@gmail.com', N'0123456', N'1.jpg', N'Du học rẻ', 15000000, N'3 năm', N'Xin chào các bạn')
INSERT [dbo].[DuHoc] ([MaKhoaDuHoc], [TenTruong], [QuocGia], [Website], [Email], [SoDienThoai], [Image], [Ten], [HocPhi], [ThoiGian], [MoTa]) VALUES (2, N'Đại học Kim Tân', N'Trung Quốc', N'kimtan.com', N'2@gmail.com', N'012345', N'2.jpg', N'Du học rẻ 2', 12000000, N'1 năm', N'Xin chào các bạn')
INSERT [dbo].[DuHoc] ([MaKhoaDuHoc], [TenTruong], [QuocGia], [Website], [Email], [SoDienThoai], [Image], [Ten], [HocPhi], [ThoiGian], [MoTa]) VALUES (3, N'Đại học VIP', N'Trung Quốc', N'tq.com', N'tq@m.com', N'0123456', N'b7727355-4b76-4f70-a0f6-1a676f5a5abd.jpg', N'Khóa học mới', 1, N'3 tháng', N'VIPAVISDO')
SET IDENTITY_INSERT [dbo].[DuHoc] OFF
GO
INSERT [dbo].[GiaDinhSinhVien] ([IdSinhVien], [HoTenBo], [NgheNghiepBo], [SoDienThoaiBo], [HoTenMe], [NgheNghiepMe], [SoDienThoaiMe]) VALUES (1471020211, N'123', N'456', N'789', N'123a', N'456a', N'789a')
GO
SET IDENTITY_INSERT [dbo].[GiangVien] ON 

INSERT [dbo].[GiangVien] ([MaGiangVien], [HoTen], [Email], [SoDienThoai], [DiaChi], [ChucVu], [HocVi], [SoCMND], [AnhDaiDien], [GioiTinh], [NgaySinh], [Username]) VALUES (1, N'Vũ Đình Hưng', N'hung@gmail.com123232323', N'01234561', N'HD - HN1', N'Trưởng Khoa', N'Thạc sĩ1', N'0123456', N'391cb1cb-edaa-44bf-b85b-4a2a710a5487_438817831_122115644258264105_1782296767862656483_n.jpg', N'Nam', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'admin')
SET IDENTITY_INSERT [dbo].[GiangVien] OFF
GO
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'anh-nen-trang', N'Ảnh nền trắng')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'bang-cap-3', N'Bằng cấp 3')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'bang-diem', N'Bảng điểm')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'CCCD-ban-than', N'CCCD bản thân')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'CCCD-bo', N'CCCD bố')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'CCCD-me', N'CCCD mẹ')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'form-khai', N'Form khai')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'giay-khai-sinh', N'GIấy khai sinh')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'hoc-ba-cap-3', N'Học bạ cấp 3')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'ho-chieu', N'Hộ chiếu')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'kham-suc-khoe', N'Khám sức khỏe')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'tocfl-A2', N'Năng lực tiếng Hoa TOCFL-A2')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'xac-nhan-cu-tru', N'Xác nhận cư trú(thay sổ hộ khẩu, xin ở CA xã, phường)')
INSERT [dbo].[LoaiTaiLieu] ([LoaiTaiLieu], [TenTaiLieu]) VALUES (N'xac-nhan-sinh-vien', N'Xác nhận sinh viên')
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoTen], [Email], [SoDienThoai], [DiaChi], [GioiTinh], [NgaySinh], [CMND], [HinhAnh], [MaNganh], [MaLop], [MaHeDaoTao], [MaChuongTrinhHoc], [NienKhoa], [Username]) VALUES (1, N'Học sinh 1', N'1@gmail.com', N'123456', N'HD - HN', N'Nam', CAST(N'2002-11-11T00:00:00.0000000' AS DateTime2), N'123', N'1.jpg', N'CNTT', 1405, N'CQ', N'CLC', 14, N'vip')
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoTen], [Email], [SoDienThoai], [DiaChi], [GioiTinh], [NgaySinh], [CMND], [HinhAnh], [MaNganh], [MaLop], [MaHeDaoTao], [MaChuongTrinhHoc], [NienKhoa], [Username]) VALUES (1471020211, N'Vũ Đình Hưng 123', N'hung.ddd@gm.com', N'123456', N'Hà Đông - Hà Nội - VN', N'Nam', CAST(N'2002-02-19T00:00:00.0000000' AS DateTime2), N'123456789', N'1471020211_420887454_1332707120780560_8679648984674541025_n.jpg', N'CNTT', 1405, N'CQ', N'CQ', 14, N'guest')
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoTen], [Email], [SoDienThoai], [DiaChi], [GioiTinh], [NgaySinh], [CMND], [HinhAnh], [MaNganh], [MaLop], [MaHeDaoTao], [MaChuongTrinhHoc], [NienKhoa], [Username]) VALUES (1471020288, N'Nguyễn Trịnh Văn A', N'nguyenvana@gmail.com', N'123456', N'HD - HN', N'Nam', CAST(N'2003-01-01T00:00:00.0000000' AS DateTime2), N'123456', N'1471020288.jpg', N'CNTT', 1404, N'CQ', N'CLC', 14, N'zzz')
GO
SET IDENTITY_INSERT [dbo].[TaiLieu] ON 

INSERT [dbo].[TaiLieu] ([MaTaiLieu], [FileName], [LoaiTaiLieu], [GhiChu], [MaSinhVien], [isBanGoc], [TenTaiLieu]) VALUES (1, N'header.jpg', N'bang-cap-3', N'Đã xin ở trường', 1471020211, 1, N'Bằng cấp 3')
INSERT [dbo].[TaiLieu] ([MaTaiLieu], [FileName], [LoaiTaiLieu], [GhiChu], [MaSinhVien], [isBanGoc], [TenTaiLieu]) VALUES (2, N'60c826e8-0f01-416e-a33d-e93288433c41_249249294_2942949249.jpg', N'anh-nen-trang', N'Mới chụp 2 hôm', 1471020211, 0, N'Ảnh nền trắng')
INSERT [dbo].[TaiLieu] ([MaTaiLieu], [FileName], [LoaiTaiLieu], [GhiChu], [MaSinhVien], [isBanGoc], [TenTaiLieu]) VALUES (4, N'bc000bc0-4d06-420b-b9a1-23a571ee6207_436252453_936289071612880_8812252319968718738_n.jpg', N'CCCD-bo', N'tty', 1471020211, 0, N'CCCD bố')
INSERT [dbo].[TaiLieu] ([MaTaiLieu], [FileName], [LoaiTaiLieu], [GhiChu], [MaSinhVien], [isBanGoc], [TenTaiLieu]) VALUES (5, N'9b5776f0-eccf-443f-b1b6-9aa4160d53c2_438817831_122115644258264105_1782296767862656483_n.jpg', N'bang-diem', N'rererer', 1471020211, 1, N'Bảng điểm')
SET IDENTITY_INSERT [dbo].[TaiLieu] OFF
GO
SET IDENTITY_INSERT [dbo].[TinNhan] ON 

INSERT [dbo].[TinNhan] ([Id], [NoiDung], [FullName], [Email], [NgayGui], [Subject], [DaDoc]) VALUES (2, N'1234', N'Nguyen Van A', N'a@gmail.com', CAST(N'2024-05-05T10:30:11.4296025' AS DateTime2), N'Xin chao', 1)
INSERT [dbo].[TinNhan] ([Id], [NoiDung], [FullName], [Email], [NgayGui], [Subject], [DaDoc]) VALUES (3, N'T', N'Nguyen Van B', N'b@gmail.com', CAST(N'2024-05-06T14:55:23.9771502' AS DateTime2), N'X', 1)
SET IDENTITY_INSERT [dbo].[TinNhan] OFF
GO
SET IDENTITY_INSERT [dbo].[YeuCauDuHoc] ON 

INSERT [dbo].[YeuCauDuHoc] ([MaKhoaDuHoc], [LoaiTaiLieu], [MoTa], [TenTaiLieu], [MaYeuCau]) VALUES (1, N'bang-cap-3', N'Bằng cấp 3 trên 9', N'Bằng cấp 3', 7)
INSERT [dbo].[YeuCauDuHoc] ([MaKhoaDuHoc], [LoaiTaiLieu], [MoTa], [TenTaiLieu], [MaYeuCau]) VALUES (1, N'bang-diem', N'Bảng điểm vip', N'Bảng điểm', 8)
INSERT [dbo].[YeuCauDuHoc] ([MaKhoaDuHoc], [LoaiTaiLieu], [MoTa], [TenTaiLieu], [MaYeuCau]) VALUES (1, N'tocfl-A2', N'TOCFL A2 >1000', N'Năng lực tiếng Hoa TOCFL-A2', 9)
INSERT [dbo].[YeuCauDuHoc] ([MaKhoaDuHoc], [LoaiTaiLieu], [MoTa], [TenTaiLieu], [MaYeuCau]) VALUES (1, N'ho-chieu', N'Rõ nét', N'Hộ chiếu', 10)
SET IDENTITY_INSERT [dbo].[YeuCauDuHoc] OFF
GO
ALTER TABLE [dbo].[DonDangKy] ADD  CONSTRAINT [DF_DonDangKy_NgayTao]  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[DonDangKy] ADD  CONSTRAINT [DF_DonDangKy_NgayCapNhat]  DEFAULT (getdate()) FOR [NgayCapNhat]
GO
ALTER TABLE [dbo].[TaiLieu] ADD  CONSTRAINT [DF_TaiLieu_isBanGoc]  DEFAULT ((0)) FOR [isBanGoc]
GO
USE [master]
GO
ALTER DATABASE [TestDuHoc] SET  READ_WRITE 
GO
