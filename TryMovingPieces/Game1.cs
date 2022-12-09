﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;

namespace TryMovingPieces
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bluePiece;
        Texture2D gameBoard;
        Texture2D[] darkTrianglesPointingDown = new Texture2D[6];
        Texture2D[] lightTrianglesPointingDown = new Texture2D[6];
        Texture2D[] darkTrianglesPointingUp = new Texture2D[6];
        Texture2D[] lightTrianglesPointingUp = new Texture2D[6];
        Texture2D[] blackPieces = new Texture2D[15];
        Texture2D[] whitePieces = new Texture2D[15];
        Texture2D[] winner = new Texture2D[2];
        Texture2D[] stars = new Texture2D[2];
        Texture2D[] gameStartScreens = new Texture2D[3];
        Vector2 bluePiecePosition;
        Vector2 savedPosition;
        Vector2 gameBoardPosition;
        Vector2[] blackPiecePosition = new Vector2[15];
        Vector2[] whitePiecePosition = new Vector2[15];
        int[] yPosition = new int[12];
        int[] xPosition = new int[12];
        bool rightKeyReleased;
        bool leftKeyReleased;
        bool enterKeyReleased;
        bool upKeyReleased;
        bool downKeyReleased;
        bool mReleased;
        bool player1Wins;
        bool player2Wins;
        float Of;
        MouseState mState;
        Song song;
        int screenCount = 0;

        Random random = new Random();
        Texture2D[] dieOne = new Texture2D[6];
        Texture2D[] dieTwo = new Texture2D[6];
        Texture2D[] dieThree = new Texture2D[6];
        Texture2D[] dieFour = new Texture2D[6];
        Texture2D rollDiceButton;
        int valueDieOne;
        int valueDieTwo;
        int valueDieThree;
        int valueDieFour;
        int RollDiceButtonRadius = 34;
        Vector2 RollDiceButtonPosition = new Vector2(670, 205);

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
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
           MediaPlayer.Volume -= 0.1f;
           MediaPlayer.Play(song);
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            for (int x = 0; x < 12; x++)
            {
                xPosition[x] = 59 + (x * 59);
                yPosition[x] = 2 + (x * 39);
            }

            bluePiecePosition = new Vector2(xPosition[0], yPosition[0]);
            rightKeyReleased = true;
            leftKeyReleased = true;
            upKeyReleased = true;
            downKeyReleased = true;
            gameBoardPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            blackPiecePosition = new Vector2[15] {  new Vector2(xPosition[0], yPosition[0]), new Vector2(xPosition[0], yPosition[1]),
                                                    new Vector2(xPosition[0], yPosition[2]), new Vector2(xPosition[0], yPosition[3]),
                                                    new Vector2(xPosition[0], yPosition[4]), new Vector2(xPosition[11], yPosition[0]),
                                                    new Vector2(xPosition[11], yPosition[1]), new Vector2(xPosition[4], yPosition[9]),
                                                    new Vector2(xPosition[4], yPosition[10]), new Vector2(xPosition[4], yPosition[11]),
                                                    new Vector2(xPosition[6], yPosition[7]), new Vector2(xPosition[6], yPosition[8]),
                                                    new Vector2(xPosition[6], yPosition[9]), new Vector2(xPosition[6], yPosition[10]),
                                                    new Vector2(xPosition[6], yPosition[11])};

            whitePiecePosition = new Vector2[15] {  new Vector2(xPosition[0], yPosition[7]), new Vector2(xPosition[0], yPosition[8]),
                                                    new Vector2(xPosition[0], yPosition[9]), new Vector2(xPosition[0], yPosition[10]),
                                                    new Vector2(xPosition[0], yPosition[11]), new Vector2(xPosition[4], yPosition[0]),
                                                    new Vector2(xPosition[4], yPosition[1]), new Vector2(xPosition[4], yPosition[2]),
                                                    new Vector2(xPosition[6], yPosition[0]), new Vector2(xPosition[6], yPosition[1]),
                                                    new Vector2(xPosition[6], yPosition[2]), new Vector2(xPosition[6], yPosition[3]),
                                                    new Vector2(xPosition[6], yPosition[4]), new Vector2(xPosition[11], yPosition[10]),
                                                    new Vector2(xPosition[11], yPosition[11])};
            base.Initialize();
        }

        protected override void LoadContent()

        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bluePiece = Content.Load<Texture2D>("BluePiece");
            gameBoard = Content.Load<Texture2D>("BlankGameBoard");
            stars[0] = Content.Load<Texture2D>("BlackStar");
            stars[1] = Content.Load<Texture2D>("WhiteStar");
            this.song = Content.Load<Song>("testsoundeffect2");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            winner[0] = Content.Load<Texture2D>("PlayerOneWin");
            winner[1] = Content.Load<Texture2D>("PlayerTwoWin");

            for (int x = 0; x < 6; x++)
            {
                darkTrianglesPointingDown[x] = Content.Load<Texture2D>("DarkGreenPointingDown");
                lightTrianglesPointingDown[x] = Content.Load<Texture2D>("LightGreenPointingDown");
                darkTrianglesPointingUp[x] = Content.Load<Texture2D>("DarkGreenPointingUp");
                lightTrianglesPointingUp[x] = Content.Load<Texture2D>("LightGreenPointingUp");
            }

            for (int x = 0; x < 15; x++)
            {
                blackPieces[x] = Content.Load<Texture2D>("BlackPiece");
                whitePieces[x] = Content.Load<Texture2D>("WhitePiece");
            }

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

            gameStartScreens[0] = Content.Load<Texture2D>("Welcome");
            gameStartScreens[1] = Content.Load<Texture2D>("Background");
            gameStartScreens[2] = Content.Load<Texture2D>("Controls");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (blackPiecePosition[0] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[1] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[2] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[3] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[4] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[5] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[6] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[7] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[8] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[9] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[10] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[11] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[12] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[13] == new Vector2(xPosition[11] + 59, yPosition[0]) &&
                blackPiecePosition[14] == new Vector2(xPosition[11] + 59, yPosition[0]))
            {
                player1Wins = true;
            }

            if (whitePiecePosition[0] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[1] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[2] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[3] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[4] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[5] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[6] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[7] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[8] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[9] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[10] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[11] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[12] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[13] == new Vector2(xPosition[11] + 59, yPosition[11]) &&
                whitePiecePosition[14] == new Vector2(xPosition[11] + 59, yPosition[11]))
            {
                player2Wins = true;
            }

            mState = Mouse.GetState();
            // Right Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && rightKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X + 59, savedPosition.Y);
                rightKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (bluePiecePosition.X == blackPiecePosition[x].X + 59 && bluePiecePosition.Y == blackPiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X + 59, blackPiecePosition[x].Y);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.X == whitePiecePosition[x].X + 59 && bluePiecePosition.Y == whitePiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X + 59, whitePiecePosition[x].Y);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                rightKeyReleased = true;
            }

            // Left Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && leftKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X - 59, savedPosition.Y);
                leftKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (bluePiecePosition.X == blackPiecePosition[x].X - 59 && bluePiecePosition.Y == blackPiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X - 59, blackPiecePosition[x].Y);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.X == whitePiecePosition[x].X - 59 && bluePiecePosition.Y == whitePiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X - 59, whitePiecePosition[x].Y);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                leftKeyReleased = true;
            }

            // Up Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && upKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X, savedPosition.Y - 39);
                upKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (bluePiecePosition.Y == blackPiecePosition[x].Y - 39 && bluePiecePosition.X == blackPiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X, blackPiecePosition[x].Y - 39);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.Y == whitePiecePosition[x].Y - 39 && bluePiecePosition.X == whitePiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X, whitePiecePosition[x].Y - 39);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                upKeyReleased = true;
            }

            // Down Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && downKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X, savedPosition.Y + 39);
                downKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (bluePiecePosition.Y == blackPiecePosition[x].Y + 39 && bluePiecePosition.X == blackPiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X, blackPiecePosition[x].Y + 39);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.Y == whitePiecePosition[x].Y + 39 && bluePiecePosition.X == whitePiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X, whitePiecePosition[x].Y + 39);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                downKeyReleased = true;
            }

            // Roll Dice Button Logic
            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                float mouseRollDiceButtonDistance = Vector2.Distance(new Vector2(RollDiceButtonPosition.X + RollDiceButtonRadius, RollDiceButtonPosition.Y + RollDiceButtonRadius), mState.Position.ToVector2());
                if (mouseRollDiceButtonDistance < RollDiceButtonRadius)
                {
                    RollDice();
                    mReleased = false;
                }
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && enterKeyReleased == true)
            {
                screenCount++;
                enterKeyReleased = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                enterKeyReleased = true;
            }


            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _spriteBatch.Draw(gameBoard, gameBoardPosition, null, Color.White, Of, new Vector2(gameBoard.Width / 2, gameBoard.Height / 2), Vector2.One, SpriteEffects.None, Of);
            for (int x = 0; x < 6; x++)
            {
                _spriteBatch.Draw(darkTrianglesPointingDown[x], new Vector2((118 * x) + 49, 2), Color.White);
                _spriteBatch.Draw(lightTrianglesPointingDown[x], new Vector2((118 * x) + 109, 2), Color.White);
                _spriteBatch.Draw(darkTrianglesPointingUp[x], new Vector2((118 * x) + 109, 280), Color.White);
                _spriteBatch.Draw(lightTrianglesPointingUp[x], new Vector2((118 * x) + 49, 280), Color.White);
            }

            for (int y = 0; y < 15; y++)
            {
                _spriteBatch.Draw(blackPieces[y], blackPiecePosition[y], Color.Black);
                _spriteBatch.Draw(whitePieces[y], whitePiecePosition[y], Color.White);
            }
            _spriteBatch.Draw(bluePiece, bluePiecePosition, Color.White);

            _spriteBatch.Draw(stars[0], new Vector2(xPosition[11] + 59, yPosition[0]), Color.White);
            _spriteBatch.Draw(stars[1], new Vector2(xPosition[11] + 59, yPosition[11]), Color.White);

            _spriteBatch.Draw(dieOne[valueDieOne], new Vector2(190, 226), Color.White);
            _spriteBatch.Draw(dieTwo[valueDieTwo], new Vector2(250, 226), Color.White);

            if (valueDieOne == valueDieTwo)
            {
                _spriteBatch.Draw(dieThree[valueDieThree], new Vector2(130, 226), Color.White);
                _spriteBatch.Draw(dieFour[valueDieFour], new Vector2(70, 226), Color.White);
            }

            _spriteBatch.Draw(rollDiceButton, RollDiceButtonPosition, Color.White);

            if (player1Wins == true)
            {
                _spriteBatch.Draw(winner[0], new Vector2 (-230,0), null, Color.White, 0f, Vector2.Zero, 0.47f, SpriteEffects.None, 0f);
            }

            if (player2Wins == true)
            {
                _spriteBatch.Draw(winner[1], new Vector2 (-210,0), null, Color.White, 0f, Vector2.Zero, 0.47f, SpriteEffects.None, 0f);
            }

            if (screenCount == 0)
            _spriteBatch.Draw(gameStartScreens[0], new Vector2 (-35,0), null, Color.White, 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0f);
            
            if (screenCount == 1)
            _spriteBatch.Draw(gameStartScreens[1], new Vector2 (-35,0), null, Color.White, 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0f);

            if (screenCount == 2)
            _spriteBatch.Draw(gameStartScreens[2], new Vector2 (-68,0), null, Color.White, 0f, Vector2.Zero, 0.55f, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}