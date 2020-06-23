using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ace.Gengine.Components.Drawing;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Debug = Ace.Gengine.Components.Process.Debug;

namespace Ace.Gengine.Objects
{
	public class Player
	{
		private Debug _Debug;
		private DateTime _LastUpdate;

		protected SpriteSheet _Atlas;
		protected Vector2 _Scale;
		protected StringBuilder _Name;
		protected Vector2 _Position;
		protected float _Rotation = 0f;

		protected (float Rotation, Vector2 Linear) _Velocity;

		public Player(Texture2D texture, int rows, int columns)
		{
			_Atlas = new SpriteSheet(texture, rows, columns);
		}

		public SpriteSheet Atlas => _Atlas;
		public string Name { get => _Name.ToString(); set => _Name = new StringBuilder(value); }
		public Vector2 Position { get => _Position; set => _Position = value; }
		public Vector2 Scale { get => _Scale; set => _Scale = value; }

		public bool Debug { get => _Debug.Enabled; set => _Debug.Enabled = value; }

		public void Set_Frame(int index) => _Atlas.FrameIndex = index;

		public void Set_DebugActive(bool value) => _Debug.Enabled = value;

		public Vector2 LinearVelocity { get => _Velocity.Linear; set => _Velocity.Linear = value; }

		public float RotationVelocity { get => _Velocity.Rotation; set => _Velocity.Rotation = value; }

		public (float, Vector2) Velocity { get => _Velocity; set => _Velocity = value; }

		public void Update()
		{
			_Rotation += _Velocity.Rotation;
			_Position += _Velocity.Linear;
			Atlas.Update();
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(_Atlas.Texture, _Position, _Atlas.SourceRectangle, _Atlas.Tint, _Rotation, _Atlas.Origin, _Scale, _Atlas.SpriteEffects, _Atlas.LayerDepth);

			_Debug.Draw(sb);
		}
	}
}