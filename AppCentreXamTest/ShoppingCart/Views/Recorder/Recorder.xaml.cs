using ShoppingCart.Services;
using ShoppingCart.ViewModels.ErrorAndEmpty;
using ShoppingCart.ViewModels.Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingCart.Views.Recorder
{
    public partial class Recorder : Rg.Plugins.Popup.Pages.PopupPage
    {
        private RecorderViewModel _rvm;
        public Recorder(bool IsCartPage, string headerText, string contentText, string callerPage,
             bool isNewApi = false, string screen = null)
        {
            InitializeComponent();


            var tensflowService = App.MockDataService
                ? TypeLocator.Resolve<IPytorchService>()
                : DataService.TypeLocator.Resolve<IPytorchService>();

            _rvm = new RecorderViewModel(tensflowService, IsCartPage, headerText,
                contentText, callerPage,secsBeforeRecording:5,isNewApi, screen);

            BindingContext = _rvm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _rvm.IsTimerStop = true;
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}