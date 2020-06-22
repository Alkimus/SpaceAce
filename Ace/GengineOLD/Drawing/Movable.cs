using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Ace.Gengine.Sprites
{
	  internal class Movable : Sprite
	  {
		    protected Vector2 _LinearVelocity = Vector2.Zero;
		    protected Vector2 _LinearResistance = Vector2.Zero;
		    protected float _RotationVelocity = 0f;
		    protected float _RotationResistance = 0f;

		    public Movable(Dictionary<string, Texture2D> textures, String name)
				: base(textures, name)
		    {
		    }

		    public Movable(Dictionary<string, Texture2D> textures, string name, Sprite parent)
				: base(textures, name, parent)
		    {
		    }

		    public Movable(Dictionary<string, Texture2D> textures, string name, Sprite parent, float lifetime)
				: base(textures, name, parent, lifetime)
		    {
		    }

		    public Movable(Dictionary<string, Texture2D> textures, string name, Vector2 linearVelocity, Vector2 linearResistance, float lifetime, Sprite parent)
				: base(textures, name, parent, lifetime)
		    {
				Set_LinearVelocity(linearVelocity);
				Set_LinearResistance(linearResistance);
		    }

		    public Vector2 Get_LinearVelocity => _LinearVelocity;

		    public Vector2 Get_LinearResistance => _LinearResistance;

		    public float Get_RotationVelocity => _RotationVelocity;

		    public float Get_RotationResistance => _RotationResistance;

		    public void Set_LinearVelocity(Vector2 value) => _LinearVelocity = value;

		    public void Set_LinearResistance(Vector2 value) => _LinearResistance = value;

		    public void Set_RotationVelocity(float value) => _RotationVelocity = value;

		    public void Set_RotationResistance(float value) => _RotationResistance = value;

		    public override void Update(GameTime gameTime, List<Sprite> sprites)
		    {
				_Position = Vector2.Add(_Position, _LinearVelocity);
				_Rotation += MathHelper.ToRadians(_RotationVelocity);
				StabilizeResistance();
				base.Update(gameTime, sprites);
		    }

		    private void StabilizeResistance()
		    {
				Set_LinearVelocity(new Vector2(
					  Calculate_Resistance(Get_LinearVelocity.X, Get_LinearResistance.X),
					  Calculate_Resistance(Get_LinearVelocity.Y, Get_LinearResistance.Y)));

				Set_RotationVelocity(Calculate_Resistance(Get_RotationVelocity, Get_RotationResistance));
		    }

		    private float Calculate_Resistance(float velocity, float resistance)
		    {
				bool Velocity = velocity < 0;
				bool Sign = resistance < velocity;
				bool Resistance = resistance < 0;

				return (Velocity && Sign && Resistance) || (!Velocity && !Sign && !Resistance) || velocity == 0 ? 0
				    : (!Velocity && Sign && !Resistance) || (Velocity && !Sign && Resistance) ? velocity - resistance
				    : (!Velocity && Sign && Resistance) || (Velocity && !Sign && !Resistance) ? velocity + resistance
				    : resistance == 0 ? velocity
				    : 0;
		    }
	  }
}