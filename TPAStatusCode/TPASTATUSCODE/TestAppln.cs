using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPASTATUSCODE
{
    public class TestAppln
    {
        public static void main ()
        {
            TPASTATUSCODE.Service1 obj = new Service1();
            BussLayer.ObjectData objObject = new BussLayer.ObjectData();
            BussLayer.Responce objResponse=new BussLayer.Responce();
            objObject.strCreatedBy = "Test12345";
            objObject.strTpaRefNo = "CT";
            objObject.strTpaStatusCode = "Health India";
            objResponse = obj.PushMedicalStatus(objObject);
        }
    }
}