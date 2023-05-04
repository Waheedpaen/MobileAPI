using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.ChatHub
{
     public  class MessageForAddDto
    {
            
        public int MessageToUserId { get; set; }
        public string Comment { get; set; }
        // public int MessageFromUserId { get; set; }
        public int? MessageReplyId { get; set; }
        public int  _LoggedIn_UserID { get; set; }
    }
}
