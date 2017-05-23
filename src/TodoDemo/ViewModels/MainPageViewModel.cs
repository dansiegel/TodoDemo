using System;
using System.Linq;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TodoDemo.Models;
using PropertyChanged;
using TodoDemo.Data;
using TodoDemo.Strings;
using System.Threading.Tasks;

namespace TodoDemo.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageViewModel : BaseViewModel, INavigatedAware
    {
        private IPageDialogService _pageDialogService { get; }
        private INavigationService _navigationService { get; }
        private IAppDataContext _context { get; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IAppDataContext context)
        {
            _context = context;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            Title = Resources.TodoItems;
            TodoItems = new ObservableRangeCollection<TodoItem>();
            AddItemCommand = new DelegateCommand(OnAddCommandExecuted);
            RefreshCommand = new DelegateCommand(OnRefreshCommandExecuted);
            TodoItemTappedCommand = new DelegateCommand<TodoItem>(OnTodoItemTappedCommandExecuted);
        }

        public ObservableRangeCollection<TodoItem> TodoItems { get; set; }

        public DelegateCommand AddItemCommand { get; }

        public DelegateCommand RefreshCommand { get; }

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("refresh")) return;

            OnRefreshCommandExecuted();
        }

        private async Task UpdateListAsync()
        {
            await _context.TodoItems.SyncAsync();
            TodoItems.ReplaceRange(await _context.TodoItems.ReadAllItemsAsync());
        }

        private async void OnAddCommandExecuted() =>
            await _navigationService.PushPopupPageAsync("TodoDetail");

        private async void OnRefreshCommandExecuted()
        {
            IsBusy = true;
            await UpdateListAsync();
            IsBusy = false;
        }

        private async void OnTodoItemTappedCommandExecuted(TodoItem item) =>
            await _navigationService.PushPopupPageAsync("TodoDetail", new NavigationParameters { { "item", item } });
    }
}
