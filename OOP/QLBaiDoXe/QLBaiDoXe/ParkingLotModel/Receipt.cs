//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBaiDoXe.ParkingLotModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipt
    {
        public int ReceiptID { get; set; }
        public int VehicleID { get; set; }
        public int StaffID { get; set; }
        public System.DateTime TimePaid { get; set; }
        public int ParkingFee { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
