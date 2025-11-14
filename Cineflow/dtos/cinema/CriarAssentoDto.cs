namespace Cineflow.dtos.cinema;

public class CriarAssentoDto
{
    public int Id_sala { get; set; }
    public char? fila { get; set; }
    public int? numero { get; set; }
    public bool status { get; set; }
}