using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.DeleteCustomerById
{
    internal sealed class DeleteCustomerByIdCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerByIdCommand, Result<String>>
    {
        public async Task<Result<string>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            Customer customer = await customerRepository.GetByExpressionAsync(p=>p.Id==request.Id, cancellationToken);
            if (customer is null)
            {
                return Result<String>.Failure("Müşteri Bulunamadı");
            }
            customerRepository.Delete(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Müşteri Başarıyla Silindi";
        }

    }
}
