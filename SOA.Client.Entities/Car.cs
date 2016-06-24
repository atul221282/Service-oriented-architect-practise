using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SOA.Client.Entities
{
    public class Car : ObjectBase
    {
        private long _id;
        private string _description;
        private string _color;
        private int _year;
        private decimal _rentalPrice;
        private bool _currentlyRented;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public decimal RentalPrice
        {
            get { return _rentalPrice; }
            set { _rentalPrice = value; }
        }

        public bool CurrentlyRented
        {
            get { return _currentlyRented; }
            set { _currentlyRented = value; }
        }

        protected override IValidator GetValidator()
        {
            return base.GetValidator();
        }

    }
}
