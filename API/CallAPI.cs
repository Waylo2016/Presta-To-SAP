namespace PrestaToSap.API;

public class CallAPI
{
    
    public void CallOrders()
    {
        APIController apiController = new();
        // keep existing calls; APIController should read appsettings
        var apiLinks = apiController.GetAPILinks();
        var apiOrderFilters = apiController.GetApiOrders();
        
        //TODO: call API and upload the data into the database
        
        // I don't want to do it rn ueueueueue
        foreach (var kv in apiOrderFilters)
        {
            
        }
        
    }
}

