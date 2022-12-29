using QLBaiDoXe.ParkingLotModel;
using QLBaiDoXe.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLBaiDoXe.DBClasses
{
    public class ParkingVehicle
    {
        public static void VehicleIn(string vehicleType, long cardId, string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                int vehicleTypeId = DataProvider.Ins.DB.VehicleTypes.FirstOrDefault(x => x.VehicleTypeName == vehicleType).VehicleTypeID;
                Vehicle vehicle = new Vehicle()
                {
                    VehicleID = vehicleTypeId,
                    ParkingCardID = cardId,
                    VehicleImage = imagePath,
                    VehicleState = 1,
                    TimeStartedParking = DateTime.Now,
                    ParkingCard = DataProvider.Ins.DB.ParkingCards.FirstOrDefault(x => x.ParkingCardID == cardId)
                };
                ParkingCard card = DataProvider.Ins.DB.ParkingCards.FirstOrDefault(x => x.ParkingCardID == cardId);
                card.CardState = 1;
                DataProvider.Ins.DB.Vehicles.Add(vehicle);
                DataProvider.Ins.DB.SaveChanges();
                Settings.Default.todayVehicleIn++;
            }
        }

        public static Vehicle GetVehicleInfoFromCard(long cardId)
        {
            return DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == cardId);
        }

        public static bool VehicleOut(long cardId)
        {
            if (DataProvider.Ins.DB.Vehicles.Any(x => x.ParkingCardID == cardId && x.ParkingCard.CardState == 1))
            {
                Vehicle vehicle = DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == cardId &&
                                    x.ParkingCard.CardState == 1);
                vehicle.VehicleState = 0;
                vehicle.TimeEndedParking = DateTime.Now;
                ParkingCard card = DataProvider.Ins.DB.ParkingCards.FirstOrDefault(x => x.ParkingCardID == cardId);
                card.CardState = 0;
                DataProvider.Ins.DB.SaveChanges();
                Settings.Default.todayVehicleOut++;
                return true;
            }
            else
                return false;
        }

        public static List<Vehicle> GetAllParkedVehicle()
        {
            return DataProvider.Ins.DB.Vehicles.Where(x => x.VehicleState == 1).ToList();
        }

        public static List<Vehicle> SearchVehicle_TimeIn_DateOnly(DateTime timeIn)
        {
            return DataProvider.Ins.DB.Vehicles.Where(x => x.TimeStartedParking.Day == timeIn.Day && x.TimeStartedParking.Month == timeIn.Month && x.TimeStartedParking.Year == timeIn.Year).ToList();
        }

        public static List<Vehicle> SearchVehicle_TimeIn_DateAndHour(DateTime timeIn)
        {
            return DataProvider.Ins.DB.Vehicles.Where(x => x.TimeStartedParking.Day == timeIn.Day && x.TimeStartedParking.Month == timeIn.Month && x.TimeStartedParking.Year == timeIn.Year && x.TimeStartedParking.Hour == timeIn.Hour).ToList();
        }

        public static int GetParkedVehicleNumber()
        {
            return DataProvider.Ins.DB.Vehicles.Where(x => x.VehicleState == 1).Count();
        }
    }
}
