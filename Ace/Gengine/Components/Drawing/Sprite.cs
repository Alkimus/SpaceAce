using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ace.Gengine.Components.Drawing
{
	/// <summary>
	/// Sprite Object containing everything needed to Draw, included collectons for Frames and
	/// Animations
	/// </summary>
	public class Sprite
	{
		private Process.Debug _Debug;
		private GameTime _GameTime;
		private float _FPS;

		protected Texture2D _Texture;
		protected Rectangle _Bounds;

		protected Vector2 _Origin;
		protected Color _Tint = Color.White;
		protected float _Rotation = 0f;
		protected SpriteEffects _SpriteEffect;
		protected float _LayerDepth;
		protected Vector2 _Position;
		protected Vector2 _Scale;

		public float LinearVelocity { get; private set; }
		public float RotationalVelocity { get; private set; }

		public Sprite(Texture2D texture)
		{
			(_Texture, _Bounds) = (texture, texture.Bounds);
			_Origin = new Vector2(_Texture.Width / 2, _Texture.Height / 2);
		}

		public Rectangle Bounds { get => _Bounds; }

		public virtual void Set_Texture(Texture2D texture)
		{
			_Texture = texture;
			_Bounds = texture.Bounds;
			_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
		}
		
		public Texture2D Texture { get => _Texture; }

		public Color Tint { get => _Tint; set => _Tint = value; }

		public bool Debug { get => _Debug.Enabled; set => _Debug.Enabled = value; }

		public virtual void Update(GameTime gametime)
		{
			if (_GameTime.ElapsedGameTime >= gametime.ElapsedGameTime + TimeSpan.FromSeconds(1)) 
				_Rotation += RotationalVelocity;
		}

		public virtual void Draw(SpriteBatch sb)
		{
			sb.Draw(_Texture, _Position, _Bounds, _Tint, _Rotation, _Origin, _Scale, _SpriteEffect, _LayerDepth);
			_Debug.Draw(sb);
		}
	}
}