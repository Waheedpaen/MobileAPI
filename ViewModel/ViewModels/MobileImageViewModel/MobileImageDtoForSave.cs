using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModels.MobileImageViewModel;
 
    public class MobileImageDtoForSave
    {
    [Key]
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
    public int ? MobileId { get; set; }

}
 
