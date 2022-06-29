 
namespace EntitiesClasses.Entities;
   public  class MobileImage
    {

 
    public int Id { get; set; }
  
    public string ImageUrl { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }

    [ForeignKey("Mobile")]
    public int MobileId { get; set; }
    public Mobile Mobile { get; set; }
}
 
