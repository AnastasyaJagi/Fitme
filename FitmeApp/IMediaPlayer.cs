using System;
namespace FitmeApp
{
    public interface IMediaPlayer
    {
        void Play(string url);
        void Stop();
    }
}
