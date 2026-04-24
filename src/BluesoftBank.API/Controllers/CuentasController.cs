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

    [HttpPost("{NumeroCuenta}/consignar")]
    public async Task<IActionResult> Consignar(string NumeroCuenta, [FromBody] ConsignarRequest request)
    {
        await _service.ConsignarAsync(NumeroCuenta, request.ValorConsiignacion);
        return Ok();
    }
    [HttpPost("{NumeroCuenta}/retirar")]
    public async Task<IActionResult> Retirar(string NumeroCuenta, [FromBody] RetirarRequest request)
    {
        await _service.RetirarAsync(NumeroCuenta, request.ValorRetiro, request.CiudadOperacion);
        return Ok();
    }

    [HttpGet("{NumeroCuenta}/saldo")]
    public async Task<IActionResult> Saldo(string NumeroCuenta)
    {
        var saldo = await _service.ConsultarSaldoAsync(NumeroCuenta);
        return Ok(saldo);
    }

    [HttpGet("{NumeroCuenta}/movimientos")]
    public async Task<IActionResult> Movimientos(string NumeroCuenta)
    {
        var movimientos = await _service.ConsultarMovimientosAsync(NumeroCuenta);
        return Ok(movimientos);
    }

    [HttpGet("{NumeroCuenta}/extracto")]
    public async Task<IActionResult> Extracto(string NumeroCuenta, int mes, int anio)
    {
        var result = await _service.GenerarExtractoMensualAsync(NumeroCuenta, mes, anio);
        return Ok(result);
    }
}