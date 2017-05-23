using AzureMobileClient.Helpers;
using TodoDemo.Helpers;

namespace TodoDemo.Data
{
    // This is only needed when you are using Authentication with the Azure Mobile Client
    public class TodoDemoServiceContextOptions : IAzureCloudServiceOptions
    {
        public string AppServiceEndpoint => AppConstants.AppServiceEndpoint;
        
        public string AlternateLoginHost => AppConstants.AlternateLoginHost;

        public string LoginUriPrefix => AppConstants.LoginUriPrefix;
    }
}