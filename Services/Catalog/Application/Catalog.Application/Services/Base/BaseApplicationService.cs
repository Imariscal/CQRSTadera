using Microsoft.Extensions.Logging; 
using Catalog.Application.BussinesProcess.Base.Contracts;
using Catalog.Application.Services.Base.Contracts;
using Catalog.Application.DTOs.Contracts;
using Catalog.Domain.Entities.Base.Contracts;

namespace OxxoGas.Catalogos.Retail.Application.Services.Base;

public abstract class BaseApplicationService<BaseDTO, TEntity, TKey>(
    IMediator mediator,
    ILogger<IBaseApplicationService<BaseDTO>> logger) : IBaseApplicationService<BaseDTO> 
        where BaseDTO : class, IBaseDTO
        where TEntity : class, IBaseEntity<TKey>
{
    protected readonly IMediator _mediator = mediator;
    protected readonly ILogger<IBaseApplicationService<BaseDTO>> _logger = logger;
}