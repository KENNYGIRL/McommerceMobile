using Plugin.AudioRecorder;
using Rg.Plugins.Popup.Services;
using ShoppingCart.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Recorder
{
    /// <summary>
    /// ViewModel for recorder page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class RecorderViewModel : BaseViewModel
    {
        private readonly IPytorchService _tensorFlowService;
        private readonly bool isCartPage;
        private readonly bool _isNewAPI;
        private readonly string _screen;

        public RecorderViewModel(IPytorchService tensorFlowService,
            bool IsCartPage, string headerText, string contentText,
            string callerPage, int secsBeforeRecording = 5,
            bool isNewApi = false, string screen= null)
        {
            _tensorFlowService = tensorFlowService;
            isCartPage = IsCartPage;
            Header = headerText;
            Content = contentText;
            _isNewAPI = isNewApi;
            _screen = screen;
            Error = "An error occured while recording.Please click back and try again.";

            //page that calls the recorder
            CallerPage = callerPage;


            StartTimer(secsBeforeRecording);

            recorder = new AudioRecorderService
            {
                //StopRecordingAfterTimeout = true,
               // TotalAudioTimeout = TimeSpan.FromSeconds(6),
                StopRecordingOnSilence = true,
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };
        }

        private void StartTimer(int secsBeforeRecording)
        {
            time = secsBeforeRecording.ToString();
            int valTime = int.Parse(time);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                if (!IsTimerStop)
                {

                    if (valTime == 0)
                    {

                        Task.Run(async () => await RecordAudio());
                        return false;
                    }
                    Time = (valTime - 1).ToString();
                    valTime--;
                    return true;
                }
                else
                {
                    return false;
                }

            });
        }

        #region Fields
        private string time = "10";
        private string imagePath;

        private string header;

        private string content;
        readonly AudioRecorderService recorder;
        private bool isError;
        private string error;
        #endregion

        #region Properties

        public bool IsTimerStop { get; set; } = false;
        public string CallerPage { get; set; }

        public string Time
        {
            set
            {
                if (!time.Equals(value))
                {
                    time = value;

                    OnPropertyChanged();
                }
            }
            get {
                if (time.Equals("0"))
                {
                    return "Voice recording starts now";
                }
                else if (string.IsNullOrWhiteSpace(time))
                {
                    return "";
                }
                else
                {
                    return $"Voice recording starts in {time}";
                }
            }
        }

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        public string ImagePath
        {
            get => imagePath;

            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }
        public bool IsError
        {
            get => isError;

            set
            {
                isError = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get => header;

            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content
        {
            get => content;

            set
            {
                content = value;
                OnPropertyChanged();
            }
        }


        public string Error
        {
            get => error;

            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        #endregion


        async void Record_Clicked(object sender, EventArgs e)
        {
            await RecordAudio();
        }

        async Task RecordAudio()
        {
            try
            {
                if (recorder.IsRecording) //Record button clicked
                {
                    await recorder.StopRecording();
                }
                else //Stop button clicked
                {

                    //start recording audio
                    var audioRecordTask = await recorder.StartRecording();



                    //call api
                    var audioFile = await audioRecordTask;


                    //call api
                    if (audioFile != null)
                    {
                        IsBusy = true;
                        PredictionData data = new PredictionData();
                        //sends predicted data to requesting page/viewmodel
                        if (_isNewAPI)
                        {
                            data = await _tensorFlowService.GetPredictionNewAPI(audioFile, _screen);

                            //data = new PredictionData { ClassId = "item 1 or 1", Probability = 81 };
                          
                        }
                        else
                        {
                             data = await _tensorFlowService.GetPrediction(audioFile);
                        }


                        IsBusy = false;

                        //test data
                        // var d = new PredictionData { ClassId = "0", Probability = 95.0 };


                        await PopupNavigation.Instance.PopAsync();
                        MessagingCenter.Send(this, CallerPage, data);


                    }
                    else
                    {
                        time = "";
                        IsError = true;
                        Error = "Voice not recorded. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                IsError = true;
                Error = $"An error has occured. {Environment.NewLine}{ex.Message} ";
                throw ex;
            }
        }
    }
}
