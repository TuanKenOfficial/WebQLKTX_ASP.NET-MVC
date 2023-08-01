﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HotroController : BaseController
    {
        // GET: CanBo/Dongia
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult DsHT(string tuKhoa, int trang)
        {
            try
            {

                var dsHT = (from dg in db.HOTROes
                            join p in db.PHONGs on dg.ID_PHONG equals p.ID_PHONG into phong
                            from ph in phong.Where(x=>x.TRANGTHAI == true && x.DAXOA == false)
                             where(ph.MAPHONG.ToLower().Contains(tuKhoa))
                             select new 
                                {
                                    ID_PHONG = ph.ID_PHONG,
                                    MAPHONG = ph.MAPHONG,
                                    ID_PHIEU = dg.ID_PHIEU,
                                    NOIDUNG = dg.NOIDUNG,
                                    TRANGTHAI = dg.TRANGTHAI,
                                    NGAYGUI = dg.NGAYGUI.Day + "/" + dg.NGAYGUI.Month + "/" + dg.NGAYGUI.Year
                             }).ToList();

                //30 dòng 40 dòng chẳng hạn ...
                var pageSize = 10;
                var soTrang = dsHT.Count() % pageSize == 0 ? dsHT.Count() / pageSize : dsHT.Count() / pageSize + 1;

                var dsdg = dsHT
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsHT = dsdg, msg = "Lấy danh sách đơn giá thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách đơn giá thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(int idP, string noidung, DateTime ngaygui, int trangthai)
        {
            try
            {
                var dg = new HOTRO();
                dg.ID_PHONG = idP;  
                dg.NOIDUNG = noidung;
                dg.TRANGTHAI = trangthai;
                dg.NGAYGUI = ngaygui;

                db.HOTROes.Add(dg);//them doi tuong don gia dc khai bao o phia tren
                db.SaveChanges();//luu vao csdl

                return Json(new { code = 200, msg = "Thêm hỗ trợ mới thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới hỗ trợ thất bại. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CapNhat(int id)
        {
            try
            {
                var dg = db.HOTROes.SingleOrDefault(x => x.ID_PHIEU == id);
                return Json(new { code = 200, DG = dg, msg = "Lấy thông tin cập nhật của đơn giá thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin đơn giá thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, int trangThai)
        {
            try
            {
                //tìm đơn giá dựa vào id
                var dg = db.HOTROes.SingleOrDefault(x => x.ID_PHIEU == id);

                //gán lại các thuộc tính của đơn giá đc tìm thấy
                dg.TRANGTHAI = trangThai;

                //lưu lại csdl
                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật hỗ trợ thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật hỗ trợ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //phần detail
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                var i = db.HOTROes.SingleOrDefault(x => x.ID_PHIEU == id);
                return Json(new { code = 200, I = i, msg = "Lấy thông tin chi tiết cán bộ thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy thông tin  cán bộ thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // danh sách mã phòng
        [HttpGet]
        public JsonResult ListPhong()
        {
            try
            {
                var dsp = (from p in db.PHONGs.Where(x => x.DAXOA != true)
                           select new
                           {
                               ID_PHONG = p.ID_PHONG,
                               MAPHONG = p.MAPHONG

                           }).ToList();
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}