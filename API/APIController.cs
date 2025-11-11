namespace PrestaToSap.API;

public class APIController
{
    // get API links from appsettins.json
    private IConfigurationRoot Config()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return config;
    }
    
    public List<string> GetAPILinks()
    {
        var config = Config();
        return config.GetSection("API URLs").Get<List<string>>();
    }
    
    public Dictionary<string,string> GetApiOrders()
    {
        var config = Config();
        return config.GetSection("API OrderLinks").Get<Dictionary<string,string>>();
    }
}