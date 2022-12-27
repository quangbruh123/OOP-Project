using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiDoXe.DBClasses
{
    public class Cards
    {
        public static bool AddCard(long cardId)
        {
            if (!DataProvider.Ins.DB.ParkingCards.Any(x => x.ParkingCardID == cardId))
            {
                ParkingCard card = new ParkingCard()
                {
                    ParkingCardID = cardId,
                    CardState = 0
                };
                DataProvider.Ins.DB.ParkingCards.Add(card);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool DeleteCard(long cardId)
        {
            if (!DataProvider.Ins.DB.ParkingCards.Any(x => x.ParkingCardID == cardId))
            {
                DataProvider.Ins.DB.ParkingCards.Remove(DataProvider.Ins.DB.ParkingCards.FirstOrDefault(x => x.ParkingCardID == cardId));
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
