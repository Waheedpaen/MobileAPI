
 namespace EntitiesClasses.CommonClasses;
 public abstract class CommonClass
    {
     [Key]
    public int Id { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At  { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
}
 
