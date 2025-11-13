namespace PrestaToSap.API;
using System.Xml.Linq;
using PrestaToSap.DTO;

public static class PrestaOrderXmlParser
{
    
    
    public static PrestaOrderDto ParseOrderXml(string xml)
    {
        var doc = XDocument.Parse(xml);
        var order = doc.Root?.Element("order")
                    ?? throw new InvalidOperationException("XML does not contain <order> element.");

        var dto = new PrestaOrderDto
        {
            Id = (int?)order.Element("id") ?? 0,
            CustomerId = (int?)order.Element("id_customer") ?? 0,
            InvoiceNumber = (string?)order.Element("invoice_number"),
            InvoiceDate = ParseDate((string?)order.Element("invoice_date")),
            TotalPaid = ParseDecimal((string?)order.Element("total_paid"))
        };

        var rows = order
                       .Element("associations")?
                       .Element("order_rows")?
                       .Elements("order_row")
                   ?? Enumerable.Empty<XElement>();

        foreach (var row in rows)
        {
            var rowDto = new PrestaOrderRowDto
            {
                Id = (int?)row.Element("id") ?? 0,
                ProductId = (int?)row.Element("product_id") ?? 0,
                Quantity = (int?)row.Element("product_quantity") ?? 0,
                ProductName = (string?)row.Element("product_name"),
                ProductReference = (string?)row.Element("product_reference")
            };

            dto.Rows.Add(rowDto);
        }

        dto.TotalItems = dto.Rows.Sum(r => r.Quantity);

        return dto;
    }

    private static DateTime? ParseDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return DateTime.TryParse(value, out var dt) ? dt : null;
    }

    private static decimal ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0m;

        return decimal.TryParse(value, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var d)
            ? d
            : 0m;
    }
}