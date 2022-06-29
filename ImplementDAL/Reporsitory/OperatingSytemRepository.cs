

namespace ImplementDAL.Reporsitory;

public class OperatingSytemRepository : Reporsitory<OperatingSystems, int>, IOperatingSytemRepository
{
    public OperatingSytemRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<List<OperatingSystems>> SearchOperatingSystemData(string name)
    {

        var data = Context.Set<OperatingSystems>().Where(data => data.Name.StartsWith(name)).ToList();
        return data;
    }
}
 
 