

    namespace ViewModel.ViewModels.UserViewModel; 
    public  class UserAddDto
   {
   
    public string Username { get; set; }

    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }

    public int? UserTypeId { get; set; }
    // [Required(ErrorMessage = "Restaurant Field Is Required")]

    public string? ImageUrl { get; set; } 

}
 
