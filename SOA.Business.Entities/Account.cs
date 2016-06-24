using Core.Common.Contract;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOA.Business.Entities
{
    [DataContract]
    public class Account : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public long Id { get; set; }
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
        public string ExpDate { get; set; }

        public long EntityId
        {
            get
            {
                return Id
            }

            set
            {
                Id = value;
            }
        }
    }
}
