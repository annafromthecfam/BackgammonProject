using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Media;
using System.Runtime.CompilerServices;

/*SoundPlayer = new SoundPlayer("");
PlayerIndex.PlayLooping();*/

namespace TryMovingPieces
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        Texture2D gameBoard;
        Vector2 gameBoardPosition;
        float Of;

        Texture2D rollDiceButton;
        Texture2D[] whosTurn = new Texture2D[2];

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
        int RollDiceButtonRadius = 34;
        Vector2 RollDiceButtonPosition = new Vector2(670, 205);

        int[] yPosition = new int[12] { 438, 399, 360, 321, 282, 243, 2, 41, 80, 119, 158, 197 };
        int[] xPosition = new int[12] { 709, 649, 591, 531, 473, 413, 355, 295, 238, 176, 119, 59 };

        Vector2[] blackPiecePosition;
        Vector2[] whitePiecePosition;
        Vector2 savedPosition;

        Vector2[] columnLeftRightBoundaries = new Vector2[24] { new Vector2(709,709+30), new Vector2(649,649+30), new Vector2(591, 591+30),
                                                                new Vector2(531,531+30), new Vector2(473,473+30), new Vector2(413, 413+30),
                                                                new Vector2(355, 355+30), new Vector2(295,295+30), new Vector2(238,238+30),
                                                                new Vector2(176,176+30), new Vector2(119,199+30), new Vector2(59,59+30),
                                                                new Vector2(59,59+30), new Vector2(119,199+30), new Vector2(176,176+30),
                                                                new Vector2(238,238+30), new Vector2(295,295+30), new Vector2(355, 355+30),
                                                                new Vector2(413, 413+30), new Vector2(473,473+30), new Vector2(531,531+30),
                                                                new Vector2(591, 591+30), new Vector2(649,649+30), new Vector2(709,709+30)};

        Vector2[] columnTopBottomBoundaries = new Vector2[24] { new Vector2 (254,474), new Vector2(254, 474), new Vector2(254, 474),
                                                                new Vector2 (254,474), new Vector2 (254,474), new Vector2 (254,474),
                                                                new Vector2 (254,474), new Vector2 (254,474), new Vector2 (254,474),
                                                                new Vector2 (254,474), new Vector2 (254,474), new Vector2 (254,474),
                                                                new Vector2 (2,198), new Vector2 (2,198), new Vector2 (2,198),
                                                                new Vector2 (2,198), new Vector2 (2,198), new Vector2 (2,198),
                                                                new Vector2 (2,198), new Vector2 (2,198), new Vector2 (2,198),
                                                                new Vector2 (2,198), new Vector2 (2,198), new Vector2 (2,198),};


        string[] columnStatus = new string[24] {"black", "open", "open", "open", "open", "white", "open", "white",
                                                "open", "open", "open", "black", "white", "open", "open", "open",
                                                "black", "open", "black", "open", "open", "open", "open", "white"};

        MouseState mState;
        bool mReleased;
        bool allowDiceRoll;

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

        bool PlayerOneTurn()
        {
            return true;
        }

        bool PlayerTwoTurn()
        {
            return true;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameBoardPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            
            blackPiecePosition = new Vector2[15] {  new Vector2(xPosition[0], yPosition[0]), new Vector2(xPosition[0], yPosition[1]),
                                                            new Vector2(xPosition[11], yPosition[0]), new Vector2(xPosition[11], yPosition[1]),
                                                            new Vector2(xPosition[11], yPosition[2]), new Vector2(xPosition[11], yPosition[3]),
                                                            new Vector2(xPosition[11], yPosition[4]), new Vector2(xPosition[7], yPosition[6]),
                                                            new Vector2(xPosition[7], yPosition[7]),
                                                            new Vector2(xPosition[7], yPosition[8]), new Vector2(xPosition[5], yPosition[6]),
                                                            new Vector2(xPosition[5], yPosition[7]), new Vector2(xPosition[5], yPosition[8]),
                                                            new Vector2(xPosition[5], yPosition[9]), new Vector2(xPosition[5], yPosition[10])};

            whitePiecePosition = new Vector2[15] {  new Vector2(xPosition[5], yPosition[0]), new Vector2(xPosition[5], yPosition[1]),
                                                            new Vector2(xPosition[5], yPosition[2]), new Vector2(xPosition[5], yPosition[3]),
                                                            new Vector2(xPosition[5], yPosition[4]), new Vector2(xPosition[7], yPosition[0]),
                                                            new Vector2(xPosition[7], yPosition[1]), new Vector2(xPosition[7], yPosition[2]),
                                                            new Vector2(xPosition[11], yPosition[6]),
                                                            new Vector2(xPosition[11], yPosition[7]), new Vector2(xPosition[11], yPosition[8]),
                                                            new Vector2(xPosition[11], yPosition[9]), new Vector2(xPosition[11], yPosition[10]),
                                                            new Vector2(xPosition[0], yPosition[6]), new Vector2(xPosition[0], yPosition[7])};

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            gameBoard = Content.Load<Texture2D>("BlankGameBoard");

            rollDiceButton = Content.Load<Texture2D>("RollDiceButton");

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

            whosTurn[0] = Content.Load<Texture2D>("Player1Turn");
            whosTurn[1] = Content.Load<Texture2D>("Player2Turn");

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                if (allowDiceRoll == true)
                {
                    float mouseRollDiceButtonDistance = Vector2.Distance(new Vector2(RollDiceButtonPosition.X + RollDiceButtonRadius, RollDiceButtonPosition.Y + RollDiceButtonRadius), mState.Position.ToVector2());
                    if (mouseRollDiceButtonDistance < RollDiceButtonRadius)
                    {
                        RollDice();
                        mReleased = false;
                    }
                }

                if (PlayerOneTurn())
                {
                    for (int x = 0; x < 15; x++)
                    {
                        float mousePieceDistance = Vector2.Distance(new Vector2(blackPiecePosition[x].X, blackPiecePosition[x].Y), mState.Position.ToVector2());
                        if (mousePieceDistance < 30)
                        {
                            savedPosition = blackPiecePosition[x];
                            blackPiecePosition[x] = mState.Position.ToVector2();
                        }
                    }
                }

                if (PlayerTwoTurn())
                {
                    for (int x = 0; x < 15; x++)
                    {
                        float mousePieceDistance = Vector2.Distance(new Vector2(whitePiecePosition[x].X, whitePiecePosition[x].Y), mState.Position.ToVector2());
                        if (mousePieceDistance < 30)
                        {
                            savedPosition = whitePiecePosition[x];
                            whitePiecePosition[x] = mState.Position.ToVector2();
                        }
                    }
                }

            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            if (PlayerOneTurn())
            {
                allowDiceRoll = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(
                    gameBoard,
                    gameBoardPosition,
                    null,
                    Color.White,
                    Of,
                    new Vector2(gameBoard.Width / 2, gameBoard.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    Of
                );

            for (int x = 0; x < 6; x++)
            {
                _spriteBatch.Draw(DarkTrianglesPointingDown[x], new Vector2((118 * x) + 49, 2), Color.White);
                _spriteBatch.Draw(LightTrianglesPointingDown[x], new Vector2((118 * x) + 109, 2), Color.White);
            }

            for (int x = 0; x < 6; x++)
            {
                _spriteBatch.Draw(LightTrianglesPointingUp[x], new Vector2((118 * x) + 49, 280), Color.White);
                _spriteBatch.Draw(DarkTrianglesPointingUp[x], new Vector2((118 * x) + 109, 280), Color.White);
            }

            for (int y = 0; y < 5; y++)
            {
                // Black Pieces Left Stack of 5
                _spriteBatch.Draw(blackPieces[y], blackPiecePosition[y + 2], Color.Black);
                // White Pieces Left Stack of 5
                _spriteBatch.Draw(whitePieces[y], whitePiecePosition[y + 8], Color.White);
                // Black Pieces Right Stack of 5
                _spriteBatch.Draw(blackPieces[y + 8], blackPiecePosition[y + 10], Color.Black);
                // White Pieces Right Stack of 5
                _spriteBatch.Draw(whitePieces[y + 8], whitePiecePosition[y], Color.White);
            }

            for (int y = 0; y < 3; y++)
            {
                // Black Pieces Left Stack of 3
                _spriteBatch.Draw(blackPieces[y + 5], blackPiecePosition[y + 7], Color.Black);
                // White Pieces Left Stack of 3
                _spriteBatch.Draw(whitePieces[y + 5], whitePiecePosition[y + 5], Color.White);
            }

            for (int y = 0; y < 2; y++)
            {
                // Black Pieces Right Stack of 2
                _spriteBatch.Draw(blackPieces[y + 13], blackPiecePosition[y], Color.Black);
                // White Pieces Left Stack of 2
                _spriteBatch.Draw(whitePieces[y + 13], whitePiecePosition[y + 13], Color.White);
            }

            _spriteBatch.Draw(dieOne[valueDieOne], new Vector2(190, 226), Color.White);
            _spriteBatch.Draw(dieTwo[valueDieTwo], new Vector2(250, 226), Color.White);

            if (valueDieOne == valueDieTwo)
            {
                _spriteBatch.Draw(dieThree[valueDieThree], new Vector2(130, 226), Color.White);
                _spriteBatch.Draw(dieFour[valueDieFour], new Vector2(70, 226), Color.White);
            }

            _spriteBatch.Draw(rollDiceButton, RollDiceButtonPosition, Color.White);

            _spriteBatch.Draw(whosTurn[0], new Vector2(450 ,230), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}