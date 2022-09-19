 

     namespace DataAccessLayer.IRepositories;
    public interface IBrandRepository : IRepository<Brand, int>
    {
     public Task<List<Brand>> SearchBrandData(string name);
    public Task<Brand> BrandNameAlreadyExit(string  name);

     }