/// Jacob Boyd
/// Mr T.
/// ICS3U
/// March
/// 2D air hockey game

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace airHockey
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        #region Variables
        int height = 371;
        int p1Score = 0;
        int p2Score = 0;

        int paddle1X = 150;
        int paddle1Y = 150;


        int paddle2X = 500;
        int paddle2Y = 150;


        int paddleWidth = 25;
        int paddleHeight = 25;
        int paddleSpeed = 7;

        int ballX = 300;
        int ballY = 220;
        int ballXSpeed = 4;
        int ballYSpeed = 4;
        int ballWidth = 10;
        int ballHeight = 10;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;


        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        bool gameStart = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        SoundPlayer hitNoise = new SoundPlayer(Properties.Resources.hitNoise);
        SoundPlayer airHorn = new SoundPlayer(Properties.Resources.airHorn);
        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Detects key presses
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;


                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // detect when the button is released
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;

                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void GameTimerTick_Tick(object sender, EventArgs e)
        {
            //move ball           
            if (gameStart == false) { }
            else
            {
                ballX += ballXSpeed;
                ballY += ballYSpeed;
            }

            #region move players
            if (wDown == true && paddle1Y > 90)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 108)
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && paddle1X < 395 - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }

            //move player 2 
            if (upArrowDown == true && paddle2Y > 90)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > 410)
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X < 695 - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }
            #endregion

            //check if ball hit top or bottom wall and change direction if it does 
            if (ballY < 90 || ballY > height - ballHeight)
            {
                ballYSpeed *= -1;
                hitNoise.Play();
            }

            #region Hitboxes
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y + 5, paddleWidth, paddleHeight - 10); 
            Rectangle player1RecTopBottom = new Rectangle(paddle1X + 5, paddle1Y, paddleWidth - 10, paddleHeight);

            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y + 5, paddleWidth, paddleHeight - 10);
            Rectangle player2RecTopBottom = new Rectangle(paddle2X + 5, paddle2Y, paddleWidth - 10, paddleHeight);

            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            Rectangle farWallTop = new Rectangle(700, 75, 5, 110);
            Rectangle farWallBottom = new Rectangle(700, 265, 5, 115);

            Rectangle closeWallTop = new Rectangle(100, 75, 5, 110);
            Rectangle closeWallBottom = new Rectangle(100, 265, 5, 115);

            Rectangle p1Goal = new Rectangle(85, 185, 15, 80);
            Rectangle p2Goal = new Rectangle(705, 185, 15, 80);

            #endregion

            #region wall detection
            //check if ball hits the side walls
            if (farWallTop.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                hitNoise.Play();
            }
            else if (farWallBottom.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                hitNoise.Play();
            }
            else if (closeWallTop.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                hitNoise.Play();
            }
            else if (closeWallBottom.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                hitNoise.Play();
            }
            #endregion

            #region player detection

            if (player1Rec.IntersectsWith(ballRec))
            {
                gameStart = true;
                if (ballX < paddle1X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle1X - paddleWidth - 1;
                    hitNoise.Play();
                }
                else if (ballX > paddle1X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle1X + paddleWidth + 1;
                    hitNoise.Play();
                }
            }

            if (player1RecTopBottom.IntersectsWith(ballRec))
            {
                gameStart = true;
                if (ballY < paddle1Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle1Y - paddleHeight - 1;
                    hitNoise.Play();
                }
                else if (ballY > paddle1Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle1Y + paddleHeight + 1;
                    hitNoise.Play();
                }
            }

            if (player2Rec.IntersectsWith(ballRec))
            {
                gameStart = true;
                if (ballX < paddle1X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle2X - paddleWidth - 1;
                    hitNoise.Play();
                }
                else if (ballX > paddle2X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle2X + paddleWidth + 1;
                    hitNoise.Play();
                }
            }

            if (player2RecTopBottom.IntersectsWith(ballRec))
            {
                gameStart = true;
                if (ballY < paddle2Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle2Y - paddleHeight - 1;
                    hitNoise.Play();
                }
                else if (ballY > paddle2Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle2Y + paddleHeight + 1;
                    hitNoise.Play();
                }
            }
            #endregion

            #region goal and output
            //Check for goal
            if (p1Goal.IntersectsWith(ballRec))
            {
                gameStart = false;
                p2Score++;
                ballX = 300;
                ballY = 220;
                airHorn.Play();
            }

            if (p2Goal.IntersectsWith(ballRec))
            {
                gameStart = false;
                p1Score++;
                ballX = 500;
                ballY = 220;
                airHorn.Play();
            }
            p1ScoreLabel.Text = $"{p1Score}";
            p2ScoreLabel.Text = $"{p2Score}";
            if(p1Score == 3)
            {
                winnerLabel.Visible = true;
                winnerLabel.ForeColor = Color.DodgerBlue;
                winnerLabel.Text = "P1 WINS";
                gameTimerTick.Enabled = false;
            }
            if (p2Score == 3)
            {
                winnerLabel.Visible = true;
                winnerLabel.ForeColor = Color.Red;
                winnerLabel.Text = "P2 WINS";
                gameTimerTick.Enabled = false;
            }
            #endregion
            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //white background and center line
            e.Graphics.FillRectangle(whiteBrush, 100, 75, 605, 300);
            e.Graphics.FillRectangle(redBrush, 400, 75, 5, 300);

            //borders
            e.Graphics.FillRectangle(blackBrush, 100, 75, 600, 5);
            e.Graphics.FillRectangle(blackBrush, 100, 375, 600, 5);
            e.Graphics.FillRectangle(blackBrush, 700, 75, 5, 110);
            e.Graphics.FillRectangle(blackBrush, 700, 265, 5, 115);
            e.Graphics.FillRectangle(blackBrush, 100, 75, 5, 110);
            e.Graphics.FillRectangle(blackBrush, 100, 265, 5, 115);

            //nets
            e.Graphics.FillRectangle(redBrush, 85, 185, 15, 80);
            e.Graphics.FillRectangle(redBrush, 705, 185, 15, 80);

            //ball
            e.Graphics.FillRectangle(blackBrush, ballX, ballY, ballWidth, ballHeight);

            //paddles
            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
        }
    }
}

