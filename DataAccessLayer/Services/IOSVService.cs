 

   namespace DataAccessLayer.Services;
 
    public interface IOSVService
    {
    Task<List<OSVersion>> Get();
    Task<OSVersion> Get(int id);
    Task<OSVersion> Create(OSVersion model);
    Task<OSVersion> Update(OSVersion update, OSVersion model);
    Task<List<OSVersion>> SearchOSVersionData(string name);
    Task<OSVersion> Delete(OSVersion model);
}
 
