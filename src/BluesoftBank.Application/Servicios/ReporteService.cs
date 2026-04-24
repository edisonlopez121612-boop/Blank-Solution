using BluesoftBank.Application.DTOs;
using BluesoftBank.Application.Interfaces;
using BluesoftBank.Domain.Interfaces;

namespace BluesoftBank.Application.Servicios;

public class ReporteService : IReporteService
{
    private readonly IReporteRepository _repo;

    public ReporteService(IReporteRepository repo)
    {
        _repo = repo;
    }

    // -----------------------------
    // TRANSACCIONES POR MES
    // -----------------------------
    public async Task<List<ClienteTransaccionesDto>> ClientesConMasTransaccionesAsync(int mes, int anio)
    {
        var data = await _repo.ObtenerClientesConMasTransaccionesAsync(mes, anio);

        return data.Select(x => new ClienteTransaccionesDto
        {
            NumeroCuenta = x.Cliente,
            CantidadTransacciones = x.TotalTransacciones
        }).ToList();
    }

    // -----------------------------
    // RETIROS FUERA CIUDAD
    // -----------------------------
    public async Task<List<ClienteRetirosExternoDto>> ClientesConRetirosFueraCiudadAsync(decimal montoMinimo)
    {
        var data = await _repo.ObtenerClientesConRetirosFueraCiudadAsync(montoMinimo);

        return data.Select(x => new ClienteRetirosExternoDto
        {
            NumeroCuenta = x.Cliente,
            TotalRetiros = x.TotalRetiros
        }).ToList();
    }
}