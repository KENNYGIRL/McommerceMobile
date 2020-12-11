using Rg.Plugins.Popup.Services;
using ShoppingCart.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCart.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tester : ContentPage
    {
        public Tester()
        {
            InitializeComponent();
            BindingContext = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var t = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());

            t.play("1.3gpp");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var page = new Recorder.Recorder(false, "Record Your voice", "Record Your voice", GetType().Name);

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}