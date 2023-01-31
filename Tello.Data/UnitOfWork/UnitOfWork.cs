using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;
using Tello.Core.IUnitOfWork;
using Tello.Data.DAL;
using Tello.Data.Repositories;

namespace Tello.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private CategoryRepository _categoryRepository;
        private BrandRepository _brandRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
        public IBrandRepository BrandRepository => _brandRepository?? new BrandRepository(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }   
}
