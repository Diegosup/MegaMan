using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Scroll
{
    public partial class MAIN : Form
    {
        Map map;
        Player player;

        float fElapsedTime;

        public static SoundPlayer sPlayer, sItem;
        Thread thread, thread2;
        bool isP1 = true;

        // Camera properties
        float fCameraPosX = 0.0f;
        float fCameraPosY = 0.0f;
        bool left, right, shootR, shootL;
        char lastKeyClicked;

        public MAIN()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            map = new Map(PCT_CANVAS.Size);
            player = new Player(new Size(33, 26), new Size(33, 26), new Point(), Resource1.allMegaMan, Resource1.allMegaMan);

            PCT_CANVAS.Image = map.bmp;
            fElapsedTime = 0.05f;
            left = false;
            right = false;
            sPlayer = new SoundPlayer(Resource1.MegaManIntro);
            sItem = new SoundPlayer(Resource1.fire);

            Play();
        }

        public void Play()
        {
            thread = new Thread(PlayThread);
            thread.Start();
            thread2 = new Thread(PlayThread);
            thread2.Start();
        }

        private void PlayThread()
        {
            sPlayer.Play();

        }

        private void MAIN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = true;
                    lastKeyClicked = 'l';
                    player.MainSprite.Position(0, 0);
                    break;
                case Keys.Right:
                    right = true;
                    lastKeyClicked = 'r';
                    player.MainSprite.Position(0, 26);
                    break;
                case Keys.Z:
                    if (lastKeyClicked == 'r')
                    {
                        shootR = true;
                        player.MainSprite.Position(0, 78);
                    }
                    else if (lastKeyClicked == 'l')
                    {
                        shootL = true;
                        player.MainSprite.Position(0, 52);
                    }
                    break;
                case Keys.Up:
                    player.FPlayerVelY = -9.0f;
                    if (lastKeyClicked == 'r')
                    {
                        player.MainSprite.Position(0, 130);
                    }
                    else if (lastKeyClicked == 'l')
                    {
                        player.MainSprite.Position(0, 104);
                    }
                    player.bPlayerOnGround = false;
                    break;
                case Keys.Down:
                    player.FPlayerVelY = 6.0f;
                    break;
            }

            UpdateEnv();
        }

        private void MAIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    if (player.FPlayerVelY == 0)// sin brincar o cayendo
                    {
                        player.FPlayerVelY = -15;
                        player.Frame(2);
                        player.bPlayerOnGround = false;
                    }
                    break;
                case (char)Keys.S:
                    map.shoot = true;
                    break;

            }
        }

        private void MAIN_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = false;
                    break;
                case Keys.Right:
                    right = false;
                    break;
                case Keys.Up:
                    player.bPlayerOnGround = true;
                    break;
                case Keys.Z:
                    shootL = false;
                    shootR = false;
                    break;
            }
        }

        private void MAIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            sPlayer.Stop();
            thread.Abort();

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void MAIN_Load(object sender, EventArgs e)
        {

        }

        private void PCT_CANVAS_Click(object sender, EventArgs e)
        {

        }

        public void TIMER_Tick(object sender, EventArgs e)
        {
            if (player.endGame)
            {
                TIMER.Stop();
                MessageBox.Show("¡Game Over!");
            }

            UpdateEnv();
        }


        private void UpdateEnv()
        {
            if (!right && !left && player.bPlayerOnGround)
                player.Frame(0);

            if (!shootR && !shootL && !right && !left)
            {
                if (lastKeyClicked == 'r')
                {
                    player.MainSprite.Position(0, 26);
                }
                else if (lastKeyClicked == 'l')
                {
                    player.MainSprite.Position(0, 0);
                }
            }

            if (right)
                player.Right(fElapsedTime);

            if (left)
                player.Left(fElapsedTime);

            if (!player.bPlayerOnGround)
            {
                if (lastKeyClicked == 'r')
                {
                    player.MainSprite.Position(0, 130);
                }
                else if (lastKeyClicked == 'l')
                {
                    player.MainSprite.Position(0, 104);
                }
            }


            fCameraPosX = player.fPlayerPosX;
            fCameraPosY = 8;
            if (player.fPlayerPosY > 15 && player.fPlayerPosY < 50)
            {
                fCameraPosY = player.fPlayerPosY;
            }

            map.Draw(new PointF(fCameraPosX, fCameraPosY), player.fPlayerPosX.ToString(), player);
            player.Update(fElapsedTime, map);

            PCT_CANVAS.Invalidate();
        }

    }
}
