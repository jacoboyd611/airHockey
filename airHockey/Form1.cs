using System;
using System.Drawing;
using System.Windows.Forms;

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


        int paddle1X = 150;
        int paddle1Y = 150;


        int paddle2X = 500;
        int paddle2Y = 150;


        int paddleWidth = 25;
        int paddleHeight = 25;
        int paddleSpeed = 7;

        int ballX = 400;
        int ballY = 195;
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

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
            ballX += ballXSpeed;
            ballY += ballYSpeed;

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
            }


            #region Hitboxes
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            

            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            

            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            Rectangle farWallTop = new Rectangle(700, 75, 5, 110);
            Rectangle farWallBottom = new Rectangle(700, 265, 5, 115);

            Rectangle closeWallTop = new Rectangle(100, 75, 5, 110);
            Rectangle closeWallBottom = new Rectangle(100, 265, 5, 115);

            Rectangle p1Goal = new Rectangle(85, 185, 15, 80);
            Rectangle p2Goal = new Rectangle(705, 185, 15, 80);

            #endregion

            //check if ball hits the side walls
            if (farWallTop.IntersectsWith(ballRec)) { ballXSpeed *= -1; }
            else if (farWallBottom.IntersectsWith(ballRec)) { ballXSpeed *= -1; }
            else if (closeWallTop.IntersectsWith(ballRec)) { ballXSpeed *= -1; }
            else if (closeWallBottom.IntersectsWith(ballRec)) { ballXSpeed *= -1; }

            #region player detection
            if (player1Rec.IntersectsWith(ballRec))
            {
                if (ballY > paddle1Y + paddleHeight )
                {
                    ballYSpeed *= -1;
                    ballY = paddle1Y + paddleHeight + 1;
                }
                if (ballY < paddle1Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle1Y - 1;
                }
                else if (ballY > paddle1Y && ballY < paddle1Y + paddleHeight)
                {
                    if (ballX < paddle1X)
                    {
                        ballXSpeed *= -1;
                        ballX = paddle1X - 1;
                    }
                    else if (ballX > paddle1X + paddleWidth)
                    {
                        ballXSpeed *= -1;
                        ballX = paddle1X + paddleWidth + 1;
                    }
                }
            }

            if (player2Rec.IntersectsWith(ballRec))
            {
                if (ballY > paddle2Y + paddleHeight)
                {
                    ballYSpeed *= -1;
                    ballY = paddle2Y + paddleHeight + 1;
                }
                if (ballY < paddle2Y)
                {
                    ballYSpeed *= -1;
                    ballY = paddle2Y - 1;
                }
                if (ballY > paddle2Y && ballY < paddle1Y + paddleHeight)
                {
                    if (ballX < paddle2X)
                    {
                        ballXSpeed *= -1;
                        ballX = paddle2X - 1;
                    }
                    else if (ballX > paddle2X + paddleWidth)
                    {
                        ballXSpeed *= -1;
                        ballX = paddle2X + paddleWidth + 1;
                    }
                }
            }
            #endregion

            //Check for goal
            if (p1Goal.IntersectsWith(ballRec))
            {
                ballX = 400;
                ballY = 195;
            }

            if (p2Goal.IntersectsWith(ballRec))
            {
                ballX = 400;
                ballY = 195;
            }
            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, 100, 75, 605, 300);
            e.Graphics.FillRectangle(redBrush, 400, 75, 5, 300);

            e.Graphics.FillRectangle(blackBrush, 100, 75, 600, 5);
            e.Graphics.FillRectangle(blackBrush, 100, 375, 600, 5);
            e.Graphics.FillRectangle(blackBrush, 700, 75, 5, 110);
            e.Graphics.FillRectangle(blackBrush, 700, 265, 5, 115);
            e.Graphics.FillRectangle(blackBrush, 100, 75, 5, 110);
            e.Graphics.FillRectangle(blackBrush, 100, 265, 5, 115);

            e.Graphics.FillRectangle(redBrush, 85, 185, 15, 80);
            e.Graphics.FillRectangle(redBrush, 705, 185, 15, 80);

            e.Graphics.FillRectangle(blackBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);



        }
    }
}

