using BluesoftBank.Domain.Entidades;
using BluesoftBank.Domain.Interfaces;
using BluesoftBank.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Infrastructure.Repositorios;

public class TransaccionRepository : ITransaccionRepository
{
    private readonly AppDbContext _context;

    public TransaccionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(Transaccion transaccion)
    {
        await _context.Transacciones.AddAsync(transaccion);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaccion>> ObtenerPorCuentaAsync(string numeroCuenta)
    {
        return await _context.Transacciones
            .Where(t => t.NumeroCuenta == numeroCuenta)
            .OrderByDescending(t => t.Fecha)
            .ToListAsync();
    }
}