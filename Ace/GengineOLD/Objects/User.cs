using Ace.Gengine.Input;
using Ace.Gengine.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Ace.Gengine.Objects
{
	  public class User
	  {
		    protected float _Health = 100;
		    protected float _Energy = 100;
		    protected float _Money = 0;
		    protected float _Reputation = 0;
		    protected Sprite _Sprite;
		    protected float _MovementForce = 1f;

		    protected TouchInputCollection _Controls;

		    #region Constructor

		    public User(Texture2D texture, string name, Rectangle bounds)
		    {
				_ = new TouchInputCollection();
				Setup_Controls();
				_Sprite = new Sprite(texture, name, bounds);
		    }

		    #endregion Constructor

		    public bool Debug { get => _DebugActive; set => _DebugActive = value; }

		    public Vector2 Get_Position => _Sprite.Get_Position;

		    public void Set_Position(Vector2 value) => _Sprite.Set_Position(value);

		    public float Get_Health => _Health;

		    public void Set_Health(float value) => _Health = value;

		    public float Get_Energy => _Energy;

		    public void Set_Energy(float value) => _Energy = value;

		    public float Get_Money => _Money;

		    public void Set_Money(float value) => _Money = value;

		    public void Set_LinearVelocity(Vector2 value) => _Sprite.Set_LinearVelocity(value);

		    public float Get_Reputation => _Reputation;

		    public void Set_Reputation(float value) => _Reputation = value;

		    #region Update and Draw

		    public void Update(GameTime gameTime, List<Sprite> spriteList, Rectangle bounds) => _Sprite.Update(gameTime, spriteList, bounds);

		    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		    {
				if (Debug)
				{
					  foreach (var item in _Controls.InputZones())
					  {
						    spriteBatch.Draw(Texture("Debug"), item.Get_TouchZone(), Color.White);
					  }
				}
				_Sprite.Draw(gameTime, spriteBatch);
		    }

		    #endregion Update and Draw

		    #region Controls

		    public float MovementForce { get => _MovementForce; set => _MovementForce = value; }

		    public void Add_ControlArea(string name, Touch zone)
			    => _Controls.AddArea(name, zone);

		    public void Remove_ControlArea(string name)
			    => _Controls.RemoveArea(name);

		    private void Setup_Controls()
		    {
				Touch Up = new Touch("Up",
					new Rectangle(0, 0, _Sprite._Bounds.Width, _Sprite._Bounds.Height / 4),
					new Action(Control_Up));

				_Controls.AddArea("Up", Up);

				Touch Down = new Touch("Down",
					new Rectangle(0, _Sprite._Bounds.Bottom - _Sprite._Bounds.Height / 4, _Sprite._Bounds.Width, _Sprite._Bounds.Height / 4),
					new Action(Control_Down));

				_Controls.AddArea("Down", Down);

				Touch Left = new Touch("Left",
					new Rectangle(0, _Sprite._Bounds.Height / 4, _Sprite._Bounds.Width / 4, _Sprite._Bounds.Height / 2),
					new Action(Control_Left));

				_Controls.AddArea("Left", Left);

				Touch Right = new Touch("Right",
					new Rectangle(_Sprite._Bounds.Right - _Sprite._Bounds.Width / 4, _Sprite._Bounds.Height / 4, _Sprite._Bounds.Width / 4, _Sprite._Bounds.Height / 2),
					new Action(Control_Right));

				_Controls.AddArea("Right", Right);
		    }

		    #endregion Controls
	  }
}