using Rg.Plugins.Popup.Services;
using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.AllCatalog
{
    /// <summary>
    /// The Category Tile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTilePageAll
    {
       
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTilePage" /> class.
        /// </summary>
        public CategoryTilePageAll()
        {
           
            InitializeComponent();

            var categoryDataService = App.MockDataService
                ? TypeLocator.Resolve<ICategoryDataService>()
                : TypeLocator.Resolve<ICategoryDataService>();

            this.BindingContext = new ViewModels.Catalog.CategoryPageViewModel(GetType().Name, categoryDataService, "");
        }

        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("4).3gpp");
        }
        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name,true, "Home");

            await PopupNavigation.Instance.PushAsync(page);
        }

    }
}