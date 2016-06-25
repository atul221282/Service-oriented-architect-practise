using Core.Common.Contracts;
using System;
using System.Collections.Generic;
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
        public DateTimeOffset? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdateOn { get; set; }

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
