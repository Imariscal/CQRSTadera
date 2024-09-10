using Catalog.Application.DTOs.Contracts;
namespace OxxoGas.Catalogos.Retail.Application.DTOs.Base;

public abstract class BaseCrudDTO<T> : IBaseCrudDTO<T>
{
    public required T Id { get; set; }
}