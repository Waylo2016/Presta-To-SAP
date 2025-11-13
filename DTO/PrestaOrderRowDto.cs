namespace PrestaToSap.DTO;

public class PrestaOrderRowDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
    public string? ProductReference { get; set; }
}