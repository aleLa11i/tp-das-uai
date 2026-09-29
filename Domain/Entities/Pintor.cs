namespace Domain.Entities;

public class Pintor
{

    public Pintor(string nombre, string apellido, bool isDeleted)
    {
        Nombre = nombre;
        Apellido = apellido;
        IsDeleted = isDeleted;
    }

    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

}
