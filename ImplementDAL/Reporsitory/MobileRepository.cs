

using DataAccessLayer.IRepositories;
using EntitiesClasses.Entities;
using Microsoft.EntityFrameworkCore;
using ViewModel.ViewModels.OtherDtos;

namespace ImplementDAL.Reporsitory;


public class MobileRepository : Reporsitory<Mobile, int>, DataAccessLayer.IRepositories.IMobileRepository
{
    public MobileRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<Mobile> Get(int id)
    {
        var data = await Context.Set<Mobile>().Include(data => data.Color)
                   .Include(data => data.Brand)
                   .Include(data => data.OSVersion)
                   .Include(data => data.MobileImages)
                   .Include(data => data.OSVersion.OperatingSystems)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();
        return data;
    }

    public Task<List<Mobile>> Get()
    {
        var listMobile = Context.Set<Mobile>().Include(data => data.Color)
                    .Include(data => data.Brand)
                    .Include(data => data.OSVersion)
                    .Include(data => data.MobileImages)
                    .Include(data => data.OSVersion.OperatingSystems).ToListAsync();

        return listMobile;
    }

    public async Task<List<OSVersion>> GetOSVVersion()
    {
        return await Context.Set<OSVersion>().ToListAsync();
    }

    public async Task<List<OSVersion>> GetOSVersion(int id)
    {
        return await Context.Set<OSVersion>().Where(data => data.Id == id).ToListAsync();
              
    }
    public async Task<List<Color>> GetColor()
    {
        return await Context.Set<Color>().ToListAsync();
    }
    public async Task<OperatingSystems> GetOperatingSystems(int Id)
    {
        return await Context.Set<OperatingSystems>().Where(data => data.Id == Id).FirstOrDefaultAsync();
    }

    public async Task<List<OperatingSystems>> GetOperatingSystems()
    {
        return await Context.Set<OperatingSystems>().ToListAsync();
    }

    public async Task<List<Mobile>> SearchingData(string name)
    {
      return await Context.Set<Mobile>().Include(data=>data.MobileImages).Where(data=>data.Name.StartsWith(name)).ToListAsync();
    }

    public async Task<List<OSVersion>> GetOSVersionByOperatingSystem(int id)
    {
       return await Context.Set<OSVersion>().Where(data => data.OperatingSystemId == id).ToListAsync();
    }

    public async Task<OSVersion>  GetOSVersionbyID(int id)
    {
       return await Context.Set<OSVersion>().Where(data=>data.Id== id).FirstOrDefaultAsync();
    }

    Task<OSVersion> IMobileRepository.GetOSVersionbyID(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<MobileImage> GetMobileImage(int id)
    {
        return await Context.Set<MobileImage>().FirstOrDefaultAsync(data => data.Id == id);
    }

    public async Task<List<MobileImage>> GetMobileImage()
    {
        return await Context.Set<MobileImage>().ToListAsync();
    }

    public async Task<MobileImage> SaveMobileImage(MobileImage model)
    {
        model.Updated_At = null;
         await Context.Set<MobileImage>().AddAsync(model);
        return model;
    }
    public async Task<MobileImage> SaveMobileImages(MobileImage model)
    {
        model.Updated_At = null;
       
        return model;
    }

    public async Task<List<Mobile>> GetMobileByBrand(int Id)
    { 
        return await Context.Set<Mobile>().Where(data=>data.BrandId == Id).ToListAsync();
    }

    public async Task<List<Mobile>> GetMobileListByColor(string name)
    {
        return await Context.Set<Mobile>().Include(data=>data.MobileImages).Include(data => data.Color).Where(data => data.Color.Name.Contains(name)).ToListAsync();

    }

    public async Task<List<Mobile>> GetMobilesByPrice(RangeDto model)
    {
     return await Context.Set<Mobile>().Include(data => data.MobileImages).Include(data => data.Color)
            .Where(data => data.MobilePrice >= model.First && data.MobilePrice <= model.Second )
            .ToListAsync();
    }

    public async Task<List<Mobile>> GetMobilesByScreen(RangeScreenSizeDto model)
    {
        return await Context.Set<Mobile>().Include(data => data.MobileImages).Include(data => data.Color)
            .Where(data => data.ScreenSize >= model.First && data.ScreenSize <= model.Second)
            .ToListAsync();
    }

   
}

