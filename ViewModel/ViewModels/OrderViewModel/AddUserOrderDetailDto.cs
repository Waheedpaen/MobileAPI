

   namespace ViewModel.ViewModels.OrderViewModel;
 
    public  class AddUserOrderDetailDto
    {
        public int Id { get; set; }
        public int Mobile_Id { get; set; }
        public int Order_Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

        // UserAddress
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.Now;
    }
 
