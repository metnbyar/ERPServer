﻿using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;


namespace ERPServer.Application.Features.Customers.GetAllCustomer
{
    public sealed record class GetAllCustomerQuery(): IRequest<Result<List<Customer>>>;
}
