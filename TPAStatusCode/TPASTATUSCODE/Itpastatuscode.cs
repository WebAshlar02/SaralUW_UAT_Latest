using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TPASTATUSCODE.BussLayer;

namespace TPASTATUSCODE
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    interface Itpastatuscode
    {
        [OperationContract]
        Responce PushMedicalStatus(ObjectData objValue);

        [OperationContract]
        Responce TPAIntegrationDetails_Insert(Integration objCoreInt);
    }
}
