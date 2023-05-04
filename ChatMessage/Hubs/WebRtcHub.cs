﻿
//using Microsoft.AspNet.SignalR;
//using Microsoft.Graph;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CoreWebApi.Hubs
//{
//    public class WebRtcHub : Hub
//    {
//        private static readonly List<Room> RoomsThatAreFull = new List<Room>();
//        private static readonly List<Room> RoomsThatAreActive = new List<Room>();

//        //private readonly DataContext _context;
//        //public WebRtcHub(DataContext context)
//        //{
//        //    _context = context;
//        //}
//        public string GetConnectionId()
//        {
//            return Context.ConnectionId;
//        }

//        public RtcIceServer[] GetIceServers()
//        {
//            // Perhaps Ice server management.

//            return new RtcIceServer[] { new RtcIceServer() { Username = "", Credential = "" } };
//        }

//        public async Task Join(string userName, string roomName)
//        {

//            var user = RTCUser.Get(userName, Context.ConnectionId);
//            var room = Room.Get(roomName);

//            if (user.CurrentRoom != null)
//            {
//                room.Users.Remove(user);
//                await SendUserListUpdate(Clients.Others, room, false);
//            }

//            user.CurrentRoom = room;
//            room.Users.Add(user);

//            await SendUserListUpdate(Clients.Caller, room, true);
//            await SendUserListUpdate(Clients.Others, room, false);
//            lock (RoomsThatAreActive)
//            {
//                if (!RoomsThatAreActive.Any(x => x.Name == room.Name))
//                {
//                    RoomsThatAreActive.Add(room);
//                }
//            }
//            lock (RoomsThatAreFull)
//            {
//                if (room.Users.Count() == 2)
//                {
//                    RoomsThatAreFull.Add(room);
//                }
//            }
//        }
//        public async Task CheckRoomIsFull(string roomName)
//        {
//            if (RoomsThatAreFull.Select(m => m.Name).Contains(roomName))
//                await Clients.Client(Context.ConnectionId).SendAsync("CheckRoomIsFull", true);
//            else
//                await Clients.Client(Context.ConnectionId).SendAsync("CheckRoomIsFull", false);
//        }
//        public async Task CheckRoomIsActive(string roomName)
//        {
//            if (RoomsThatAreActive.Select(m => m.Name).Contains(roomName))
//                await Clients.Client(Context.ConnectionId).SendAsync("CheckRoomIsActive", true);
//            else
//                await Clients.Client(Context.ConnectionId).SendAsync("CheckRoomIsActive", false);
//        }
//        public async Task SendCallSignalToUser(int userId, string userName, string roomName, int senderUserId)
//        {
//            await Clients.Others.SendAsync("ReceiveCallSignalFromUser", userId, userName, roomName, senderUserId);
//        }
//        public async Task SendCallSignalToGroup(string userIds, string userName, string roomName, int senderUserId, int groupId, string receiverNames, string groupName)
//        {
//            await Clients.Others.SendAsync("ReceiveCallSignalFromGroup", userIds, userName, roomName, senderUserId, groupId, receiverNames, groupName);
//        }
//        public async Task CheckRoomUsers(string roomName)
//        {
//            var room = Room.Get(roomName);
//            var userCount = room.Users.Count();
//            if (RoomsThatAreActive.Contains(room))
//            {
//                if (RoomsThatAreFull.Contains(room))
//                    await Clients.All.SendAsync("RoomUsersCount", 2);
//                else
//                    await Clients.All.SendAsync("RoomUsersCount", userCount);
//            }
//            else
//                await Clients.All.SendAsync("RoomUsersCount", 2);
//        }
//        public async Task StartScreenSharing(string roomName)
//        {
//            var room = Room.Get(roomName);
//            var roomUser = room.Users.Where(m => m.ConnectionId != Context.ConnectionId).FirstOrDefault();
//            //var users = JsonConvert.SerializeObject(roomUsers);
//            //room.Users.RemoveAll(m => roomUsers.Select(n => n.UserName).Contains(m.UserName));
//            await Clients.Others.SendAsync("ScreenSharingStarted", roomName, roomUser);
//        }
//        public async Task UserDisconnected(string userName)
//        {
//            var user = RTCUser.Get(userName, Context.ConnectionId);
//            await Clients.All.SendAsync("UserDisconnected", user.UserName);
//        }

//        public override async Task OnDisconnectedAsync(Exception exception)
//        {
//            await HangUp();

//            await base.OnDisconnectedAsync(exception);
//        }

//        public async Task HangUp()
//        {
//            var callingUser = RTCUser.Get(Context.ConnectionId);

//            if (callingUser == null)
//            {
//                return;
//            }

//            if (callingUser.CurrentRoom != null)
//            {
//                callingUser.CurrentRoom.Users.Remove(callingUser);
//                await SendUserListUpdate(Clients.Others, callingUser.CurrentRoom, false);
//            }

//            lock (RoomsThatAreActive)
//            {
//                if (RoomsThatAreActive.Count() > 0 && callingUser.CurrentRoom != null)
//                {
//                    var toRemove = RoomsThatAreActive.Where(m => m.Name == callingUser.CurrentRoom.Name).Select(m => m.Users).FirstOrDefault();
//                    if (toRemove != null)
//                    {
//                        toRemove.Remove(callingUser);
//                    }
//                }
//                lock (RoomsThatAreFull)
//                {
//                    if (callingUser.CurrentRoom != null && callingUser.CurrentRoom.Users.Count() == 0)
//                    {
//                        RoomsThatAreFull.Remove(callingUser.CurrentRoom);
//                        RoomsThatAreActive.Remove(callingUser.CurrentRoom);
//                        Room.Remove(callingUser.CurrentRoom);
//                    }
//                }
//            }
//            RTCUser.Remove(callingUser);
//        }

//        // WebRTC Signal Handler
//        public async Task SendSignal(string signal, string targetConnectionId)
//        {
//            var callingUser = RTCUser.Get(Context.ConnectionId);
//            var targetUser = RTCUser.Get(targetConnectionId);

//            // Make sure both users are valid
//            if (callingUser == null || targetUser == null)
//            {
//                return;
//            }

//            // These folks are in a call together, let's let em talk WebRTC
//            await Clients.Client(targetConnectionId).SendAsync("receiveSignal", callingUser, signal);
//        }

//        private async Task SendUserListUpdate(IClientProxy to, Room room, bool callTo)
//        {
//            await to.SendAsync(callTo ? "callToUserList" : "updateUserList", room.Name, room.Users);
//        }
//    }
//}
