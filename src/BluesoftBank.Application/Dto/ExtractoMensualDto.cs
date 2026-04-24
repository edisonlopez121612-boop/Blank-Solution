using BluesoftBank.Application.DTOs;

namespace BluesoftBank.Application.DTOs;

public class ExtractoMensualDto
{
    public string NumeroCuenta { get; set; } = string.Empty;
    public int Mes { get; set; }
    public int Anio { get; set; }

    public decimal? SaldoInicial { get; set; }
    public decimal? SaldoFinal { get; set; }

    public List<MovimientoDto> Movimientos { get; set; } = new();
}