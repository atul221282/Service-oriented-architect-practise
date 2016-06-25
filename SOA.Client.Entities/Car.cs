using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CarRental.Client.Entities
{
    public class Car : ObjectBase
    {

        private string _description;
        private string _color;
        private DateTimeOffset _year;
        private decimal _rentalPrice;
        private bool _currentlyRented;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged(() => Color);
                }

            }
        }

        public DateTimeOffset Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public decimal RentalPrice
        {
            get { return _rentalPrice; }
            set {
                if (_rentalPrice != value)
                {
                    _rentalPrice = value;
                    OnPropertyChanged(() => RentalPrice);
                }
            }
        }

        public bool CurrentlyRented
        {
            get { return _currentlyRented; }
            set {
                if (_currentlyRented != value)
                {
                    _currentlyRented = value;
                    OnPropertyChanged(() => CurrentlyRented);
                }
            }
        }

        protected override IValidator GetValidator()
        {
            return base.GetValidator();
        }

    }
}
