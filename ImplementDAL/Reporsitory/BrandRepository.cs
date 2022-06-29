
namespace ImplementDAL.Reporsitory;

    public  class BrandRepository: Reporsitory<Brand,int>,IBrandRepository
    {
    public BrandRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<List<Brand>> SearchBrandData(string name)
    {
        return  DataContexts.Brands.Where(b => b.Name.StartsWith(name) ).ToList(); 
       
    }
}

