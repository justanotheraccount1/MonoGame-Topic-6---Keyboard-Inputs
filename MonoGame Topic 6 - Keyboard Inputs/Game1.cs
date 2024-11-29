using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Topic_6___Keyboard_Inputs
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D pacTextureRight, pacTextureLeft, pacTextureUp, pacTextureDown, pacTextureSleep;
        Rectangle pacLocation;
        Vector2 pacSpeed;
        KeyboardState keyboardState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pacLocation = new Rectangle(10, 10, 75, 75);
            pacSpeed = new Vector2();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacTextureRight = Content.Load<Texture2D>("PacRight");
            pacTextureLeft = Content.Load<Texture2D>("PacLeft");
            pacTextureUp = Content.Load<Texture2D>("PacUp");
            pacTextureDown = Content.Load<Texture2D>("PacDown");
            pacTextureSleep = Content.Load<Texture2D>("PacSleep");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();
            pacSpeed = new Vector2();
            pacSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacSpeed.Y = -2;
                
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacSpeed.Y = 2;

            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacSpeed.X = -2;

            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacSpeed.X = 2;

            }
            pacLocation.Offset(pacSpeed);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (pacSpeed.X > 0)
                _spriteBatch.Draw(pacTextureRight, pacLocation, Color.White);
            if (pacSpeed.Y > 0)
                _spriteBatch.Draw(pacTextureDown, pacLocation, Color.White);
            if (pacSpeed.X < 0)
                _spriteBatch.Draw(pacTextureLeft, pacLocation, Color.White);
            if (pacSpeed.Y < 0)
                _spriteBatch.Draw(pacTextureUp, pacLocation, Color.White);
            if (pacSpeed.X == 0 &&  pacSpeed.Y == 0)
                _spriteBatch.Draw(pacTextureSleep, pacLocation, Color.White);


            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
