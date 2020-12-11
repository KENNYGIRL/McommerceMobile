using Rg.Plugins.Popup.Services;
using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Transaction;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Transaction
{
    /// <summary>
    /// Page to show the Checkout details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutPage" /> class.
        /// </summary>
        public CheckoutPage()
        {
            InitializeComponent();
            var userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            BindingContext = new CheckoutPageViewModel(GetType().Name, userDataService, cartDataService, catalogDataService);
        }
        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();

             audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("8.3gpp");          
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name,true, "pay");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}