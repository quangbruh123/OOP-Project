using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBaiDoXe.Properties;
using System.Diagnostics;

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
            return DataProvider.Ins.DB.Receipts.Where(x => x.TimePaid.Day == date.Day && x.TimePaid.Month == date.Month && x.TimePaid.Year == date.Year).ToList();
        }

        public static void UpdateFinancialReport()
        {           
            int income = 0;
            DateTime yesterday = new DateTime();
            try
            {
                yesterday = Settings.Default.currentDate.AddDays(-1).Date;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return;
            }
            List<Receipt> receipts = DataProvider.Ins.DB.Receipts.Where(x => x.TimePaid.Day == yesterday.Day && x.TimePaid.Month == yesterday.Month && x.TimePaid.Year == yesterday.Year).ToList();
            foreach (Receipt receipt in receipts)
            {
                income += (int)receipt.ParkingFee;
            }
            FinancialReport financialReport = new FinancialReport()
            {
                FinancialReportDate = DateTime.Now.AddDays(-1),
                Income = income
            };
            DataProvider.Ins.DB.FinancialReports.Add(financialReport);
            DataProvider.Ins.DB.SaveChanges();
        }

        public static FinancialReport GetFinancialReportForDate(int month, int year)
        {
            return DataProvider.Ins.DB.FinancialReports.FirstOrDefault(x => x.FinancialReportDate.Date.Month == month && x.FinancialReportDate.Year == year);
        }

        public static List<FinancialReport> GetAllFinancialReports()
        {
            return DataProvider.Ins.DB.FinancialReports.ToList();
        }
    }
}
