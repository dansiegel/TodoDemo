using Rg.Plugins.Popup.Pages;

namespace TodoDemo.Views
{
    public partial class TodoDetail : PopupPage
    {
        public TodoDetail()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}