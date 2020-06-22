using Ace.Gengine.Input;
using Ace.Gengine.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Ace.Gengine.Sprites
{
	  public class Sprite : ICloneable
	  {
		    #region System

		    internal Rectangle _Bounds = new Rectangle();
		    protected float _LifeSpan = 0f;
		    protected string _Name = string.Empty;
		    protected ObjectType _Type;

		    #endregion System

		    #region Draw

		    protected float _DrawDepth = 0;
		    protected Vector2 _Scale = new Vector2(1);
		    protected SpriteEffects _SpriteEffect = SpriteEffects.None;
		    protected Dictionary<string, Texture2D> _Textures;
		    protected Color _Tint = Color.White;

		    #endregion Draw

		    #region Movement

		    protected Vector2 _LinearResistance = Vector2.Zero;
		    protected Vector2 _LinearVelocity = Vector2.Zero;
		    protected Vector2 _Origin = new Vector2(0);
		    protected Vector2 _Position = new Vector2(0);
		    protected float _Rotation = 0;
		    protected float _RotationResistance = 0f;
		    protected float _RotationVelocity = 0f;

		    #endregion Movement

		    #region Animation

		    protected int _FrameIndex;
		    protected List<Rectangle> _Frames = new List<Rectangle>();
		    protected Rectangle? _SourceRectangle = null;

		    #endregion Animation

		    #region Cloning

		    protected List<Sprite> _Children = new List<Sprite>();
		    protected Sprite _Parent = null;

		    #endregion Cloning

		    #region Constructors

		    public Sprite(Texture2D texture, string name, Rectangle bounds)
		    {
				_Textures = new Dictionary<string, Texture2D>();

				_Bounds = bounds;

				_Textures.Add(name, texture);
				_Name = name;

				InitializeSprite();
		    }

		    public Sprite(Dictionary<string, Texture2D> textures, string name, Rectangle bounds)
		    {
				_Textures = textures;
				_Name = name;
				_Bounds = bounds;

				InitializeSprite();
		    }

		    #endregion Constructors

		    public float DrawDepth { get => _DrawDepth; set => _DrawDepth = value; }

		    public string Get_Name
					  => _Name;

		    public Vector2 Get_Origin
					  => _Origin;

		    public Vector2 Get_Position
					  => _Position;

		    public float Get_Rotation
					  => _Rotation;

		    public Vector2 Get_Scale
					  => _Scale;

		    public Rectangle? Get_SourceRectangle
					  => _SourceRectangle;

		    public SpriteEffects Get_SpriteEffect
					  => _SpriteEffect;

		    public Color Get_Tint
					  => _Tint;

		    public ObjectType Get_Type
					  => _Type;

		    public Rectangle Get_Frame(int index)
					  => _Frames[index];

		    public void Set_DebugActive(bool value)
					  => _DebugActive = value;

		    public void Set_DrawDepth(float value)
					  => _DrawDepth = value;

		    public void Set_Name(string value)
					  => _Name = value;

		    public void Set_Origin(Vector2 value)
					  => _Origin = value;

		    public void Set_Position(Vector2 value)
					  => _Position = value;

		    public void Set_Rotation(float value)
					  => _Rotation = value;

		    public void Set_Scale(Vector2 value)
					  => _Scale = value;

		    public void Set_Scale(int value)
				=> _Scale = new Vector2(value);

		    public void Set_SourceRectangle(Rectangle? value)
					  => _SourceRectangle = (Rectangle)value;

		    public void Set_SpriteEffect(SpriteEffects value)
					  => _SpriteEffect = value;

		    public void Set_Tint(Color value)
					  => _Tint = value;

		    public Texture2D Texture(string name) => _Textures[name];

		    public void Texture(string name, Texture2D value)
		    {
				if (_Textures.ContainsKey(name))
				{ _Textures.Remove(name); }

				_Textures.Add(name, value);
		    }

		    private void InitializeSprite()
		    {
				Texture(_Name, _Textures[_Name]);
				Texture("Debug", _Textures["Debug"]);
				_Origin = new Vector2(Texture(_Name).Width / 2, _Textures[_Name].Height / 2);
		    }

		    #region Update and Draw

		    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		    {
				spriteBatch.Draw(Texture(_Name), _Position, _SourceRectangle, _Tint, _Rotation, Get_Origin, _Scale, _SpriteEffect, _DrawDepth);
		    }

		    public void Update(GameTime gameTime, List<Sprite> sprites, Rectangle bounds)
		    {
				UpdateControls(gameTime, sprites, bounds);
				if (!Equals(Get_LinearVelocity, Vector2.Zero))
				{
					  _Position = Vector2.Add(_Position, _LinearVelocity);
					  _Rotation += MathHelper.ToRadians(_RotationVelocity);
					  StabilizeResistance();
				}
		    }

		    private void UpdateControls(GameTime gameTime, List<Sprite> sprites, Rectangle bounds)
		    {
				if (_Bounds != bounds)
				{
					  _Bounds = bounds;
					  _Controls.Clear();
					  Setup_Controls();
				}
				_Controls.Update();
		    }

		    #endregion Update and Draw

		    #region Animation

		    public int FrameCount => _Frames.Count;

		    public void Next_Frame() => Set_Frame(_FrameIndex++ < _Frames.Count ? _FrameIndex++ : 0);

		    public void ParseAtlas(int rows, int columns)
		    {
				int w = Texture(_Name).Width / columns;
				int h = Texture(_Name).Height / rows;

				for (int y = 0; y < rows; y++)
				{
					  for (int x = 0; x < columns; x++)
					  {
						    _Frames.Add(new Rectangle(x * w, y * h, w, h));
					  }
				}
				_FrameIndex = 0;
				_SourceRectangle = _Frames[_FrameIndex];
				_Origin = _SourceRectangle.Value.Center.ToVector2();
		    }

		    public void Previous_Frame() => Set_Frame(_FrameIndex = _FrameIndex-- > 0 ? _Frames.Count - 1 : _FrameIndex--);

		    public void Set_Frame(int index)
		    {
				if (index >= 0 && index < _Frames.Count)
				{
					  _FrameIndex = index;
					  _SourceRectangle = _Frames[_FrameIndex];
				}
		    }

		    public virtual void Unload() => Texture(_Name).Dispose();

		    #endregion Animation

		    #region Clone

		    public object Clone() => MemberwiseClone();

		    public Sprite DeepClone()
		    {
				Sprite cloned = (Sprite)MemberwiseClone();

				cloned._Parent = this;

				cloned._DrawDepth = _DrawDepth;
				cloned._Name = $"{_Name}";
				cloned._Origin = _Origin;
				cloned._Position = _Position;
				cloned._Rotation = _Rotation;
				cloned._Scale = _Scale;
				cloned._SpriteEffect = _SpriteEffect;
				cloned._Tint = _Tint;
				cloned._Frames = _Frames;
				cloned._SourceRectangle = _SourceRectangle;

				_Children.Add(cloned);

				return cloned;
		    }

		    #endregion Clone

		    #region Movable

		    public Vector2 Get_LinearResistance => _LinearResistance;

		    public Vector2 Get_LinearVelocity => _LinearVelocity;
		    public float Get_RotationResistance => _RotationResistance;

		    public float Get_RotationVelocity => _RotationVelocity;

		    public void Set_LinearResistance(Vector2 value) => _LinearResistance = value;

		    public void Set_LinearVelocity(Vector2 value) => _LinearVelocity = value;

		    public void Set_RotationResistance(float value) => _RotationResistance = value;

		    public void Set_RotationVelocity(float value) => _RotationVelocity = value;

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

		    private void StabilizeResistance()
		    {
				Set_LinearVelocity(new Vector2(
					  Calculate_Resistance(Get_LinearVelocity.X, Get_LinearResistance.X),
					  Calculate_Resistance(Get_LinearVelocity.Y, Get_LinearResistance.Y)));

				Set_RotationVelocity(Calculate_Resistance(Get_RotationVelocity, Get_RotationResistance));
		    }

		    #endregion Movable

		    #region Control Actions

		    private void Control_Down()
			    => Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(0, MovementForce)));

		    private void Control_Left()
			    => Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(-MovementForce, 0)));

		    private void Control_Right()
			    => Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(MovementForce, 0)));

		    private void Control_Up()
			    => Set_LinearVelocity(Vector2.Add(Get_LinearVelocity, new Vector2(0, -MovementForce)));

		    #endregion Control Actions
	  }
}