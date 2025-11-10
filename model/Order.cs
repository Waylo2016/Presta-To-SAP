namespace PrestaToSap.model;

public class Order {
    
    public int Id { get; set; }

    public int IdAddressDelivery { get; set; }

    public int IdAddressInvoice { get; set; }

    public int IdCart { get; set; }

    public int IdCurrency { get; set; }

    public int IdLang { get; set; }

    public int IdCustomer { get; set; }

    public int IdCarrier { get; set; }

    public int CurrentState { get; set; }

    public string Module { get; set; }

    public string InvoiceNumber { get; set; }

    public DateTime InvoiceDate { get; set; }

    public int DeliveryNumber { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int Valid { get; set; }

    public DateTime DateAdd { get; set; }

    public DateTime DateUpd { get; set; }

    public string ShippingNumber { get; set; }

    public string Note { get; set; }

    public int IdShopGroup { get; set; }

    public int IdShop { get; set; }

    public string SecureKey { get; set; }

    public string Payment { get; set; }

    public int Recyclable { get; set; }

    public int Gift { get; set; }

    public string GiftMessage { get; set; }

    public string MobileTheme { get; set; }

    public decimal TotalDiscounts { get; set; }

    public decimal TotalDiscountsTaxIncl { get; set; }

    public decimal TotalDiscountsTaxExcl { get; set; }

    public decimal TotalPaid { get; set; }

    public decimal TotalPaidTaxIncl { get; set; }

    public decimal TotalPaidTaxExcl { get; set; }

    public decimal TotalPaidReal { get; set; }

    public decimal TotalProducts { get; set; }

    public decimal TotalProductsWt { get; set; }

    public decimal TotalShipping { get; set; }

    public decimal TotalShippingTaxIncl { get; set; }

    public decimal TotalShippingTaxExcl { get; set; }

    public decimal CarrierTaxRate { get; set; }

    public decimal TotalWrapping { get; set; }

    public decimal TotalWrappingTaxIncl { get; set; }

    public decimal TotalWrappingTaxExcl { get; set; }

    public int RoundMode { get; set; }

    public int RoundType { get; set; }

    public decimal ConversionRate { get; set; }

    public int Reference { get; set; }
    
    public string CRNInvoiceNumber { get; set; }
    
    public string CRNDeliveryNumber { get; set; }

    public Prestashop prestashop { get; set; } = null!;

    public Associations? Associations { get; set; }

    public ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
}