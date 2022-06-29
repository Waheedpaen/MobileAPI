        

   namespace DataAccessLayer.IRepositories;
 
    public  interface IOSVRepository : IRepository<OSVersion, int>
    {
    public Task<List<OSVersion>> Get();
    public Task<OSVersion> Get(int Id);

    public Task<List<OSVersion>> SearchingData(string name);
}
 
