namespace BluesoftBank.Domain.Entidades;

public class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string CiudadOrigen { get; set; } = string.Empty;

    public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}