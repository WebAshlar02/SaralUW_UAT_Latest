using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
namespace TPASTATUSCODE.BussLayer
{
    [DataContract]
    public class ObjectData
    {        
        [DataMember]
        public string strTpaRefNo { get; set; }
        [DataMember]
        public string strTpaStatusCode { get; set; }
        [DataMember]
        public string strCreatedBy { get; set; }
    }
}