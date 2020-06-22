using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using Microsoft.Xna.Framework;

namespace Ace
{
	  [Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		Icon = "@drawable/icon",
		AlwaysRetainTaskState = true,
		LaunchMode = LaunchMode.SingleInstance,
		ScreenOrientation = ScreenOrientation.FullSensor,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
	  )]
	  public class Activity_GameLaunch : AndroidGameActivity
	  {
		    private SpaceGame _game;
		    private View _view;

		    protected override void OnCreate(Bundle bundle)
		    {
				base.OnCreate(bundle);

				_game = new SpaceGame();
				_view = _game.Services.GetService(typeof(View)) as View;

				SetContentView(_view);
				_game.Run();
		    }
	  }
}

// *** Notes ***

// Make direction system using radians

// Add Move Commands to Movable Sprite Class

// Add Input Controls to Controllable Sprite Class

// Add more Object Types