using BluesoftBank.Domain.Entidades;

namespace BluesoftBank.Domain.Interfaces;

public interface IExtractoRepository
{
    Task<ExtractoMensual> ObtenerExtractoMensualAsync(string numeroCuenta, int mes, int anio);
}