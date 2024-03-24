using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Model
{
    
    public class EmailModel
    {
        private string from = "infor @EO.com";

        // For the subject of Email
        public string Subject { get; set; } = "";

        //For the From Email
        public string From
        {
            get { return from; }   // get method
            set { from = value; }  // set method
        }
        //To add the Email Body
        public string Body { get; set; } = "";

        //To add the extra string in the body
        public string extraBodyOption { get; set; } = "";
    }
}
