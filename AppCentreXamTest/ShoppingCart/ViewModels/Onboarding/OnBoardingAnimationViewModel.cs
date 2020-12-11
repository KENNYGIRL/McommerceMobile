using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ShoppingCart.Commands;
using ShoppingCart.Models;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Onboarding;
using Syncfusion.SfRotator.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ShoppingCart;

namespace ShoppingCart.ViewModels.Onboarding
{
    public class SubmitCommandBind : IMarkupExtension<Grid[]>
    {
        public Grid G1 { get; set; }
        public Grid G2 { get; set; }

        Grid[] IMarkupExtension<Grid[]>.ProvideValue(IServiceProvider serviceProvider)
        {
            return new Grid[] { G1, G2 };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Grid[]>).ProvideValue(serviceProvider);
        }
    }

    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class OnBoardingAnimationViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationView" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel()
        {
            SkipCommand = new DelegateCommand(Skip);
            NextCommand = new DelegateCommand(Next);
            Boardings = new ObservableCollection<Boarding>
            {
                new Boarding
                {
                    ImagePath = "ChooseGradient.svg",
                    Header = StringResource.Choose,
                    Content = StringResource.ChooseContent,//"Pick the item that is right for you",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath = "ConfirmGradient.svg",
                    Header = StringResource.OnBoard_Order_Header,
                    Content = StringResource.OnBoard_Order_Content,
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath = "DeliverGradient.svg",
                    Header =StringResource.OnBoard_Deliver_Header,
                    Content = StringResource.OnBoard_Deliver_Content,
                    RotatorItem = new WalkthroughItemPage()
                }
            };

            // Set bindingcontext to content view.
            foreach (var boarding in Boardings) boarding.RotatorItem.BindingContext = boarding;
        }

        #endregion

        #region Fields

        private ObservableCollection<Boarding> boardings;

        private string nextButtonText = GetNextTextByCulture();

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        private string languageSelected;

        #endregion

        #region Properties

        public string LanguageSelected
        {
            get => languageSelected; set
            {
                if (languageSelected == value) return;

                languageSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Boarding> Boardings
        {
            get => boardings;

            set
            {
                if (boardings == value) return;

                boardings = value;
                OnPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get => nextButtonText;

            set
            {
                if (nextButtonText == value) return;

                nextButtonText = value;
                OnPropertyChanged();
            }
        }

        public bool IsSkipButtonVisible
        {
            get => isSkipButtonVisible;

            set
            {
                if (isSkipButtonVisible == value) return;

                isSkipButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;

            set
            {
                if (selectedIndex == value) return;

                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public DelegateCommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public DelegateCommand NextCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public DelegateCommand SubmitCommand { get; set; }

        #endregion

        #region Methods     

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (SelectedIndex >= itemCount - 1) return true;

            SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            var itemCount = (obj as SfRotator).ItemsSource.Count();
            if (ValidateAndUpdateSelectedIndex(itemCount)) MoveToNextPage();
        }

        private void MoveToNextPage()
        {
            Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
        }

       private static string GetNextTextByCulture()
        {
            if (CultureInfo.CurrentCulture.Name == "yo-NG")
            {
                return "ITELE";
            }
            else
            {
                return "NEXT";
            }
        }
        private static string GetDoneTextByCulture()
        {
            if (CultureInfo.CurrentCulture.Name == "yo-NG")
            {
                return "Ti ṣee";
            }
            else
            {
                return "DONE";
            }
        }
        #endregion
    }
}