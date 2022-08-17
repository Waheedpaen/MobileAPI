

using HelperData;

namespace ImplementDAL.Reporsitory;
      public class OderRepository : Reporsitory<Order, int>, IOderRepository
  
    {

    public ServiceResponse<object> _serviceResponse;
    public OderRepository(DataContexts context) : base(context)
    {

        _serviceResponse = new ServiceResponse<object>();
    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<List<OrderDetail>> SaveOrderDetail(List<OrderDetail> model)
    {
        await DataContexts.Set<OrderDetail>().AddRangeAsync(model);
    
        return model;
    }

   


}
 
