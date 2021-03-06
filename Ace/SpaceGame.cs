﻿using Ace.Gengine.Components.Drawing;
using Ace.Gengine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ace
{
	public class SpaceGame : Game
	{
		public const bool DebugHelper = false;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Dictionary<string, Texture2D> Textures;
		private Dictionary<string, Player> SpriteObjects;
		private Random random = new Random();

		private Rectangle StarFieldBounds;
		private Vector2 StarFieldOrigin;

		private bool GameActive;
		private bool PlayerActive;

		public SpaceGame()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				SupportedOrientations = DisplayOrientation.LandscapeLeft
				 | DisplayOrientation.LandscapeRight
				 | DisplayOrientation.Portrait
				 | DisplayOrientation.PortraitDown
			};

			graphics.PreferMultiSampling = true;
			graphics.ApplyChanges();

			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			StarFieldBounds = new Rectangle(
				  -Window.ClientBounds.Width,
				  -Window.ClientBounds.Height,
				  (Window.ClientBounds.Width + Window.ClientBounds.Height) * 2,
				  (Window.ClientBounds.Width + Window.ClientBounds.Height) * 2);

			StarFieldOrigin = new Vector2(StarFieldBounds.Center.X, StarFieldBounds.Center.Y);
			Window.ClientSizeChanged += OnResize;
			Window.OrientationChanged += OnOrientationChanged;
			GameActive = false;
			PlayerActive = false;

			base.Initialize();
		}

		private void OnOrientationChanged(object sender, EventArgs e)
		{
		}

		protected override void OnActivated(object sender, EventArgs args) => base.OnActivated(sender, args);

		private void OnResize(object sender, EventArgs e)
		{
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			SpriteObjects = new Dictionary<string, Player>();

			Textures = new Dictionary<string, Texture2D>
				{
					  { "Debug", Content.Load<Texture2D>("Debug") },
					  { "Player", Content.Load<Texture2D>("Player") },
					  { "Star", Content.Load<Texture2D>("Star") },
					  { "Bullet", Content.Load<Texture2D>("Bullet") },
					  { "Plume", Content.Load<Texture2D>("Plume") }
				};
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

		protected override void Dispose(bool disposing)
		{
			foreach (var item in Textures) item.Value.Dispose();
			//foreach (var item in SpriteObjects.Values) item.Unload();
			base.Dispose(disposing);
		}

		private void CreateBackground()
		{
			for (int i = 0; i < 2000; i++)
			{
				MakeStar($"Star{i}", new Vector2(random.Next(0, StarFieldBounds.Width), random.Next(0, StarFieldBounds.Height)));
			}
			GameActive = true;
		}

		private void MakeStar(string name, Vector2 position)
		{
			SpriteObjects.Add(name, new Sprite(Textures, "Star"));
			SpriteObjects[name].Name(name);
			SpriteObjects[name].Position(position);
			SpriteObjects[name].Scale(new Vector2((float)random.NextDouble() * 0.3f));
			SpriteObjects[name].LinearVelocity(new Vector2(0, random.Next(1, 3)));
		}

		private void CreatePlayer(string name)
		{
			Player player = new Player(Textures["Player"], 2, 3);
			SpriteObjects.Add(name, player);
			SpriteObjects[name].Name = "Player";
			SpriteObjects[name].Position = Window.ClientBounds.Location.ToVector2();
			SpriteObjects[name].Scale = Vector2.One;
			SpriteObjects[name].Set_Frame(0);
			SpriteObjects[name].Set_DebugActive(DebugHelper);
			PlayerActive = true;
		}

		private void KillPlayer(string name)
		{
			SpriteObjects.Remove(name);
			PlayerActive = false;
		}

		private void DestroyWorld()
		{
			GameActive = false;
		}

		protected override void Update(GameTime gameTime)
		{
			var list = new List<Player>();
			list.AddRange(SpriteObjects.Values);

			if (GameActive)
			{
				foreach (var item in list)
				{
					var name = item.Name;

					// Player

					SpriteObjects[name].Position = Window.ClientBounds.Center.ToVector2();

					// ObjectType.Star:
					if (!StarFieldBounds.Contains(SpriteObjects[name].Position))
					{
						SpriteObjects.Remove(name);
						MakeStar(name, new Vector2(item.Position.X, StarFieldBounds.Top));
					}

					// All

					SpriteObjects[name].Update();
				}
			}
			base.Update(gameTime);
			if (GameActive && !PlayerActive) CreatePlayer("PlayerOne");
			if (!GameActive && PlayerActive) KillPlayer("PlayerOne");
			if (!GameActive) CreateBackground();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp);
			foreach (var item in SpriteObjects.Values) item.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}