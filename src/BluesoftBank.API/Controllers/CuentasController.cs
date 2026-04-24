using BluesoftBank.API.Models;
using BluesoftBank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BluesoftBank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _service;

    public CuentasController(ICuentaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCuentaRequest request)
    {
        await _service.CrearCuentaAsync(request.NumeroCuenta, request.SaldoInicial, request.CiudadOrigen);

        return Ok();
    }

    [HttpPost("{numero}/consignar")]
    public async Task<IActionResult> Consignar(string numero, [FromBody] ConsignarRequest request)
    {
        await _service.ConsignarAsync(numero, request.SaldoInicial);
        return Ok();
    }
    [HttpPost("{numero}/retirar")]
    public async Task<IActionResult> Retirar(string numero, [FromBody] RetirarRequest request)
    {
        await _service.RetirarAsync(numero, request.Valor, request.CiudadOperacion);
        return Ok();
    }

    [HttpGet("{numero}/saldo")]
    public async Task<IActionResult> Saldo(string numero)
    {
        var saldo = await _service.ConsultarSaldoAsync(numero);
        return Ok(saldo);
    }

    [HttpGet("{numero}/movimientos")]
    public async Task<IActionResult> Movimientos(string numero)
    {
        var movimientos = await _service.ConsultarMovimientosAsync(numero);
        return Ok(movimientos);
    }

    [HttpGet("{numero}/extracto")]
    public async Task<IActionResult> Extracto(string numero, int mes, int anio)
    {
        var result = await _service.GenerarExtractoMensualAsync(numero, mes, anio);
        return Ok(result);
    }
}