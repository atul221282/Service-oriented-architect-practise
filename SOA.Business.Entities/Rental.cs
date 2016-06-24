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
    public class Rental : EntityBase, IIdentifiableEntity,IAccountOwnedEntity
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long AccountId { get; set; }
        [DataMember]
        public long CarId { get; set; }
        [DataMember]
        public DateTime DateRented { get; set; }
        [DataMember]
        public DateTime DateDue { get; set; }
        [DataMember]
        public DateTime? Datereturned { get; set; }
        public long EntityId
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        long IAccountOwnedEntity.OwnerAccount
        {
            get
            {
                return AccountId;
            }

           
        }
    }
}
