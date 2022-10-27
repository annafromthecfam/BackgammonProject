using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TryMovingPieces
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D gameBoard;
        Vector2 gameBoardPosistion;
        float Of;

        Texture2D[] DarkTrianglesPointingDown = new Texture2D[6];
        Texture2D[] LightTrianglesPointingDown = new Texture2D[6];
        Texture2D[] DarkTrianglesPointingUp = new Texture2D[6];
        Texture2D[] LightTrianglesPointingUp = new Texture2D[6];
        Texture2D[] SpotAvailablePointingUp = new Texture2D[6];

        Texture2D[] blackPieces = new Texture2D[15];
        Texture2D[] whitePieces = new Texture2D[15];

        Random random = new Random();
        Texture2D[] dieOne = new Texture2D[6];
        Texture2D[] dieTwo = new Texture2D[6];
        Texture2D[] dieThree = new Texture2D[6];
        Texture2D[] dieFour = new Texture2D[6];
        int valueDieOne;
        int valueDieTwo;
        int valueDieThree;
        int valueDieFour;

        MouseState mState;

        void RollDice()
        {
            valueDieOne = random.Next(0, 6);
            valueDieTwo = random.Next(0, 6);
            if (valueDieOne == valueDieTwo)
            {
                valueDieThree = valueDieOne;
                valueDieFour = valueDieOne;
            }

        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameBoardPosistion = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            RollDice();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            gameBoard = Content.Load<Texture2D>("BlankGameBoard");

            dieOne[0] = Content.Load<Texture2D>("whitedie1");
            dieOne[1] = Content.Load<Texture2D>("whitedie2");
            dieOne[2] = Content.Load<Texture2D>("whitedie3");
            dieOne[3] = Content.Load<Texture2D>("whitedie4");
            dieOne[4] = Content.Load<Texture2D>("whitedie5");
            dieOne[5] = Content.Load<Texture2D>("whitedie6");

            dieTwo[0] = Content.Load<Texture2D>("whitedie1");
            dieTwo[1] = Content.Load<Texture2D>("whitedie2");
            dieTwo[2] = Content.Load<Texture2D>("whitedie3");
            dieTwo[3] = Content.Load<Texture2D>("whitedie4");
            dieTwo[4] = Content.Load<Texture2D>("whitedie5");
            dieTwo[5] = Content.Load<Texture2D>("whitedie6");

            dieThree[0] = Content.Load<Texture2D>("whitedie1");
            dieThree[1] = Content.Load<Texture2D>("whitedie2");
            dieThree[2] = Content.Load<Texture2D>("whitedie3");
            dieThree[3] = Content.Load<Texture2D>("whitedie4");
            dieThree[4] = Content.Load<Texture2D>("whitedie5");
            dieThree[5] = Content.Load<Texture2D>("whitedie6");

            dieFour[0] = Content.Load<Texture2D>("whitedie1");
            dieFour[1] = Content.Load<Texture2D>("whitedie2");
            dieFour[2] = Content.Load<Texture2D>("whitedie3");
            dieFour[3] = Content.Load<Texture2D>("whitedie4");
            dieFour[4] = Content.Load<Texture2D>("whitedie5");
            dieFour[5] = Content.Load<Texture2D>("whitedie6");

            for (int x = 0; x < 6; x++)
            {
                DarkTrianglesPointingDown[x] = Content.Load<Texture2D>("DarkGreenPointingDown");
                LightTrianglesPointingDown[x] = Content.Load<Texture2D>("LightGreenPointingDown");
                DarkTrianglesPointingUp[x] = Content.Load<Texture2D>("DarkGreenPointingUp");
                LightTrianglesPointingUp[x] = Content.Load<Texture2D>("LightGreenPointingUp");
                SpotAvailablePointingUp[x] = Content.Load<Texture2D>("SpotAvailablePointingUp");
            }

            for (int x = 0; x < 15; x++)
            {
                blackPieces[x] = Content.Load<Texture2D>("BlackPiece");
                whitePieces[x] = Content.Load<Texture2D>("WhitePiece");
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();

            while (mState.LeftButton == ButtonState.Pressed)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(blackPieces[5], new Vector2(100, 100), Color.Black);
                //_spriteBatch.Draw(SpotAvailablePointingUp[0], new Vector2(250, 250), Color.White);
                _spriteBatch.End();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(
                    gameBoard,
                    gameBoardPosistion,
                    null,
                    Color.White,
                    Of,
                    new Vector2(gameBoard.Width / 2, gameBoard.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    Of
                );

            for (int x = 0; x < 3; x++)
            {
                _spriteBatch.Draw(DarkTrianglesPointingDown[x], new Vector2((140 * x) + 49, 2), Color.White);
                _spriteBatch.Draw(LightTrianglesPointingDown[x], new Vector2((140 * x) + 407, 2), Color.White);
            }

            for (int x = 0; x < 2; x++)
            {
                _spriteBatch.Draw(LightTrianglesPointingDown[x], new Vector2((140 * x) + 120, 2), Color.White);
                _spriteBatch.Draw(DarkTrianglesPointingDown[x], new Vector2((140 * x) + 480, 2), Color.White);
            }

            for (int x = 0; x < 3; x++)
            {
                _spriteBatch.Draw(LightTrianglesPointingUp[x], new Vector2((140 * x) + 49, 280), Color.White);
                _spriteBatch.Draw(DarkTrianglesPointingUp[x], new Vector2((140 * x) + 407, 280), Color.White);
            }

            for (int x = 0; x < 2; x++)
            {
                _spriteBatch.Draw(LightTrianglesPointingUp[x], new Vector2((140 * x) + 480, 280), Color.White);
                _spriteBatch.Draw(DarkTrianglesPointingUp[x], new Vector2((140 * x) + 120, 280), Color.White);
            }

            for (int y = 0; y < 5; y++)
            {
                // Black Pieces Left Stack of 5
                _spriteBatch.Draw(blackPieces[y], new Vector2(59, 438 - (y * 39)), Color.Black);
                // White Pieces Left Stack of 5
                _spriteBatch.Draw(whitePieces[y], new Vector2(59, 0 + (y * 39)), Color.White);
                // Black Pieces Right Stack of 5
                _spriteBatch.Draw(blackPieces[y + 8], new Vector2(417, 0 + (y * 39)), Color.Black);
                // White Pieces Right Stack of 5
                _spriteBatch.Draw(whitePieces[y + 8], new Vector2(417, 438 - (y * 39)), Color.White);
            }

            for (int y = 0; y < 3; y++)
            {
                // Black Pieces Left Stack of 3
                _spriteBatch.Draw(blackPieces[y + 5], new Vector2(199, 0 + (y * 39)), Color.Black);
                // White Pieces Left Stack of 3
                _spriteBatch.Draw(whitePieces[y + 5], new Vector2(199, 438 - (y * 39)), Color.White);
            }

            for (int y = 0; y < 2; y++)
            {
                // Black Pieces Right Stack of 2
                _spriteBatch.Draw(blackPieces[y + 13], new Vector2(697, 438 - (y * 39)), Color.Black);
                // White Pieces Left Stack of 3
                _spriteBatch.Draw(whitePieces[y + 13], new Vector2(697, 0 + (y * 39)), Color.White);
            }

            _spriteBatch.Draw(dieOne[valueDieOne], new Vector2(190, 226), Color.White);
            _spriteBatch.Draw(dieTwo[valueDieTwo], new Vector2(250, 226), Color.White);

            if (valueDieOne == valueDieTwo)
            {
                _spriteBatch.Draw(dieThree[valueDieThree], new Vector2(130, 226), Color.White);
                _spriteBatch.Draw(dieFour[valueDieFour], new Vector2(70, 226), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}