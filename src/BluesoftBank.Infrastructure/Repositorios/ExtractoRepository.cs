using BluesoftBank.Domain.Entidades;
using BluesoftBank.Domain.Enumeraciones;
using BluesoftBank.Domain.Interfaces;
using BluesoftBank.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Infrastructure.Repositorios;

public class ExtractoRepository : IExtractoRepository
{
    private readonly AppDbContext _context;

    public ExtractoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ExtractoMensual> ObtenerExtractoMensualAsync(string numeroCuenta, int mes, int anio)
    {
        var inicio = new DateTime(anio, mes, 1);
        var fin = inicio.AddMonths(1);

        var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta) ?? throw new Exception("Cuenta no existe");
        var transacciones = await _context.Transacciones.Where(t => t.NumeroCuenta == numeroCuenta && t.Fecha >= inicio && t.Fecha < fin).OrderBy(t => t.Fecha).ToListAsync();
        if (!transacciones.Any())
        {
            return new ExtractoMensual(numeroCuenta, mes, anio, transacciones);
        }
        var saldoFinal = cuenta.Saldo;
        decimal saldoInicial = saldoFinal;
        foreach (var t in transacciones.AsEnumerable().Reverse())
        {
            if (t.TipoTransaccion == TipoTransaccion.Consignacion)
                saldoInicial -= t.Valor;
            else
                saldoInicial += t.Valor;
        }
        return new ExtractoMensual(numeroCuenta, mes, anio, saldoInicial, saldoFinal, transacciones);
    }
}