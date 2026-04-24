using BluesoftBank.Domain.Entidades;
using BluesoftBank.Domain.Interfaces;
using BluesoftBank.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Infrastructure.Repositorios;

public class CuentaRepository : ICuentaRepository
{
    private readonly AppDbContext _context;

    public CuentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta)
    {
        return await _context.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }

    public async Task CrearAsync(Cuenta cuenta)
    {
        await _context.Cuentas.AddAsync(cuenta);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Cuenta cuenta)
    {
        try
        {
            _context.Entry(cuenta).Property(x => x.RowVersion).OriginalValue = cuenta.RowVersion;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception("Conflicto de concurrencia detectado en la cuenta.");
        }
    }
}