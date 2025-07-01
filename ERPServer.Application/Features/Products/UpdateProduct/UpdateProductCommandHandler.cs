using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (product.Name != request.Name)
        {
            bool isNameExits = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (isNameExits)
            {
                return Result<string>.Failure("Ürün Adı daha önce kullanılmış");
            }
        }

        mapper.Map(request, product);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ("Ürün Başarıyla Güncellendi");
    }
}