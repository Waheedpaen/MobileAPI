 
namespace ViewModel.ViewModels.PaymentCardViewModel; 
    public   class PaymentCardDto
    {
    public int Id { get; set; }
    public int CardType { get; set; }
    public string Title { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public bool IsAuthenticated { get; set; }
    public int CreatedById { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Modified_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
}
 
