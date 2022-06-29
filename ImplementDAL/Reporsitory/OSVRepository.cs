

using Microsoft.EntityFrameworkCore;

namespace ImplementDAL.Reporsitory;

public class OSVRepository : Reporsitory<OSVersion, int>, IOSVRepository
{
    public OSVRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;
    public async Task<List<OSVersion>> Get()
    {
         return await Context.Set<OSVersion>().Include(data=>data.OperatingSystems).ToListAsync();
    }
    public async Task< OSVersion> Get(int Id)
    {
       return await Context.Set<OSVersion>().Include(data=>data.OperatingSystems).Where(data=>data.Id==Id) .FirstOrDefaultAsync(); 
    }

    public async Task<List<OSVersion>> SearchingData(string name)
    {
        return await Context.Set<OSVersion>().Include(data=>data.OperatingSystems).Where(data=>data.Name.StartsWith(name)).ToListAsync();
    }
}
 
