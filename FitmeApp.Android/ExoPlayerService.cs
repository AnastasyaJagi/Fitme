using System;
using Android.Content;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Util;
using FitmeApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExoPlayerService))]
namespace FitmeApp.Droid
{
    public class ExoPlayerService : IMediaPlayer
    {
        Context context = Android.App.Application.Context;

        public void Play(string url)
        {
            SimpleExoPlayer _player;
            var mediaUri = Android.Net.Uri.Parse(url);

            var userAgent = Util.GetUserAgent(context, "ExoPlayerDemo");
            var defaultHttpDataSourceFactory = new DefaultHttpDataSourceFactory(userAgent);
            var defaultDataSourceFactory = new DefaultDataSourceFactory(context, null, defaultHttpDataSourceFactory);
            var extractorMediaSource = new ExtractorMediaSource(mediaUri, defaultDataSourceFactory, new DefaultExtractorsFactory(), null, null);
            var defaultBandwidthMeter = new DefaultBandwidthMeter();
            var adaptiveTrackSelectionFactory = new AdaptiveTrackSelection.Factory(defaultBandwidthMeter);
            var defaultTrackSelector = new DefaultTrackSelector(adaptiveTrackSelectionFactory);

            _player = ExoPlayerFactory.NewSimpleInstance(context, defaultTrackSelector);
            _player.Prepare(extractorMediaSource);
            _player.PlayWhenReady = true;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
