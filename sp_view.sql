USE [QLDSV_TC]
GO
/****** Object:  View [dbo].[V_DSKHOA]    Script Date: 10/4/2024 11:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_DSKHOA]
AS 
SELECT TOP (100) PERCENT PUBS.description AS TENKHOA, SUBS.subscriber_server AS TENSERVER
FROM     dbo.sysmergepublications AS PUBS INNER JOIN
                  dbo.sysmergesubscriptions AS SUBS ON PUBS.pubid = SUBS.pubid AND PUBS.publisher <> SUBS.subscriber_server WHERE SUBS.subscriber_server <> 'ADMIN\MSSQLSERVER03'
GO
/****** Object:  View [dbo].[V_DSPM]    Script Date: 10/4/2024 11:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_DSPM]
AS
SELECT        TOP (100) PERCENT PUBS.description AS TENKHOA, SUBS.subscriber_server AS TENSERVER
FROM            dbo.sysmergepublications AS PUBS INNER JOIN
                         dbo.sysmergesubscriptions AS SUBS ON PUBS.pubid = SUBS.pubid AND PUBS.publisher <> SUBS.subscriber_server

GO
/****** Object:  StoredProcedure [dbo].[SP_ADD_LTC]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_ADD_LTC]
@MALTC NVARCHAR(10),
@NIENKHOA NVARCHAR(9),
@HOCKY INT,
@MAMH NVARCHAR(10),
@NHOM INT,
@MAGV NVARCHAR(10),
@MAKHOA NVARCHAR(10),
@SOSVTOITHIEU INT,
@HUYLOP BIT,
@TYPE NVARCHAR(6)
AS
BEGIN
	IF (@TYPE='ADD')
		BEGIN
			INSERT INTO LOPTINCHI(MALTC,NIENKHOA,HOCKY,MAMH,NHOM,MAGV,MAKHOA,SOSVTOITHIEU,HUYLOP)
	VALUES(@MALTC,@NIENKHOA,@HOCKY,@MAMH,@NHOM,@MAGV,@MAKHOA,@SOSVTOITHIEU,@HUYLOP)
		END
	IF (@TYPE = 'UPDATE')
		BEGIN
			UPDATE DBO.LOPTINCHI SET NIENKHOA = @NIENKHOA,HOCKY = @HOCKY,MAMH = @MAMH,NHOM =@NHOM,MAGV = @MAGV,MAKHOA = @MAKHOA, SOSVTOITHIEU = @SOSVTOITHIEU,HUYLOP = @HUYLOP
			WHERE MALTC = @MALTC
		END
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BANGDIEM]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BANGDIEM] @NienKhoa varchar(9), @HocKy int, @Nhom int,@MonHoc nvarchar(50)
as
declare @LOPTINCHI int
select @LOPTINCHI= MALTC from LOPTINCHI,MONHOC
where LOPTINCHI.NIENKHOA = @NienKhoa AND LOPTINCHI.HOCKY = @HocKy AND LOPTINCHI.NHOM = @Nhom
AND MONHOC.TENMH = @MonHoc AND LOPTINCHI.MAMH = MONHOC.MAMH
select MALTC=@LOPTINCHI,SINHVIEN.MASV,HOTEN=HO+' '+TEN,DIEM_CC,DIEM_GK,DIEM_CK,DIEM_TK= DIEM_CC*0.1 + DIEM_GK*0.3 + DIEM_CK*0.6 from DANGKY,SINHVIEN
where  DANGKY.MASV = SINHVIEN.MASV AND DANGKY.MALTC = @LOPTINCHI AND HUYDANGKY = 0
order by TEN,HO
GO
/****** Object:  StoredProcedure [dbo].[SP_BDTK]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BDTK] @MALOP varchar(10)
as
declare @columns NVARCHAR(MAX)
SET FMTONLY OFF
select INFO = SINHVIEN.MASV + '-' + HO + ' ' +TEN,TENMH,DIEM INTO #GetAllMark
from SINHVIEN left join 
(select MASV,A.TENMH,DIEM=COALESCE(DIEM_CC*0.1+DIEM_GK*0.3+DIEM_CK*0.6,0) 
from DANGKY, (select MALTC,TENMH from LOPTINCHI,MONHOC where LOPTINCHI.MAMH = MONHOC.MAMH) as A
where DANGKY.MALTC = A.MALTC) as B 
on SINHVIEN.MASV = B.MASV
where MALOP =  @MALOP  AND DANGHIHOC = 0
Select TOP(1) @columns = TENMH FROM #GetAllMark where TENMH IS NOT NULL
Select INFO,TENMH=COALESCE(TENMH,@columns),DIEM from #GetAllMark
SET FMTONLY ON
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECKID]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CHECKID] @Code NVARCHAR(15),
@Type NVARCHAR(15)
AS
BEGIN
	-- LOP
	IF(@Type = 'MALOP')
	BEGIN
		
		IF EXISTS(SELECT * FROM dbo.LOP WHERE dbo.LOP.MALOP = @Code)
			RETURN 1; -- Mã tồn tại ở phân mãnh hiện tại
	
		ELSE IF EXISTS(SELECT * FROM LINK0.QLDSV_TC.dbo.LOP AS LOP WHERE LOP.MALOP = @Code)
			RETURN 2;	--Mã NV tồn tại ở phân mãnh  khác
	END

	--MON Hoc
	IF(@Type = 'MAMONHOC')
	BEGIN
		IF EXISTS(SELECT * FROM dbo.MONHOC WHERE MAMH = @Code)
		RETURN 1;
	END
	
	


		IF(@Type = 'MASV')
	BEGIN
		
		IF EXISTS(SELECT * FROM dbo.SINHVIEN WHERE dbo.SINHVIEN.MASV = @Code)
			RETURN 1; -- Mã tồn tại ở phân mãnh hiện tại
	
		ELSE IF EXISTS(SELECT * FROM LINK0.QLDSV_TC.dbo.SINHVIEN AS SV WHERE SV.MASV = @Code)
			RETURN 2;	--Mã NV tồn tại ở phân mãnh  khác
	END

	RETURN 0	--Không bị trùng được thêm


	IF(@Type = 'LOPTINCHI')
	BEGIN
		
		IF EXISTS(SELECT * FROM dbo.LOPTINCHI WHERE dbo.LOPTINCHI.MALTC = @Code)
			RETURN 1; -- Mã tồn tại ở phân mãnh hiện tại
	
		ELSE IF EXISTS(SELECT * FROM LINK0.QLDSV_TC.dbo.LOPTINCHI WHERE LOPTINCHI.MALTC = @Code)
			RETURN 2;	--Mã NV tồn tại ở phân mãnh  khác
	END

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECKNAME]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CHECKNAME]
@Name NVARCHAR(50),
@Type NVARCHAR(15)
AS
BEGIN
	--Kiểm tra Table lop của server hiện tại
	IF(@Type = 'TENLOP')
	BEGIN
		IF EXISTS(SELECT * FROM dbo.LOP WHERE dbo.LOP.TENLOP = @Name)
			RETURN 1; -- @name bị trùng  ở phân mãnh hiện tại
	
		ELSE IF EXISTS(SELECT * FROM LINK0.QLDSV_TC.dbo.LOP AS LOP WHERE LOP.TENLOP = @Name)
			RETURN 2;	--@name bị trùng ở phân mãnh  khác
	END

	IF(@Type = 'TENMONHOC')
	BEGIN
		IF EXISTS(SELECT * FROM dbo.MONHOC WHERE TENMH = @Name)
		RETURN 1;
	END
	
	RETURN 0	--Không bị trùng ,được thêm
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DANGNHAP]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DANGNHAP]
@TENLOGIN NVARCHAR (50)
AS
DECLARE @TENUSER NVARCHAR(50)
SELECT @TENUSER=NAME FROM sys.sysusers WHERE sid = SUSER_SID(@TENLOGIN)
 
 SELECT USERNAME = @TENUSER, 
  HOTEN = (SELECT HO+ ' '+ TEN FROM GIANGVIEN  WHERE MAGV = @TENUSER ),
   ROLENAME=NAME
   FROM sys.sysusers 
   WHERE UID = (SELECT GROUPUID 
                 FROM SYS.SYSMEMBERS 
                   WHERE MEMBERUID= (SELECT UID FROM sys.sysusers 
                                      WHERE NAME=@TENUSER))
GO
/****** Object:  StoredProcedure [dbo].[SP_DANHSACH_LTC]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DANHSACH_LTC]
@MAKHOA NVARCHAR(10)
AS
BEGIN
    SELECT
        LT.MALTC,
        LT.NIENKHOA,
        LT.HOCKY,
		LT.MAMH,
        MH.TENMH,
        LT.NHOM,
		LT.MAGV,
        HOTENGV = GV.HO + ' ' + GV.TEN,
        LT.MAKHOA,
        LT.SOSVTOITHIEU,
        LT.HUYLOP
    FROM
        LOPTINCHI LT
    INNER JOIN
        MONHOC MH ON LT.MAMH = MH.MAMH
    INNER JOIN
        GIANGVIEN GV ON LT.MAGV = GV.MAGV
    WHERE
        LT.MAKHOA = @MAKHOA
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DONGHOCPHI]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DONGHOCPHI] @MASV VARCHAR(20),
    @NienKhoa VARCHAR(20),
    @HocKy INT, 
    @SoTienDong INT
AS
BEGIN
    BEGIN DISTRIBUTED TRANSACTION;

    BEGIN TRY
        IF EXISTS (SELECT 1 FROM HOCPHI WHERE MASV = @MASV AND NIENKHOA = @NienKhoa AND HOCKY = @HocKy)
        BEGIN
            INSERT INTO CT_DONGHOCPHI (MASV, NIENKHOA, HOCKY, SOTIENDONG)
            VALUES (@MASV, @NienKhoa, @HocKy, @SoTienDong);
			COMMIT TRANSACTION;
			return 1;
        END
        ELSE
        BEGIN
            RAISERROR(N'Thông tin bạn nhập không tồn tại', 16, 1);
			ROLLBACK TRANSACTION;
			return 0;
        END     
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
      
        THROW;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_DS_DIEMSV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DS_DIEMSV] @MSSV varchar(10) , @Type int

as
IF (@Type = 0)
BEGIN
	IF EXISTS (SELECT 1 FROM SINHVIEN WHERE MASV=@MSSV)  
				BEGIN
                    with GETDIEMMON (MAMH,DIEMTK) as
					(select LOPTINCHI.MAMH,DIEMTK= DIEM_CC * 0.1 + DIEM_GK * 0.3 + DIEM_CK * 0.6 
					from DANGKY,LOPTINCHI 
					where DANGKY.MASV = @MSSV AND DANGKY.MALTC = LOPTINCHI.MALTC AND HUYDANGKY=0)
					select TENMH,DIEMTK= MAX(DIEMTK) 
					from MONHOC,GETDIEMMON
					where GETDIEMMON.MAMH = MONHOC.MAMH
					group by TENMH
                END
	ELSE IF  EXISTS (SELECT 1 FROM LINK0.QLDSV_TC.dbo.SINHVIEN WHERE MASV=@MSSV) 
				BEGIN
                    with GETDIEMMON (MAMH,DIEMTK) as
					(select LOPTINCHI.MAMH,DIEMTK= DIEM_CC * 0.1 + DIEM_GK * 0.3 + DIEM_CK * 0.6 
					from DANGKY,LOPTINCHI 
					where DANGKY.MASV = @MSSV AND DANGKY.MALTC = LOPTINCHI.MALTC AND HUYDANGKY=0)
					select TENMH,DIEMTK= MAX(DIEMTK) 
					from MONHOC,GETDIEMMON
					where GETDIEMMON.MAMH = MONHOC.MAMH
					group by TENMH
                END
ELSE
	RAISERROR(N'SINH VIÊN NÀY KHÔNG TỒN TẠI',16,1)
END
ELSE
BEGIN
	IF EXISTS (SELECT 1 FROM SINHVIEN WHERE MASV=@MSSV)  
				BEGIN
                    with GETDIEMMON (MAMH,DIEMTK) as
					(select LOPTINCHI.MAMH,DIEMTK= DIEM_CC * 0.1 + DIEM_GK * 0.3 + DIEM_CK * 0.6 
					from DANGKY,LOPTINCHI 
					where DANGKY.MASV = @MSSV AND DANGKY.MALTC = LOPTINCHI.MALTC AND HUYDANGKY=0)
					select TENMH,DIEMTK= MAX(DIEMTK) 
					from MONHOC,GETDIEMMON
					where GETDIEMMON.MAMH = MONHOC.MAMH
					group by TENMH
                END
	ELSE
		RAISERROR(N'SINH VIÊN NÀY KHÔNG TỒN TẠI',16,1)
END

GO
/****** Object:  StoredProcedure [dbo].[SP_DS_DONGHP_THEOLOP]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DS_DONGHP_THEOLOP] @MALOP varchar(10), @NienKhoa varchar(9), @HocKy int
as
with GETSVDONGTIEN(MASV,HOCPHI,SOTIENDONG)
as
(select HOCPHI.MASV,HOCPHI,SOTIENDADONG = SUM(SOTIENDONG) 
from LINK0.QLDSV_TC.DBO.HOCPHI left join LINK0.QLDSV_TC.DBO.CT_DONGHOCPHI
on HOCPHI.MASV = CT_DONGHOCPHI.MASV AND CT_DONGHOCPHI.HOCKY = HOCPHI.HOCKY AND CT_DONGHOCPHI.NIENKHOA = HOCPHI.NIENKHOA
where HOCPHI.NIENKHOA = @NienKhoa AND HOCPHI.HOCKY = @HocKy
group by HOCPHI.MASV,HOCPHI)
select HOTEN=SINHVIEN.HO + ' '+ SINHVIEN.TEN,HOCPHI,SOTIENDONG= COALESCE(SOTIENDONG,0)  
from SINHVIEN,GETSVDONGTIEN
where SINHVIEN.MASV = GETSVDONGTIEN.MASV
AND SINHVIEN.MALOP = @MALOP
GO
/****** Object:  StoredProcedure [dbo].[SP_DS_SVDANGKYLTC]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DS_SVDANGKYLTC] @NienKhoa VARCHAR(9),
@HocKy int , @Nhom int, @TenMh nvarchar(50)
as
declare @LOPTINCHI int
select @LOPTINCHI = MALTC  from LOPTINCHI,MONHOC
where LOPTINCHI.NIENKHOA=@NienKhoa AND LOPTINCHI.HOCKY = @HocKy
AND LOPTINCHI.NHOM = @Nhom AND MONHOC.TENMH = @TenMh AND LOPTINCHI.MAMH = MONHOC.MAMH
select SINHVIEN.MASV,HOTEN=HO+' '+TEN,PHAI = IIF(PHAI=1,N'Nữ',N'Nam'),MALOP from SINHVIEN,DANGKY
where DANGKY.MASV = SINHVIEN.MASV AND DANGKY.MALTC = @LOPTINCHI AND HUYDANGKY = 0
order by TEN,HO
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_MAKHOASV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GET_MAKHOASV]
@masv nvarchar(10)
as
SELECT MAKHOA FROM SINHVIEN INNER JOIN LOP ON (SINHVIEN.MALOP = LOP.MALOP) WHERE SINHVIEN.MASV = @masv
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCTHP_SV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetCTHP_SV]
@MASV VARCHAR(10), @NIENKHOA NCHAR(9), @HOCKY INT
AS SELECT NGAYDONG, SOTIENDONG FROM dbo.CT_DONGHOCPHI WHERE NIENKHOA = @NIENKHOA AND HOCKY = @HOCKY AND MASV = @MASV



GO
/****** Object:  StoredProcedure [dbo].[SP_GetDSHP_SV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetDSHP_SV]
@MASV VARCHAR(10)
AS
BEGIN
	with GETINFOHP(MASV,NIENKHOA,HOCKY,TONGSOTIENDADONG)
as (select HOCPHI.MASV,HOCPHI.NIENKHOA,HOCPHI.HOCKY,SUM(SOTIENDONG) from HOCPHI,CT_DONGHOCPHI 
where HOCPHI.MASV = CT_DONGHOCPHI.MASV AND HOCPHI.NIENKHOA = CT_DONGHOCPHI.NIENKHOA AND HOCPHI.HOCKY = CT_DONGHOCPHI.HOCKY
group by HOCPHI.MASV,HOCPHI.NIENKHOA,HOCPHI.HOCKY)
select HOCPHI.NIENKHOA,HOCPHI.HOCKY,HOCPHI,TONGSOTIENDADONG=COALESCE(TONGSOTIENDADONG,0), SOTIENCANDONG= COALESCE(HOCPHI-TONGSOTIENDADONG,HOCPHI) 
from HOCPHI left join GETINFOHP
on HOCPHI.MASV = GETINFOHP.MASV AND HOCPHI.NIENKHOA = GETINFOHP.NIENKHOA AND HOCPHI.HOCKY = GETINFOHP.HOCKY
where HOCPHI.MASV = @MASV
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetInfoSV_HP]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetInfoSV_HP]
@masv VARCHAR(10)
AS SELECT HOTEN=HO +' '+ TEN, MALOP FROM dbo.SINHVIEN WHERE MASV = @masv
GO
/****** Object:  StoredProcedure [dbo].[SP_getInfoSVDKI]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_getInfoSVDKI]
@masv NCHAR(10)
AS
BEGIN
	SELECT MASV,HO,TEN,MALOP FROM dbo.SINHVIEN WHERE MASV = @masv
END

GO
/****** Object:  StoredProcedure [dbo].[SP_InDanhSachLopTinChi]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InDanhSachLopTinChi] @NienKhoa varchar(9), @HocKy int
AS
with SOSVDK (MALTC,MAMH,MAGV,NHOM,SOSVTOITHIEU,AMOUNT) as
(select LOPTINCHI.MALTC,MAMH,MAGV,NHOM,SOSVTOITHIEU,count(DANGKY.MALTC) from LOPTINCHI 
left join DANGKY
on DANGKY.MALTC = LOPTINCHI.MALTC AND DANGKY.HUYDANGKY = 0
where HUYLOP=0 AND LOPTINCHI.NIENKHOA = @NienKhoa AND LOPTINCHI.HOCKY = @HocKy 
group by LOPTINCHI.MALTC,MAMH,MAGV,NHOM,SOSVTOITHIEU)
select A.MALTC,TENMH,NHOM,HOTEN,SOSVTOITHIEU,AMOUNT
from (select MALTC,TENMH from SOSVDK,MONHOC where SOSVDK.MAMH = MONHOC.MAMH) as A,
(select MALTC,NHOM,HOTEN=HO+' '+TEN,SOSVDK.SOSVTOITHIEU,AMOUNT from SOSVDK,GIANGVIEN where SOSVDK.MAGV=GIANGVIEN.MAGV) as B
where A.MALTC=B.MALTC



GO
/****** Object:  StoredProcedure [dbo].[SP_LAY_TENGV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_LAY_TENGV]
@MAGV NVARCHAR(10)
AS
BEGIN
	SELECT HOTEN=HO+''+TEN FROM GIANGVIEN WHERE GIANGVIEN.MAGV = @MAGV 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LayDSGV]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_LayDSGV] AS
BEGIN
	SELECT MAGV,HOTEN = HO+' '+TEN FROM dbo.GIANGVIEN 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LayDSMH]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_LayDSMH] AS
BEGIN
	SELECT MAMH,TENMH  FROM dbo.MONHOC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LayMaLTCLonNhat]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_LayMaLTCLonNhat] AS
SELECT MAX(MALTC) AS MaxMALTC
FROM link0.QLDSV_TC.dbo.LOPTINCHI
GO
/****** Object:  StoredProcedure [dbo].[SP_LIST_HOCKY]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_LIST_HOCKY] @NIENKHOA nchar(9)  as 
select HOCKY from LINK0.QLDSV_TC.DBO.LOPTINCHI where NIENKHOA= @NIENKHOA group by HOCKY


GO
/****** Object:  StoredProcedure [dbo].[SP_LIST_NHOM]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_LIST_NHOM] @NIENKHOA varchar(9), @HOCKI int
as select NHOM FROM LOPTINCHI where @NIENKHOA = NIENKHOA AND HOCKY = @HOCKI group by NHOM


GO
/****** Object:  StoredProcedure [dbo].[SP_LIST_NIENKHOA]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_LIST_NIENKHOA] as 
select NIENKHOA from LINK0.QLDSV_TC.DBO.LOPTINCHI group by NIENKHOA


GO
/****** Object:  StoredProcedure [dbo].[SP_LIST_SVHUYDANGKY]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_LIST_SVHUYDANGKY] @MASSV varchar(20)
as
with GETLIST_HUYDANGKY(MALTC,NIENKHOA,HOCKY,MAMH,MAGV,NHOM)
as
(select DANGKY.MALTC,NIENKHOA,HOCKY,MAMH,MAGV,NHOM 
from DANGKY,LOPTINCHI
where DANGKY.MASV=@MASSV AND DANGKY.MALTC = LOPTINCHI.MALTC AND DANGKY.HUYDANGKY = 0 
AND DANGKY.DIEM_CC IS NULL AND DANGKY.DIEM_CK IS NULL AND DANGKY.DIEM_GK IS NULL	
)
select A.MALTC,NIENKHOA,HOCKY,TENMH,HOTENGV=HO + ' '+ TEN,NHOM
from (select MALTC,NIENKHOA,HOCKY,HO,TEN,NHOM
from GETLIST_HUYDANGKY,GIANGVIEN
where GETLIST_HUYDANGKY.MAGV = GIANGVIEN.MAGV) as A,
(select MALTC,TENMH from GETLIST_HUYDANGKY,MONHOC 
where GETLIST_HUYDANGKY.MAMH = MONHOC.MAMH) as B
where A.MALTC = B.MALTC


GO
/****** Object:  StoredProcedure [dbo].[SP_SinhVienDangNhap]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SinhVienDangNhap]
@masv NCHAR(10),@password NVARCHAR(40)
AS
BEGIN
	SELECT MASV,HOTEN = HO+' '+TEN FROM dbo.SINHVIEN WHERE MASV = @masv AND PASSWORD = @password
END

GO
/****** Object:  StoredProcedure [dbo].[SP_SUM_HP_THEOLOP]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SUM_HP_THEOLOP] @MALOP varchar(10), @NienKhoa varchar(9), @HocKy int
AS

DECLARE @tonghocphi INT
;with GETSVDONGTIEN(MASV,HOCPHI,SOTIENDONG)
as
(select HOCPHI.MASV,HOCPHI,SOTIENDADONG = SUM(SOTIENDONG) 
from HOCPHI left join CT_DONGHOCPHI
on HOCPHI.MASV = CT_DONGHOCPHI.MASV AND CT_DONGHOCPHI.HOCKY = HOCPHI.HOCKY AND CT_DONGHOCPHI.NIENKHOA = HOCPHI.NIENKHOA
where HOCPHI.NIENKHOA = @NienKhoa AND HOCPHI.HOCKY = @HocKy
group by HOCPHI.MASV,HOCPHI)
select @tonghocphi =SUM(A.SOTIENDONG) from (select HOTEN=SINHVIEN.HO + ' '+ SINHVIEN.TEN,HOCPHI,SOTIENDONG= COALESCE(SOTIENDONG,0)  
from SINHVIEN,GETSVDONGTIEN
where SINHVIEN.MASV = GETSVDONGTIEN.MASV
AND SINHVIEN.MALOP = @MALOP) as A
PRINT(@tonghocphi)
IF(@tonghocphi IS NULL) 
SELECT TONGHOCPHI = 0
ELSE SELECT TONGHOCPHI = @tonghocphi
GO
/****** Object:  StoredProcedure [dbo].[SP_TAOLOGIN]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TAOLOGIN]
  @LGNAME VARCHAR(50),
  @PASS VARCHAR(50),
  @USERNAME VARCHAR(50),
  @ROLE VARCHAR(50)
AS

	-- check login , user bị trùng 
	IF EXISTS(SELECT name FROM sys.server_principals 
				WHERE TYPE IN ('U', 'S', 'G')	--U: Windows Login Accounts
				AND name NOT LIKE '%##%'		--S: SQL Login Accounts
				AND name = @LGNAME)				--G: Windows Group Login Accounts
		RETURN 1	--Trùng Login
	ELSE IF EXISTS(SELECT name FROM sys.database_principals
					WHERE type_desc = 'SQL_USER'
					AND name = @USERNAME)
		RETURN 2	--Trùng User

	-- băt đầu tạo tài khoản
  DECLARE @RET INT
  EXEC @RET= SP_ADDLOGIN @LGNAME, @PASS,'QLDSV_TC'
  IF (@RET =1) 
     RETURN 3 -- tạo tài khoản không thành công
 
  EXEC @RET= SP_GRANTDBACCESS @LGNAME, @USERNAME
  IF (@RET =1) 
  BEGIN
       EXEC SP_DROPLOGIN @LGNAME
       RETURN 3 -- tạo  tài khoảng không thành công
  END
  EXEC sp_addrolemember @ROLE, @USERNAME

  --THEM ROLE SECURITYADMIN DE CO QUYEN TAO TAI KHOAN
  EXEC sp_addsrvrolemember @LGNAME, 'SecurityAdmin'

  RETURN 0  -- THANH CONG

GO
/****** Object:  StoredProcedure [dbo].[SP_XULY_DIEM]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_XULY_DIEM]
@MSSV varchar(20),
@MALTC int,
@DIEMCC float,
@DIEMGK float,
@DIEMCK float
as 
BEGIN
	IF EXISTS (Select 1 From DANGKY where MASV = @MSSV AND MALTC = @MALTC)
	BEGIN
		UPDATE DANGKY
		SET DIEM_CC = @DIEMCC, DIEM_GK = @DIEMGK, DIEM_CK = @DIEMCK
		WHERE MASV = @MSSV AND MALTC = @MALTC
	END
	ELSE 
	RAISERROR(N'THÔNG TIN ĐĂNG KÝ KHÔNG TỒN TẠI',16,1)
	END

GO
/****** Object:  StoredProcedure [dbo].[SP_XULY_LTC]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_XULY_LTC] @MASV NVARCHAR(10),  
@MALTC INT,  
@type smallint
--type=1 : đăng ký
--type=0 : hủy
-- SP này để vừa cho sinh viên đăng ký hoặc hủy môn đăng ký
AS  
BEGIN  
	IF (@type=1)
	BEGIN
            IF EXISTS (SELECT 1 FROM DANGKY  WHERE MASV=@MASV AND MALTC=@MALTC)  
				BEGIN
					IF EXISTS (SELECT 1 FROM DANGKY WHERE MASV=@MASV AND MALTC=@MALTC AND HUYDANGKY = 0)
						BEGIN
							raiserror(N'SINH VIÊN ĐÃ ĐĂNG KÝ LỚP',16,1)
							RETURN
						END 
					ELSE 
						BEGIN
							 UPDATE DANGKY SET HUYDANGKY=0 WHERE MALTC = @MALTC AND MASV = @MASV
						END
                END  
            ELSE  
                BEGIN  
                    INSERT INTO DBO.DANGKY(MALTC, MASV,HUYDANGKY)  
                    VALUES (@MALTC, @MASV,0)  
                END
	END
	ELSE IF(@type = 2)
	BEGIN
		IF EXISTS (SELECT 1 FROM DANGKY WHERE MASV=@MASV AND MALTC=@MALTC)
				BEGIN
					IF EXISTS (SELECT 1 FROM DANGKY WHERE MASV=@MASV AND MALTC=@MALTC AND (DIEM_CC IS NOT NULL OR DIEM_CK IS NOT NULL OR DIEM_GK IS NOT NULL))
						BEGIN
							raiserror(N'LỚP TÍN CHỈ ĐÃ CÓ ĐIÊM KHÔNG THỂ HUỶ',16,1)
							RETURN
						END
					ELSE
						BEGIN
							UPDATE DANGKY SET HUYDANGKY=1 WHERE MALTC = @MALTC AND MASV = @MASV
						END
                    
                END
		ELSE
			BEGIN
				raiserror(N'SINH VIÊN CHƯA ĐĂNG KÝ LỚP',16,1)
				
			END
	END
		
END


GO
/****** Object:  StoredProcedure [dbo].[TAO_THONGTINHOCPHI]    Script Date: 10/4/2024 11:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TAO_THONGTINHOCPHI]
    @MASV NVARCHAR(50),
    @NienKhoa NVARCHAR(50),
    @HocKy NVARCHAR(50),
    @HocPhi INT,
	@TYPE NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
		BEGIN TRANSACTION; -- Bắt đầu giao dịch

		IF (@TYPE = 'ADD')
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM LINK0.QLDSV_TC.DBO.DANGKY INNER JOIN LINK0.QLDSV_TC.DBO.LOPTINCHI  ON DANGKY.MALTC = LOPTINCHI.MALTC WHERE DANGKY.MASV = @MASV
			AND NIENKHOA = @NienKhoa AND HOCKY = @HocKy)
			BEGIN
				RAISERROR(N'SINH VIÊN KHÔNG CÓ ĐĂNG KÝ HỌC KỲ NÀY', 16, 1);
				ROLLBACK; -- Quay lại trạng thái trước khi bắt đầu giao dịch
				RETURN; -- Kết thúc thủ tục lưu trữ
			END
			ELSE
			IF EXISTS (SELECT 1 FROM HOCPHI WHERE MASV = @MASV AND NIENKHOA = @NienKhoa AND HOCKY = @HocKy)
			BEGIN
				RAISERROR(N'ĐÃ TỒN TẠI THÔNG TIN HỌC PHÍ', 16, 1);
				ROLLBACK; -- Quay lại trạng thái trước khi bắt đầu giao dịch
				RETURN; -- Kết thúc thủ tục lưu trữ
			END
			ELSE
			BEGIN
				INSERT INTO HOCPHI (MASV, NIENKHOA, HOCKY, HOCPHI)
				VALUES (@MASV, @NienKhoa, @HocKy, @HocPhi);
			
			END
		END

        IF (@TYPE = 'UPDATE')
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM HOCPHI WHERE MASV = @MASV AND NIENKHOA = @NienKhoa AND HOCKY = @HocKy)
			BEGIN
				RAISERROR(N'KHÔNG TỒN TẠI THÔNG TIN HỌC PHÍ', 16, 1);
				ROLLBACK; -- Quay lại trạng thái trước khi bắt đầu giao dịch
				RETURN; -- Kết thúc thủ tục lưu trữ
			END
			ELSE
			BEGIN
				UPDATE HOCPHI
				SET HOCPHI = @HocPhi
				WHERE MASV = @MASV AND NienKhoa = @NienKhoa AND HOCKY = @HocKy;
				
			END
			
		END

        COMMIT;
		return 1;-- Kết thúc giao dịch
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK; -- Quay lại trạng thái trước khi bắt đầu giao dịch
        
		THROW;
    END CATCH;
END;
GO
