using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModel.ViewModel.NotificationViewModel;

namespace MobileManagementSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly DataContexts _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public NotificationsController(DataContexts context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Notifications/notificationcount  
        [Route("notificationcount")]
        [HttpGet]
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
        {
            var count = (from not in _context.Notifications
                         select not).CountAsync();
            NotificationCountResult result = new NotificationCountResult
            {
                Count = await count
            };
            return result;
        }

        // GET: api/Notifications/notificationresult  
        [Route("notificationresult")]
        [HttpGet]
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()
        {
            var results = from message in _context.Notifications
                          orderby message.Id descending
                          select new NotificationResult
                          {
                              EmployeeName = message.EmployeeName,
                              TranType = message.TranType
                          };
            return await results.ToListAsync();
        }

        // DELETE: api/Notifications/deletenotifications  
        [HttpDelete]
        [Route("deletenotifications")]
        public async Task<IActionResult> DeleteNotifications()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notification");
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage();

            return NoContent();
        }
    }
}
 
