using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities
{
   public  class OrderDetail
    {
        public int Id { get; set; }
 
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Price { get; set; }
        public DateTime? Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; } = DateTime.Now;
        public Boolean IsDeleted { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }
        public virtual Mobile Mobile { get; set; }
    }
}
