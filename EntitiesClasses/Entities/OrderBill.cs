    

    namespace EntitiesClasses.Entities;
    public class OrderBill
    {
    [Key]
    public int Id { get; set; }

    public double Amount { get; set; } 
    public int updatebyId { get; set; }
    public int Tip { get; set; }
    public int OrderType { get; set; } 
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}
 
