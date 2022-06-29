
     namespace EntitiesClasses.Entities;
     public  class User : CommonClass
    {
    [Required]
    [StringLength(50, ErrorMessage = "Username cannot be longer then 50 characters")]
    public string UserName { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Email cannot be longer then 50 characters")]
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(50, ErrorMessage = "FullName cannot be longer then 50 characters")]

 
    public DateTime? LastActive { get; set; }
    public DateTime? LastLogout { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
 
    public int CreatedById { get; set; } 
 
    [StringLength(30, ErrorMessage = "Contact Number is not loger then 30 characters")]
  
    [ForeignKey("UserTypes ")]
    public int UserTypesId  { get; set; }
    public virtual UserTypes  UserTypes { get; set; }

    //[ForeignKey("Shop")]
    //public int ShopId { get;set; }
    //public virtual Shop Shop { get; set; }
  
    public int updatebyId { get; set; }

    public string ImageUrl { get;set;}
}
 
