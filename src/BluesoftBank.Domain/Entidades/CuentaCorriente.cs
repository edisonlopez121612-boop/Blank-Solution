namespace BluesoftBank.Domain.Entidades;
public class CuentaCorriente : Cuenta
{
    public CuentaCorriente(string numero, decimal saldo, string ciudad) : base(numero, saldo, ciudad)
    {
    }

    private CuentaCorriente() { }
}