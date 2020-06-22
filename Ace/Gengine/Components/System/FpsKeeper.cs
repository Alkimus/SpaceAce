using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ace.Gengine.Components.System
{
	public class FPSkeeper
	{
		protected DateTime _TimeKeeper;

		protected int _FPS;
		protected bool _Enabled;

		public FPSkeeper()
		{
			_TimeKeeper = DateTime.Now;
		}

		public FPSkeeper(int fps)
		{
			_FPS = fps; _TimeKeeper = DateTime.Now;
		}

		public void Start() => _Enabled = true;

		public void Stop() => _Enabled = false;

		public void Restart()
		{
			_TimeKeeper = DateTime.Now;
			_Enabled = true;
		}

		public int Interval { get => _FPS > 0 ? _FPS : 0; }

		public int FPS { set => _FPS = value; }

		public bool? Update()
		{
			bool result = (_TimeKeeper + TimeSpan.FromMilliseconds(Interval) >= DateTime.Now);

			if (!(_Enabled || result)) return null;

			_TimeKeeper = DateTime.Now;
			return result;
		}
	}
}