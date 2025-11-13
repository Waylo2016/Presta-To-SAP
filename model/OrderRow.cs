namespace PrestaToSap.model;

public class OrderRow { 
    
    public int Id { get; set; } 

    // Foreign key to Order
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public int ProductId { get; set; }
    
    public int? ProductAttributeId { get; set; } 
    
    public int? ProductQuantity { get; set; } 
    
    public string? ProductName { get; set; } 
    
    public string? ProductReference { get; set; }
    
    public string? ProductEan13 { get; set; }
    
    public string? ProductIsbn{ get; set; }
    
    public string? ProductUpc { get; set; } 
    
    public decimal? ProductPrice { get; set; }
    
    public int? IdCustomization { get; set; } 
    
    public decimal? UnitPriceTaxIncl { get; set; } 
    
    public decimal? UnitPriceTaxExcl { get; set; }

    public Associations Associations { get; set; } = null!;

}