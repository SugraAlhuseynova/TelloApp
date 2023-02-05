using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Core.IRepositories;
using Tello.Data.DAL;

namespace Tello.Data.Repositories
{
    public class ProductItemVariationRepository : Repository<ProductItemVariation>, IProductItemVariationRepository
    {
        public ProductItemVariationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
