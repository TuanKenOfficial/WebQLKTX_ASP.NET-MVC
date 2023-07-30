﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QLKyTucXa.Areas.CanBo.Models
{
    public class ViewModel_HDĐN_HDP
    {
        public int ID_PHONG { get; set; }
        public int ID_HOADONDIENNUOC { get; set; }
        public int ID_HOADONPHONG { get; set; }
        public int ID_DIEN { get; set; }
        public int ID_NUOC { get; set; }
        // Not used
        public string MAPHONG { get; set; }
        public string MADAYPHONG { get; set; }

        // In used
        public int? THANGDIEN { get; set; }
        public int? THANG { get; set; }
        public int? THANGNUOC { get; set; }
        public int? NAM { get; set; }
        public int? NAMDIEN { get; set; }
        public int? NAMNUOC { get; set; }

        public double DONGIA_DIEN { get; set; }
        public double DONGIA_NUOC { get; set; }
       
        public int? NUOCCHISODAU { get; set; }
        public int? NUOCCHISOCUOI { get; set; }
        public int? DIENCHISODAU { get; set; }
        public int? DIENCHISOCUOI { get; set; }

        public int? CHISODIEN { get; set; }
        public int? CHISONUOC { get; set; }
        public double? THANHTIEN { get; set; }
        public double? THANHTIEN_DIEN { get; set; }
        public double? THANHTIEN_NUOC { get; set; }
        public int? KY { get; set; }
        public double? DONGIA { get; set; }
        public double? DONGIADIEN { get; set; }
        public double? DONGIANUOC { get; set; }
        public int? TRANGTHAI { get; set; }
        public int? TRANGTHAIDIEN { get; set; }
        public int? TRANGTHAINUOC { get; set; }
    }
}