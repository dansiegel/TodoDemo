using AzureMobileClient.Helpers;
using TodoDemo.Models;

namespace TodoDemo.Data
{
    public interface IAppDataContext
    {
        ICloudSyncTable<TodoItem> TodoItems { get; }
    }
}