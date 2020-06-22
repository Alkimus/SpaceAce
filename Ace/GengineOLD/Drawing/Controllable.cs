using Ace.Gengine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Ace.Gengine.Sprites
{
	  internal class Controllable : Movable
	  {
		    private TouchInputCollection Controls;
		    internal Rectangle _Bounds = new Rectangle();
		    internal float _MovementForce = 1f;

		    public Controllable(Dictionary<string, Texture2D> textures, string name, Rectangle bounds)
				: base(textures, name)
		    {
				Bounds = bounds;
				Setup_Controls();
		    }

		    public Controllable(Dictionary<string, Texture2D> textures, string name, Sprite parent, Rectangle bounds)
				: base(textures, name, parent)
		    {
				Bounds = bounds;
				Setup_Controls();
		    }

		    public Controllable(Dictionary<string, Texture2D> textures, string name, Sprite parent, float lifetime, Rectangle bounds)
				: base(textures, name, parent, lifetime)
		    {
				Bounds = bounds;
				Setup_Controls();
		    }

		    private void Setup_Controls()
		    {
				Controls = new TouchInputCollection();

				Touch Up = new Touch("Up",
					new Rectangle(0, 0, Bounds.Width, Bounds.Height / 4),
					new Action(Control_Up));

				Controls.AddArea("Up", Up);

				Touch Down = new Touch("Down",
					new Rectangle(0, Bounds.Bottom - Bounds.Height / 4, Bounds.Width, Bounds.Height / 4),
					new Action(Control_Down));

				Controls.AddArea("Down", Down);

				Touch Left = new Touch("Left",
					new Rectangle(0, Bounds.Height / 4, Bounds.Width / 4, Bounds.Height / 2),
					new Action(Control_Left));

				Controls.AddArea("Left", Left);

				Touch Right = new Touch("Right",
					new Rectangle(Bounds.Right - Bounds.Width / 4, Bounds.Height / 4, Bounds.Width / 4, Bounds.Height / 2),
					new Action(Control_Right));

				Controls.AddArea("Right", Right);
		    }

		    public Rectangle Bounds { get => _Bounds; set => _Bounds = value; }

		    public float MovementForce { get => _MovementForce; set => _MovementForce = value; }

		    public void Add_ControlArea(string name, Touch zone)
			    => Controls.AddArea(name, zone);

		    public void Remove_ControlArea(string name)
			    => Controls.RemoveArea(name);

		    public override void Update(GameTime gameTime, List<Sprite> sprites)
				=> base.Update(gameTime, sprites);

		    public void Update(GameTime gameTime, List<Sprite> sprites, Rectangle bounds)
		    {
				if (Bounds != bounds)
				{
					  Bounds = bounds;
					  Controls.Clear();
					  Setup_Controls();
				}
				Controls.Update();
				base.Update(gameTime, sprites);
		    }

		    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		    {
				if (Get_DebugActive)
				{
					  foreach (var item in Controls.InputZones())
					  {
						    spriteBatch.Draw(Get_DebugTexture, item.Get_TouchArea(), Color.White);
					  }
				}
				base.Draw(gameTime, spriteBatch);
		    }

		    private void Control_Up()
			    => this.Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(0, -MovementForce)));

		    private void Control_Down()
			    => this.Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(0, MovementForce)));

		    private void Control_Right()
			    => this.Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(MovementForce, 0)));

		    private void Control_Left()
			    => this.Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(-MovementForce, 0)));
	  }
}