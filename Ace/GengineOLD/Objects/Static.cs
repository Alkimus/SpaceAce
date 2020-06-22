using Ace.Gengine.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Ace.Gengine.Objects
{
	  public class Staticold
	  {
		    protected Sprite _Sprite;
		    protected ObjectType _objectType = ObjectType.Bullet;
		    protected string _Name;
		    protected Texture2D _Texture;
		    protected Rectangle _Bounds;
		    protected object _Parent;

		    public Staticold(Texture2D texture, string name, Rectangle bounds, object parent)
		    {
				_Texture = texture;
				_Name = name;
				_Bounds = bounds;
				_Parent = parent;

				_Sprite = new Sprite(_Texture, _Name, _Bounds);
		    }
	  }
}