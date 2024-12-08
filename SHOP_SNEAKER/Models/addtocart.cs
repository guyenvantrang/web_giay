using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SHOP_SNEAKER.Models;

namespace SHOP_SNEAKER.Models
{
    public class GIOHANG
    {
        dulieuDataContext dl = new dulieuDataContext();
        public int masp { get; set; }
        public string tensp { get; set; }
        public int soluong { get; set; }
        public string mausac { get; set; }
        public string hinhanh { get; set; }
        public string kichco { get; set; }
        public int dongia { get; set; }
        public int thanhtien { get { return dongia * soluong; } }
        public GIOHANG(int? id, string mams, string makc, int soluong)
        {
            CHITIETSANPHAM p = dl.CHITIETSANPHAMs.FirstOrDefault(t => t.MASP == id && t.MAKC == makc.Trim() && t.MAMS == mams.Trim());
            Debug.WriteLine("===============>masp{0}", p.MASP);
            SANPHAM k = dl.SANPHAMs.FirstOrDefault(t => t.MASP == p.MASP);
            if (p != null && k != null)
            {
                masp = p.MASP;
                this.soluong = soluong;
                mausac = p.MAMS;
                kichco = p.MAKC;
                dongia = int.Parse(k.GIACHINHTHUCSP.ToString());
                hinhanh = k.MAHA;
            }
            else
            {
                throw new ArgumentException("Sản phẩm không tồn tại hoặc không đủ số lượng");
            }
        }

    }
    public class THEMGIAY
    {
        dulieuDataContext dl = new dulieuDataContext();
       public List<GIOHANG> lst_gh = new List<GIOHANG>();
        public int soluongsanpham()
        {
            return lst_gh.Count();
        }
        public int tongtien { get { return lst_gh.Sum(t => t.thanhtien); } }
        public bool xulisolieu(int? id, string mams, string makc,int soluong)
        {
            foreach (GIOHANG a in lst_gh)
            {
                Debug.WriteLine("=----->{0},{1},{2}", a.masp,a.mausac.Trim(),a.kichco.Trim());
            }
            GIOHANG p = lst_gh.FirstOrDefault(t => t.masp == id && t.mausac.Trim() == mams.Trim() && t.kichco.Trim() == makc.Trim());           
            if (p == null)
            {
                GIOHANG k = new GIOHANG(id, mams, makc,soluong);
                if (k != null)
                {
                    lst_gh.Add(k);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                p.soluong += soluong;
            }
            return true;
        }
    }
}