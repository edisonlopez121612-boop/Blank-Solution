using BluesoftBank.Domain.Entidades;

namespace BluesoftBank.Domain.Interfaces;

public interface ICuentaRepository
{
    Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta);
    Task CrearAsync(Cuenta cuenta);
    Task ActualizarAsync(Cuenta cuenta);
}