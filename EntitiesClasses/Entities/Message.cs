using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities
{
    public  class Message
    {
        public int Id { get; set; }
    
        public string Comment { get; set; }
      
        public int? MessageReplyId { get; set; }
        public string Attachment { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [ForeignKey("MessageFromUserId")]

        public int ? MessageFromUserId { get; set; }
        public virtual User MessageFromUser { get; set; }
   
     
 

        [ForeignKey("MessageToUserId")]
        public int? MessageToUserId { get; set; }
        public virtual User MessageToUser { get; set; }

    }
}
