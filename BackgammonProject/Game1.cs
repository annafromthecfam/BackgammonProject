using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// in process of composing music, adding sound effects, looking into animation -anja
namespace BackgammonOct12
{
    public class Game1 : Game
    {   
        Texture2D gameBoardTexture;
        Texture2D[] blackPieces = new Texture2D[15];
        Texture2D[] whitePieces = new Texture2D[15];
        Vector2 gameBoardPosition;
        float Of;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameBoardPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameBoardTexture = Content.Load<Texture2D>("GameBoard");
            for (int x = 0; x < 4; x++)
            {
                blackPieces[x] = Content.Load<Texture2D>("BlackPiece");
                //whitePieces[x] = Content.Load<Texture2D>("WhitePiece");
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Game Board
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                    gameBoardTexture,
                    gameBoardPosition,
                    null,
                    Color.White,
                    Of,
                    new Vector2(gameBoardTexture.Width / 2, gameBoardTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    Of
                );
            _spriteBatch.End();
            // Black Piece 0            
            _spriteBatch.Begin();
            _spriteBatch.Draw(blackPieces[0], new Vector2(57, 438), Color.Black);
            _spriteBatch.End();
            // Black Piece 1
            _spriteBatch.Begin();
            _spriteBatch.Draw(blackPieces[1], new Vector2(57, 399), Color.Black);
            _spriteBatch.End();
            // Black Piece 2
            _spriteBatch.Begin();
            _spriteBatch.Draw(blackPieces[2], new Vector2(57, 360), Color.Black);
            _spriteBatch.End();
            // Black Piece 3
            _spriteBatch.Begin();
            _spriteBatch.Draw(blackPieces[3], new Vector2(57, 321), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
