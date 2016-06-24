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
    public class Car : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal RentalPrice { get; set; }

        [DataMember]
        public bool CurrentlyRented { get; set; }

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
    }
}
