using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{

    public class Reservation : ObjectBase
    {
        private long _accountId;
        private long _carId;
        private DateTimeOffset _rentalDate;
        private DateTimeOffset _returnDate;


        public long AccountId
        {
            get { return _accountId; }
            set
            {
                if (_accountId != value)
                {
                    _accountId = value;
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
        public DateTimeOffset RentalDate
        {
            get { return _rentalDate; }
            set
            {
                if (_rentalDate != value)
                {
                    _rentalDate = value;
                    OnPropertyChanged(() => RentalDate);
                }
            }

        }
        public DateTimeOffset ReturnDate
        {
            get { return _returnDate; }
            set
            {
                if (_returnDate != value)
                {
                    _returnDate = value;
                    OnPropertyChanged(() => ReturnDate);
                }
            }
        }

    }
}
