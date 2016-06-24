using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOA.Client.Entities
{
    public class Rental
    {
        private long _id;
        private long _accountid;
        private long _carId;
        private DateTime _dateRented;
        private DateTime _dateDue;
        private DateTime? _dateReturned;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public long AccountId
        {
            get { return _accountid; }
            set { _id = value; }
        }
        public long CarId
        {
            get { return _carId; }
            set { _carId = value; }
        }
        public DateTime DateRented
        {
            get { return _dateRented; }
            set { _dateRented = value; }
        }
        public DateTime DateDue
        {
            get { return _dateDue; }
            set { _dateDue = value; }
        }
        public DateTime? Datereturned
        {
            get { return _dateReturned; }
            set { _dateReturned = value; }
        }

    }
}
