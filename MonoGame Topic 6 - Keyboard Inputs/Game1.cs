using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Emit;

namespace MonoGame_Topic_6___Keyboard_Inputs
{
    enum Screen
    { 

        Game,
        End

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D pacTextureRight, pacTextureLeft, pacTextureUp, pacTextureDown, pacTextureSleep;
        Rectangle pacLocation;
        Vector2 pacSpeed;
        KeyboardState keyboardState;
        MouseState mouseboardState;
        Texture2D greyTribbleTexture;
        Rectangle greyTribbleRect, window;
        Vector2 greyTribbleSpeed, scoreLocation;
        Random generator;
        Screen screen;
        SpriteFont scoreFont;
        float score;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            generator = new Random();
            greyTribbleRect = new Rectangle((generator.Next(700)), generator.Next(400), 100, 100);
            greyTribbleSpeed = new Vector2(5, 5);
            pacLocation = new Rectangle(10, 10, 75, 75);
            pacSpeed = new Vector2();
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            window = new Rectangle(0, 0, (_graphics.PreferredBackBufferWidth), (_graphics.PreferredBackBufferHeight));
            _graphics.ApplyChanges();
            screen = Screen.Game;
            scoreLocation = new Vector2(0, 0);
            score = 0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            pacTextureRight = Content.Load<Texture2D>("PacRight");
            pacTextureLeft = Content.Load<Texture2D>("PacLeft");
            pacTextureUp = Content.Load<Texture2D>("PacUp");
            pacTextureDown = Content.Load<Texture2D>("PacDown");
            pacTextureSleep = Content.Load<Texture2D>("PacSleep");
            scoreFont = Content.Load<SpriteFont>("Font");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Game)
            {
                score += (float)gameTime.ElapsedGameTime.TotalSeconds;
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
                if (greyTribbleRect.Right >= window.Width || greyTribbleRect.Left <= window.Left)
                    greyTribbleSpeed.X *= -1;
                if (greyTribbleRect.Top <= window.Top || greyTribbleRect.Bottom >= window.Height)
                    greyTribbleSpeed.Y *= -1;
                mouseboardState = Mouse.GetState();
                keyboardState = Keyboard.GetState();
                pacSpeed = Vector2.Zero;
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    pacSpeed.Y = -3;
                }

                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    pacSpeed.Y = 3;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    pacSpeed.X = -3;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    pacSpeed.X = 3;
                }
                if (!window.Contains(pacLocation))
                {
                    pacLocation.Offset(-pacSpeed);
                }
                
                if (mouseboardState.LeftButton == ButtonState.Pressed)
                {
                    pacLocation.X = (int)mouseboardState.X;
                    pacLocation.Y = (int)mouseboardState.Y;
                }
                pacLocation.Offset(pacSpeed);
                if (pacLocation.Contains(greyTribbleRect))
                    screen = Screen.End;
            }
            
            
            
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Game)
            {
                if (pacSpeed.X > 0)
                    _spriteBatch.Draw(pacTextureRight, pacLocation, Color.White);
                if (pacSpeed.Y > 0)
                    _spriteBatch.Draw(pacTextureDown, pacLocation, Color.White);
                if (pacSpeed.X < 0)
                    _spriteBatch.Draw(pacTextureLeft, pacLocation, Color.White);
                if (pacSpeed.Y < 0)
                    _spriteBatch.Draw(pacTextureUp, pacLocation, Color.White);
                if (pacSpeed.X == 0 && pacSpeed.Y == 0)
                    _spriteBatch.Draw(pacTextureSleep, pacLocation, Color.White);
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.DrawString(scoreFont, score.ToString("00.0"), scoreLocation, Color.White);
            }
            if (screen == Screen.End)
            {
                _spriteBatch.DrawString(scoreFont, "Game Over", scoreLocation, Color.White);
            }


            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
