using QLBaiDoXe.ParkingLotModel;
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
            if (DataProvider.Ins.DB.ParkingCards.Any(x => x.ParkingCardID == cardId) && !string.IsNullOrEmpty(imagePath))
            {
                int vehicleTypeId = DataProvider.Ins.DB.VehicleTypes.FirstOrDefault(x => x.VehicleTypeName == vehicleType).VehicleTypeID;
                Vehicle vehicle = new Vehicle()
                {
                    VehicleID = vehicleTypeId,
                    ParkingCardID = cardId,
                    VehicleImage = imagePath,
                    VehicleState = 1,
                    TimeStartedParking = DateTime.Now
                };
                DataProvider.Ins.DB.Vehicles.Add(vehicle);
                DataProvider.Ins.DB.SaveChanges();
            }
        }

        public static string GetVehicleImagePath(long cardId, string imagePath)
        {
            if (DataProvider.Ins.DB.Vehicles.Any(x => x.ParkingCardID == cardId) && !string.IsNullOrEmpty(imagePath))
            {
                Vehicle vehicle = DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == cardId);
                return vehicle.VehicleImage;
            }
            else
                return null;
        }

        public static bool VehicleOut(long cardId)
        {
            if (DataProvider.Ins.DB.Vehicles.Any(x => x.ParkingCardID == cardId))
            {
                Vehicle vehicle = DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == cardId);
                vehicle.VehicleState = 0;
                vehicle.TimeEndedParking = DateTime.Now;
                DataProvider.Ins.DB.SaveChanges();
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
            if (DataProvider.Ins.DB.Vehicles.Any(x => x.TimeStartedParking.Date == timeIn.Date))
            {
                return DataProvider.Ins.DB.Vehicles.Where(x => x.TimeStartedParking.Date == timeIn.Date).ToList();
            }
            else
                return null;
        }

        public static List<Vehicle> SearchVehicle_TimeIn_DateAndHour(DateTime timeIn)
        {
            if (DataProvider.Ins.DB.Vehicles.Any(x => x.TimeStartedParking.Date == timeIn.Date && x.TimeStartedParking.Hour == timeIn.Hour))
            {
                return DataProvider.Ins.DB.Vehicles.Where(x => x.TimeStartedParking.Date == timeIn.Date).ToList();
            }
            else
                return null;
        }
    }
}
