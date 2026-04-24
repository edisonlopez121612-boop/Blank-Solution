using BluesoftBank.Domain.Entidades;

namespace BluesoftBank.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObtenerPorIdAsync(int clienteId);

    Task<List<Cliente>> ObtenerTodosAsync();
}