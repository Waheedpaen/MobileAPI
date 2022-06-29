
    namespace ViewModel.ViewModels.CarouselViewModel;
    public class CarouselDto  
    {

    [Key]
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public int Priority { get; set; }
    public string Caption { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Modified_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }

     }
 
