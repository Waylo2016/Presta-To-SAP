using Microsoft.EntityFrameworkCore;
using PrestaToSap.model.Context;
using PrestaToSap.DTO;
using PrestaToSap.API;
using PrestaToSap.model;

namespace PrestaToSap.services;

public class PrestaOrderIngestionService
{
    private readonly PrestaApiService _prestaApiService;
    private readonly AppDbContext _db;
    private readonly ILogger<PrestaOrderIngestionService> _logger;

    public PrestaOrderIngestionService(
        PrestaApiService prestaApiService,
        AppDbContext db,
        ILogger<PrestaOrderIngestionService> logger)
    {
        _prestaApiService = prestaApiService;
        _db = db;
        _logger = logger;
    }

    public async Task SyncLastHourOrdersAsync()
    {
        string lastHour = GetLastHour();
        
        
        // region -> list of full order XML strings
        var detailsPerRegion = await _prestaApiService.GetOrderDetailsFromAllEndpointsAsync(lastHour);

        foreach (var (region, ordersXml) in detailsPerRegion)
        {
            if (ordersXml.Count == 0)
            {
                _logger.LogInformation($"Region {region}: no orders to sync.");
                continue;
            }

            _logger.LogInformation($"Region {region}: syncing {ordersXml.Count} orders.");

            foreach (var xml in ordersXml)
            {
                try
                {
                    // PrestaOrderXmlParser should return a DTO that matches your model fields
                    PrestaOrderDto dto = PrestaOrderXmlParser.ParseOrderXml(xml);
                    await InsertOrderAsync(region, dto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to parse or save order XML for region {Region}", region);
                }
            }
        }

        await _db.SaveChangesAsync();
    }

    private async Task InsertOrderAsync(string region, PrestaOrderDto dto)
    {
        // Correct uniqueness check: Presta order id + webshop
        Order? existing = await _db.Orders
            .Include(o => o.OrderRows)
            .FirstOrDefaultAsync(o => o.PrestaOrderId == dto.Id && o.Webshop == region);

        if (existing is null)
        {
            var entity = new Order
            {
                // Id is identity - do NOT set it
                PrestaOrderId = dto.Id,
                IdCustomer = dto.CustomerId,
                InvoiceNumber = dto.InvoiceNumber,
                Webshop = region,
                OrderRows = dto.Rows.Select(r => new OrderRow
                {
                    ProductId = r.ProductId,
                    ProductName = r.ProductName ?? string.Empty,
                    ProductReference = r.ProductReference ?? string.Empty,
                    ProductQuantity = r.Quantity
                }).ToList()
            };

            _logger.LogInformation("Inserting new order {PrestaOrderId} from region {Region}", entity.PrestaOrderId, region);
            _db.Orders.Add(entity);
        }
        else
        {
            // For now, we just skip if it already exists.
            _logger.LogInformation("Order {PrestaOrderId} from region {Region} already exists, skipping insert.", dto.Id, region);
        }
    }
    
    private string GetLastHour()
    {
        DateTime targetTime = DateTime.Now.AddHours(-1);
        string date = targetTime.ToString("yyyy-MM-dd");
        string hour = targetTime.ToString("HH");
        // return $"{date} {hour}:";
        // if you still need the test value, you could temporarily: return "2025-11-11";
        // test value!!
        return "2025-11-11";
    }
}