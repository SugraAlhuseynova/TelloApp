using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.DTOs.SlideDTOs;
using Tello.Service.Apps.Admin.IServices;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(SlidePostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SlideGetDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SlideGetDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SlidePostDto slidePostDto)
        {
            throw new NotImplementedException();
        }
    }
}
