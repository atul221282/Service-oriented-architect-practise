using Core.Common.Contracts;
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
    public class Reservation : EntityBase, IAccountOwnedEntity
    {
        [DataMember]
        public long AccountId { get; set; }
        [DataMember]
        public long CarId { get; set; }

        public long? OwnerAccountId
        {
            get
            {
                return Id;
            }
        }

        [DataMember]
        public DateTimeOffset RentalDate { get; set; }
        [DataMember]
        public DateTimeOffset ReturnDate { get; set; }
    }
}
