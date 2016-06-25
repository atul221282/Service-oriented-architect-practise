using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Core
{
    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject, IIdentifiableEntity
    {
        public ExtensionDataObject ExtensionData { get; set; }
        [DataMember]
        public long? Id { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTimeOffset? CreatedOn { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public DateTimeOffset? UpdateOn { get; set; }
        [DataMember, Timestamp]
        public byte[] RowVersion { get; set; }
        public long? EntityId
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
