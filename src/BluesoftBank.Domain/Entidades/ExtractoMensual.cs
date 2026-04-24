namespace BluesoftBank.Domain.Entidades;

public class ExtractoMensual
{
    public string NumeroCuenta { get; private set; }
    public int Mes { get; private set; }
    public int Anio { get; private set; }
    public decimal? SaldoInicial { get; private set; }
    public decimal? SaldoFinal { get; private set; }
    public IReadOnlyList<Transaccion> Transacciones => _transacciones.AsReadOnly();

    private List<Transaccion> _transacciones = new();

    public ExtractoMensual(string numeroCuenta, int mes, int anio, decimal? saldoInicial, decimal? saldoFinal, List<Transaccion> transacciones)
    {
        NumeroCuenta = numeroCuenta;
        Mes = mes;
        Anio = anio;
        SaldoInicial = saldoInicial;
        SaldoFinal = saldoFinal;
        _transacciones = transacciones;
    }

    public ExtractoMensual(string numeroCuenta, int mes, int anio, List<Transaccion> transacciones)
    {
        NumeroCuenta = numeroCuenta;
        Mes = mes;
        Anio = anio;
        _transacciones = transacciones;
    }
    private ExtractoMensual() { }
}