namespace DataAccessLayer.IRepositories;
   public interface IOperatingSytemRepository :IRepository<OperatingSystems, int>
    {
    public Task<List<OperatingSystems>> SearchOperatingSystemData(string name);




    Task<OperatingSystems> OperatingSystemAlreadyExit(string name);
    
}
 
