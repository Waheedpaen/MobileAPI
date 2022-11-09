

using Microsoft.EntityFrameworkCore;

namespace ImplementDAL.Reporsitory;

public class OperatingSytemRepository : Reporsitory<OperatingSystems, int>, IOperatingSytemRepository
{
    public OperatingSytemRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<OperatingSystems> OperatingSystemAlreadyExit(string name)
    {
        return await DataContexts.Set <OperatingSystems > ().Where(data => data.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<OperatingSystems>> SearchOperatingSystemData(string name)
    {

        var data = Context.Set<OperatingSystems>().Where(data => data.Name.StartsWith(name)).ToList();
        return data;
    }
}
 
 