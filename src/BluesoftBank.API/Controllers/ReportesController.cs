using BluesoftBank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BluesoftBank.API.Controllers;

[ApiController]
[Route("api/reportes")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet("retiros-externos")]
    public async Task<IActionResult> RetirosExternos(decimal minimo = 1000000)
    {
        var result = await _reporteService.ClientesConRetirosFueraCiudadAsync(minimo);
        return Ok(result);
    }

    [HttpGet("mas-transacciones")]
    public async Task<IActionResult> MasTransacciones(int mes, int anio)
    {
        var result = await _reporteService.ClientesConMasTransaccionesAsync(mes, anio);
        return Ok(result);
    }
}