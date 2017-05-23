using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using TodoDemo.Data;
using TodoDemo.Models;
using TodoDemo.Strings;

namespace TodoDemo.ViewModels
{
    [ImplementPropertyChanged]
    public class TodoDetailViewModel : BaseViewModel, INavigatingAware
    {
        private INavigationService _navigationService { get; }

        private IAppDataContext _context { get; }

        public TodoDetailViewModel(INavigationService navigationService, IAppDataContext context)
        {
            _context = context;
            _navigationService = navigationService;
            Title = Resources.TodoDetail;
            CancelCommand = new DelegateCommand(OnCancelCommandExecuted);
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted, () => IsNotBusy).ObservesProperty(() => IsBusy);
        }

        public TodoItem Model { get; set; }

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand SaveCommand { get; }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Model = parameters.GetValue<TodoItem>("item") ?? new TodoItem();
        }

        private async void OnCancelCommandExecuted() =>
            await _navigationService.PopupGoBackAsync("refresh", false);

        private async void OnSaveCommandExecuted()
        {
            IsBusy = true;
            if (string.IsNullOrWhiteSpace(Model.Id))
            {
                await _context.TodoItems.CreateItemAsync(Model);
            }
            else
            {
                await _context.TodoItems.UpdateItemAsync(Model);
            }

            await _navigationService.PopupGoBackAsync();
            IsNotBusy = false;
        }
    }
}