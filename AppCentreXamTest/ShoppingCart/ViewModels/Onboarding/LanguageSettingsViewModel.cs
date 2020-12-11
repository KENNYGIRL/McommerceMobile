using ShoppingCart.Commands;
using ShoppingCart.Services;
using ShoppingCart.ViewModels.Recorder;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Onboarding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ShoppingCart.ViewModels.Onboarding
{
    public class LanguageSettingsViewModel : BaseViewModel
    {
        public LanguageSettingsViewModel(string callerPage)
        {
            SubmitCommand = new DelegateCommand(Submit);
            MessagingCenter.Subscribe<RecorderViewModel, PredictionData>(this, callerPage, MessageCenterSubmitAsync);


        }
        public DelegateCommand SubmitCommand { get; set; }

        private string languageSelected;
        public string LanguageSelected
        {
            get => languageSelected; set
            {
                if (languageSelected == value) return;

                languageSelected = value;
                OnPropertyChanged();
            }
        }
        private async void MessageCenterSubmitAsync(RecorderViewModel source, PredictionData data)
        {
            if (data == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No data returned. Please record your command again or contact support", "Cancel", "ok");
                                       
                }); 
            }
            //if english
            if (data.ClassId == "English" && data.Probability > 70.0)
            {
                SubmitCommand.Execute(null);
            }
            else if(data.ClassId == "Yoruba" && data.Probability > 70.0)
            {
                LanguageSelected = "Yoruba";
                SubmitCommand.Execute(null);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please record your command again. Recording is not clear enough", "Cancel", "ok");

                });
            }

        }
        private void Submit(object obj)
        {
            

            if (LanguageSelected == "Yoruba")
            {
                SetCultureToYoruba();
            }

            Device.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new SimpleLoginPage()  );

            });           

        }
        private void SetCultureToYoruba()
        {
            var yorubaCultureInfo = new CultureInfo("yo-NG");
            CultureInfo.DefaultThreadCurrentCulture = yorubaCultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = yorubaCultureInfo;
        }
    }
}
