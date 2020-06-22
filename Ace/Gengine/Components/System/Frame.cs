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

namespace Ace.Gengine.Components.Drawing
{
	public class Frame
	{
		protected Rectangle _Bounds;

		public Frame(Rectangle bounds) => _Bounds = bounds;

		public Frame(int x, int y, int width, int height) => _Bounds = new Rectangle(x, y, width, height);
	}
}