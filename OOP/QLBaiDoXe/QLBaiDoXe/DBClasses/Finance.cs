using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiDoXe.DBClasses
{
    public class Finance
    {
        public static void AddReceipt(int cardId, int staffId)
        {
            Vehicle vehicle = DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == cardId);
            Receipt receipt = new Receipt()
            {
                VehicleID = vehicle.VehicleID,
                StaffID = staffId,
                TimePaid = DateTime.Now,
                ParkingFee = vehicle.VehicleType.ParkingFee,
                Vehicle = vehicle,
                Staff = DataProvider.Ins.DB.Staffs.FirstOrDefault(x => x.StaffID == staffId)
            };
            DataProvider.Ins.DB.Receipts.Add(receipt);
            DataProvider.Ins.DB.SaveChanges();
        }

        public static List<Receipt> GetAllReceipt()
        {
            return DataProvider.Ins.DB.Receipts.ToList();
        }

        public static List<Receipt> GetAllReceipt_Date(DateTime date)
        {
            return DataProvider.Ins.DB.Receipts.Where(x => x.TimePaid.Date == date.Date).ToList();
        }
    }
}
