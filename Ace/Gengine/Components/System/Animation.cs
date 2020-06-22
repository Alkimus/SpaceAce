using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ace.Gengine.Components.System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace Ace.Gengine.Components.Drawing
{
	public class Animation
	{
		protected StringBuilder _Name;
		protected FrameCollection _Frames;
		protected FPSkeeper _FPSkeeper;

		public Animation(string name, FrameCollection frames, int fps)
			=> (_Name, _Frames, _FPSkeeper.FPS) = (new StringBuilder(name), frames, fps);

		public string Name { get => _Name.ToString(); set => _Name = new StringBuilder(value); }

		public Frame CurrentFrame { get => _Frames.CurrentFrame; set => _Frames.Index = _Frames.GetIndex(value); }

		public int SetFPS { set => _FPSkeeper.FPS = value; }

		public void Update(GameTime gametime)
		{
			if (_FPSkeeper.Update() != true) return;
			_Frames.Index = _Frames.Index++ < _Frames.Count ? _Frames.Index++ : 0;
		}
	}
}