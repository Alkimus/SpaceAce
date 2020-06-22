using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Ace.Gengine.Components.Process
{
	/// <summary>
	/// Enabling this class in other classes allows for code to run which can help in debuging.
	/// Mainly used to add a texture to and Draw rectangles that would normally be invisable.
	/// </summary>

	public class Debug
	{
		protected List<Rectangle> _Drawables;
		protected bool _Enabled;
		protected Texture2D _Texture;

		public Debug() => _Enabled = false;

		/// <summary>
		/// Enable or Disable Debug, If there is no texture set this will always return false
		/// </summary>
		public bool Enabled { get => _Enabled; set => _Enabled = _Texture is null ? false : value; }

		/// <summary> The Texture to draw </summary>
		public Texture2D Texture { get => _Texture; set => _Texture = value; }

		/// <summary> Adds a Rectangle to the Debug List </summary>
		public void Add(Rectangle rectangle) => _Drawables.Add(rectangle);

		/// <summary> Adds a List to the Debug List </summary>
		public void AddRange(List<Rectangle> range) => _Drawables.AddRange(range);

		/// <summary> Clears the Debug List </summary>
		public void Clear() => _Drawables.Clear();

		/// <summary> Draws the Rectangles in the Debug List </summary>
		public void Draw(SpriteBatch spritebatch)
		{
			if (!Enabled) return;
			for (int i = 0; i < _Drawables.Count; i++)
			{
				spritebatch.Draw(Texture, _Drawables[i], Color.White);
			}
		}

		/// <summary> Removes a Rectangle from the Debug List </summary>
		public void Remove(Rectangle rectangle) => _Drawables.Remove(rectangle);
	}
}