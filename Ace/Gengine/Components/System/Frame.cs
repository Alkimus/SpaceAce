using Microsoft.Xna.Framework;

namespace Ace.Gengine.Components.Drawing
{
	public class Frame
	{
		protected Rectangle _Bounds;

		public Frame(Rectangle bounds) => _Bounds = bounds;

		public Frame(int x, int y, int width, int height) => _Bounds = new Rectangle(x, y, width, height);

		public Rectangle Bounds { get => _Bounds; }
		public Vector2 Center { get => _Bounds.Center.ToVector2(); }
		public int Width { get => _Bounds.Width; }
		public int Height { get => _Bounds.Height; }
	}
}