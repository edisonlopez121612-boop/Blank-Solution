using BluesoftBank.Application.DTOs;

namespace BluesoftBank.Application.Interfaces;

public interface IReporteService
{
    Task<List<ClienteRetirosExternoDto>> ClientesConRetirosFueraCiudadAsync(decimal montoMinimo);

    Task<List<ClienteTransaccionesDto>> ClientesConMasTransaccionesAsync(int mes, int anio);
}