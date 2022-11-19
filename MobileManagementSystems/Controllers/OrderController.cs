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
    private readonly IUserService _userService;
    public OrderController(IOderService oderService, IMapper mapper,   IUserService  userService  )
    {
        _userService = userService;
       _oderService = oderService;
        _mapper = mapper;
    }

    [HttpPost("SaveUserOrder/{Id}")]
    public async Task<IActionResult> SaveUserOrder(int Id, List<AddUserOrderDetailDto> listOrderDetailData)
    {
        var _response = _oderService.AddingUserOrder(Id, listOrderDetailData);
        return Ok(_response);
    }
    
     
}
 
