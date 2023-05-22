



using EntitiesClasses.CommonClasses;
using Microsoft.AspNetCore.Http;

namespace ViewModel.ViewModels.BrandViewModel;
  public class BrandDto : CommonClass
   {
    public IFormFile? Photo { get; set; }
     
    public string? FullPath { get; set; }
    public string? FileName { get; set; }
    public string ? FilePath { get; set; }
    public string? ImageUrl { get; set; }
   

}
