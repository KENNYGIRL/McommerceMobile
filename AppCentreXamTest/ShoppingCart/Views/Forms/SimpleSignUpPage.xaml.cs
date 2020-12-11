using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Forms
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleSignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSignUpPage" /> class.
        /// </summary>
        public SimpleSignUpPage()
        {
            InitializeComponent();
            var userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            BindingContext = new SignUpPageViewModel(userDataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            var audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("b.3gpp");
        }
    }
}