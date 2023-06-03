

using DocumentFormat.OpenXml.InkML;
using EntitiesClasses.DataContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Graph.Models.Security;
using ViewModel.ViewModel.OperatingSystemViewModel;
using ViewModel.ViewModels.BrandViewModel;
using ViewModel.ViewModels.OperatingSystemViewModel;

namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
    
public class OperatingSystemController : BaseController
    {
    private readonly IMapper _mapper;
    private readonly DataContexts _dataContext;
    private readonly IOperatingSystemService _operatingSystemService;
    private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
    public OperatingSystemController(IHubContext<BroadcastHub, IHubClient> hubContext ,DataContexts dataContext,IMapper mapper, IOperatingSystemService OperatingSystemService)
     { 
        _mapper = mapper;
        _operatingSystemService = OperatingSystemService;
        _dataContext = dataContext;
        _hubContext = hubContext;
    }


    //public async Task<IActionResult> OperatingSystemList([FromQuery(Name = "searchTerm")] string ? searchTerm,
    //        [FromQuery(Name = "page")] int page = 1 ,
    //        [FromQuery(Name = "pageSize")] int pageSize = 5)
    //{
    //    IQueryable<OperatingSystems> query = _dataContext.OperatingSystems    ;
    //    var totalPageNumber = await query.CountAsync();

    //    // Search
    //    if (!string.IsNullOrEmpty(searchTerm))
    //    {
    //        query = query.Where(m => m.Name.Contains(searchTerm)  );
    //    }


    //    // Calculate the total number of records
    //    int totalRecords = await query.CountAsync();

    //    // Calculate the total number of pages based on the page size
    //    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

    //    // Calculate the start index of the records to be returned
    //    int startIndex = (page - 1) * pageSize;

    //    // Get the records for the current page
    //    List<OperatingSystems> data = await query
    //        .Skip(startIndex)
    //        .Take(pageSize)
    //        .ToListAsync();

    //    // Return the records along with the total number of records and pages
    //    return Ok(new
    //    {
    //        data = data,
    //        totalRecords = totalRecords,
    //        totalPages = totalPages,

    //    });


    //}

    //    [HttpGet("OperatingSystemList")]
    //  public async Task<IActionResult> OperatingSystemList()
    //  {
    //    if (!ModelState.IsValid) return BadRequest(ModelState);
    //    var operatingSytemList = await _operatingSystemService.Get();
    //    var osDto = _mapper.Map<List<OperatingSystemDto>>(operatingSytemList);
    //    if (osDto != null) {
    //        _response.Data = osDto;
    //        _response.Success = true;
    //        return Ok(_response);
    //    }
    //    else
    //    {
    //        _response.Success = false;
    //        _response.Message = CustomMessage.DataNotExit;
    //        return Ok(_response);
    //    }

    //  }
    [HttpGet("Get")]
    public async Task<IActionResult> OperatingSystemList([FromQuery] OperatingSystemSearch operatingSystemSearch)
    {
        IQueryable<OperatingSystems> query = _dataContext.OperatingSystems; 
        var totalPageNumber = await query.CountAsync();
        var blogs = _dataContext.OperatingSystems
           .FromSql($"SELECT * FROM dbo.OperatingSystems")
           .ToList();
        var blogfs = _dataContext.OperatingSystems
           .FromSql($"EXECUTE dbo.StpGetAllMembers")
           .AsEnumerable().ToList();



        // Sear.ch
        if ((!string.IsNullOrEmpty(operatingSystemSearch.SearchTerm) && !string.IsNullOrEmpty(operatingSystemSearch.Age)) || (!string.IsNullOrEmpty(operatingSystemSearch.Age)) || (!string.IsNullOrEmpty(operatingSystemSearch.SearchTerm)))
        {
            query = query.Where(m => (m.Name.Contains(operatingSystemSearch.SearchTerm) && m.Age.Equals(Convert.ToInt32(operatingSystemSearch.Age))) || ((m.Age.Equals(Convert.ToInt32(operatingSystemSearch.Age))) || (m.Name.Contains(operatingSystemSearch.SearchTerm))));
        }
        //if (!string.IsNullOrEmpty(operatingSystemSearch.SearchTerm) )
        //{
        //    query = query.Where(m => m.Name.Contains(operatingSystemSearch.SearchTerm)  );
        //}
        //if (operatingSystemSearch.Age != null)
        //{
        //    query = query.Where(m =>m.Age.Equals(operatingSystemSearch.Age));
        //}


        // Calculate the total number of records
        int totalRecords = await query.CountAsync();

        // Calculate the total number of pages based on the page size
        int totalPages = (int)Math.Ceiling((double)totalRecords / operatingSystemSearch.PageSize);

        // Calculate the start index of the records to be returned
        int startIndex = (operatingSystemSearch.Page - 1) * operatingSystemSearch.PageSize;

        // Get the records for the current page
        List<OperatingSystems> data = await query
            .Skip(startIndex)
            .Take(operatingSystemSearch.PageSize)
            .ToListAsync();

        // Return the records along with the total number of records and pages
          return Ok(new
        {
            data = data.Where(data=>data.UserId == operatingSystemSearch.UserId).ToList(),
            totalRecords = totalRecords,
            totalPages = totalPages,
            totalRecordNumber = query.Count()
        });


    }
    [HttpGet("OperatingSystemDetail/{Id}")]
      public async Task<IActionResult> OperatingSystemDetail(int Id)
       {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osObj = await _operatingSystemService.Get(Id);
        var osDto = _mapper.Map<OperatingSystemDto>(osObj);
        if (osDto != null)
        {
            _response.Data = osDto;
            _response.Success=true;
            return Ok(_response);
        }
        else
        {
            _response.Data=null;
            _response.Success = false;
            return Ok(_response);
        }

       }
       
      [HttpDelete("DeleteOperatingSystemData/{Id}")]
      public async Task<IActionResult> DeleteOperatingSystemData(int Id)
      {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osObj = await _operatingSystemService.Get(Id);
        if(osObj != null)
        {
           
        
            await _operatingSystemService.Delete(osObj);
            _response.Success = true;
            _response.Message = CustomMessage.Deleted;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        } 
      }

      [HttpPost("SaveOperatingSystemData")]
      public async Task<IActionResult> SaveOperatingSystemData(OperatingSystemDto operatingSystemDto)
     {
        var operatingSystemalreadyExit = await _operatingSystemService.OperatingSystemAlreadyExit(operatingSystemDto.Name);
        if (operatingSystemalreadyExit != null)
        {
            _response.Success = false;
            _response.Message = operatingSystemalreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Notification notification = new Notification()
            {
                EmployeeName = operatingSystemDto.Name,
                TranType = "Edit"
            };

             _dataContext.Notifications.Add(notification);
            await _dataContext.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage();
            var operatingSystemsDto = _mapper.Map<OperatingSystems>(operatingSystemDto);
            await _operatingSystemService.Create(operatingSystemsDto);
            _response.Success = true;
            _response.Message = CustomMessage.Added;
            return Ok(_response);
        }


     }

      [HttpPut("UpdateOperatingSystemData")]
      public async Task<IActionResult> UpdateOperatingSystemData(OperatingSystemDto OperatingSystemDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var operatingSystemEntity = _mapper.Map<OperatingSystems>(OperatingSystemDto);

        var operatingSystemalreadyExit = await _operatingSystemService.OperatingSystemAlreadyExit(operatingSystemEntity.Name);
        if (operatingSystemalreadyExit != null)
        {
            _response.Success = false;
            _response.Message = operatingSystemalreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            var objOS = await _operatingSystemService.Get(operatingSystemEntity.Id);
            if (objOS != null)
            {
                Notification notification = new Notification()
                {
                    EmployeeName = objOS.Name,
                    TranType = "Edit"
                };

                _dataContext.Notifications.Add(notification);
                await _dataContext.SaveChangesAsync();
                await _hubContext.Clients.All.BroadcastMessage();
                await _operatingSystemService.Update(objOS, operatingSystemEntity);
                _response.Success = true;
                _response.Message = CustomMessage.Updated; ;
                return Ok(_response);
            }
            else
            {
                _response.Success = false; ;
                _response.Message = CustomMessage.RecordNotFound;
                return Ok(_response);
            }
        }
        
    }
      [HttpGet("SearchData/{Name}")]
      public async Task<IActionResult> SearchOperatingSystem(string Name)
   {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var osSearchData = _operatingSystemService.SearchOperatingSystemsData(Name);
        _response.Data = osSearchData;
        _response.Success = true;
        return Ok(_response);
    }
}
 
