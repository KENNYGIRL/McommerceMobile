using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.DependencyServices
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Stop();
        void Release();
        Action OnFinishedPlaying { get; set; }
    }
}