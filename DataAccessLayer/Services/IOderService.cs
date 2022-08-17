
using HelperData;
using ViewModel.ViewModels.OrderViewModel;

namespace DataAccessLayer.Services;
 
   public  interface IOderService
    {
    Task<ServiceResponse<object>> AddingUserOrder(int userId, List<AddUserOrderDetailDto> listOrderDetailData);
    }
 
