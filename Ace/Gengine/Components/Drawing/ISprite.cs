using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ace.Gengine.Components.Drawing
{
	public interface ISprite
	{
		Texture2D Texture { get; set; }
		Vector2 Origin { get; }
		Rectangle SourceRectangle { get; }
		Color Tint { get; set; }
	}
}