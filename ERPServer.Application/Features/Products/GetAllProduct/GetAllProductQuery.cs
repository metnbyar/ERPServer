using ERPServer.Application.Features.Depots.GetAllDepot;
using ERPServer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQuery() : IRequest<Result<List<Product>>>;
