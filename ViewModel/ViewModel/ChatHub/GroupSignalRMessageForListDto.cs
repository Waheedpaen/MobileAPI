﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.ChatHub
{
    public class GroupSignalRMessageForListDto
    {
        public int Id { get; set; }
        public string TimeToDisplay { get; set; }
        public string DateTimeToDisplay { get; set; }
        public string Type { get; set; }
        public string MessageToUserIdsStr { get; set; }
        public string MessageToUser { get; set; }
        public string Comment { get; set; }
        public string Attachment { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int ? MessageFromUserId { get; set; }
        public string MessageFromUser { get; set; }
        public int? MessageReplyId { get; set; }
        public int GroupId { get; set; }
    }
}
