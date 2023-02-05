using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IUnitOfWork;
using Tello.Service.Apps.Admin.IServices;

namespace Tello.Service.Apps.Admin.Implementations
{
    public class ProductItemVariationService : IProductItemVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItemVariationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
