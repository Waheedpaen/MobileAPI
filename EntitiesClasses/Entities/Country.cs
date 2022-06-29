   namespace EntitiesClasses.Entities;
    public  class Country : CommonClass
    {
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string CountryCode { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")] 
    public string CurrencyCode { get; set; }

}
 
