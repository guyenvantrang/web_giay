using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_SNEAKER.Models;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace SHOP_SNEAKER.Controllers
{
    public class QuanliController : Controller
    {
        dulieuDataContext dl = new dulieuDataContext();
        public ActionResult Index_ql()
        {
            return View();
        }
        // Sản phẩm =============================================================================
        public ActionResult SANPHAM()
        {
            return View();
        }
        public ActionResult SANPHAM_pv()
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            return PartialView(p);
        }
        public ActionResult Search(string searchString)
        {
            List<SANPHAM> products = dl.SANPHAMs.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = dl.SANPHAMs.Where(t => t.TENSP.Contains(searchString.ToUpper())).ToList();
            }
            foreach (SANPHAM a in products)
            {
                Debug.WriteLine("--------{0}", a.TENSP);
            }
            return PartialView("SANPHAM_pv", products); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult TIMKIEMTHEOMA(string a)
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            if (a.Length == 0)
            {
                p = dl.SANPHAMs.ToList();
                return PartialView("SANPHAM_pv", p);
            }
            else
            {
                p = dl.SANPHAMs.Where(t => t.MASP.ToString() == a.Trim()).ToList();
                return PartialView("SANPHAM_pv", p);
            }
        }
        public ActionResult THEMSP()
        {
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            List<LOAISANPHAM> l = dl.LOAISANPHAMs.ToList();
            List<HINHANH> ha = dl.HINHANHs.ToList();
            List<KICHCO> kc = dl.KICHCOs.ToList();
            List<MAUSAC> ms = dl.MAUSACs.ToList();
            List<CHITIETSANPHAM> ctsp = dl.CHITIETSANPHAMs.ToList();
            List<NHANHANG> nh = dl.NHANHANGs.ToList();

            Session["LoaiSanPham"] = l;
            Session["HinhAnh"] = ha;
            Session["KichCo"] = kc;
            Session["MauSac"] = ms;
            Session["ChiTietSanPham"] = ctsp;
            Session["nhanhang"] = nh;
            return View(p);
        }
        [HttpPost]
        public ActionResult THEMSP(FormCollection e, HttpPostedFileBase hinhanhchinh_sp)
        {
            int MALOAI = int.Parse(e["loai_sp"]);
            string TENSP = e["ten_sp"];
            int GIAGOCSP = int.Parse(e["dongia_sp"]);
            string MANH = e["nhanhang_sp"];
            string MOTA = e["mota_sp"];
            string maha = e["maha_sp"];


            if (string.IsNullOrEmpty(MALOAI.ToString()) || MALOAI == 0)
            {
                ModelState.AddModelError("loai_sp", "* không được bỏ trống.");
                return View();
            }

            // Kiểm tra Tên Sản Phẩm
            if (string.IsNullOrEmpty(TENSP))
            {
                ModelState.AddModelError("ten_sp", "* không được bỏ trống.");
                return View();
            }

            // Kiểm tra Đơn Giá
            if (string.IsNullOrEmpty(GIAGOCSP.ToString()) || GIAGOCSP == 0)
            {
                ModelState.AddModelError("GIAGOCSP", "* không được bỏ trống.");
                return View();
            }

            // Kiểm tra Nhãn Hàng
            if (string.IsNullOrEmpty(MANH))
            {
                ModelState.AddModelError("nhanhang_sp", "* không được bỏ trống.");
                return View();
            }

            // Kiểm tra Mô Tả
            if (string.IsNullOrEmpty(MOTA))
            {
                ModelState.AddModelError("mota_sp", "* không được bỏ trống.");
                return View();
            }
            if (string.IsNullOrEmpty(maha))
            {
                ModelState.AddModelError("maha_sp", "* không được bỏ trống.");
                return View();
            }
            if (hinhanhchinh_sp == null || hinhanhchinh_sp.ContentLength == 0)
            {
                ModelState.AddModelError("hinhanhchinh_sp", "* không được bỏ trống.");
                return View();
            }
            string fileName = Path.GetFileName(hinhanhchinh_sp.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/hinhanh/hinhanhsanpham/"), fileName);
            hinhanhchinh_sp.SaveAs(path);
            SANPHAM p = new SANPHAM();
            p.MALOAI = MALOAI;
            p.TENSP = TENSP;
            p.GIAGOCSP = GIAGOCSP;
            p.SOLUONGTON = 0;
            p.MANH = MANH;
            p.HINHANHBIA = fileName;
            p.MAHA = maha;
            p.MOTA = MOTA;
            p.GIACHINHTHUCSP = GIAGOCSP;
            dl.SANPHAMs.InsertOnSubmit(p);
            dl.SubmitChanges();
            return RedirectToAction("SANPHAM");
        }
        public ActionResult SUA_SP(int? id)
        {
            List<LOAISANPHAM> l = dl.LOAISANPHAMs.ToList();
            List<HINHANH> ha = dl.HINHANHs.ToList();
            List<KICHCO> kc = dl.KICHCOs.ToList();
            List<MAUSAC> ms = dl.MAUSACs.ToList();
            List<CHITIETSANPHAM> ctsp = dl.CHITIETSANPHAMs.ToList();
            List<NHANHANG> nh = dl.NHANHANGs.ToList();

            Session["LoaiSanPham"] = l;
            Session["HinhAnh"] = ha;
            Session["KichCo"] = kc;
            Session["MauSac"] = ms;
            Session["ChiTietSanPham"] = ctsp;
            Session["nhanhang"] = nh;
            List<SANPHAM> p = dl.SANPHAMs.ToList();
            SANPHAM k = p.FirstOrDefault(t => t.MASP == id);
            return View(k);
        }
        public ActionResult SUA_SP_(FormCollection e, HttpPostedFileBase hinhanhchinh_sp)
        {
            int id = int.Parse(e["ma_sp"]);
            int MALOAI = int.Parse(e["loai_sp"]);
            string TENSP = e["ten_sp"];
            int GIAGOCSP = int.Parse(e["dongia_sp"]);
            string MANH = e["nhanhang_sp"];
            string MOTA = e["mota_sp"];
            string maha = e["maha_sp"];

            // Kiểm tra các điều kiện trước khi thực hiện cập nhật
            if (string.IsNullOrEmpty(MALOAI.ToString()) || MALOAI == 0)
            {
                ModelState.AddModelError("loai_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            if (string.IsNullOrEmpty(TENSP))
            {
                ModelState.AddModelError("ten_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            if (string.IsNullOrEmpty(GIAGOCSP.ToString()) || GIAGOCSP == 0)
            {
                ModelState.AddModelError("dongia_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            if (string.IsNullOrEmpty(MANH))
            {
                ModelState.AddModelError("nhanhang_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            if (string.IsNullOrEmpty(MOTA))
            {
                ModelState.AddModelError("mota_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            if (string.IsNullOrEmpty(maha))
            {
                ModelState.AddModelError("maha_sp", "* không được bỏ trống.");
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                return View("SUA_SP", product); // Trả lại đối tượng SANPHAM khi có lỗi
            }

            string fileName = string.Empty;
            if (hinhanhchinh_sp != null && hinhanhchinh_sp.ContentLength > 0)
            {
                // Nếu người dùng chọn tệp mới, lưu tệp và lấy tên tệp
                fileName = Path.GetFileName(hinhanhchinh_sp.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/hinhanh/hinhanhsanpham/"), fileName);
                hinhanhchinh_sp.SaveAs(path);
            }
            else
            {
                // Nếu không có hình ảnh mới, giữ lại tên hình ảnh cũ
                SANPHAM existingProduct = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                fileName = existingProduct.HINHANHBIA; // Lấy tên hình ảnh hiện tại nếu không chọn hình ảnh mới
            }


            try
            {
                // Lưu hình ảnh lên server
                string fileName_1 = Path.GetFileName(hinhanhchinh_sp.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/hinhanh/hinhanhsanpham/"), fileName_1);
                hinhanhchinh_sp.SaveAs(path);

                // Cập nhật thông tin sản phẩm
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                product.MALOAI = MALOAI;
                product.TENSP = TENSP;
                product.GIAGOCSP = GIAGOCSP;
                List<CHITIETSANPHAM> h = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
                Debug.WriteLine("----------------------------------{0}", h.Count());
                if (h.Count() > 0)
                {
                    product.SOLUONGTON = h.Sum(t => t.SOLUONG);
                }
                if (h.Count() == 0)
                {
                    product.SOLUONGTON = 0;
                }
                product.MANH = MANH;
                product.HINHANHBIA = fileName;
                product.MAHA = maha;
                product.MOTA = MOTA;
                product.GIACHINHTHUCSP = GIAGOCSP;

                // Lưu thay đổi vào database
                dl.SubmitChanges();
                return RedirectToAction("SANPHAM");
            }
            catch
            {
                // Nếu có lỗi khi lưu hình ảnh
                SANPHAM product = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                product.MALOAI = MALOAI;
                product.TENSP = TENSP;
                product.GIAGOCSP = GIAGOCSP;
                List<CHITIETSANPHAM> h = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
                Debug.WriteLine("----------------------------------{0}", h.Count());
                if (h.Count() > 0)
                {
                    product.SOLUONGTON = h.Sum(t => t.SOLUONG);
                }
                if (h.Count() == 0)
                {
                    product.SOLUONGTON = 0;
                }
                product.MANH = MANH;
                product.MAHA = maha;
                product.MOTA = MOTA;
                product.GIACHINHTHUCSP = GIAGOCSP;
                dl.SubmitChanges();
                return RedirectToAction("SANPHAM");
            }
        }

        public ActionResult DANHSACHCHITIETSP(int? id)
        {
            List<CHITIETSANPHAM> p = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
            SANPHAM k = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            Session["sanpham_ctsp"] = k;
            return View(p);
        }

        public ActionResult THEMCTSP()
        {
            List<KICHCO> kc = dl.KICHCOs.ToList();
            List<MAUSAC> ms = dl.MAUSACs.ToList();
            List<NHANHANG> nh = dl.NHANHANGs.ToList();
            Session["KichCo_ctsp"] = kc;
            Session["MauSac_ctsp"] = ms;
            Session["nhanhang_ctsp"] = nh;
            SANPHAM sp = Session["sanpham_ctsp"] as SANPHAM;
            return View(sp);
        }
        [HttpPost]
        public ActionResult THEMCTSP(FormCollection e)
        {
            int ma_ctsp = int.Parse(e["ma_ctsp"]);
            string kc_ctsp = e["kc_ctsp"];
            string ms_ctsp = e["ms_ctsp"];
            string mota_ctsp = e["mota_ctsp"];
            int soluong_ctsp = int.Parse(e["soluong_ctsp"]);

            if (string.IsNullOrEmpty(kc_ctsp) || kc_ctsp.Length == 0)
            {
                ModelState.AddModelError("kc_ctsp", "* không được bỏ trống.");
                SANPHAM sp = Session["sanpham_ctsp"] as SANPHAM;
                return View("THEMCTSP", sp); // Trả lại đối tượng SANPHAM khi có lỗi
            }
            if (string.IsNullOrEmpty(ms_ctsp) || ms_ctsp.Length == 0)
            {
                ModelState.AddModelError("ms_ctsp", "* không được bỏ trống.");
                SANPHAM sp = Session["sanpham_ctsp"] as SANPHAM;
                return View("THEMCTSP", sp); // Trả lại đối tượng SANPHAM khi có lỗi
            }
            if (string.IsNullOrEmpty(mota_ctsp) || mota_ctsp.Length == 0)
            {
                ModelState.AddModelError("mota_ctsp", "* không được bỏ trống.");
                SANPHAM sp = Session["sanpham_ctsp"] as SANPHAM;
                return View("THEMCTSP", sp); // Trả lại đối tượng SANPHAM khi có lỗi
            }
            CHITIETSANPHAM n = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == ma_ctsp && t.MAKC == kc_ctsp && t.MAMS == ms_ctsp);
            if (n != null)
            {
                ModelState.AddModelError("ma_ctsp", "* Đã tồn tại sản phẩm này");
                SANPHAM sp = Session["sanpham_ctsp"] as SANPHAM;
                return View("THEMCTSP", sp); // Trả lại đối tượng SANPHAM khi có lỗi
            }
            CHITIETSANPHAM p = new CHITIETSANPHAM();
            p.MASP = ma_ctsp;
            p.MAKC = kc_ctsp;
            p.MAMS = ms_ctsp;
            p.MOTA = mota_ctsp;
            p.SOLUONG = soluong_ctsp;
            dl.CHITIETSANPHAMs.InsertOnSubmit(p);
            dl.SubmitChanges();

            SANPHAM sp_ = Session["sanpham_ctsp"] as SANPHAM;
            int id = sp_.MASP;
            List<CHITIETSANPHAM> tong = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
            SANPHAM tim = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            Debug.WriteLine("===================[[[[]{0}", tong.Sum(t => t.SOLUONG));
            tim.SOLUONGTON = tong.Sum(t => t.SOLUONG);
            dl.SubmitChanges();
            return RedirectToAction("DANHSACHCHITIETSP", new { id });
        }
        public ActionResult XOA_SP(int? id)
        {
            SANPHAM p = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            try
            {
                dl.SANPHAMs.DeleteOnSubmit(p);
                List<CHITIETSANPHAM> k = dl.CHITIETSANPHAMs.Where(t => t.MASP == p.MASP).ToList();
                dl.CHITIETSANPHAMs.DeleteAllOnSubmit(k);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* Không thể xoá vì đã tồn tại trong đơn hàng");
                return View("SANPHAM");
            }
            return RedirectToAction("SANPHAM");
        }
        public ActionResult SUA_CTSP(int? id, string makc, string mams)
        {

            List<KICHCO> kc = dl.KICHCOs.ToList();
            List<MAUSAC> ms = dl.MAUSACs.ToList();
            List<NHANHANG> nh = dl.NHANHANGs.ToList();
            Session["KichCo_sua_ctsp"] = kc;
            Session["MauSac_sua_ctsp"] = ms;
            Session["nhanhang_sua_ctsp"] = nh;
            CHITIETSANPHAM p = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == id && t.MAKC == makc && t.MAMS == mams);
            Session["giatrisua"] = p;
            return View(p);
        }
        [HttpPost]
        public ActionResult SUA_CTSP(FormCollection e)
        {
            //    int ma_ctsp = int.Parse(e["ma_ctsp"]);
            //    string nhanhang_sp = e["nhanhang_sp"];
            //    string kc_ctsp = e["kc_ctsp"];
            //    string ms_ctsp = e["ms_ctsp"];
            string mota_ctsp = e["mota_ctsp"];
            int soluong_ctsp = int.Parse(e["soluong_ctsp"]);
            CHITIETSANPHAM s = Session["giatrisua"] as CHITIETSANPHAM;
            int id = s.MASP;
            string makc = s.MAKC;
            string mams = s.MAMS;

            if (string.IsNullOrEmpty(mota_ctsp) || mota_ctsp.Length == 0)
            {
                ModelState.AddModelError("mota_ctsp", "* không được bỏ trống.");
                return View("SUA_CTSP", new { id, makc, mams });
            }
            //CHITIETSANPHAM n = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == ma_ctsp && t.MAKC == kc_ctsp && t.MAMS == ms_ctsp);
            CHITIETSANPHAM sua = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == id && t.MAKC == makc && t.MAMS == mams);
            sua.MOTA = mota_ctsp;
            sua.SOLUONG = soluong_ctsp;
            dl.SubmitChanges();
            List<CHITIETSANPHAM> tong = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
            SANPHAM tim = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
            tim.SOLUONGTON = tong.Sum(t => t.SOLUONG);
            dl.SubmitChanges();
            return RedirectToAction("DANHSACHCHITIETSP", new { id });
        }
        public ActionResult XOA_CTSP(int? id, string makc, string mams)
        {
            try
            {
                CHITIETSANPHAM sua = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == id && t.MAKC == makc && t.MAMS == mams);
                dl.CHITIETSANPHAMs.DeleteOnSubmit(sua);
                dl.SubmitChanges();
                List<CHITIETSANPHAM> tong = dl.CHITIETSANPHAMs.Where(t => t.MASP == id).ToList();
                SANPHAM tim = dl.SANPHAMs.FirstOrDefault(t => t.MASP == id);
                tim.SOLUONGTON = tong.Sum(t => t.SOLUONG);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* Không thể xoá vì đã tồn tại trong đơn hàng");
                return RedirectToAction("DANHSACHCHITIETSP", new { id });
            }
            return RedirectToAction("DANHSACHCHITIETSP", new { id });
        }
        // ĐƠN HÀNG======================================================================
        public ActionResult DONHANG()
        {
            return View();
        }
        public ActionResult DONHANG_pv()
        {
            List<DONHANG> p = dl.DONHANGs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMNGAY(DateTime a, DateTime b)
        {
            List<DONHANG> donhang = dl.DONHANGs.ToList();
            donhang = dl.DONHANGs.Where(t => t.NGAYDAT >= a && t.NGAYGIAO <= b).ToList();
            return PartialView("DONHANG_pv", donhang); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult TIMKIEMTHEOMADON(string a)
        {
            List<DONHANG> p = dl.DONHANGs.ToList();
            if (a.Length == 0)
            {
                p = dl.DONHANGs.ToList();
                return PartialView("DONHANG_pv", p);
            }
            else
            {
                p = dl.DONHANGs.Where(t => t.MADH.ToString() == a.Trim()).ToList();
                return PartialView("DONHANG_pv", p);
            }
        }
        public ActionResult CHITIETDONHANG(int? id)
        {
            List<CHITIETDONHANG> p = dl.CHITIETDONHANGs.Where(t => t.MADH == id).ToList();
            DONHANG k = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
            Session["donhang_donhang"] = k;
            return View(p);
        }
        public ActionResult THEMCHITIETDONHANG()
        {
            List<SANPHAM> l = dl.SANPHAMs.ToList();
            List<KICHCO> kc = dl.KICHCOs.ToList();
            List<MAUSAC> ms = dl.MAUSACs.ToList();

            Session["SanPham"] = l;
            Session["KichCo"] = kc;
            Session["MauSac"] = ms;
            int? id = ViewBag.mahoadon as int?;
            DONHANG k = Session["donhang_donhang"] as DONHANG;
            DONHANG p = dl.DONHANGs.FirstOrDefault(t => t.MADH == k.MADH);
            return View(p);
        }
        [HttpPost]
        public ActionResult THEMCHITIETDONHANG(FormCollection e)
        {
            string kc = e["kc_ctsp"];
            int sp = int.Parse(e["sp_ctsp"]);
            string ms = e["ms_ctsp"];
            int sl = int.Parse(e["soluong_ctsp"]);
            if (string.IsNullOrEmpty(kc))
            {
                ModelState.AddModelError("kc_ctsp", "* Không để trống");
                DONHANG k = Session["donhang_donhang"] as DONHANG;
                return View(k);
            }
            if (string.IsNullOrEmpty(ms))
            {
                ModelState.AddModelError("ms_ctsp", "* Không để trống");
                DONHANG k = Session["donhang_donhang"] as DONHANG;
                return View(k);
            }
            if (sp == 0)
            {
                ModelState.AddModelError("sp_ctsp", "* Không để trống");
                DONHANG k = Session["donhang_donhang"] as DONHANG;
                return View(k);
            }
            DONHANG n = Session["donhang_donhang"] as DONHANG;
            CHITIETDONHANG u = dl.CHITIETDONHANGs.FirstOrDefault(t => t.MADH == n.MADH && t.MAKC == kc && t.MAMS == ms && t.MASP == sp);
            if (u == null)
            {
                CHITIETDONHANG y = new CHITIETDONHANG();
                y.MADH = n.MADH;
                y.MAKC = kc;
                y.MAMS = ms;
                y.MASP = sp;
                y.SOLUONG = sl;
                SANPHAM sp_sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == sp);
                y.THANHTIEN = sl * sp_sp.GIACHINHTHUCSP;
                dl.CHITIETDONHANGs.InsertOnSubmit(y);
                dl.SubmitChanges();
                DONHANG dh = dl.DONHANGs.FirstOrDefault(t => t.MADH == n.MADH);
                dh.TONGTIEN = dl.CHITIETDONHANGs.Where(t => t.MADH == n.MADH).Sum(t => t.THANHTIEN);
                dl.SubmitChanges();
                int id = n.MADH;
                return RedirectToAction("CHITIETDONHANG", new { id });
            }
            else
            {
                u.SOLUONG = u.SOLUONG + sl;
                SANPHAM sp_sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == sp);
                u.THANHTIEN = sl * sp_sp.GIACHINHTHUCSP;
                dl.SubmitChanges();
                DONHANG dh = dl.DONHANGs.FirstOrDefault(t => t.MADH == n.MADH);
                dh.TONGTIEN = dl.CHITIETDONHANGs.Where(t => t.MADH == n.MADH).Sum(t => t.THANHTIEN);
                dl.SubmitChanges();
                int id = n.MADH;
                return RedirectToAction("CHITIETDONHANG", new { id });
            }
        }
        public ActionResult XACNHANDONHANG(int id)
        {
            DONHANG p = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
            if (p != null)
            {
                if (p.TRANGTHAI == "Chờ xác nhận")
                {
                    p.TRANGTHAI = "Đang giao";
                    dl.SubmitChanges();
                    return RedirectToAction("DONHANG");
                }
                if (p.TRANGTHAI == "Đang giao")
                {
                    p.TRANGTHAI = "Hoàn thành";
                    dl.SubmitChanges();
                    List<CHITIETDONHANG> l = dl.CHITIETDONHANGs.Where(t => t.MADH == p.MADH).ToList();
                    foreach (CHITIETDONHANG a in l)
                    {
                        CHITIETSANPHAM h = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == a.MASP && t.MAMS == a.MAMS && t.MAKC == a.MAKC);
                        h.SOLUONG = h.SOLUONG - a.SOLUONG;
                        SANPHAM sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == a.MASP);
                        sp.SOLUONGTON = sp.SOLUONGTON - a.SOLUONG;
                        dl.SubmitChanges();

                    }
                    return RedirectToAction("DONHANG");
                }
            }
            return RedirectToAction("DONHANG");
        }
        public ActionResult XOADONHANG(int id)
        {
            try
            {
                DONHANG p = dl.DONHANGs.FirstOrDefault(t => t.MADH == id);
                List<CHITIETDONHANG> k = dl.CHITIETDONHANGs.Where(t => t.MADH == p.MADH).ToList();
                dl.CHITIETDONHANGs.DeleteAllOnSubmit(k);
                dl.SubmitChanges();
                dl.DONHANGs.DeleteOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                return RedirectToAction("DONHANG");
            }
           
            return RedirectToAction("DONHANG");
        }
        //KHÁCH HÀNG =======================================================================================
        public ActionResult KHACHHANG()
        {
            return View();
        }
        public ActionResult KHACHHANG_pv()
        {
            List<KHACHHANG> p = dl.KHACHHANGs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMTENKH(string a)
        {
            List<KHACHHANG> KH = dl.KHACHHANGs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                KH = dl.KHACHHANGs.Where(t => t.TENKH.Contains(a.ToUpper())).ToList();
            }
            return PartialView("KHACHHANG_pv", KH); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult TIMKIEMMAKH(string a)
        {
            List<KHACHHANG> KH = dl.KHACHHANGs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                KH = dl.KHACHHANGs.Where(t => t.MAKH.ToString().Trim() == a.Trim()).ToList();
            }
            return PartialView("KHACHHANG_pv", KH); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult THEMKHACHHANG()
        {

            return View();
        }
        [HttpPost]
        public ActionResult THEMKHACHHANG(FormCollection e)
        {
            string tenKhachHang = e["tenkh"];

            string diaChi = e["diachikh"];
            string soDienThoai = e["sodienthoaikh"].ToString();
            string tenDangNhap = e["tendangnhapkh"];
            string matKhau = e["matkhaukh"];
            string gioiTinh = e["gioitinh_kh"];
            string sothichkh = e["sothichkhachhang"];
            if (string.IsNullOrEmpty(tenKhachHang))
            {
                ModelState.AddModelError("tenkh", "Tên khách hàng không được để trống.");
                return View();
            }

            DateTime ngaySinh;
            if (!DateTime.TryParse(e["ngaysinh"], out ngaySinh))
            {
                ModelState.AddModelError("ngaysinh", "Ngày sinh không hợp lệ.");
                return View();
            }
            ngaySinh = DateTime.Parse(e["ngaysinh"].ToString());
            if (string.IsNullOrEmpty(diaChi))
            {
                ModelState.AddModelError("diachikh", "Địa chỉ không được để trống.");
                return View();
            }

            if (soDienThoai.ToString().Length < 10 || !soDienThoai.All(char.IsDigit))
            {
                ModelState.AddModelError("sodienthoaikh", "Số điện thoại không được để trống.");
                return View();
            }

            if (string.IsNullOrEmpty(tenDangNhap))
            {
                ModelState.AddModelError("tendangnhapkh", "Tên đăng nhập không được để trống.");
                return View();
            }
            if (string.IsNullOrEmpty(sothichkh))
            {
                ModelState.AddModelError("tendangnhapkh", "Tên đăng nhập không được để trống.");
                return View();
            }

            if (string.IsNullOrEmpty(matKhau) || System.Text.RegularExpressions.Regex.IsMatch(matKhau, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                ModelState.AddModelError("matkhaukh", "Mật khẩu không được để trống hay có kí tự tiếng việt.");
                return View();
            }
            KHACHHANG k = dl.KHACHHANGs.FirstOrDefault(t => t.TENDANGNHAP == tenDangNhap.Trim());
            if (k != null)
            {
                ModelState.AddModelError("tendangnhapkh", "Tên đăng nhập đã tồn tại");
                return View();
            }
            KHACHHANG q = dl.KHACHHANGs.FirstOrDefault(t => t.TENDANGNHAP == tenDangNhap.Trim());
            if (q != null)
            {
                ModelState.AddModelError("sodienthoaikh", "Số điện thoại đã tồn tại");
                return View();
            }
            try
            {
                KHACHHANG p = new KHACHHANG();
                p.TENKH = tenKhachHang;
                p.NGAYSINH = ngaySinh;
                p.DIACHI = diaChi;
                p.SODIENTHOAI = soDienThoai.ToString().Trim();
                p.TENDANGNHAP = tenDangNhap;
                p.MATKHAU = matKhau;
                p.GIOITINH = gioiTinh;
                p.SOTHICH = sothichkh;
                dl.KHACHHANGs.InsertOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("tenkh", "Tên khách hàng không được để trống.");
                return View();
            }

            return RedirectToAction("KHACHHANG");
        }
        public ActionResult XOAKHACHHANG(int? id)
        {
            KHACHHANG k = dl.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
            List<ADMIN> ad = dl.ADMINs.Where(t => t.TENDANGNHAP == k.TENDANGNHAP).ToList();
            if (ad.Count() == 0)
            {
                try
                {
                    KHACHHANG p = dl.KHACHHANGs.FirstOrDefault(t => t.MAKH == id);
                    dl.KHACHHANGs.DeleteOnSubmit(p);
                    dl.SubmitChanges();
                }
                catch
                {
                    ModelState.AddModelError("baoloi", "Khách hàng đã tồn tại trong hoá đơn");
                    return View("KHACHHANG");

                }
                return RedirectToAction("KHACHHANG");
            }
            else
            {
                ModelState.AddModelError("baoloi", "Không thế xoá chính bạn");
                return View("KHACHHANG");
            }
           
        }
        public ActionResult DONHANGKHACHHANG(string id)
        {
            List<DONHANG> p = dl.DONHANGs.ToList();
            if (id.Length == 0)
            {
                p = dl.DONHANGs.ToList();
                return PartialView("DONHANG_pv", p);
            }
            else
            {
                p = dl.DONHANGs.Where(t => t.MAKH.ToString().Trim() == id.Trim()).ToList();
                return PartialView("DONHANG_pv", p);
            }
        }
        //PHIẾU NHẬP ================================================
        public ActionResult PHIEUNHAP()
        {
            return View();
        }
        public ActionResult PHIEUNHAP_pv()
        {
            List<PHIEUNHAP> p = dl.PHIEUNHAPs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMNGAY_PN(DateTime a)
        {
            List<PHIEUNHAP> PHIEUNHAP = dl.PHIEUNHAPs.ToList();
            PHIEUNHAP = dl.PHIEUNHAPs.Where(t => t.NGAYNHAP == a).ToList();
            return PartialView("PHIEUNHAP_pv", PHIEUNHAP); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult THEMPN()
        {
            List<NHACUNGCAP> p = dl.NHACUNGCAPs.ToList();
            return View(p);
        }
        [HttpPost]
        public ActionResult THEMPN(FormCollection e)
        {

            int MANCC = int.Parse(e["ncc_pn"].ToString());
            if (string.IsNullOrEmpty(MANCC.ToString())||MANCC == 0)
            {
                ModelState.AddModelError("ncc_pn", "* không được bỏ trống.");
                List<NHACUNGCAP> ncc = dl.NHACUNGCAPs.ToList();
                return View(ncc);
            }           
            PHIEUNHAP p = new PHIEUNHAP();
            p.MANCC = MANCC;
            p.TONGTIEN = 0;
            p.TRANGTHAI = "Mới Lập";
            p.NGAYNHAP = DateTime.Now;
            dl.PHIEUNHAPs.InsertOnSubmit(p);
            dl.SubmitChanges();
            return RedirectToAction("PHIEUNHAP");
        }

        public ActionResult DANHSACHCHITIETSPPHIEUNHAP(int? id)
        {
            List<CHITIETPHIEUNHAP> p = dl.CHITIETPHIEUNHAPs.Where(t => t.MAPN == id).ToList();
            PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
            Session["phieunhap_pn"] = k;
            Session["kiemtrachitietphieunhap"] = p;
            ViewBag.madonhang = id;
            return View(p);
        }

        public ActionResult XOAPHIEUNHAP(int? id)
        {
            try
            {
                PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
                if (k.TRANGTHAI == "Mới Lập")
                {
                    dl.PHIEUNHAPs.DeleteOnSubmit(k);
                    dl.SubmitChanges();
                    return RedirectToAction("PHIEUNHAP");
                }
                else
                {
                    ModelState.AddModelError("baoloi", "Không thể xoá phiếu nhập này");
                    return View("PHIEUNHAP");
                }
            }
            catch
            {
                ModelState.AddModelError("baoloi", "Không thể xoá phiếu nhập này");
                return View("PHIEUNHAP");
            }
            
           
        }

        public ActionResult THEMCTPN(int id)
        {
            PHIEUNHAP p = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
            if (p.TRANGTHAI == "Mới Lập")
            {
                Session["sanpham_pn"] = dl.SANPHAMs.ToList();
                Session["KichCo_pn"] = dl.KICHCOs.ToList();
                Session["mausac_pn"] = dl.MAUSACs.ToList();
                return View(p);
            }
            else
            {
                ModelState.AddModelError("baoloi", "* Không thể sửa trong trạng thái này  .");
                List<CHITIETPHIEUNHAP> o = dl.CHITIETPHIEUNHAPs.Where(t => t.MAPN == p.MAPN).ToList();
                ViewBag.madonhang = id;
                return View("DANHSACHCHITIETSPPHIEUNHAP", o);
            }
            
        }
        [HttpPost]
        public ActionResult THEMCTPN(FormCollection e)
        {
            int id = int.Parse(e["ma_pn"]);     
            int masp =int.Parse(e["masp_pn"]);
            string kichco = e["kc_pn"].Trim();
            string mausac = e["ms_pn"].Trim();    
            int soluong = int.Parse(e["soluong_pn"]);

            if (masp == 0)
            {
                ModelState.AddModelError("masp_pn", "* Vui lòng chọn sản phẩm .");
                PHIEUNHAP i = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
                return  View("THEMCTPN",i); 
            }
            if (string.IsNullOrEmpty(kichco))
            {
                ModelState.AddModelError("KichCo_pn", "* Vui lòng chọn màu sắc .");
                PHIEUNHAP i = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
                return View("THEMCTPN", i); 
            }
            if (string.IsNullOrEmpty(mausac))
            {
                ModelState.AddModelError("mausac_pn", "* Vui lòng chọn kích thước .");
                PHIEUNHAP i = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
                return View("THEMCTPN", i); 
            }
            CHITIETPHIEUNHAP n = dl.CHITIETPHIEUNHAPs.FirstOrDefault(t => t.MAPN == id && t.MASP == masp && t.MAKC == kichco && t.MAMS == mausac);
            if (n != null)
            {
                ModelState.AddModelError("ma_pn", "* Đã tồn tại sản phẩm này");
                PHIEUNHAP i = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
                return View("THEMCTPN", i); 
            }
            CHITIETPHIEUNHAP p = new CHITIETPHIEUNHAP();
            p.MAPN = id;
            p.MASP = masp;
            p.MAKC = kichco;
            p.MAMS = mausac;
            p.SOLUONG = soluong;
            SANPHAM sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == masp);
            p.DONGIA = sp.GIAGOCSP * soluong;
            dl.CHITIETPHIEUNHAPs.InsertOnSubmit(p);
            dl.SubmitChanges();
            PHIEUNHAP pn = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
            pn.TONGTIEN = dl.CHITIETPHIEUNHAPs.Where(t => t.MAPN == id).Sum(t => t.DONGIA);
            dl.SubmitChanges();
            return RedirectToAction("DANHSACHCHITIETSPPHIEUNHAP", new { id });
        }
        public ActionResult SUA_CTPN(int id , int msp , string makc , string mams)
        {
            CHITIETPHIEUNHAP p = dl.CHITIETPHIEUNHAPs.FirstOrDefault(t => t.MAPN == id && t.MASP == msp && t.MAKC == makc && t.MAMS == mams);
            Session["chitietpnsua"] = p;
            PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == p.MAPN);
            if (k.TRANGTHAI == "Mới Lập")
            {
            return View(p);
            }
            else
            {
                ModelState.AddModelError("baoloi", "* Không thể sửa trong trạng thái này  .");
                List<CHITIETPHIEUNHAP> o = dl.CHITIETPHIEUNHAPs.Where(t=>t.MAPN==p.MAPN).ToList();
                ViewBag.madonhang = id;
                return View("DANHSACHCHITIETSPPHIEUNHAP",o);
            }
        }
        [HttpPost]
        public ActionResult SUA_CTPN(FormCollection e)
        {

            CHITIETPHIEUNHAP a = Session["chitietpnsua"]  as CHITIETPHIEUNHAP;
             PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == a.MAPN);
            if (k.TRANGTHAI == "Mới Lập")
            {
            int id = a.MAPN;
            int msp = a.MASP;
            string makc = a.MAKC;
            string mams = a.MAMS;
            int soluong = int.Parse(e["soluong_pn"]);
            CHITIETPHIEUNHAP p = dl.CHITIETPHIEUNHAPs.FirstOrDefault(t => t.MAPN == id && t.MASP == msp && t.MAKC == makc && t.MAMS == mams);
            p.SOLUONG = soluong;
            dl.SubmitChanges();
            Session["chitietpnsua"] = null;
            return RedirectToAction("DANHSACHCHITIETSPPHIEUNHAP", new { id });
            }
            else
            {
                int id = a.MAPN;
                ModelState.AddModelError("baoloi", "* Không thể xoá .");
                return View("DANHSACHCHITIETSPPHIEUNHAP", new { id });
            }
        }
        public ActionResult XACNHANPHIEUNHAP(FormCollection e)
        {
            PHIEUNHAP a = Session["phieunhap_pn"] as PHIEUNHAP;
            PHIEUNHAP p = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == a.MAPN);
            if (p.TRANGTHAI == "Mới Lập")
            {
                p.TRANGTHAI = "Đã xác nhận";
                dl.SubmitChanges();
                return RedirectToAction("PHIEUNHAP");

            }

            if (p.TRANGTHAI.TrimEnd() == "Đã xác nhận")
            {
                //ModelState.AddModelError("baoloi", "Đơn này đã giao hoặc huỷ");
                p.TRANGTHAI = "Đang Giao";
                dl.SubmitChanges();
                return RedirectToAction("PHIEUNHAP");
            }
            if (p.TRANGTHAI.TrimEnd() == "Đang Giao")
            {
                p.TRANGTHAI = "Đã Nhận";
                dl.SubmitChanges();
                return RedirectToAction("PHIEUNHAP");

            }
            return RedirectToAction("PHIEUNHAP");
        }
        public ActionResult XACNHANNHAPHANG(){
            PHIEUNHAP p = Session["phieunhap_pn"] as PHIEUNHAP;
            PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == p.MAPN);
            if (p.TRANGTHAI=="Đã Nhận")
            {
                List<CHITIETPHIEUNHAP> ctpn = dl.CHITIETPHIEUNHAPs.Where(t => t.MAPN == p.MAPN).ToList();
                foreach (CHITIETPHIEUNHAP a in ctpn)
                {
                    CHITIETSANPHAM b = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == a.MASP && t.MAMS == a.MAMS && t.MAKC == a.MAKC);

                    if (b != null)
                    {
                        b.SOLUONG = b.SOLUONG + a.SOLUONG;
                        dl.SubmitChanges();
                        int tong = int.Parse(dl.CHITIETSANPHAMs.Where(t => t.MASP == a.MASP).Sum(t => t.SOLUONG).ToString());
                        SANPHAM sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == a.MASP);
                        sp.SOLUONGTON = tong;
                        dl.SubmitChanges();
                    }
                    else
                    {
                        CHITIETSANPHAM u = new CHITIETSANPHAM();
                        u.MASP = a.MASP;
                        u.MAMS = a.MAMS;
                        u.MAKC = a.MAKC;
                        u.MOTA = "None";
                        u.SOLUONG = a.SOLUONG;
                        dl.CHITIETSANPHAMs.InsertOnSubmit(u);
                        dl.SubmitChanges();
                        int tong = int.Parse(dl.CHITIETSANPHAMs.Where(t => t.MASP == a.MASP).Sum(t => t.SOLUONG).ToString());
                        SANPHAM sp = dl.SANPHAMs.FirstOrDefault(t => t.MASP == a.MASP);
                        sp.SOLUONGTON = tong;
                        dl.SubmitChanges();

                    }
                }
                k.TRANGTHAI = "Đã Nhập";
                dl.SubmitChanges();
                return RedirectToAction("SANPHAM");
            }
            return RedirectToAction("SANPHAM");
        }
        public ActionResult HUYPHIEUNHAP()
        {
            PHIEUNHAP k = Session["phieunhap_pn"] as PHIEUNHAP;
            PHIEUNHAP p = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == k.MAPN);
            p.TRANGTHAI = "Đã Huỷ";
            dl.SubmitChanges();
            int id = k.MAPN;
            return RedirectToAction("DANHSACHCHITIETSPPHIEUNHAP", new { id });
        }
        public ActionResult XOA_CTPN(int id, int msp, string makc, string mams)
        {
            PHIEUNHAP k = dl.PHIEUNHAPs.FirstOrDefault(t => t.MAPN == id);
            if (k.TRANGTHAI == "Mới Lập")
            {
                CHITIETPHIEUNHAP p = dl.CHITIETPHIEUNHAPs.FirstOrDefault(t => t.MAPN == id && t.MASP == msp && t.MAKC == makc && t.MAMS == mams);
                dl.CHITIETPHIEUNHAPs.DeleteOnSubmit(p);
                dl.SubmitChanges();
                return RedirectToAction("DANHSACHCHITIETSPPHIEUNHAP", new { id });
            }
            else
            {
                ModelState.AddModelError("baoloi", "* Không thể xoá trong trạng thái này  .");
                List<CHITIETPHIEUNHAP> o = dl.CHITIETPHIEUNHAPs.Where(t => t.MAPN == k.MAPN).ToList();
                ViewBag.madonhang = id;
                return View("DANHSACHCHITIETSPPHIEUNHAP", o);
            }
            
        }
        public ActionResult TIMKIEMTHEOMAPHIEUNHAP(string a)
        {
            List<PHIEUNHAP> p = dl.PHIEUNHAPs.ToList();
            if (a.Length == 0)
            {
                p = dl.PHIEUNHAPs.ToList();
                return PartialView("PHIEUNHAP_pv", p);
            }
            else
            {
                p = dl.PHIEUNHAPs.Where(t => t.MAPN.ToString() == a.Trim()).ToList();
                return PartialView("PHIEUNHAP_pv", p);
            }
        }
        public ActionResult TIMKIEMTHEONGAYPHIEUNHAP(DateTime a)
        {
            List<PHIEUNHAP> p = dl.PHIEUNHAPs.ToList();

            p = dl.PHIEUNHAPs.Where(t => t.NGAYNHAP == a).ToList();
            return PartialView("PHIEUNHAP_pv", p);

        }

        //ncc ================================================
        public ActionResult NHACUNGCAP()
        {
            return View();
        }
        public ActionResult NHACUNGCAP_pv()
        {
            List<NHACUNGCAP> p = dl.NHACUNGCAPs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMTENNCC(string a)
        {
            List<NHACUNGCAP> KH = dl.NHACUNGCAPs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                KH = dl.NHACUNGCAPs.Where(t => t.TENNCC.Contains(a.ToUpper())).ToList();
            }
            return PartialView("NHACUNGCAP_pv", KH); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult TIMKIEMTHEOMANHACUNGCAP(string a)
        {
            List<NHACUNGCAP> p = dl.NHACUNGCAPs.ToList();
            if (a.Length == 0)
            {
                p = dl.NHACUNGCAPs.ToList();
                return PartialView("NHACUNGCAP_pv", p);
            }
            else
            {
                p = dl.NHACUNGCAPs.Where(t => t.MANCC.ToString() == a.Trim()).ToList();
                return PartialView("NHACUNGCAP_pv", p);
            }
        }
        public ActionResult THEMNCC()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMNCC(FormCollection e )
        {
            
            string ten = e["ten_ncc"];
            string diachi = e["diachi_ncc"];
            if (string.IsNullOrEmpty(ten))
            {
                ModelState.AddModelError("ten_ncc", "Không để trống tên");
                return View();
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ModelState.AddModelError("diachi_ncc", "Không để trống địa chỉ");
                return View();
            }
            try
            {
                NHACUNGCAP p = new NHACUNGCAP();
                p.TENNCC = ten;
                p.DIACHI = diachi;
                dl.NHACUNGCAPs.InsertOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("ten_ncc", "Lỗi thêm vào");
                return View();
            }
            return RedirectToAction("NHACUNGCAP");
        }
        public ActionResult SUACUNGCAP(int id)
        {
            NHACUNGCAP p = dl.NHACUNGCAPs.FirstOrDefault(t => t.MANCC == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult SUACUNGCAP(FormCollection e)
        {
            int id = int.Parse(e["ma_ncc"]);
            string ten = e["ten_ncc"];
            string diachi = e["diachi_ncc"];
            if (string.IsNullOrEmpty(ten))
            {
                ModelState.AddModelError("ten_ncc", "Không để trống tên");
                 NHACUNGCAP p = dl.NHACUNGCAPs.FirstOrDefault(t => t.MANCC == id); 
                return View("SUACUNGCAP",p);
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ModelState.AddModelError("diachi_ncc", "Không để trống địa chỉ");
                NHACUNGCAP p = dl.NHACUNGCAPs.FirstOrDefault(t => t.MANCC == id);
                return View("SUACUNGCAP", p);
            }
            try
            {
                NHACUNGCAP p = dl.NHACUNGCAPs.FirstOrDefault(t => t.MANCC == id); 
                p.TENNCC = ten;
                p.DIACHI = diachi;
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("ten_ncc", "Lỗi sửa");
                return View("SUACUNGCAP", new { id });
            }
            return RedirectToAction("NHACUNGCAP");
        }
        public ActionResult XOANHACUNGCAP(int id)
        {
            try
            {
                NHACUNGCAP p = dl.NHACUNGCAPs.FirstOrDefault(t => t.MANCC == id);
                dl.NHACUNGCAPs.DeleteOnSubmit(p);
                dl.SubmitChanges();
               
            }
            catch
            {

                ModelState.AddModelError("baoloi", "Không thể xoá vì tồn tại trong phiếu nhập");
                return RedirectToAction("NHACUNGCAP");
            }
            return RedirectToAction("NHACUNGCAP");
            
        }
        // khuyến mai======================================================================
        public ActionResult KHUYENMAI()
        {
            return View();
        }
        public ActionResult KHUYENMAI_pv()
        {
            List<KHUYENMAI> p = dl.KHUYENMAIs.ToList();
            return PartialView(p);
        }
        public ActionResult THEMKHUYENMAI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMKHUYENMAI(FormCollection e)
        {
            // Lấy dữ liệu từ form
            string maKM = e["makm"];
            string tenKM = e["tenkm"];
            DateTime ngayBD;
            DateTime ngayKT;
            bool isNgayBDValid = DateTime.TryParse(e["ngaybd"], out ngayBD);
            bool isNgayKTValid = DateTime.TryParse(e["ngaykt"], out ngayKT);

            // Kiểm tra dữ liệu
            if (string.IsNullOrEmpty(maKM))
            {
                ModelState.AddModelError("makm", "Mã khuyến mãi là bắt buộc");
                return View();
            }

            if (string.IsNullOrEmpty(tenKM))
            {
                ModelState.AddModelError("tenkm", "Tên khuyến mãi là bắt buộc");
                return View();
            }

            if (!isNgayBDValid)
            {
                ModelState.AddModelError("ngaybd", "Ngày bắt đầu không hợp lệ");
                return View();
            }

            if (!isNgayKTValid)
            {
                ModelState.AddModelError("ngaykt", "Ngày kết thúc không hợp lệ");
                return View();
            }
            if (ngayBD > ngayKT)
            {
                ModelState.AddModelError("ngaybd", "Ngày bắt đầu khôbg được nhỏ hơn ngày kết thúc");
                return View();
            }
            try
            {
                KHUYENMAI p = new KHUYENMAI();
                p.MAKM = maKM;
                p.TENKHUYENMAI = tenKM;
                p.NGAYBATDAU = ngayBD;
                p.NGAYKETTHUC = ngayKT;
                dl.KHUYENMAIs.InsertOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("makm", "Mã khuyễn mãi đá tồn tại");
                return View();
            }
            return RedirectToAction("KHUYENMAI");
        }
        public ActionResult SUAKM(string id)
        {
            KHUYENMAI p = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult SUAKM(FormCollection e)
        {
            string maKM = e["makm"];
            string tenKM = e["tenkm"];
            DateTime ngayBD;
            DateTime ngayKT;
            bool isNgayBDValid = DateTime.TryParse(e["ngaybd"], out ngayBD);
            bool isNgayKTValid = DateTime.TryParse(e["ngaykt"], out ngayKT);

            if (string.IsNullOrEmpty(maKM))
            {
                ModelState.AddModelError("makm", "Mã khuyến mãi là bắt buộc");
                return View();
            }

            if (string.IsNullOrEmpty(tenKM))
            {
                ModelState.AddModelError("tenkm", "Tên khuyến mãi là bắt buộc");
                return View();
            }

            if (!isNgayBDValid)
            {
                ModelState.AddModelError("ngaybd", "Ngày bắt đầu không hợp lệ");
                return View();
            }

            if (!isNgayKTValid)
            {
                ModelState.AddModelError("ngaykt", "Ngày kết thúc không hợp lệ");
                return View();
            }
            if (ngayBD > ngayKT)
            {
                ModelState.AddModelError("ngaybd", "Ngày bắt đầu khôbg được nhỏ hơn ngày kết thúc");
                return View();
            }
            try
            {
                KHUYENMAI p = dl.KHUYENMAIs.FirstOrDefault(t=>t.MAKM==maKM);
                p.TENKHUYENMAI = tenKM;
                p.NGAYBATDAU = ngayBD;
                p.NGAYKETTHUC = ngayKT;
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("makm", "Không thể sửa");
                return View();
            }
            return RedirectToAction("KHUYENMAI");
        }
        public ActionResult XOAKM(string id)
        {
            try
            {
                KHUYENMAI p = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
                dl.KHUYENMAIs.DeleteOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("makm", "Không thể sửa");
                return RedirectToAction("KHUYENMAI");
            }
            return RedirectToAction("KHUYENMAI");
        }
        public ActionResult TIMKIEMNGAYBATDAU(DateTime a, DateTime b)
        {
            List<KHUYENMAI> KHUYENMAI = dl.KHUYENMAIs.ToList();
            KHUYENMAI = dl.KHUYENMAIs.Where(t => t.NGAYBATDAU >= a && t.NGAYKETTHUC <= b).ToList();
            return PartialView("KHUYENMAI_pv", KHUYENMAI); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult DANHSACHCHITIETKHUYENMAI(string id)
        {
            List<CHITIETKHUYENMAI> p = dl.CHITIETKHUYENMAIs.Where(t => t.MAKM == id).ToList();
            ViewBag.makhuyenmai = id;
            return View(p);
        }
        public ActionResult THEMCTKM(string id)
        {
            KHUYENMAI p = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
            List<SANPHAM> k = dl.SANPHAMs.ToList();
            Session["sanpham_km"] = k;
            return View(p);
        }

        [HttpPost]
        public ActionResult THEMCTKM(FormCollection e)
        {
            string id = e["ma_km"].Trim();
            int masp = int.Parse(e["masp_km"]);
            int soluong = int.Parse(e["tilekm"]);

            if (masp == 0)
            {
                ModelState.AddModelError("masp_km", "* Vui lòng chọn sản phẩm .");
                KHUYENMAI i = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
                return View("THEMCTKM", i);
            }

            // Kiểm tra xem sản phẩm này có nằm trong khuyến mãi nào khác không
            var existingPromotion = (from km in dl.KHUYENMAIs
                                     join ctkm in dl.CHITIETKHUYENMAIs on km.MAKM equals ctkm.MAKM
                                     where ctkm.MASP == masp && km.NGAYKETTHUC >= DateTime.Now
                                     select km).FirstOrDefault();

            if (existingPromotion != null)
            {
                // Nếu sản phẩm đang nằm trong một khuyến mãi khác chưa hết hạn
                ModelState.AddModelError("masp_km", "* Sản phẩm này đang nằm trong một khuyến mãi khác chưa hết hạn.");
                KHUYENMAI i = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
                return View("THEMCTKM", i);
            }

            // Thêm chi tiết khuyến mãi mới
            try
            {
                CHITIETKHUYENMAI p = new CHITIETKHUYENMAI();
                p.MAKM = id.TrimEnd();
                p.MASP = masp;
                p.TILEGIAM = soluong;
                dl.CHITIETKHUYENMAIs.InsertOnSubmit(p);
                dl.SubmitChanges();
            }
            catch
            {
                ModelState.AddModelError("ma_km", "* Đã xảy ra lỗi khi thêm chi tiết khuyến mãi.");
                KHUYENMAI i = dl.KHUYENMAIs.FirstOrDefault(t => t.MAKM == id);
                return View("THEMCTKM", i);
            }
            return RedirectToAction("DANHSACHCHITIETKHUYENMAI", new { id });
        }

        public ActionResult XOA_CTKM( string id , int masp)
        {
            CHITIETKHUYENMAI p = dl.CHITIETKHUYENMAIs.FirstOrDefault(t => t.MAKM == id && t.MASP == masp);
            if (p != null)
            {
                dl.CHITIETKHUYENMAIs.DeleteOnSubmit(p);
                dl.SubmitChanges();
                return RedirectToAction("DANHSACHCHITIETKHUYENMAI", new { id }); 
            }
            return RedirectToAction("DANHSACHCHITIETKHUYENMAI", new { id }); 
        }
        public ActionResult TIMKIEMTHEOMAKHUYENMAI(string a)
        {
            List<KHUYENMAI> p = dl.KHUYENMAIs.ToList();
            if (a.Length == 0)
            {
                p = dl.KHUYENMAIs.ToList();
                return PartialView("KHUYENMAI_pv", p);
            }
            else
            {
                p = dl.KHUYENMAIs.Where(t => t.MAKM.ToString() == a.Trim()).ToList();
                return PartialView("KHUYENMAI_pv", p);
            }
        }

        //Giao diện ========================================================
        public ActionResult GIAODIEN()
        {
            return View();
        }
        public ActionResult GIAODIEN_PV()
        {
            List<GIAODIENTRANGCHINH> p = dl.GIAODIENTRANGCHINHs.ToList();
            return PartialView(p);
        }
        // Hình ảnh =======================================================
        public ActionResult HINHANH()
        {
            return View();
        }
        public ActionResult HINHANH_PV()
        {
            List<HINHANH> p = dl.HINHANHs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMTENHINHANH(string a)
        {
            List<HINHANH> P = dl.HINHANHs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                P = dl.HINHANHs.Where(t => t.TENHINHANH.Contains(a.ToUpper())).ToList();
            }
            return PartialView("HINHANH_PV", P); // Trả về PartialView với danh sách sản phẩm đã lọc
        }
        public ActionResult TIMKIEMTHEOMAHINHANH(string a)
        {
            List<HINHANH> p = dl.HINHANHs.ToList();
            if (a.Length == 0)
            {
                p = dl.HINHANHs.ToList();
                return PartialView("HINHANH_PV", p);
            }
            else
            {
                p = dl.HINHANHs.Where(t => t.MAHA.ToString() == a.Trim()).ToList();
                return PartialView("HINHANH_PV", p);
            }
        }
        public ActionResult CHITIET_HINHANH(string id)
        {
            HINHANH p = dl.HINHANHs.FirstOrDefault(t => t.MAHA == id.Trim());
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        public ActionResult SUA_CHITIET_HINHANH(FormCollection a, List<HttpPostedFileBase> hinhanh)
        {
            string id = a["id"].Trim();

            HINHANH p = dl.HINHANHs.FirstOrDefault(t => t.MAHA == id.Trim());
            if (p == null)
            {
                return HttpNotFound();
            }
            string imageDirectory = Server.MapPath("~/Content/hinhanh/hinhanhsanpham/");
            string duongdan = "";
            // Lấy danh sách tên hình từ form
            List<string> ten = a.GetValues("tenha").ToList();
            foreach (string k in ten)
            {
                duongdan += k + ',';
            }

            foreach (HttpPostedFileBase k in hinhanh)
            {
                try
                {
                    if (k != null && k.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(k.FileName);
                        string filePath = Path.Combine(imageDirectory, fileName);
                        k.SaveAs(filePath);
                    }
                }
                catch
                {

                }
            }
            p.DUONGDAN = duongdan.TrimEnd(',');
            dl.SubmitChanges();
            return RedirectToAction("CHITIET_HINHANH", new { id });
        }
        public ActionResult XOA_CHITIETHINHANH(string id)
        {
            HINHANH p = dl.HINHANHs.FirstOrDefault(t => t.MAHA == id.Trim());
            try
            {
                if (p != null)
                {
                    dl.HINHANHs.DeleteOnSubmit(p);
                    dl.SubmitChanges();
                }
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* Không thể xoá vì đã tồn tại trong sản phẩm");
                return View("HINHANH");
            }
            return RedirectToAction("HINHANH");
        }
        public ActionResult THEMHINHANH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMHINHANH(FormCollection e, List<HttpPostedFileBase> hinhanh)
        {
            string ma = e["mahinhanh"];
            string ten = e["tenhinhanh"];
            if (ma.Length == 0 || System.Text.RegularExpressions.Regex.IsMatch(ma, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                ModelState.AddModelError("mahinhanh", "* không được bỏ trống hay có kí tự tiếng việt");
                return View("THEMHINHANH");
            }
            if (ma.Length == 0)
            {
                ModelState.AddModelError("tenhinhanh", "* không được bỏ trống");
                return View("THEMHINHANH");
            }
            string imageDirectory = Server.MapPath("~/Content/hinhanh/hinhanhsanpham/");
            string duongdan = "";
            foreach (HttpPostedFileBase k in hinhanh)
            {
                try
                {
                    if (k != null && k.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(k.FileName);
                        string filePath = Path.Combine(imageDirectory, fileName);
                        k.SaveAs(filePath);
                        duongdan += k.FileName + ',';
                    }
                }
                catch
                {
                    duongdan += k.FileName + ',';
                }
            }
            HINHANH kt = dl.HINHANHs.FirstOrDefault(t => t.MAHA == ma);
            if (kt == null)
            {
                duongdan = duongdan.TrimEnd(',');
                HINHANH p = new HINHANH();
                p.TENHINHANH = ten;
                p.MAHA = ma.Trim();
                p.DUONGDAN = duongdan;
                dl.HINHANHs.InsertOnSubmit(p);
                dl.SubmitChanges();
                return RedirectToAction("HINHANH");
            }
            else
            {
                ModelState.AddModelError("baoloi", "* không được bỏ trống.");
                return View("HINHANH");
            }
        }

        // kích cở =======================================================
        public ActionResult KICHCO()
        {
            return View();
        }
        public ActionResult KICHCO_PV()
        {
            List<KICHCO> p = dl.KICHCOs.ToList();
            return PartialView(p);
        }

        public ActionResult TIMKIEMTENKICHCO(string a)
        {
            List<KICHCO> P = dl.KICHCOs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                P = dl.KICHCOs.Where(t => t.KICHCO1.ToString().Contains(a)).ToList();
            }
            return PartialView("KICHCO_PV", P);
        }
        public ActionResult TIMKIEMTHEOMAKICHCO(string a)
        {
            List<KICHCO> p = dl.KICHCOs.ToList();
            if (a.Length == 0)
            {
                p = dl.KICHCOs.ToList();
                return PartialView("KICHCO_PV", p);
            }
            else
            {
                p = dl.KICHCOs.Where(t => t.MAKC.ToString() == a.Trim()).ToList();
                return PartialView("KICHCO_PV", p);
            }
        }
        public ActionResult THEMKICHCO()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMKICHCO(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            int ten = int.Parse(e["ten_kc"]);
            if (ma.Length == 0 || ma == null || ma.Length > 10 || System.Text.RegularExpressions.Regex.IsMatch(ma, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                ModelState.AddModelError("ma_kc", "* không được bỏ trống hay vượt quá 10 kí tự , hoặc có kí tự tiếng việt");
                return View("THEMKICHCO");
            }
            KICHCO kt = dl.KICHCOs.FirstOrDefault(t => t.MAKC == ma.Trim());
            if (kt == null)
            {
                KICHCO kc = new KICHCO();
                kc.MAKC = ma;
                kc.KICHCO1 = ten;
                dl.KICHCOs.InsertOnSubmit(kc);
                dl.SubmitChanges();
                return RedirectToAction("KICHCO");
            }
            else
            {
                ModelState.AddModelError("ma_kc", "* Đã tồn tại mã này");
                return View("THEMKICHCO");
            }

        }
        public ActionResult SUAKICHCO(string id)
        {
            KICHCO k = dl.KICHCOs.FirstOrDefault(t => t.MAKC == id);
            return View(k);
        }
        [HttpPost]
        public ActionResult SUAKICHCO(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            int ten = int.Parse(e["ten_kc"]);
            KICHCO kt = dl.KICHCOs.FirstOrDefault(t => t.MAKC == ma.Trim());
            kt.KICHCO1 = ten;
            dl.SubmitChanges();
            return View("KICHCO");
        }
        public ActionResult XOAKICHCO(string id)
        {
            try
            {
                KICHCO kt = dl.KICHCOs.FirstOrDefault(t => t.MAKC == id.Trim());
                dl.KICHCOs.DeleteOnSubmit(kt);
                dl.SubmitChanges();
                return RedirectToAction("KICHCO");
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* không thể xoá vì đã tồn tại trong sản phẩm");
                return View("KICHCO");
            }
        }
        // Màu sắc =======================================================
        public ActionResult MAUSAC()
        {
            return View();
        }
        public ActionResult MAUSAC_PV()
        {
            List<MAUSAC> p = dl.MAUSACs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMTENMAUSAC(string a)
        {
            List<MAUSAC> P = dl.MAUSACs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                P = dl.MAUSACs.Where(t => t.TENMAUSAC.ToString().Contains(a)).ToList();
            }
            return PartialView("MAUSAC_PV", P);
        }
        public ActionResult TIMKIEMTHEOMAMAUSAC(string a)
        {
            List<MAUSAC> p = dl.MAUSACs.ToList();
            if (a.Length == 0)
            {
                p = dl.MAUSACs.ToList();
                return PartialView("MAUSAC_PV", p);
            }
            else
            {
                p = dl.MAUSACs.Where(t => t.MAMS.ToString() == a.Trim()).ToList();
                return PartialView("MAUSAC_PV", p);
            }
        }

        public ActionResult THEMMAUSAC()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMMAUSAC(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            string ten = e["ten_kc"];
            if (ma.Length == 0 || ma == null || ma.Length > 10 || System.Text.RegularExpressions.Regex.IsMatch(ma, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                ModelState.AddModelError("ma_kc", "* không được bỏ trống hay vượt quá 10 kí tự , hoặc có kí tự tiếng việt");
                return View("THEMMAUSAC");
            }
            if (ten.Length == 0 || ten == null)
            {
                ModelState.AddModelError("ten_kc", "* Không được bỏ trống");
                return View("THEMMAUSAC");
            }
            MAUSAC kt = dl.MAUSACs.FirstOrDefault(t => t.MAMS == ma.Trim());
            if (kt == null)
            {
                MAUSAC kc = new MAUSAC();
                kc.MAMS = ma;
                kc.TENMAUSAC = ten;
                dl.MAUSACs.InsertOnSubmit(kc);
                dl.SubmitChanges();
                return RedirectToAction("MAUSAC");
            }
            else
            {
                ModelState.AddModelError("ma_kc", "* Đã tồn tại mã này");
                return View("THEMMAUSAC");
            }

        }
        public ActionResult SUAMAUSAC(string id)
        {
            MAUSAC k = dl.MAUSACs.FirstOrDefault(t => t.MAMS == id);
            return View(k);
        }
        [HttpPost]
        public ActionResult SUAMAUSAC(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            string ten = e["ten_kc"];
            if (ten.Length == 0 || ten == null)
            {
                ModelState.AddModelError("ten_kc", "* Không được bỏ trống");
                string id = ma;
                return View("SUAMAUSAC", new { id });
            }
            MAUSAC kt = dl.MAUSACs.FirstOrDefault(t => t.MAMS == ma.Trim());
            kt.TENMAUSAC = ten;
            dl.SubmitChanges();
            return View("MAUSAC");
        }
        public ActionResult XOAMAUSAC(string id)
        {
            try
            {
                MAUSAC kt = dl.MAUSACs.FirstOrDefault(t => t.MAMS == id.Trim());
                dl.MAUSACs.DeleteOnSubmit(kt);
                dl.SubmitChanges();
                return RedirectToAction("MAUSAC");
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* không thể xoá vì đã tồn tại trong sản phẩm");
                return View("MAUSAC");
            }
        }

        // Nhãn Hàng =======================================================
        public ActionResult NHANHANG()
        {
            return View();
        }
        public ActionResult NHANHANG_PV()
        {
            List<NHANHANG> p = dl.NHANHANGs.ToList();
            return PartialView(p);
        }
        public ActionResult TIMKIEMTENNH(string a)
        {
            List<NHANHANG> P = dl.NHANHANGs.ToList();
            if (!string.IsNullOrEmpty(a))
            {
                P = dl.NHANHANGs.Where(t => t.TENNH.ToString().Contains(a)).ToList();
            }
            return PartialView("NHANHANG_PV", P);
        }
        public ActionResult TIMKIEMTHEOMANHANHANG(string a)
        {
            List<NHANHANG> p = dl.NHANHANGs.ToList();
            if (a.Length == 0)
            {
                p = dl.NHANHANGs.ToList();
                return PartialView("NHANHANG_PV", p);
            }
            else
            {
                p = dl.NHANHANGs.Where(t => t.MANH.ToString() == a.Trim()).ToList();
                return PartialView("NHANHANG_PV", p);
            }
        }

        public ActionResult THEMNHANHANG()
        {
            return View();
        }
        [HttpPost]
        public ActionResult THEMNHANHANG(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            string ten = e["ten_kc"];
            if (ma.Length == 0 || ma == null || ma.Length > 10 || System.Text.RegularExpressions.Regex.IsMatch(ma, "[\u00C0-\u017F\u1E00-\u1EFF]"))
            {
                ModelState.AddModelError("ma_kc", "* không được bỏ trống hay vượt quá 10 kí tự , hoặc có kí tự tiếng việt");
                return View("THEMNHANHANG");
            }
            if (ten.Length == 0 || ten == null)
            {
                ModelState.AddModelError("ten_kc", "* Không được bỏ trống");
                return View("THEMNHANHANG");
            }
            NHANHANG kt = dl.NHANHANGs.FirstOrDefault(t => t.MANH == ma.Trim());
            if (kt == null)
            {
                NHANHANG kc = new NHANHANG();
                kc.MANH = ma;
                kc.TENNH = ten;
                dl.NHANHANGs.InsertOnSubmit(kc);
                dl.SubmitChanges();
                return RedirectToAction("NHANHANG");
            }
            else
            {
                ModelState.AddModelError("ma_kc", "* Đã tồn tại mã này");
                return View("THEMNHANHANG");
            }

        }
        public ActionResult SUANHANHANG(string id)
        {
            NHANHANG k = dl.NHANHANGs.FirstOrDefault(t => t.MANH == id);
            return View(k);
        }
        [HttpPost]
        public ActionResult SUANHANHANG(FormCollection e)
        {
            string ma = e["ma_kc"].Trim();
            string ten = e["ten_kc"];
            if (ten.Length == 0 || ten == null)
            {
                ModelState.AddModelError("ten_kc", "* Không được bỏ trống");
                string id = ma;
                return View("SUANHANHANG", new { id });
            }
            NHANHANG kt = dl.NHANHANGs.FirstOrDefault(t => t.MANH == ma.Trim());
            kt.TENNH = ten;
            dl.SubmitChanges();
            return View("NHANHANG");
        }
        public ActionResult XOANHANHANG(string id)
        {
            try
            {
                NHANHANG kt = dl.NHANHANGs.FirstOrDefault(t => t.MANH == id.Trim());
                dl.NHANHANGs.DeleteOnSubmit(kt);
                dl.SubmitChanges();
                return RedirectToAction("NHANHANG");
            }
            catch
            {
                ModelState.AddModelError("baoloi", "* không thể xoá vì đã tồn tại trong sản phẩm");
                return View("NHANHANG");
            }
        }
        public ActionResult admin()
        {
            List<DONHANG> p = dl.DONHANGs.ToList();

            // Lấy doanh thu theo tháng từ dữ liệu
            var doanhThuThang = p
                .GroupBy(d => d.NGAYDAT.HasValue ? d.NGAYDAT.Value.Month : 0) // Nhóm theo tháng
                .Select(g => new { Month = g.Key, Total = g.Sum(d => d.TONGTIEN) })
                .ToList();

            // Chuyển đổi thành JSON để sử dụng trong View
            ViewBag.DoanhThuThang = JsonConvert.SerializeObject(doanhThuThang);

            return View(p);
        }
        
    }
}
