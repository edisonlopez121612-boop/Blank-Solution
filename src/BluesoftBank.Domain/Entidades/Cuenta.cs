using BluesoftBank.Domain.Entidades;
using BluesoftBank.Domain.Excepciones;
using System.ComponentModel.DataAnnotations;

public class Cuenta
{
    public string NumeroCuenta { get; private set; } = string.Empty;
    public decimal Saldo { get; private set; }
    public string CiudadOrigen { get; private set; } = string.Empty;
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    private readonly List<Transaccion> _transacciones = new();
    public IReadOnlyCollection<Transaccion> Transacciones => _transacciones;

    protected Cuenta() { }

    public Cuenta(string numeroCuenta, decimal saldoInicial, string ciudadOrigen)
    {
        NumeroCuenta = numeroCuenta;
        Saldo = saldoInicial;
        CiudadOrigen = ciudadOrigen;
    }

    public void Consignar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor inválido");
        Saldo += valor;
    }

    public void Retirar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor inválido");

        if (Saldo < valor)
            throw new SaldoInsuficienteException();
        Saldo -= valor;
    }
}