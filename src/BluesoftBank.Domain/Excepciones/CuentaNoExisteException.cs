namespace BluesoftBank.Domain.Excepciones;

public class CuentaNoExisteException : DomainException
{
    public CuentaNoExisteException(string numeroCuenta) : base($"La cuenta {numeroCuenta} no existe") { }
}