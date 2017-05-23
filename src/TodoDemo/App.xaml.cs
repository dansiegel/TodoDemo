using System.Threading.Tasks;
using TodoDemo.Views;
using DryIoc;
using Prism.DryIoc;
using AzureMobileClient.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TodoDemo.Helpers;
using TodoDemo.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoDemo
{
    public partial class App : PrismApplication
    {
        public App() : base(null) { }

        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLogging();

            // determine the correct, supported .NET culture
            // set the RESX for resource localization
            // set the Thread for locale-aware methods
            var localize = Container.Resolve<i18n.ILocalize>();
            localize.SetLocale(Strings.Resources.Culture = localize.GetCurrentCultureInfo());

            await NavigationService.NavigateAsync("NavigationPage/MainPage?todo=Item1&todo=Item2&todo=Item3");
        }

        protected override void RegisterTypes()
        {
            // ICloudTable is only needed for Online Only data
            Container.Register(typeof(ICloudTable<>), typeof(AzureCloudTable<>));
            Container.Register(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>));

            // If you are not using Authentication
            Container.UseInstance<IMobileServiceClient>(new MobileServiceClient(AppConstants.AppServiceEndpoint));

            // If you are using Authentication
            // If using Facebook or some other 3rd Party OAuth provider be sure to register ILoginProvider
            // in IPlatformServices in your Platform Project. If you are using a custom auth provider, you may
            // be able to author an ILoginProvider from shared code.
            // Container.Register<IAzureCloudServiceOptions, TodoDemoServiceContextOptions>(Reuse.Singleton);
            var dataContext = new AppDataContext(Container);
            //Container.UseInstance<ICloudService>(dataContext);
            Container.UseInstance<IAppDataContext>(dataContext);
            Container.UseInstance<ICloudAppContext>(dataContext);
            // Container.Register<IMobileServiceClient>(reuse: Reuse.Singleton,
            //                                         made: Made.Of(() => Arg.Of<ICloudService>().Client));

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<TodoDetail>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetupLogging()
        {
            // By default, PrismApplication sets the logger to use the included DebugLogger,
            // which uses System.Diagnostics.Debug.WriteLine to print your message. If you have
            // overridden the default DebugLogger, you will need to update the Logger here to
            // ensure that any calls to your logger in the App.xaml.cs will use your logger rather
            // than the default DebugLogger.
            Logger = Container.Resolve<ILoggerFacade>();
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Logger.Log(e.Exception.ToString(), Category.Exception, Priority.High);
            };
        }
    }
}
