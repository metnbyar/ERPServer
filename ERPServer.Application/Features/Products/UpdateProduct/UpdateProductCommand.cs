using ERPServer.Application.Features.Depots.UpdateDepot;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(
  Guid Id,
  string Name,
  int TypeValue) : IRequest<Result<string>>;
