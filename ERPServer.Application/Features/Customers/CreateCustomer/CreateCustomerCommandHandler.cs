﻿using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler (
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
       bool isTaxNumberExits= await customerRepository.AnyAsync(p=>p.TaxNumber==request.TaxNumber,cancellationToken);

        if(isTaxNumberExits) 
        {
            return Result<string>.Failure("Vergi numarası daha önceden kaydedilmiş");
        }
        Customer customer = mapper.Map<Customer>(request);

        await customerRepository.AddAsync(customer,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Müşteri Kaydı Başarıyla Tamamlandı";

    }
}


