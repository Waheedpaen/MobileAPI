using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAL.Services
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
