using BluesoftBank.Application.DTOs;
using BluesoftBank.Application.Interfaces;
using BluesoftBank.Domain.Entidades;
using BluesoftBank.Domain.Enumeraciones;
using BluesoftBank.Domain.Excepciones;
using BluesoftBank.Domain.Interfaces;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ITransaccionRepository _transaccionRepository;
    private readonly IExtractoRepository _extractoRepository;

    public CuentaService(ICuentaRepository cuentaRepository, ITransaccionRepository transaccionRepository, IExtractoRepository extractoRepository)
    {
        _cuentaRepository = cuentaRepository;
        _transaccionRepository = transaccionRepository;
        _extractoRepository = extractoRepository;
    }

    public async Task CrearCuentaAsync(string numero, decimal saldoInicial, string ciudadOrigen)
    {
        var cuenta = new Cuenta(numero, saldoInicial, ciudadOrigen);
        await _cuentaRepository.CrearAsync(cuenta);
    }
    public async Task ConsignarAsync(string numeroCuenta, decimal valor)
    {
        var cuenta = await _cuentaRepository.ObtenerPorNumeroAsync(numeroCuenta);
        if (cuenta is null)
            throw new CuentaNoExisteException(numeroCuenta);
        cuenta.Consignar(valor);
        await _cuentaRepository.ActualizarAsync(cuenta);
        await _transaccionRepository.RegistrarAsync(new Transaccion(numeroCuenta, valor, TipoTransaccion.Consignacion, cuenta.CiudadOrigen)
        );
    }

    public async Task RetirarAsync(string numeroCuenta, decimal valor, string ciudadOperacion)
    {
        var cuenta = await _cuentaRepository.ObtenerPorNumeroAsync(numeroCuenta);

        if (cuenta is null)
            throw new CuentaNoExisteException(numeroCuenta);
        cuenta.Retirar(valor);
        await _cuentaRepository.ActualizarAsync(cuenta);
        await _transaccionRepository.RegistrarAsync(new Transaccion(numeroCuenta, valor, TipoTransaccion.Retiro, ciudadOperacion));
    }

    public async Task<decimal> ConsultarSaldoAsync(string numeroCuenta)
    {
        var cuenta = await _cuentaRepository.ObtenerPorNumeroAsync(numeroCuenta) ?? throw new CuentaNoExisteException(numeroCuenta);
        return cuenta.Saldo;
    }

    public async Task<List<MovimientoDto>> ConsultarMovimientosAsync(string numeroCuenta)
    {
        var movimientos = await _transaccionRepository.ObtenerPorCuentaAsync(numeroCuenta);

        return movimientos.Select(t => new MovimientoDto
        {
            Tipo = t.TipoTransaccion.ToString(),
            Valor = t.Valor,
            Fecha = t.Fecha
        }).ToList();
    }

    public async Task<ExtractoMensualDto> GenerarExtractoMensualAsync(string numeroCuenta, int mes, int anio)
    {
        var extracto = await _extractoRepository.ObtenerExtractoMensualAsync(numeroCuenta, mes, anio);
        var movimientos = extracto.Transacciones.OrderBy(t => t.Fecha)
            .Select(t => new MovimientoDto
            {
                Tipo = t.TipoTransaccion.ToString(),
                Valor = t.Valor,
                Fecha = t.Fecha
            })
            .ToList();

        var dto = new ExtractoMensualDto
        {
            NumeroCuenta = extracto.NumeroCuenta,
            Mes = extracto.Mes,
            Anio = extracto.Anio,
            Movimientos = movimientos
        };

        if (movimientos.Any())
        {
            dto.SaldoInicial = extracto.SaldoInicial;
            dto.SaldoFinal = extracto.SaldoFinal;
        }
        return dto;
    }
}