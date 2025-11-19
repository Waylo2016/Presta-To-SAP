namespace PrestaToSap.DTO;

public class PrestaOrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal TotalPaid { get; set; }
    public int TotalItems { get; set; }
    public List<PrestaOrderRowDto> Rows { get; set; } = new();
}