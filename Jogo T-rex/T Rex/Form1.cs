using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex
{
    public partial class Form1 : Form
    {

        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 5;
        Random rand = new Random();
        int position;
        bool isGameOver = false;
        int scoreDefinition = 1;
        private int timeElapsed = 0;
        private int scoreIncrementInterval = 300;

        public Form1()
        {
            InitializeComponent();

            GameReset();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            timeElapsed += gameTimer.Interval;
            if (score <= 500)
            {

                if (score >= 20 && score <= 50)
                {
                    scoreDefinition = 2;
                }
                else if (score >= 100)
                {
                    scoreDefinition = 2;
                }
                else if (score >= 300)
                {
                    scoreDefinition = 3;
                }
                else
                {
                    scoreDefinition = 1;
                }

                if (timeElapsed >= scoreIncrementInterval)
                {
                    score = score + scoreDefinition;
                    txtScore.Text = "Score: " + score;
                    timeElapsed = 0;
                }

                trex.Top += jumpSpeed;

                txtScore.Text = "Score: " + score;

                if (jumping == true && force < 0)
                {
                    jumping = false;
                }
                if (jumping == true)
                {
                    jumpSpeed = -12;
                    force -= 1;
                }
                else
                {
                    jumpSpeed = 12;
                }
                if (trex.Top > 351 && jumping == false)
                {
                    force = 12;
                    trex.Top = 352;
                    jumpSpeed = 0;
                }
                foreach (Control x in this.Controls)
                {
                    if (score < 300 && score > 100)
                    {
                        obstacleSpeed = 15;
                    }
                    else if (score > 300 && score < 500)
                    {
                        obstacleSpeed = 20;
                    }
                    if (x is PictureBox && (string)x.Tag == "obstacle")
                    {
                        x.Left -= obstacleSpeed;

                        if (x.Left < -100)
                        {
                            x.Left = this.ClientSize.Width + rand.Next(200, 600) + (x.Width * 20);
                            score = score + scoreDefinition;
                        }

                        if (trex.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            trex.Image = Properties.Resources.dead;
                            txtScore.Text += " Press R to restart the game!";
                            isGameOver = true;
                        }
                    }
                }
            }
            else
            {
                txtVencedor.Visible = true;
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !jumping)
            {
                if (trex.Top == 352) 
                {
                    jumpSpeed = -12; 
                    force = 10;
                    jumping = true;
                }
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
               
            }
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            trex.Top = 351;
            scoreDefinition = 1;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle" )
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    x.Left = position;  
                }

            }

            gameTimer.Start();




        }
    }
}
