﻿ 

  namespace ViewModel.ViewModels.UserViewModel;
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
         public string Role { get;set; }   
        public int UserTypeId { get; set; }
        public byte[] PasswordHash { get; set; }
        public string ImageUrl { get;set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }

        public DateTime LastActive { get; set; }
   


    }
  
