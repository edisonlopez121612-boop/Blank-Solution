namespace BluesoftBank.Domain.Excepciones;

public abstract class DomainException(string mensaje) : Exception(mensaje)
{
}

public class SaldoInsuficienteException : DomainException
{
    public SaldoInsuficienteException() : base("Saldo insuficiente para realizar la operación") { }
}

public class ValorNoPermitidoException : DomainException
{
    public ValorNoPermitidoException() : base("El valor debe ser mayor a cero") { }
}