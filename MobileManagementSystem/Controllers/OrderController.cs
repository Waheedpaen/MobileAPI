using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels.OrderViewModel;

namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
    private readonly IMapper _mapper;
    private readonly IOderService _oderService;
    public OrderController(IOderService oderService, IMapper mapper)
    {
        _oderService = oderService;
        _mapper = mapper;
    }

    [HttpPost("SaveBrand")]
    public async Task<IActionResult> SaveUserAdd(int userId, List<AddUserOrderDetailDto> listOrderDetailData)
    {
        var _response = _oderService.AddingUserOrder(userId, listOrderDetailData);
        return Ok(_response);
    }
    
     
}
 
