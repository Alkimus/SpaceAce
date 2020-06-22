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

namespace Ace.Gengine.Components.Drawing
{
	public class SpriteSheet : Sprite
	{
		protected FrameCollection _Frames;
		protected int _Rows;
		protected int _Columns;
		protected int _Width;
		protected int _Height;

		public SpriteSheet(Texture2D texture, int rows, int columns) : base(texture)
		{
			_Frames = new FrameCollection(texture.Bounds);
			(_Rows, _Columns) = (rows, columns);
			(_Width, _Height) = (texture.Width / columns, texture.Height / rows);

			Parse();
		}

		public void Set_Texture(Texture2D texture, int rows, int columns) 
		{
			base.Set_Texture(texture);
			_Frames = new FrameCollection(texture.Bounds);
			(_Rows, _Columns) = (rows, columns);
			(_Width, _Height) = (texture.Width / columns, texture.Height / rows);
			Parse();
		}

		private void Parse()
		{
			for (int y = 0; y < _Rows; y++)
			{
				for (int x = 0; x < _Columns; x++)
				{
					_Frames.Add(new Frame(x, y, x * _Width, y * _Height));
				}
			}
		}

		public FrameCollection Frames => _Frames;

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}
	}
}