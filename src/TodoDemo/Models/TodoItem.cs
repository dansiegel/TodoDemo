using System;
using AzureMobileClient.Helpers;
using PropertyChanged;

namespace TodoDemo.Models
{
    [ImplementPropertyChanged]
    public class TodoItem : EntityData
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }
    }
}