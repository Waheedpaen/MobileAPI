using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitofWork _unitOfWork; 
        private readonly ILoggerManager _logger;
        public BrandService(IUnitofWork unitOfWork , ILoggerManager logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Brand> Create(Brand model)
        {
            
            try
            {
                model.Updated_At = null;
                await _unitOfWork.BrandRepository.AddAsync(model);
                await _unitOfWork.CommitAsync();
                return model;
            }
            finally
            {
                _logger.LogExit();
            }
        }

        public async Task<Brand> Delete(Brand model)
        {
            try
            {
                _logger.LogEnter();
                model.IsDeleted = true;
                await _unitOfWork.CommitAsync();
                return model;
            }
            finally { _logger.LogExit(); }
        }

        public async Task<IEnumerable<Brand>> Get()
        {
            try
            {
                _logger.LogEnter();
                return await _unitOfWork.BrandRepository.GetAllAsync();
            }
            finally
            {
                _logger.LogExit();
            }
        }

        public async Task<Brand> Get(int id)
        {
            try
            {
                _logger.LogEnter();
                return await _unitOfWork.BrandRepository.GetByIdAsync(id);
            }
            finally
            {
                _logger.LogExit();
            }
        }

        public async Task<List<Brand>> SearchBrandData(string name)
        {
            return await _unitOfWork.BrandRepository.SearchBrandData(name);
         
        }

        public async Task<Brand> Update(Brand update, Brand model)
        {
            try
            {
                _logger?.LogEnter();
                update.Name = model.Name;
                update.ImageUrl = model.ImageUrl;
                update.Updated_At = model.Updated_At;
                await _unitOfWork.CommitAsync();
                return update;
            }
           finally { _logger?.LogExit(); }

        }
    }
}
