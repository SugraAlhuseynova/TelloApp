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
    public class CartRepository : Repository<Cart>, ICartRepository 
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
