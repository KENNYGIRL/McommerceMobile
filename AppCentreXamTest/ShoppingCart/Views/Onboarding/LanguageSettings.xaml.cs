using Rg.Plugins.Popup.Services;
using ShoppingCart.DependencyServices;
using ShoppingCart.ViewModels.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCart.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguageSettings : ContentPage
    {
        public LanguageSettings()
        {
            InitializeComponent();
            BindingContext = new LanguageSettingsViewModel(GetType().Name);
        }
        IAudioPlayerService audServ;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            audServ = DependencyService.Get<IAudioPlayerService>();
          
            audServ.Play("1).3gpp");
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            if (audServ != null)
            {
                audServ.Stop();
            }
            var page = new Recorder.Recorder(false, StringResource.RecordYourVoice, "Record Your voice", GetType().Name,true,"Language");

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}