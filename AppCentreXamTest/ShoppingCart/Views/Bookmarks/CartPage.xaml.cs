using Rg.Plugins.Popup.Services;
using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Bookmarks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Bookmarks
{
    /// <summary>
    /// Page to show the cart list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage
    {
        private bool fin;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage" /> class.
        /// </summary>
        public CartPage()
        {
            InitializeComponent();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            BindingContext = new CartPageViewModel(GetType().Name, cartDataService);
        }
        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();

             audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("6.3gpp");

            audServ.OnFinishedPlaying = () =>
            {
                if (!fin)
                {
                    fin = true;
                    audServ.Play("7.3gpp");
                }
            };
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name,true, "cart");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}