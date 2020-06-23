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
using Microsoft.Xna.Framework.Graphics;

namespace Ace.Gengine.Components.Drawing
{
	/// <summary>
	/// Sprite Object containing everything needed to Draw, included collectons for Frames and
	/// Animations
	/// </summary>
	public class Sprite : ISprite
	{
		protected Rectangle _SourceRectangle;
		protected Vector2 _Origin;
		protected Color _Tint = Color.White;

		public Rectangle SourceRectangle { get => _SourceRectangle; }

		public Vector2 Origin { get => _Origin; }

		public Color Tint { get => _Tint; set => _Tint = value; }
	}
}