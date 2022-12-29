using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiDoXe.DBClasses
{
    public class Regulation
    {
        public static List<VehicleType> GetAllVehicleTypes()
        {
            return DataProvider.Ins.DB.VehicleTypes.ToList();
        }

        public static List<string> GetAllVehicleTypeNames()
        {
            if (DataProvider.Ins.DB.VehicleTypes.Count() > 0)
                return DataProvider.Ins.DB.VehicleTypes.Select(x => x.VehicleTypeName).ToList();
            else
                return null;
        }
        
        public static bool AddVehicleType(string vehicleTypeName, int parkingFee)
        {
            if (!DataProvider.Ins.DB.VehicleTypes.Any(x => x.VehicleTypeName == vehicleTypeName))
            {
                VehicleType vehicleType = new VehicleType()
                {
                    VehicleTypeName = vehicleTypeName,
                    ParkingFee = parkingFee,
                };
                DataProvider.Ins.DB.VehicleTypes.Add(vehicleType);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool ChangeParkingFee(string vehicleTypeName, int parkingFee)
        {
            if (!DataProvider.Ins.DB.VehicleTypes.Any(x => x.VehicleTypeName == vehicleTypeName))
            {
                VehicleType vehicleType = DataProvider.Ins.DB.VehicleTypes.FirstOrDefault(x => x.VehicleTypeName == vehicleTypeName);
                vehicleType.ParkingFee = parkingFee;
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
