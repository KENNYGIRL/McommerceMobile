using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using ShoppingCart.DataService;
using ShoppingCart.Mapping;
using ShoppingCart.Models;
using ShoppingCart.Views.AllCatalog;
using ShoppingCart.Views.ErrorAndEmpty;
using ShoppingCart.Views.Home;
using ShoppingCart.Views.Onboarding;
using ShoppingCart.Views.Test;
using Xamarin.Essentials;
using Xamarin.Forms;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart
{
    public partial class App : Application
    {
        public static string BaseUri = "Your Web API";

        public static bool MockDataService = true;
        public static IMapper Mapper;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM5OTk5QDMxMzgyZTMxMmUzMEFnUXFRcnloZ1VyZDVHd2lETnhocmNTWjhWQU9OeDdhY0xMZC9kc0MxYkE9;MjQwMDAwQDMxMzgyZTMxMmUzMEFobERaU0IrWW93WURhSnczRzE4aHRncEswZWoydWJjUzFqQmhWaW5zemM9;MjQwMDAxQDMxMzgyZTMxMmUzMEJNTDkxZFFDNmN1anV3UW5tR0tMTnJIUk1uMnpMSHdqN20wQ3ZQWFV4UTg9;MjQwMDAyQDMxMzgyZTMxMmUzMEdzVnJiUnR0bWJEdmFLSWNEVWxZVWxtL2NnYjkrSzEwWGtiZENTR3BMVFU9;MjQwMDAzQDMxMzgyZTMxMmUzMG1ocnJyanRVMHlObW90Sy9kVklscDRjNlg4anZDViswRnQxOHFBamllR1E9;MjQwMDA0QDMxMzgyZTMxMmUzMG9yK2pXWlpQSVU0Z2M3d1pBMi83NmVOdUliRTFhQ0xGb04xYytCYW1EVFk9;MjQwMDA1QDMxMzgyZTMxMmUzMEgranBheFRNS0JHT0ZiLzQrTWZ2WlR5QktPeC9UL3gzbWJ2WDFQVmtaMm89;MjQwMDA2QDMxMzgyZTMxMmUzMGhteCtNM3NRN0FsczVvU3YwdzdUZHpPc1ExcXZwS21vMjlNLzVNVzBpVG89;MjQwMDA3QDMxMzgyZTMxMmUzMGV0QlZOckpwdkxOZmxBTkFQRFFPcDdLSi9sTHRIZGhIMW5IKzlVQW5OR3M9;NT8mJyc2IWhia31ifWN9Z2FoYmF8YGJ8ampqanNiYmlmamlmanMDHmg1ITI9ODw1PDY3JhM0PjI6P30wPD4=;MjQwMDA4QDMxMzgyZTMxMmUzMFc0aHlLSEFNZlJ5L3BDU2YwSERBaTdEZ3J6V25lN01rUmxNU2NjeldaSGM9");

            InitializeComponent();

            if (MockDataService)
            {
                TypeLocator.Start();
                MainPage = new NavigationPage(new LanguageSettings());
            }
            else
            {
                ListenNetworkChanges();
                if (!SQLiteDatabase.Shared.Initialized) SQLiteDatabase.Shared.Init();

                DataService.TypeLocator.Start();
               Mapper = MapperConfig.Config();
                GetUserInfo();
            }
        }

        public static string BaseImageUrl { get; } =
            "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public static int CurrentUserId { get; set; }
        public static string UserEmailId { get; set; }

        public static string UserName { get; set; }

        public class Singleton
        {
            private Singleton()
            {
                // Prevent outside instantiation
            }

            private static readonly Singleton _singleton = new Singleton();

            public static Singleton GetSingleton()
            {
                return _singleton;
            }
        }

        private void GetUserInfo()
        {
            var userInfo = SQLiteDatabase.Shared.GetUserInfo();
            if (userInfo != null)
            {
                CurrentUserId = userInfo.UserId;
                UserEmailId = userInfo.EmailId;
                UserName = userInfo.UserName;
                MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                MainPage = new NavigationPage(new LanguageSettings());
            }
        }

        private static void ListenNetworkChanges()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckInternet();
        }


        private static void CheckInternet()
        {
            var onErrorPage = false;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                onErrorPage = true;
                Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());
            }
            else if (onErrorPage)
            {
                Current.MainPage.Navigation.PopAsync();
                onErrorPage = false;
            }
        }
      
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}