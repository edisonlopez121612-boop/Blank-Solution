using BluesoftBank.Application.DTOs;
using BluesoftBank.Domain.Enumeraciones;

namespace BluesoftBank.Application.Interfaces;

public interface ICuentaService
{
    Task CrearCuentaAsync(string numero, decimal saldoInicial, string ciudadOrigen);
    Task ConsignarAsync(string numeroCuenta, decimal valor);
    Task RetirarAsync(string numeroCuenta, decimal valor, string ciudadOperacion);
    Task<decimal> ConsultarSaldoAsync(string numeroCuenta);
    Task<List<MovimientoDto>> ConsultarMovimientosAsync(string numeroCuenta);
    Task<ExtractoMensualDto> GenerarExtractoMensualAsync(string numeroCuenta, int mes, int anio);
}