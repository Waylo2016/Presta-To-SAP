using PrestaToSap.services;

namespace PrestaToSap.API;

public class CallAPI
{
    private readonly PrestaApiService _prestaApiService;
    private readonly ILogger<CallAPI> _logger;

    public CallAPI(PrestaApiService prestaApiService, ILogger<CallAPI> logger)
    {
        _prestaApiService = prestaApiService;
        _logger = logger;
    }
/*
    public async Task<Dictionary<string, List<string>>> CallOrdersAsync()
    {
        string lastHour = GetLastHour();
        
        Dictionary<string, List<string>> results = await _prestaApiService.GetOrderDetailsFromAllEndpointsAsync(lastHour);
        return results;
    }
    */

}

