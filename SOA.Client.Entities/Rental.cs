using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Rental : ObjectBase
    {

        private long _accountid;
        private long _carId;
        private DateTimeOffset _dateRented;
        private DateTimeOffset _dateDue;
        private DateTimeOffset? _dateReturned;

        public long AccountId
        {
            get { return _accountid; }
            set
            {
                if (_accountid != value)
                {
                    _accountid = value;
                    OnPropertyChanged(() => AccountId);
                }
            }
        }
        public long CarId
        {
            get { return _carId; }
            set
            {
                if (_carId != value)
                {
                    _carId = value;
                    OnPropertyChanged(() => CarId);
                }
            }
        }
        public DateTimeOffset DateRented
        {
            get { return _dateRented; }
            set
            {
                if (_dateRented != value)
                {
                    _dateRented = value;
                    OnPropertyChanged(() => DateRented);
                }
            }
        }
        public DateTimeOffset DateDue
        {
            get { return _dateDue; }
            set
            {
                if (_dateDue != value)
                {
                    _dateDue = value;
                    OnPropertyChanged(() => DateDue);
                }
            }
        }
        public DateTimeOffset? DateReturned
        {
            get { return _dateReturned; }
            set
            {
                if (_dateRented != value)
                {
                    _dateReturned = value;
                    OnPropertyChanged(() => DateReturned);
                }
            }
        }

    }
}
