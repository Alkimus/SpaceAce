using Ace.Gengine.Components.System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ace.Gengine.Components.Drawing
{
	public class SpriteSheet : ISprite
	{
		protected FPSkeeper _FPSkeep;

		protected Texture2D _Texture;
		protected FrameCollection _Frames;
		protected AnimationCollection _Animations;

		protected int _Rows;
		protected int _Columns;
		protected int _Width;
		protected int _Height;
		protected int _FrameIndex;
		protected int _AnimationIndex;
		protected float _LayerDepth;
		protected SpriteEffects _SpriteEffect;

		public FrameCollection Frames { get => _Frames; }
		public Vector2 Origin { get => _Frames.CurrentFrame.Center; }
		public Rectangle SourceRectangle { get; }
		public Color Tint { get; set; }
		public Texture2D Texture { get; set; }
		public SpriteEffects SpriteEffects { get => _SpriteEffect; set => _SpriteEffect = value; }
		public int FrameIndex { get => _Frames.Index; set => _Frames.Index = value; }
		public float LayerDepth { get => _LayerDepth; set => _LayerDepth = value; }

		public SpriteSheet(Texture2D texture, int rows, int columns)
		{
			_Frames = new FrameCollection(texture.Bounds);
			(_Rows, _Columns) = (rows, columns);
			(_Width, _Height) = (texture.Width / columns, texture.Height / rows);

			Parse();
		}

		public void Set_Texture(Texture2D texture, int rows, int columns)
		{
			_Texture = texture;
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

		public void Update()
		{
		}
	}
}