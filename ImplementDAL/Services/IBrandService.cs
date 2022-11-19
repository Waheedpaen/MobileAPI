 

namespace ImplementDAL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitofWork _unitOfWork; 
      
        public BrandService(IUnitofWork unitOfWork  )
        {
          
            _unitOfWork = unitOfWork;
        }

        public async Task<Brand> BrandNameAlreadyExit(string name)
        {
             return await _unitOfWork.BrandRepository.BrandNameAlreadyExit(name);
        }

        public async Task<Brand> Create(Brand model)
        {
            
           
                model.Updated_At = null;
                await _unitOfWork.BrandRepository.AddAsync(model);
                await _unitOfWork.CommitAsync();
                return model;
           
          
        }

        public async Task<Brand> Delete(Brand model)
        {
           
                model.IsDeleted = true;
                await _unitOfWork.CommitAsync();
                return model;
           
        }

        public async Task<IEnumerable<Brand>> Get()
        {
             
                return await _unitOfWork.BrandRepository.GetAllAsync();
            
        }

        public async Task<Brand> Get(int id)
        {
          
                return await _unitOfWork.BrandRepository.GetByIdAsync(id);
             
        }

        public async Task<List<Brand>> SearchBrandData(string name)
        {
            return await _unitOfWork.BrandRepository.SearchBrandData(name);
         
        }

        public async Task<Brand> Update(Brand update, Brand model)
        {
         
                update.Name = model.Name;
                update.ImageUrl = model.ImageUrl;
                update.Updated_At = model.Updated_At;
                await _unitOfWork.CommitAsync();
                return update;
        

        }
    }
}
