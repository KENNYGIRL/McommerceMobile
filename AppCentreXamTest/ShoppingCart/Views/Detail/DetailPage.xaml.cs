using Rg.Plugins.Popup.Services;
using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.Models;
using ShoppingCart.ViewModels.Detail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Detail
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage
    {
        private bool fin;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPage" /> class.
        /// </summary>
        public DetailPage(Product selectedProduct)
        {
            InitializeComponent();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            var wishlistDataService = App.MockDataService
                ? TypeLocator.Resolve<IWishlistDataService>()
                : DataService.TypeLocator.Resolve<IWishlistDataService>();
            BindingContext = new DetailPageViewModel(GetType().Name, catalogDataService, cartDataService, wishlistDataService,
                selectedProduct);
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
                Rotator.ItemTemplate = (DataTemplate)Resources["LandscapeTemplate"];
            else
                Rotator.ItemTemplate = (DataTemplate)Resources["PortraitTemplate"];
        }

        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var product = (DetailPageViewModel)BindingContext;

            var productNarration = product.ProductDetail.Narration;

            audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play($"{productNarration}");

            audServ.OnFinishedPlaying = () =>
            {
                if (!fin)
                {
                    fin = true;
                    audServ.Play("10).3gpp");
                }
                

            };
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name,true, "singleItem");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}