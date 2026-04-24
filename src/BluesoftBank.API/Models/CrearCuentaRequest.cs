namespace BluesoftBank.API.Models;
public class CrearCuentaRequest
{
    public string NumeroCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public string CiudadOrigen { get; set; }
}