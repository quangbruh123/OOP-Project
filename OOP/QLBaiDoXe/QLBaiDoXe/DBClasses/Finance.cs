using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBaiDoXe.Properties;

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

        public static void UpdateFinancialReport()
        {
            if (DateTime.Now.Month == Settings.Default.currentDate.Month && DateTime.Now.Year == Settings.Default.currentDate.Year)
                return;
            else
            {
                if (DataProvider.Ins.DB.FinancialReports.Last().FinancialReportMonth.Month != Settings.Default.currentDate.Month)
                {
                    int income = 0;
                    int expenditure = 0;
                    List<Receipt> receipts = DataProvider.Ins.DB.Receipts.Where(x => x.TimePaid.Month == Settings.Default.currentDate.AddMonths(-1).Month).ToList();
                    List<Staff> staffs = DataProvider.Ins.DB.Staffs.ToList();
                    foreach (Receipt receipt in receipts)
                    {
                        income += receipt.ParkingFee;
                    }
                    foreach (Staff staff in staffs)
                    {
                        expenditure += staff.Wage;
                    }
                    FinancialReport financialReport = new FinancialReport()
                    {
                        FinancialReportMonth = DateTime.Now.AddMonths(-1),
                        Income = income,
                        Expenditure = expenditure
                    };
                    DataProvider.Ins.DB.FinancialReports.Add(financialReport);
                    DataProvider.Ins.DB.SaveChanges();
                }
                else
                    return;
            }
        }

        public static FinancialReport GetFinancialReportForDate(int month, int year)
        {
            return DataProvider.Ins.DB.FinancialReports.FirstOrDefault(x => x.FinancialReportMonth.Date.Month == month && x.FinancialReportMonth.Year == year);
        }

        public static List<FinancialReport> GetAllFinancialReports()
        {
            return DataProvider.Ins.DB.FinancialReports.ToList();
        }
    }
}
