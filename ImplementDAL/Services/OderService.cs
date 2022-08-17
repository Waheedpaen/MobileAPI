using AutoMapper;
  
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

using HelperData;
using ViewModel.ViewModels.OrderViewModel;

namespace ImplementDAL.Services;
 
    public  class OderService : IOderService
    {
    public ServiceResponse<object> _serviceResponse;
    private readonly IUnitofWork _unitOfWork;
    private readonly ILoggerManager _logger;
    public OderService(IUnitofWork unitOfWork, ILoggerManager logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _serviceResponse = new ServiceResponse<object>();
    }

    public async Task <ServiceResponse<object>> AddingUserOrder(int  userId, List<AddUserOrderDetailDto> listOrderDetailData)
    {
        var OrderOjb = new Order()
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            OrderStatus = "pending",
            Updated_At = null,
        };

        await _unitOfWork.IOderRepository.AddAsync(OrderOjb);
        await _unitOfWork.CommitAsync();

        var orderdetail = new List<OrderDetail>();
        foreach (var OrderData in listOrderDetailData)
        {
            orderdetail.Add(new OrderDetail()
            {
                OrderId = OrderOjb.Id,
                MobileId = OrderData.Mobile_Id,
                Quantity = OrderData.Quantity,
                Price = OrderData.Price,
                Updated_At = null



            }) ;
            MailMessage msgObj = new MailMessage("abobakarpaen@gmail.com", OrderData.UserEmail);
            msgObj.Subject = "New Mobiles Home Delivery";
            msgObj.IsBodyHtml = true;
            msgObj.Body = @$"<h3>{OrderData.UserName},</h3><p>Your Order Has been Succeussfully Done!</p>  
                                  <p>Your Address is {OrderData.UserAddress},</p><p>Your Email Is: {OrderData.UserEmail}</p>
                                  <p>Your Product Name is {OrderData.ProductName} and Quantity is {OrderData.Quantity}</p>
                                   <p>Your Mobile Number Is: {OrderData.MobileNumber}</p><p>Your Order Date Was: {OrderData.OrderDate}</p><hr>
                                    <p>Your Total Amount of this Products Rs{OrderData.Price * OrderData.Quantity}</p>
                                   <h3>Thank You For Order and your Order Has been on Pending!</h3><p>Regards From <strong>New Mobiles Home Delivery</strong></p>";


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential() { UserName = "abobakarpaen@gmail.com", Password = "kzrcgwljcfpvbpko" };
            client.Send(msgObj);

        }
         await _unitOfWork.IOderRepository.SaveOrderDetail(orderdetail);
        await _unitOfWork.CommitAsync();
        _serviceResponse.Success = true;
        _serviceResponse.Message = CustomMessage.Added;

        return _serviceResponse;
    }
}
 
