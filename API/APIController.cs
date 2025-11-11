namespace PrestaToSap.API;

public class APIController
{
    // get API links from appsettins.json
    public IConfigurationRoot Config()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return config;
    }
    
    public List<string> GetAPILinks(IConfigurationRoot config)
    {
        return config.GetSection("API URLs").Get<List<string>>();
    }
}