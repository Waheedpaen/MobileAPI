
using Microsoft.EntityFrameworkCore;

namespace ImplementDAL.Reporsitory;

    public  class BrandRepository: Reporsitory<Brand,int>,IBrandRepository
    {
    public BrandRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<Brand> BrandNameAlreadyExit(string name)
    {
       return await DataContexts.Set<Brand>().Where(data=>data.Name == name ).FirstOrDefaultAsync();
    }

    public async Task<List<Brand>> SearchBrandData(string name)
    {
        return  DataContexts.Brands.Where(b => b.Name.StartsWith(name) ).ToList(); 
       
    }
}

