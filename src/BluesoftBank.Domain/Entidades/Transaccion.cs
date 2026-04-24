using BluesoftBank.Domain.Enumeraciones;
using BluesoftBank.Domain.Excepciones;

using System;

namespace BluesoftBank.Domain.Entidades;

public class Transaccion
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Valor { get; set; }

    public TipoTransaccion TipoTransaccion { get; set; }

    public string CiudadOperacion { get; set; } = string.Empty;
    public string NumeroCuenta { get; set; } = string.Empty;
    public Cuenta Cuenta { get; set; } = null!;

    public Transaccion(string numeroCuenta, decimal valor, TipoTransaccion tipoTransaccion, string ciudadOperacion)
    {
        if (string.IsNullOrWhiteSpace(numeroCuenta))
            throw new ArgumentException("Número de cuenta inválido");

        if (string.IsNullOrWhiteSpace(ciudadOperacion))
            throw new ArgumentException("Ciudad inválida");

        if (valor <= 0)
            throw new ValorNoPermitidoException();

        NumeroCuenta = numeroCuenta;
        Valor = valor;
        TipoTransaccion = tipoTransaccion;
        CiudadOperacion = ciudadOperacion;
        Fecha = DateTime.UtcNow;
    }

    private Transaccion() { }
}