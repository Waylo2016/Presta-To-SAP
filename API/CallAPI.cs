namespace PrestaToSap.API;

public class CallAPI
{
    
    public void CallOrders()
    {
        APIController apiController = new();
        var config = apiController.Config();
        List<string> apiLinks = apiController.GetAPILinks(config);

        foreach (var apiLink in apiLinks)
        {
            
        }
        
    }
}

