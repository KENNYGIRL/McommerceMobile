using ShoppingCart.DataService;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Forms;
using ShoppingCart.Views.Test;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

namespace ShoppingCart.Views.Forms
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleLoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLoginPage" /> class.
        /// </summary>
        public SimpleLoginPage()
        {
            InitializeComponent();
            var userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            BindingContext = new LoginPageViewModel(userDataService, GetType().Name);
        }

        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();


            audServ = DependencyService.Get<IAudioPlayerService>();

            audServ.Play("a.3gpp");

           

            
            //audServ.OnFinishedPlaying = () => {
            //    if (!fin)
            //    {
            //        fin = true;
            //        audServ.Play("b.3gpp");
            //    }                
            //};

        }


        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name, true, "Login");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}