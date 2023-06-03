
    namespace EntitiesClasses.Entities;

    public  class OperatingSystems :    CommonClass
    {
    public int Age { get; set; }

    [ForeignKey("Users")]
    public int UserId { get; set; }
    public virtual User Users { get; set; }
}

