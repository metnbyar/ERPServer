using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExits = await productRepository.AnyAsync(p=>p.Name == request.Name,cancellationToken);
        if (isNameExits)
        {
            return Result<string>.Failure("Ürün Adı daha önce kullanılmış");
        }
        Product product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün Başarıyla Kaydedildi";

    }
}
