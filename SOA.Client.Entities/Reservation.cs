using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOA.Client.Entities
{

    public class Reservation
    {
        private long _id;
        private long _accountId;
        private long _carId;
        private DateTime _rentalDate;
        private DateTime _returnDate;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        public long CarId
        {
            get { return _carId; }
            set { _carId = value; }
        }
        public DateTime RentalDate
        {
            get { return _rentalDate; }
            set { _rentalDate = value; }
        }
        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }

    }
}
