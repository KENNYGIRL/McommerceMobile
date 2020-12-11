using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Catalog;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Catalog
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductHomePage : ContentPage
    {
        private bool fin;

        public ProductHomePage()
        {
            InitializeComponent();
            var productHomeDataService = App.MockDataService
                ? TypeLocator.Resolve<IProductHomeDataService>()
                : DataService.TypeLocator.Resolve<IProductHomeDataService>();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            BindingContext = new ProductHomePageViewModel(productHomeDataService, catalogDataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("1.3gpp");

            audServ.OnFinishedPlaying = () =>
            {
                if (!fin)
                {
                    fin = true;
                    audServ.Play("2.3gpp");
                }
            };
        }
    }
}