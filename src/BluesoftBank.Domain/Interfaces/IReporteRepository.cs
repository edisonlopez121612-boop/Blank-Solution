namespace BluesoftBank.Domain.Interfaces;

public interface IReporteRepository
{
    Task<List<(string Cliente, int TotalTransacciones)>> ObtenerClientesConMasTransaccionesAsync(int mes, int anio);

    Task<List<(string Cliente, decimal TotalRetiros)>> ObtenerClientesConRetirosFueraCiudadAsync(decimal montoMinimo);
}