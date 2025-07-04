﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product, ApplicationDbContext>,IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
