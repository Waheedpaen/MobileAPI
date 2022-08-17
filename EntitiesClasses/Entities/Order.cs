
namespace EntitiesClasses.Entities;
    public class Order  
    {
    [Key ]
    public int  Id { get; set; }

    public string OrderStatus { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public Order()
    {
        OrderDetails = new List<OrderDetail>();
    }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
 

}

