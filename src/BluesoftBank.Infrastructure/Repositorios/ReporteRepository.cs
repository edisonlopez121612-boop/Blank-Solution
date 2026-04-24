using BluesoftBank.Domain.Enumeraciones;
using BluesoftBank.Domain.Interfaces;
using BluesoftBank.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Infrastructure.Repositorios;

public class ReporteRepository : IReporteRepository
{
    private readonly AppDbContext _context;

    public ReporteRepository(AppDbContext context)
    {
        _context = context;
    }

    // -----------------------------
    // CLIENTES POR TRANSACCIONES
    // -----------------------------
    public async Task<List<(string Cliente, int TotalTransacciones)>> ObtenerClientesConMasTransaccionesAsync(int mes, int anio)
    {
        var inicio = new DateTime(anio, mes, 1);
        var fin = inicio.AddMonths(1);

        var resultado = await _context.Transacciones
            .Where(t => t.Fecha >= inicio && t.Fecha < fin)
            .GroupBy(t => t.NumeroCuenta)
            .Select(g => new { Cliente = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .ToListAsync();

        return resultado.Select(x => (x.Cliente, x.Total)).ToList();
    }

    // -----------------------------
    // RETIROS FUERA CIUDAD
    // -----------------------------
    public async Task<List<(string Cliente, decimal TotalRetiros)>> ObtenerClientesConRetirosFueraCiudadAsync(decimal montoMinimo)
    {
        var transaccionesRaw = await _context.Transacciones.Where(t => t.TipoTransaccion == TipoTransaccion.Retiro && t.CiudadOperacion != t.Cuenta.CiudadOrigen)
            .Select(t => new { t.NumeroCuenta, t.Valor }).ToListAsync();

        var agrupado = transaccionesRaw.GroupBy(x => x.NumeroCuenta)
            .Select(g => new
            {
                Cliente = g.Key,
                Total = g.Sum(x => x.Valor)
            })
            .Where(x => x.Total > montoMinimo).OrderByDescending(x => x.Total).ToList();
        return agrupado.Select(x => (x.Cliente, x.Total)).ToList();
    }
}