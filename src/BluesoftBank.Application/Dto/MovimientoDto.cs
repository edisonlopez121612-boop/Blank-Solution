namespace BluesoftBank.Application.DTOs;

public class MovimientoDto
{
    public string Tipo { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime Fecha { get; set; }
}