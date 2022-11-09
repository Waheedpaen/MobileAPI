 
 
    namespace ViewModel.ViewModels.UserViewModel;
    public  class UserTypesDto
    {
    public int Id { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "Name cannot be longer then 30 characters.")]
    public string UserType { get; set; }
    public string AccountType { get; set; }
    public string Platform { get; set; }
}
 
