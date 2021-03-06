﻿using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Entities
{
    [DataContract]
    public class Account : EntityBase,IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public string LoginEmail { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string CreditCard { get; set; }
        [DataMember]
        public DateTimeOffset? ExpiryDate { get; set; }

        public long? OwnerAccountId
        {
            get
            {
                return Id;
            }
        }
    }
}
