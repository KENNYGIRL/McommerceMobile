using ShoppingCart.DependencyServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingCart.Views.Test
{
   public class AudioPlayerViewModel : INotifyPropertyChanged
    {
        private string _commandText;
        public string CommandText
        {
            get { return _commandText; }
            set
            {
                _commandText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommandText"));
            }
        }
        private IAudioPlayerService _audioPlayer;
        private bool _isStopped;
        public event PropertyChangedEventHandler PropertyChanged;


        public AudioPlayerViewModel(IAudioPlayerService audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioPlayer.OnFinishedPlaying = () => {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;
        }

        //private ICommand _playPauseCommand;

        //public ICommand PlayPauseCommand
        //{
        //    get
        //    {
        //        return _playPauseCommand ?? (_playPauseCommand = new Command(
        //          (obj) =>
        //          {
        //              if (CommandText == "Play")
        //              {
        //                  if (_isStopped)
        //                  {
        //                      _isStopped = false;
        //                      _audioPlayer.Play("1.3gpp");
        //                  }
        //                  else
        //                  {
        //                      _audioPlayer.Play();
        //                  }
        //                  CommandText = "Pause";
        //              }
        //              else
        //              {
        //                  _audioPlayer.Pause();
        //                  CommandText = "Play";
        //              }
        //          }));
        //    }
        //}

        public void play(string fileName)
        {
            _audioPlayer.Play(fileName);
        }
    }
}
