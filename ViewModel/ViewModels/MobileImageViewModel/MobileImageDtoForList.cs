
    namespace ViewModel.ViewModels.MobileImageViewModel;
    public  class MobileImageDtoForList
    {
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }

     }

