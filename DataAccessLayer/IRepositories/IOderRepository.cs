 

    namespace DataAccessLayer.IRepositories;
 
    public interface IOderRepository : IRepository<Order, int>
{
    Task<List<OrderDetail>> SaveOrderDetail(List<OrderDetail> model);
}
 
