
namespace EntitiesClasses.Entities;
    public class Order
    {
    [Key ]
    public int  Id { get; set; }
     
    public int OrderId { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
 

}

