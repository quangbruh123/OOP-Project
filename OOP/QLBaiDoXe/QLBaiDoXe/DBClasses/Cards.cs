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

        public static List<ParkingCard> GetAllParkingCards()
        {
            return DataProvider.Ins.DB.ParkingCards.ToList();
        }

        /// <summary>
        /// Check parking card state
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns>
        /// 1 if the card is being used, 0 if the card is usused
        /// </returns>
        public static int CheckCardState(long cardId)
        {
            return DataProvider.Ins.DB.ParkingCards.FirstOrDefault(x => x.ParkingCardID == cardId).CardState;
        }

        public static List<ParkingCard> GetCardsFromId(long cardId)
        {
            return DataProvider.Ins.DB.ParkingCards.Where(x => x.ParkingCardID.ToString().Contains(cardId.ToString())).ToList();
        }
    }
}
