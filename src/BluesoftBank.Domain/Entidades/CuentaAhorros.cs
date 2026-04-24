using BluesoftBank.Domain.Entidades;

namespace BluesoftBank.Domain.Entidades
{
    public class CuentaAhorros : Cuenta
    {
        public CuentaAhorros(string numero, decimal saldo, string ciudad) : base(numero, saldo, ciudad)
        {
        }

        private CuentaAhorros() { }
    }
}