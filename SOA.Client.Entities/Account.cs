using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CarRental.Client.Entities
{
    public class Account : ObjectBase
    {
        private string _loginEmail;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _zipCode;
        private string _creditCard;
        private DateTimeOffset? _expiryDate;

        public string LoginEmail
        {
            get { return _loginEmail; }
            set
            {
                if (_loginEmail != value)
                {
                    _loginEmail = value;
                    OnPropertyChanged(() => LoginEmail);
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(() => LastName);
                }

            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (_zipCode != value)
                {
                    _zipCode = value;
                    OnPropertyChanged(() => ZipCode);
                }
            }
        }

        public string CreditCard
        {
            get { return _creditCard; }
            set
            {
                if (_creditCard != value)
                {
                    _creditCard = value;
                    OnPropertyChanged(() => CreditCard);
                }
            }
        }

        public DateTimeOffset? ExpiryDate
        {
            get { return _expiryDate; }
            set
            {
                if (_expiryDate != value)
                {
                    _expiryDate = value;
                    OnPropertyChanged(() => ExpiryDate);
                }
            }
        }


    }
}
