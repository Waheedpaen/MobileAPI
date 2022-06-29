 
namespace EntitiesClasses.Entities;
    public  class City : CommonClass
    {

    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }
}
 
