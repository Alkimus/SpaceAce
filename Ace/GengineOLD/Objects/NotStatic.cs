using Ace.Gengine.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Ace.Gengine.Objects
{
	  public class NotStatic
	  {
		    protected Sprite _Sprite;
		    protected ObjectType _objectType = ObjectType.Star;
		    protected string _Name;
		    protected Rectangle _Bounds;
		    protected object _Parent;

		    public NotStatic(Texture2D texture, string name, Rectangle bounds, object parent)
		    {
				_Name = name;
				_Bounds = bounds;
				_Parent = parent;

				_Sprite = new Sprite(texture, name, bounds);
		    }
	  }
}