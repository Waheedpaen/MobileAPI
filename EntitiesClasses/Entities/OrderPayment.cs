

    namespace EntitiesClasses.Entities;
    public class OrderPayment
    {
    public int Id { get; set; }
    public int PaymentMode { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int Amount { get; set; }
    public int DiscountId { get; set; }
    public int updatebyId { get; set; } 
    public int CreatedById { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
    //[ForeignKey("User")]
    //public int UserId { get; set; }
    //public virtual User User { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}
 
