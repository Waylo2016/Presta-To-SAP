namespace PrestaToSap.services;
using System.Xml.Linq;

public class PrestaApiService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _encodedApiKey;
    private readonly string _orderFilter;
    

    public PrestaApiService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        
        
        string _apiKey = _configuration["API Key"] ?? throw new InvalidOperationException("API Key not configured");
        byte[] apiKeyBytes = System.Text.Encoding.UTF8.GetBytes($"{_apiKey}:");
        _encodedApiKey = Convert.ToBase64String(apiKeyBytes);
        
        _orderFilter = _configuration["API OrderLinks:OrderFilter"] ?? throw new InvalidOperationException("Order Filter not configured");
    }

    /// <summary>
    /// Gets orderID's from all endpoints
    /// </summary>
    /// <param name="lastHour">string consisting of today's data and the last hour</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">u dun gun goofed ur API links</exception>
    public async Task<Dictionary<string, string>> GetOrdersFromAllEndpointsAsync(string lastHour)
    {
        
        Dictionary<string, string> apiUrls = _configuration.GetSection("API URLs").Get<Dictionary<string, string>>()
                                             ?? throw new InvalidOperationException("Api urls not configured");
        
        Dictionary<string, string> results = new();
        
        var httpClient = _clientFactory.CreateClient();
        
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {_encodedApiKey}");

        IEnumerable<Task<(string region, string? content, bool success, string? error)>> tasks = apiUrls.Select(async kvp =>
        {
            var (region, baseUrl) = (kvp.Key, kvp.Value);
            var fullUrl = $"{baseUrl}{_orderFilter}[{lastHour}]%";

            try
            {
                var response = await httpClient.GetAsync(fullUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                return (region, content, success: true, error: (string?)null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error calling {region}: {e.Message}");
                return (region, content: (string?)null, success: false, error: e.Message);
            }
        });

        var apiResults = await Task.WhenAll(tasks);

        foreach (var result in apiResults)
        {
            results[result.region] = result.success ? result.content! : $"Error: {result.error}";
        }
        
        
        return results;
    }
    
    
    /// <summary>
    /// For the given time filter, fetches:
    /// 1) order list XML per region
    /// 2) for each order id in that list, fetches full order XML
    /// Returns: region -> list of full order XML strings.
    /// </summary>
    public async Task<Dictionary<string, List<string>>> GetOrderDetailsFromAllEndpointsAsync(string lastHour)
    {
        // first get all orderIDs from all endpoints
        Dictionary<string, string> orderIdsPerRegion = await GetOrdersFromAllEndpointsAsync(lastHour);
        
        var apiUrls = _configuration.GetSection("API URLs").Get<Dictionary<string, string>>()
                      ?? throw new InvalidOperationException("Api urls not configured");
        
        var orderLinkPath = _configuration["API OrderLinks:OrderLink"]
                            ?? throw new InvalidOperationException("OrderLink not configured");
        
        var httpClient = _clientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {_encodedApiKey}");
        
        var result = new Dictionary<string, List<string>>();

        foreach (var (region, xml) in orderIdsPerRegion)
        {
            // Skip regions where the call failed
            if (xml.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Skipping {region} because of error: {xml}");
                continue;
            }

            if (!apiUrls.TryGetValue(region, out var baseUrl))
            {
                Console.WriteLine($"No base URL configured for region {region}, skipping.");
                continue;
            }

            // 2) Parse the list XML and extract order IDs
            var orderIds = ExtractOrderIds(xml);

            if (orderIds.Count == 0)
            {
                result[region] = new List<string>();
                continue;
            }

            // 3) For each order id, call baseUrl + orderLinkPath + id
            var detailTasks = orderIds.Select(async id =>
            {
                var detailUrl = $"{baseUrl}{orderLinkPath}{id}";
                var response = await httpClient.GetAsync(detailUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); // full order XML
            });

            var detailXmls = await Task.WhenAll(detailTasks);
            result[region] = detailXmls.ToList();
        }
        
        return result;
    }
    
    
    /// <summary>
    /// Parses your Presta XML like:
    /// &lt;prestashop&gt;&lt;orders&gt;&lt;order id="23995" xlink:href="..." /&gt;...&lt;/orders&gt;&lt;/prestashop&gt;
    /// and returns the list of IDs as strings.
    /// </summary>
    private static List<string> ExtractOrderIds(string xml)
    {
        var doc = XDocument.Parse(xml);

        var ids = doc
            .Descendants("order")
            .Select(x => (string?)x.Attribute("id"))
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id!)
            .ToList();

        return ids;
    }
}