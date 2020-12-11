using Rg.Plugins.Popup.Services;
using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Catalog;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Catalog
{
    /// <summary>
    /// Page to show the catalog list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogListPage" /> class.
        /// </summary>
        public CatalogListPage(string selectedCategory)
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
            BindingContext = new CatalogPageViewModel(GetType().Name, catalogDataService, cartDataService, wishlistDataService,
                selectedCategory);
        }
        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();

             audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("6).3gpp");
        }
        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name, true, "category");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}