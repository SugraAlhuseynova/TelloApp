using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        private SlideRepository _slideRepository;
        private VariationRepository _variationRepository;
        private VariationCategoryRepository _variationCategoryRepository;
        private VariationOptionRepository _variationOptionRepository;
        private ProductRepository _productRepository;
        private ProductItemRepository _productItemRepository;
        private ProductItemVariationRepository _productItemVariationRepository;
        private UserRepository _userRepository;
        private SettingRepository _settingRepository;
        private CommentRepository _commentRepository;
        private CartRepository _cartRepository;
        private ProductOrderRepository _productOrderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
        public IBrandRepository BrandRepository => _brandRepository ?? new BrandRepository(_context);
        public ISlideRepository SlideRepository => _slideRepository ?? new SlideRepository(_context);
        public IVariationRepository VariationRepository => _variationRepository ?? new VariationRepository(_context);
        public IVariationCategoryRepository VariationCategoryRepository => _variationCategoryRepository ?? new VariationCategoryRepository(_context);
        public IVariationOptionRepository VariationOptionRepository => _variationOptionRepository ?? new VariationOptionRepository(_context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
        public IProductItemRepository ProductItemRepository => _productItemRepository ?? new ProductItemRepository(_context);
        public IProductItemVariationRepository ProductItemVariationRepository => _productItemVariationRepository ?? new ProductItemVariationRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public ISettingRepository SettingRepository => _settingRepository ?? new SettingRepository(_context);
        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(_context);
        public ICartRepository CartRepository => _cartRepository ?? new CartRepository(_context);
        public IProductOrderRepository ProductOrderRepository => _productOrderRepository ?? new ProductOrderRepository(_context);

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
