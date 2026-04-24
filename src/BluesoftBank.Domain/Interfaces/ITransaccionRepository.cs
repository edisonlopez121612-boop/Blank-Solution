using BluesoftBank.Domain.Entidades;

namespace BluesoftBank.Domain.Interfaces;

public interface ITransaccionRepository
{
    Task RegistrarAsync(Transaccion transaccion);

    Task<List<Transaccion>> ObtenerPorCuentaAsync(string numeroCuenta);
}