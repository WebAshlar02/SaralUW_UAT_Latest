using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWSaralTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Program objProgram = new Program();
            objProgram.Test();
        }

        private void Test()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                string strEx = string.Empty;
                 strEx =ex.Message;
            }
        }
    }
    public class Test
    {

        private string strName;

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strLastName;

        public string LastName
        {
            get { return strLastName; }
            set { strLastName = value; }
        }
        

       public string Return
        {
            get { return "i'm "+Name +" LastName "+LastName; }            
        }
        
    }
}
