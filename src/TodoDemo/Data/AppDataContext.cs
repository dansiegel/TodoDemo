using AzureMobileClient.Helpers;
using TodoDemo.Helpers;
using TodoDemo.Models;
using DryIoc;

namespace TodoDemo.Data
{
    // To use authentication inherit from DryIocCloudServiceContext
    public class AppDataContext : DryIocCloudAppContext, IAppDataContext
    {
        public AppDataContext(IContainer container)
            : base(container)
        {

        }
        // Any ICloudSyncTable's that you have here will be automatically registered with the local store.
        public ICloudSyncTable<TodoItem> TodoItems => SyncTable<TodoItem>();
    }
}