//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GarageManager.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEUTHUTIEN
    {
        public int MaPTT { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string BienSo { get; set; }
        public Nullable<long> SoTienThu { get; set; }
    
        public virtual XE XE { get; set; }
    }
}
