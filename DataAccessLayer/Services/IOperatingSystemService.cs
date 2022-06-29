 
    namespace DataAccessLayer.Services;
 
    public interface IOperatingSystemService 
    { 
    Task<IEnumerable<OperatingSystems>> Get();
    Task<OperatingSystems> Get(int id);
    Task<OperatingSystems> Create(OperatingSystems model);
    Task<OperatingSystems> Update(OperatingSystems update, OperatingSystems model);
    Task<OperatingSystems> Delete(OperatingSystems model);
    Task<List<OperatingSystems>> SearchOperatingSystemsData(string name); 
}
 
