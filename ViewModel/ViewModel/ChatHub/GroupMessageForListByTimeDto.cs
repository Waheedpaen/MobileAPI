using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.ChatHub
{
    public  class GroupMessageForListByTimeDto
    {
        public string TimeToDisplay { get; set; } = "";
        public List<GroupMessageForListDto> Messages { get; set; } = new List<GroupMessageForListDto>();
    }
}
