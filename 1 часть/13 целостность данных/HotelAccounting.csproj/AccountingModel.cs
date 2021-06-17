using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price, discount, total;
        private int nightsCount;

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0) price = value;
                else throw new ArgumentException();
                UpdateTotal();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value > 0) nightsCount = value;
                else throw new ArgumentException();
                UpdateTotal();
                Notify(nameof(NightsCount));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                UpdateTotal();
                Notify(nameof(Discount));
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                if (value > 0) total = value;
                else throw new ArgumentException();
                UpdateDiscount();
                Notify(nameof(Total));
            }
        }

        public AccountingModel()
        {
            price = 0;
            nightsCount = 1;
            discount = 0;
            total = 0;
        }

        private void UpdateTotal()
        {
            Total = Price * NightsCount * (1 - Discount / 100);
        }

        private void UpdateDiscount()
        {
            Discount = ((-1) * Total / (Price * NightsCount) + 1) * 100;
        }
    }
}


