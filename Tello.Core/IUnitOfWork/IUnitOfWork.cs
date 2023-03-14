using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.IRepositories;

namespace Tello.Core.IUnitOfWork
{
    public interface IUnitOfWork
    {
        ISettingRepository SettingRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        ISlideRepository SlideRepository { get; }
        IVariationRepository VariationRepository { get; }
        IVariationCategoryRepository VariationCategoryRepository { get; }
        IVariationOptionRepository VariationOptionRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductItemRepository ProductItemRepository { get; }
        IProductItemVariationRepository ProductItemVariationRepository { get; }
        IUserRepository UserRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICardRepository CardRepository { get; }
        IProductOrderRepository ProductOrderRepository { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
