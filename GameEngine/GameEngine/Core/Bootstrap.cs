using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Extra;
using Microsoft.Xna.Framework.Media;
using GameEngine.Assets;
using GameEngine.Components.Audio;
using GameEngine.Components.Rendering;

namespace GameEngine.Core
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Bootstrap : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static EngineSpriteBatch spriteBatch;
        GameObject m, n;
        Texture2D test;
        ImageMap map;
        AudioFile audio;

        public Bootstrap()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m = new GameObject();
            n = new GameObject();
            n.Transform.Position = Vector2.One * 10;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new EngineSpriteBatch(GraphicsDevice);

            test = Content.Load<Texture2D>("yellowbox");
            map = new ImageMap("cucumber", "Cucumber.png");
            map.Load();
            audio = new AudioFile("audio", "test.ogg");
            audio.Load();

            AudioSource source = new AudioSource(n);
            source.Active = true;
            source.AddSound(audio);
            source.Range = 200;
            source.Volume = 1;

            GameEngine.Components.Audio.AudioListener listener = new Components.Audio.AudioListener(m);
            listener.RegisterSource(source);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyInput.Update();
            MouseInput.Update();
            PadInput.Update();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    m.Transform.Position -= Vector2.UnitY;

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    m.Transform.Position += Vector2.UnitY;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    m.Transform.Position -= Vector2.UnitX;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    m.Transform.Position += Vector2.UnitX;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    m.Transform.Rotation += 0.2f;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    m.Transform.Rotation -= 0.2f;
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    n.Transform.LocalPosition -= Vector2.UnitY;

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    n.Transform.LocalPosition += Vector2.UnitY;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    n.Transform.LocalPosition -= Vector2.UnitX;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    n.Transform.LocalPosition += Vector2.UnitX;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    n.Transform.Rotation += 0.2f;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    n.Transform.Rotation -= 0.2f;
            }
            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                m.Transform.Translate(Vector2.UnitY, Space.Local);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Camera.main != null ? Camera.main.BackgroundColor : Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(
                map.Texture,
                Vector2.One * 20,
                map[2],
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0
                );

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
