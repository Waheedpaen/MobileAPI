 

   namespace DataAccessLayer.Services;
 
    public interface IBrandService
    {
    Task<IEnumerable<Brand>> Get();
    Task<Brand> Get(int id);
    Task<Brand> Create(Brand model);
    Task<Brand> Update(Brand update, Brand model);
     Task<List<Brand>> SearchBrandData(string name);
    Task<Brand> Delete(Brand model);
}
 
