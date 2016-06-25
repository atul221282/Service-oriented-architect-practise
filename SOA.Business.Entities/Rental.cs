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
        public long AccountId { get; set; }
        [DataMember]
        public long CarId { get; set; }
        [DataMember]
        public DateTimeOffset DateRented { get; set; }
        [DataMember]
        public DateTimeOffset DateDue { get; set; }
        [DataMember]
        public DateTimeOffset? Datereturned { get; set; }
      
        long IAccountOwnedEntity.OwnerAccount
        {
            get
            {
                return AccountId;
            }

           
        }
    }
}
