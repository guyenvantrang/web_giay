using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_SNEAKER.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using PagedList;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace SHOP_SNEAKER.Controllers
{
    public class KhachhangController : Controller
    {
        dulieuDataContext dl = new dulieuDataContext();
        // Giao diện của GD01
        public ActionResult Index_KH()
        {
            return View();
        }
        public ActionResult GD01_1()
        {
            return View();
        }
        public ActionResult GD01_2()
        {
            return View();
        }
        public ActionResult GD01_3()
        {
            return View();
        }

        public ActionResult GD01_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            List<GIAODIENTRANGCHINH> k = p.Where(t => t.MA != null && t.MA.Split('_')[0] == "GD01").ToList();
            return PartialView(k);
        }

        // Giao diện của GD02
        public ActionResult GD02_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            List<GIAODIENTRANGCHINH> k = p.Where(t => t.MA != null && t.MA.Split('_')[0] == "GD02").ToList();
            return PartialView(k);
        }
        // Giao diện của GD03
        public ActionResult GD03_KH(int page = 1, int pageSize = 8)
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            foreach (SANPHAM a in p)
            {
                if (a.MOTA.Length > 50)
                {
                    a.MOTA = a.MOTA.Substring(0, 50) + "...";
                }
                else
                {
                    a.MOTA = a.MOTA;
                }
            }
            Session["dulieuhinhanh"] = dl.HINHANHs.ToList();
            IPagedList<SANPHAM> k = p.OrderBy(sp => sp.TENSP).ToPagedList(page, pageSize);
            return PartialView(k);
        }
        // Giao diện của GD04
        public ActionResult GD04_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            GIAODIENTRANGCHINH k = p.FirstOrDefault(t => t.MA.Split('_')[0].TrimEnd() == "GD04");
            return PartialView(k);
        }
        // Giao diện của GD05
        public ActionResult GD05_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            List<GIAODIENTRANGCHINH> k = p.Where(t => t.MA.Split('_')[0].TrimEnd() == "GD05").ToList();
            return PartialView(k);
        }
        public ActionResult GD05_1()
        {
            return View();
        }
        public ActionResult GD05_2()
        {
            return View();
        }
        public ActionResult GD05_3()
        {
            return View();
        }
        // Giao diện của GD06
        public ActionResult GD06_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            GIAODIENTRANGCHINH k = p.FirstOrDefault(t => t.MA.Split('_')[0].TrimEnd() == "GD06");
            return PartialView(k);
        }
        public ActionResult GD06_1()
        {
            return View();
        }
        // Giao diện của GD07
        public ActionResult GD07_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            List<GIAODIENTRANGCHINH> k = p.Where(t => t.MA.Split('_')[0].TrimEnd() == "GD07").ToList();
            return PartialView(k);
        }
        // Giao diện của GD08
        public ActionResult GD08_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            GIAODIENTRANGCHINH k = p.FirstOrDefault(t => t.MA.Split('_')[0].TrimEnd() == "GD08");
            return PartialView(k);
        }
        // Giao diện của GD09
        public ActionResult GD09_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            GIAODIENTRANGCHINH k = p.FirstOrDefault(t => t.MA.Split('_')[0].TrimEnd() == "GD09");
            return PartialView(k);
        }
        // Giao diện của GD10
        public ActionResult GD10_KH()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            List<GIAODIENTRANGCHINH> k = p.Where(t => t.MA.Split('_')[0].TrimEnd() == "GD10").ToList();
            return PartialView(k);
        }
        // Giao diện của Phân loại menu
        public ActionResult PHANLOAISP_MENU_KH()
        {
            List<LOAISANPHAM> p = dl.LOAISANPHAMs.ToList();
            return PartialView(p);
        }
        // Giao diện lấy tất cả sản phẩm menu
        public ActionResult LAY_SANPHAM_KH()
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            Session["dulieuhinhanh"] = dl.HINHANHs.ToList();
            return View(p);
        }
        
        public ActionResult LAY_SANPHAM_KH_TIMKIEM_TEN(FormCollection e)
        {
            string a = e["timkiemtensp"].ToUpper(); 
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            if (a.Length == 0)
            {
                p = dl.SANPHAMs.ToList();
            }
            else
            {
                p = p.Where(t => t.TENSP.ToUpper().Contains(a)).ToList();
            }          
            Session["dulieuhinhanh"] = dl.HINHANHs.ToList();
            return PartialView("LAY_SANPHAM_KH",p);
        }
        public ActionResult SAPXEP_SP( FormCollection e)
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            string a = e["sapxep_sp"];
            if (a == "1")
            {
                p = p.OrderBy(t => t.GIACHINHTHUCSP).ToList();
                return PartialView("LAY_SANPHAM_KH", p);
            }
            else if (a == "2")
            {
                p = p.OrderByDescending(t => t.GIACHINHTHUCSP).ToList();
                return PartialView("LAY_SANPHAM_KH", p);
            }
            else if (a == "3")
            {
                List<CHITIETDONHANG> orderDetails = dl.CHITIETDONHANGs.ToList();
                List<SANPHAM> products = dl.SANPHAMs.ToList();
                Dictionary<int, int> productOrderCount = new Dictionary<int, int>();
                foreach (SANPHAM product in products)
                {
                    int orderCount = orderDetails.Count(t => t.MASP == product.MASP);
                    if (orderCount > 0)
                    {
                        productOrderCount[product.MASP] = orderCount;
                    }
                }
                var sortedProductOrderCount = productOrderCount.OrderByDescending(t => t.Value).ToDictionary(t => t.Key, t => t.Value);
                var sortedProductIds = sortedProductOrderCount.Keys.ToList();
                 p = products.Where(t => sortedProductIds.Contains(t.MASP)).ToList();
                return PartialView("LAY_SANPHAM_KH", p);
            }
            else
            {
                return PartialView("LAY_SANPHAM_KH", p);
            }
        }
        public ActionResult LOCGIAY(FormCollection e)
        {
            string nam = e["giaynam"];
            string nu = e["giaynu"];
            string giaythethao = e["giaythethao"];
            string giaythoitrang = e["giaythoitrang"];

            List<SANPHAM> p = dl.SANPHAMs.ToList();

            if (string.IsNullOrEmpty(nam) && string.IsNullOrEmpty(nu) && string.IsNullOrEmpty(giaythethao) && string.IsNullOrEmpty(giaythoitrang))
            {
                return PartialView("LAY_SANPHAM_KH", p); // Hiển thị tất cả sản phẩm nếu không áp dụng bộ lọc
            }

            var filteredProducts = new List<SANPHAM>();

            if (!string.IsNullOrEmpty(nam))
            {
                var namProducts = p.Where(t => t.TENSP.ToUpper().Contains(nam.ToUpper())).ToList();
                filteredProducts.AddRange(namProducts);
            }
            if (!string.IsNullOrEmpty(nu))
            {
                var nuProducts = p.Where(t => t.TENSP.ToUpper().Contains(nu.ToUpper())).ToList();
                filteredProducts.AddRange(nuProducts);
            }
            if (!string.IsNullOrEmpty(giaythethao))
            {
                var giaythethaoProducts = p.Where(t => t.TENSP.ToUpper().Contains(giaythethao.ToUpper())).ToList();
                filteredProducts.AddRange(giaythethaoProducts);
            }
            if (!string.IsNullOrEmpty(giaythoitrang))
            {
                var giaythoitrangProducts = p.Where(t => t.TENSP.ToUpper().Contains(giaythoitrang.ToUpper())).ToList();
                filteredProducts.AddRange(giaythoitrangProducts);
            }

            // Loại bỏ các sản phẩm trùng lặp
            filteredProducts = filteredProducts.Distinct().ToList();

            return PartialView("LAY_SANPHAM_KH", filteredProducts);
        }




        // Giao diện lấy loại sản phẩm
        public ActionResult LAY_LOAIISANPHAM_KH()
        {
            List<LOAISANPHAM> p = dl.LOAISANPHAMs.ToList();
            return PartialView(p);
        }
        // Giao diện tìm sản phẩm theo loại
        public ActionResult LAY_LOAIISANPHAM_KH_(int? id)
        {
            List<SANPHAM> p_SP = dl.SANPHAMs.ToList();
            List<SANPHAM> k = p_SP.Where(t => t.MALOAI == id).ToList();            
            return PartialView("LAY_SANPHAM_KH",k);
        }

        // Giao diện đăng nhập khách hàng
        public ActionResult DANGNHAP_KH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DANGNHAP_KH(FormCollection e)
        {
            string tendn = e["tendangnhap"].Trim();
            string matkhau = e["mkdangnhap"].Trim();
            if (tendn.Trim().Length == 0 || matkhau.Trim().Length == 0)
            {
                ModelState.AddModelError("tendangnhap", "*");
                return View();
            }
            if (matkhau.Trim().Length == 0)
            {
                ModelState.AddModelError("mkdangnhap", "*");
                return View();
            }
            KHACHHANG p = dl.KHACHHANGs.FirstOrDefault(t => t.TENDANGNHAP.Trim() == tendn && t.MATKHAU.Trim() == matkhau);
            if (p == null)
            {
                ModelState.AddModelError("tendangnhap", "*Tài khoản không đúng");
                return View();
            }
            else
            {
                Session["khachhangdangnhap"] = p;
                List<ADMIN> k = dl.ADMINs.ToList();
                Session["admin"] = k;
                var kh = Session["khachhangdangnhap"] as KHACHHANG;
                System.Diagnostics.Debug.WriteLine(kh.TENKH); // In tên khách hàng ra console
                return RedirectToAction("Index_KH");
            }
        }
        public ActionResult DANGKI_KH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DANGKI_KH(FormCollection e)
        {
            string tendn = e["tendangki"].Trim();
            string matkhau = e["mkdangki"].Trim();
            string laplaimatkhau = e["laplaimkdangki"].Trim();
            string ten = e["ten"].Trim();
            ViewBag.matkhaulaplai = null;
            KHACHHANG p = dl.KHACHHANGs.FirstOrDefault(t => t.TENDANGNHAP == tendn);
            if (tendn.Length == 0 || matkhau.Length == 0 || laplaimatkhau.Length == 0 || ten.Length == 0)
            {
                ModelState.AddModelError("","Không để trống");
                return View();
            }
            if (p != null)
            {
                ModelState.AddModelError("tendangki", "Tên đăng nhập đã tồn tại");
                return View();
            }
            if (matkhau != laplaimatkhau)
            {
                ModelState.AddModelError("mkdangki", "Mật khẩu không khớp");
                return View();
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(matkhau, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                // Nếu mật khẩu chứa ký tự tiếng Việt, hiển thị thông báo lỗi
                ModelState.AddModelError("mkdangki", "Mật khẩu không được chứa ký tự tiếng Việt.");
                return View();
            }
                KHACHHANG k = new KHACHHANG();
                k.TENKH = ten;
                k.TENDANGNHAP = tendn;
                k.MATKHAU = matkhau;
                Session["dangkibuoc1"] = k;
                //dl.KHACHHANGs.InsertOnSubmit(k);
                //dl.SubmitChanges();
                return View("DANGKI_KH_HOANTHANH");
        }
        public ActionResult DANGKI_KH_HOANTHANH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DANGKI_KH_HOANTHANH(FormCollection e)
        {
            KHACHHANG s = Session["dangkibuoc1"] as KHACHHANG; 
            KHACHHANG p = new KHACHHANG();         
            p.GIOITINH = e["gioitinh_kh"];
            p.DIACHI = e["diachi_kh"];
            p.SODIENTHOAI = e["sodienthoai_kh"];
            p.SOTHICH = e["sothich_kh"];
            if (s == null)
            {
                return RedirectToAction("DANGKI_KH");
            }
            p.TENDANGNHAP = s.TENDANGNHAP;
            p.MATKHAU = s.MATKHAU;
            p.TENKH = s.TENKH;
            KHACHHANG k = dl.KHACHHANGs.FirstOrDefault(t => t.SODIENTHOAI == p.SODIENTHOAI);
            if (k != null)
            {
                ModelState.AddModelError("sodienthoai_kh", "Số điện thoại đã tồn tại. Vui lòng chọn số điện thoại khác.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(p.GIOITINH))
            {
                ModelState.AddModelError("gioitinh_kh", "Giới tính không được bỏ trống.");
                return View();
            }
            DateTime ngaysinh;
            if (!DateTime.TryParse(e["ngaysinh_kh"], out ngaysinh))
            {
                ModelState.AddModelError("ngaysinh_kh", "Ngày sinh không hợp lệ.");
            }
            else
            {
                p.NGAYSINH =DateTime.Parse(ngaysinh.ToString("yyyy-MM-dd"));
            }

            if (string.IsNullOrWhiteSpace(p.DIACHI))
            {
                ModelState.AddModelError("diachi_kh", "Địa chỉ không được bỏ trống.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(p.SODIENTHOAI))
            {
                ModelState.AddModelError("sodienthoai_kh", "Số điện thoại không được bỏ trống.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(p.SOTHICH))
            {
                ModelState.AddModelError("sothich_kh", "Sở thích không được bỏ trống.");
                return View();
            }
            dl.KHACHHANGs.InsertOnSubmit(p);
            dl.SubmitChanges();
            return RedirectToAction("DANGNHAP_KH");
        }

        public ActionResult DANGXUAT_KH()
        {
            Session.Clear();
            return RedirectToAction("Index_KH");
        }
        public ActionResult CHITIETSANPHAM(int? id)
        {
            List<CHITIETSANPHAM> p = dl.CHITIETSANPHAMs.ToList();
            List<HINHANH> y = dl.HINHANHs.ToList();
            List<KICHCO> x = dl.KICHCOs.ToList();
            List<MAUSAC> g = dl.MAUSACs.ToList();
            List<KHUYENMAI> m = dl.KHUYENMAIs.ToList();
            List<CHITIETKHUYENMAI> ctm = dl.CHITIETKHUYENMAIs.ToList();

            Session["chitietsanpham_chitietsp"] = p;
            Session["hinhanh_chitietsp"] = y;
            Session["kichco_chitietsp"] = x;
            Session["mausac_chitietsp"] = g;
            Session["khuyenmai_chitietsp"] = m;
            Session["chitietkhuyenmai_chitietsp"] = ctm;
            
            SANPHAM k = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            return View(k);
        }
        [HttpPost]
        public ActionResult THEMVAOGIOHANG(FormCollection e)
        {
            int id = int.Parse(e["masp"]);
            string mams = e["mausp"].Trim();
            string makc = e["sizesp"].Trim();
            int soluong = int.Parse(e["soluong"]);
            TempData["ErrorMessage"] = null;
            Debug.WriteLine("================>sl:{0}", soluong);
            CHITIETSANPHAM p = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == id && t.MAMS == mams && t.MAKC == makc && t.SOLUONG > soluong);
            //Debug.WriteLine("================>sl:{0},{1},{2}", soluong,p.MAMS,p.MAKC);
            if (p == null)
            {
                TempData["ErrorMessage"] = "Hết hàng";
                return RedirectToAction("CHITIETSANPHAM", new { id });
            }
            else
            {
                THEMGIAY gh = Session["gh"] as THEMGIAY;
                if (gh == null)
                {
                    gh = new THEMGIAY();
                }
                bool kq = gh.xulisolieu(id, mams, makc, soluong);
                if (!kq)
                {
                    return RedirectToAction("CHITIETSANPHAM", new { id });
                }
                Session["gh"] = gh;
                return RedirectToAction("Index_KH");
            }          
        }
        public ActionResult HIENTHIGIOHANG()
        {
            THEMGIAY gh = Session["gh"] as THEMGIAY;
            if (gh == null)
            {
                gh = new THEMGIAY();
            }
            List<HINHANH> y = dl.HINHANHs.ToList();
            List<KICHCO> x = dl.KICHCOs.ToList();
            List<MAUSAC> g = dl.MAUSACs.ToList();
            List<SANPHAM> i = dl.SANPHAMs.ToList();
            Session["hinhanh_giohang"] = y;
            Session["kichco_giohang"] = x;
            Session["mausac_giohang"] = g;
            Session["sanpham_giohang"] = i;
            return View(gh);
        }
        public ActionResult XOASP_GIOHANG(int? masp)
        {
            if (masp == null)
            {
                return RedirectToAction("HIENTHIGIOHANG");
            }
            else
            {
                THEMGIAY gh = Session["gh"] as THEMGIAY;
                GIOHANG spxoa = gh.lst_gh.FirstOrDefault(t => t.masp == masp);
                if (spxoa != null)
                {
                    gh.lst_gh.Remove(spxoa);
                }
                Session["gh"] = gh;
                return RedirectToAction("HIENTHIGIOHANG");
            }
        }
        public ActionResult CAPNHATSL_GIOHANG(int? masp,FormCollection e)
        {
            int soluong = int.Parse(e["soluongcapnhat"]);
            string mams = e["mams"].Trim();
            string makc = e["makc"].Trim();
            THEMGIAY gh = Session["gh"] as THEMGIAY;
            CHITIETSANPHAM p = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == masp && t.MAMS == mams && t.MAKC == makc);
            GIOHANG spxoa = gh.lst_gh.FirstOrDefault(t => t.masp == masp && p.SOLUONG > soluong);            
            if (spxoa != null)
            {
                spxoa.soluong = soluong;
                Session["gh"] = gh;
                return RedirectToAction("HIENTHIGIOHANG");
            }
            else
            {
                return RedirectToAction("HIENTHIGIOHANG");
            }
            
        }
        
        public ActionResult DATHANG()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DATHANG(FormCollection e)
        {
            KHACHHANG p = Session["khachhangdangnhap"] as KHACHHANG;
            string tp=e["city"];
            string quan=e["district"];
            string phuong=e["ward"];
            string pttt = e["phuongthucthanhtoan"];
            string diachicuthe = e["diachicuthe"];
            string ghitru= e["ghitru"];
            if (string.IsNullOrWhiteSpace(tp) || tp == "Chọn tỉnh thành")
            {
                ModelState.AddModelError("city", "Vui lòng chọn tỉnh thành.");
                return View();
            }
            Debug.WriteLine("=============>>>{0}", quan);
            if (string.IsNullOrWhiteSpace(quan) || quan == "Chọn Quận / Huyện"||quan.Length==0)
            {
                ModelState.AddModelError("district", "Vui lòng chọn quận huyện.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(phuong) || phuong == "Chọn Phường / Xã"|| phuong.Length==0)
            {
                ModelState.AddModelError("ward", "Vui lòng chọn phường xã.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(pttt)||pttt.Length==0)
            {
                ModelState.AddModelError("phuongthucthanhtoan", "Vui lòng chọn phương thức thanh toán.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(diachicuthe) || diachicuthe.Length==0)
            {
                ModelState.AddModelError("diachicuthe", "Vui lòng nhập địa chỉ cụ thể.");
                return View();
            }
            
          
                DONHANG n = new DONHANG();
                n.MAKH = p.MAKH;
                DateTime now = DateTime.Now;
                n.NGAYDAT = new DateTime(now.Year, now.Month, now.Day);
                n.NGAYGIAO = new DateTime(now.Year, now.Month, now.Day).AddDays(5);
                n.TRANGTHAI = "Chờ xác nhận";
                n.GHITRU = ghitru;
                THEMGIAY gh = Session["gh"] as THEMGIAY;
                n.TONGTIEN = gh.tongtien;
                n.QUAN = quan;
                n.PHUONG = phuong;
                n.TINHTHANH = tp;
                n.DIACHICUTHE = diachicuthe;
                dl.DONHANGs.InsertOnSubmit(n);
                dl.SubmitChanges();
                foreach (GIOHANG a in gh.lst_gh)
                {
                    CHITIETDONHANG k = new CHITIETDONHANG();
                    k.MADH = n.MADH;
                    k.MASP = a.masp;
                    k.MAKC = a.kichco;
                    k.MAMS = a.mausac;
                    k.THANHTIEN = a.thanhtien;
                    k.SOLUONG = a.soluong;
                    dl.CHITIETDONHANGs.InsertOnSubmit(k);
                    dl.SubmitChanges();
                }
                gh.lst_gh.Clear();
                gh = null;
                return RedirectToAction("Index_KH");
            
            
        }
        public ActionResult THONGTINKHACHHANG()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CAPNHAT_TTTKH( FormCollection e)
        {
            if (Session["khachhangdangnhap"] != null)
            {
                KHACHHANG n = Session["khachhangdangnhap"] as KHACHHANG;
                KHACHHANG p = dl.KHACHHANGs.FirstOrDefault(t=>t.MAKH==n.MAKH);
                p.TENKH = e["ten_kh"];
                p.NGAYSINH = DateTime.Parse(e["ngaysinh_kh"]);
                p.GIOITINH = e["gioitinh_kh"];
                p.DIACHI = e["diachi_kh"];
                p.SODIENTHOAI = e["sodienthoai_kh"];
                KHACHHANG k = dl.KHACHHANGs.FirstOrDefault(t => t.SODIENTHOAI == p.SODIENTHOAI && t.MAKH != p.MAKH);
                if (k != null)
                {                
                    ModelState.AddModelError("sodienthoai_kh","Số điện thoại đã tồn tại. Vui lòng chọn số điện thoại khác.");
                    return View("THONGTINKHACHHANG");
                }
                p.SOTHICH = e["sothich_kh"];
                dl.SubmitChanges();
                Session["khachhangdangnhap"] = p;
                return RedirectToAction("Index_KH");
            }

            return RedirectToAction("DANGNHAP_KH");
        }
        public ActionResult DOIMATKHAU_KH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DOIMATKHAU_KH( FormCollection e)
        {
            KHACHHANG n = Session["khachhangdangnhap"] as KHACHHANG;
            KHACHHANG p = dl.KHACHHANGs.FirstOrDefault(t => t.MAKH == n.MAKH);
            string mkcu=e["mkcu_kh"].Trim(); 
            string mkmoi=e["mkmoi_kh"].Trim();
            string xacnhan = e["xacnhanmkmoi_kh"].Trim();
            if (n == null)
            {             
                return RedirectToAction("DANGNHAP_KH");
            }
            if (p == null)
            {
                // Nếu không tìm thấy khách hàng, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login");
            }
            if (p.MATKHAU.Trim() != mkcu)
            {
                // Nếu mật khẩu cũ không đúng, hiển thị thông báo lỗi
                ModelState.AddModelError("mkcu_kh", "Mật khẩu cũ không đúng.");
                return View();
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới có khớp nhau không
            if (mkmoi != xacnhan)
            {
                // Nếu mật khẩu mới và xác nhận mật khẩu không khớp, hiển thị thông báo lỗi
                ModelState.AddModelError("xacnhanmkmoi_kh", "Mật khẩu mới và xác nhận mật khẩu không khớp.");
                return View();
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(mkmoi, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                // Nếu mật khẩu chứa ký tự tiếng Việt, hiển thị thông báo lỗi
                 ModelState.AddModelError("mkmoi_kh", "Mật khẩu không được chứa ký tự tiếng Việt.");
                return View();
            }
            p.MATKHAU = mkmoi;
            dl.SubmitChanges();
            return RedirectToAction("Index_KH");
        }
        public ActionResult LICHSUDONHANG_KH()
        {
            KHACHHANG n = Session["khachhangdangnhap"] as KHACHHANG;
            List<DONHANG> p = dl.DONHANGs.Where(t => t.MAKH == n.MAKH).ToList();
            return View(p);
        }
        public ActionResult HUYDONHANG_KH(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("LICHSUDONHANG_KH");
            }
            else
            {
                DONHANG k = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
                return View(k);
            }           
        }
        [HttpPost]
        public ActionResult HUYDONHANG_KH(FormCollection e)
        {
          int id =int.Parse(e["MADH"]);
          string lido = e["LIDOHUY"];
          if (lido.Length == 0 || lido == null)
          {
              ModelState.AddModelError("LIDOHUY", "*");
              DONHANG donHang = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
              return View(donHang);
          }
          DONHANG p = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
          p.TRANGTHAI = "Huỷ đơn";
          p.GHITRUHUYHANG = lido;
          dl.SubmitChanges();
          return RedirectToAction("LICHSUDONHANG_KH");
        }
        public ActionResult CHITIETDONHANG(int? id)
        {
            List<CHITIETDONHANG> p = dl.CHITIETDONHANGs.ToList();
            if (id != null)
            {
                p = p.Where(t => t.MADH == id).ToList();
            }
            List<HINHANH> y = dl.HINHANHs.ToList();
            List<KICHCO> x = dl.KICHCOs.ToList();
            List<MAUSAC> g = dl.MAUSACs.ToList();
            List<SANPHAM> i = dl.SANPHAMs.ToList();
            Session["hinhanh_chitietdonhang"] = y;
            Session["kichco_chitietdonhang"] = x;
            Session["mausac_chitietdonhang"] = g;
            return View(p);
        }
        public ActionResult DANGNHAP_ADMIN()
        {
            return RedirectToAction("Index_ql","Quanli");
        }
    }
}
