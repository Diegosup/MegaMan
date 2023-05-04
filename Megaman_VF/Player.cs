using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Scroll
{
    public class Player
    {
        public int life = 28;
        Sprite mainSprite;
        
        public float fPlayerPosX = 0.0f;
        public float fPlayerPosY = 5.0f;

        private float fPlayerVelX = 0.0f;
        private float fPlayerVelY = 0.0f;
        public float mov;
        public bool isDown = false;

        public bool hit=false,endGame=false;
        public Sprite MainSprite
        {
            get { return mainSprite; }
        }

        public float FPlayerVelX
        {
            get { return fPlayerVelX; }
            set { fPlayerVelX = value; }
        }        

        public float FPlayerVelY
        {
            get { return fPlayerVelY; }
            set { fPlayerVelY = value; }
        }

        public Player(Size original, Size display,Point pos, Bitmap resourceR,Bitmap resourceL)
        {
            
            mainSprite = new Sprite(original,display, pos, resourceR, resourceL);
            //mainSprite = new Sprite(new Size(32, 110), new Size(40, 100), new Point(), Resource1.PREN, Resource1.PREN);

        }

        public void Right(float fElapsedTime)
        {
            FPlayerVelX += (bPlayerOnGround ? 25.0f : 15.0f) * fElapsedTime;
            if(bPlayerOnGround)
                mainSprite.MoveRight();
        }

        public void Left(float fElapsedTime)
        {
            FPlayerVelX += (bPlayerOnGround ? -25.0f : -15.0f) * fElapsedTime;
            if(bPlayerOnGround)
                mainSprite.MoveLeft();
        }

        public void Frame(int x)
        {
            mainSprite.Frame(x);
        }
        public void Stop()
        {
            mainSprite.Frame(0);
        }

        public bool bPlayerOnGround = false;
        public bool bPlayerOnLTM = false;
        public bool bPlayerOnRTM = false;

        public void UpdateT(float fElapsedTime,Map map,bool fall)
        {

            float inc=0;
            // Clamp velocities

            if (fPlayerVelY > 300.0f)
                fPlayerVelY = 300.0f;
            if (fPlayerVelY < -250.0f)
                fPlayerVelY = -250.0f;



            float fNewPlayerPosX = fPlayerPosX + fPlayerVelX * fElapsedTime;
            float fNewPlayerPosY = fPlayerPosY + fPlayerVelY * fElapsedTime;

            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'o', '.');
            

            char cTile = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.0f));
            char cTile3 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 0.0f));
            char cTile4 = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fNewPlayerPosY + 1.0f));
            char cTile5 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 1f));
            // COLLISION
            
            if(fall)
            {
                inc = 20.0f;
            }
            if (!fall)
            {
                inc = -20.0f;
            }
            if (fPlayerVelY <= 0)// up
            {
                if ((cTile != '.' && cTile != '@' && cTile != '-') || (cTile3 != '.' && cTile3 != '@' && cTile3 != '-'))
                {
                    fNewPlayerPosY = (int)fNewPlayerPosY + 1;
                    inc=inc+8;
                }
            }
            else if ((cTile4 != '.' ) || (cTile5 != '.' ))
                {
                    
                    fNewPlayerPosY = (int)fNewPlayerPosY;
                    inc=inc-8;

                    if (!bPlayerOnGround)
                        Frame(0);
                    bPlayerOnGround = true;
                    
                }
            //Gravity
            fPlayerVelY += inc * fElapsedTime;//---------------

            fPlayerPosX = fNewPlayerPosX;
            fPlayerPosY = fNewPlayerPosY;

            mainSprite.Display(map.g);
        }

        public void RotateImage180()
        {
            MainSprite.imgDisplay.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        public void UpdateEP(float fElapsedTime, Map map,float velY)
        {
            //Gravity
            fPlayerVelY += 20.0f * fElapsedTime;//---------------


            if (bPlayerOnLTM)
            {
                fPlayerVelX += -0.9f;
            }
            if (bPlayerOnRTM)
            {
                fPlayerVelX += +0.9f;
            }

            // Clamp velocities



            if (fPlayerVelY > velY )
                fPlayerVelY =  velY;


            float fNewPlayerPosX = fPlayerPosX + fPlayerVelX * fElapsedTime;
            float fNewPlayerPosY = fPlayerPosY + fPlayerVelY * fElapsedTime;



            char cTile = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.0f));
            char cTile2 = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.95f));
            char cTile3 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 0.0f));
            char cTile4 = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fNewPlayerPosY + 1.0f));
            char cTile5 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 1f));
            // COLLISION
            if (fPlayerVelX <= 0)//left
            {

                if ((cTile != '.') || (cTile2 != '.'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX + 1;
                    //fPlayerVelX = 0;
                }
            }
            else//right
            {
                if ((cTile != '.') || (cTile2 != '.'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX;

                    //fPlayerVelX = 0;
                }
            }

            //bPlayerOnGround = false;
            bPlayerOnLTM = false;
            bPlayerOnRTM = false;
            if (fPlayerVelY <= 0)// up
            {

            }
            else
            {

                if ((cTile4 != '.') || (cTile5 != '.'))
                {

                    fNewPlayerPosY = (int)fNewPlayerPosY;
                    //fPlayerVelY = 0;

                    if (!bPlayerOnGround)
                        Frame(0);


                    bPlayerOnGround = true;
                    if (cTile4 == 'L' || cTile5 == 'L')
                    {
                        bPlayerOnLTM = true;
                    }
                    if (cTile4 == 'R' || cTile5 == 'R')
                    {
                        bPlayerOnRTM = true;
                    }
                }

            }

            fPlayerPosX = fNewPlayerPosX;
            fPlayerPosY = fNewPlayerPosY;

            mainSprite.Display(map.g);
        }

        public void UpdateE(float fElapsedTime, Map map,float lim)
        {
            float fNewPlayerPosX = fPlayerPosX + fPlayerVelX * fElapsedTime;
            float fNewPlayerPosY = fPlayerPosY + fPlayerVelY * fElapsedTime;
            
            fPlayerPosX = fNewPlayerPosX;
            fPlayerPosY = fNewPlayerPosY;
            
            mov = 0;

            if (fPlayerPosY < 3.5f && fPlayerPosY >= lim && !isDown)
            {
                
                if (fPlayerPosY >= 3.2f)
                    isDown = true;
                mov = 55;
            }else if (fPlayerPosY < 3.5f && fPlayerPosY >= lim && isDown)
            {
                if (fPlayerPosY <= lim+0.7f)
                    isDown = false;
                mov = -20;
            }
           
            fPlayerVelY += mov * fElapsedTime;

            mainSprite.Display(map.g);
        }
        public void Update(float fElapsedTime, Map map)
        {

            //Gravity
            fPlayerVelY += 20.0f * fElapsedTime;//---------------

            // Drag
            if (bPlayerOnGround)
            {
                fPlayerVelX += -2.5f * fPlayerVelX * fElapsedTime;

                if (Math.Abs(fPlayerVelX) < 0.01f)
                    fPlayerVelX = 0.0f;
            }
            if (bPlayerOnLTM && bPlayerOnGround)
            {
                fPlayerVelX += -0.35f;
            }
            if (bPlayerOnRTM && bPlayerOnGround)
            {
                fPlayerVelX += 0.35f;
            }

            // Clamp velocities
            if (fPlayerVelX > 7.0f)
                fPlayerVelX = 7.0f;

            if (fPlayerVelX < -7.0f)
                fPlayerVelX = -7.0f;

            if (fPlayerVelY > 5.0f)
                fPlayerVelY = 5.0f;

            if (fPlayerVelY < -5.0f)
                fPlayerVelY = -5.0f;

            float fNewPlayerPosX = fPlayerPosX + fPlayerVelX * fElapsedTime;
            float fNewPlayerPosY = fPlayerPosY + fPlayerVelY * fElapsedTime;

            CheckPicks(map, fNewPlayerPosX, fNewPlayerPosY, 'e', '.');
            

            char cTile = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.0f));
            char cTile2 = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fPlayerPosY + 0.95f));
            char cTile3 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 0.0f));
            char cTile4 = map.GetTile((int)(fNewPlayerPosX + 0.0f), (int)(fNewPlayerPosY + 1.0f));
            char cTile5 = map.GetTile((int)(fNewPlayerPosX + 0.9f), (int)(fNewPlayerPosY + 1f));
            // COLLISION
            if (fPlayerVelX <= 0)//left
            {

                if ((cTile != '.' && cTile != '@' && cTile != '-') || (cTile2 != '.' && cTile2 != '@' && cTile2 != '-'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX + 1;
                    fPlayerVelX = 0;
                }
            }
            else//right
            {
                if ((cTile != '.' && cTile != '@' && cTile != '-') || (cTile2 != '.' && cTile2 != '@' && cTile2 != '-'))
                {
                    if (fPlayerVelX != 0)
                        fNewPlayerPosX = (int)fNewPlayerPosX;

                    fPlayerVelX = 0;
                }
            }

            //bPlayerOnGround = false;
            bPlayerOnLTM = false;
            bPlayerOnRTM = false;

            if (fPlayerVelY <= 0)// up
            {
                if ((cTile != '.' && cTile != '@' && cTile != '-') || (cTile3 != '.' && cTile3 != '@' && cTile3 != '-'))
                {
                    fNewPlayerPosY = (int)fNewPlayerPosY + 1;
                    fPlayerVelY = 0;
                }
            }
            else
            {

                if ((cTile4 != '.' && cTile4 != '@' && cTile4 != '-') || (cTile5 != '.' && cTile5 != '@' && cTile5 != '-'))
                {

                    fNewPlayerPosY = (int)fNewPlayerPosY;
                    fPlayerVelY = 0;

                    if (!bPlayerOnGround)
                        Frame(0);


                    bPlayerOnGround = true;
                    if (cTile4 == 'L' || cTile5 == 'L')
                    {
                        bPlayerOnLTM = true;
                    }
                    if (cTile4 == 'R' || cTile5 == 'R')
                    {
                        bPlayerOnRTM = true;
                    }
                }

            }

            fPlayerPosX = fNewPlayerPosX;
            fPlayerPosY = fNewPlayerPosY;

            mainSprite.Display(map.g);
        }

        private static void CheckPicks(Map map, float fNewPlayerPosX, float fNewPlayerPosY,char c, char c2)
        {
            // Check for pickups!
            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f) == c)
                map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 0.0f, c2);

            if (map.GetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f) == c)
                map.SetTile(fNewPlayerPosX + 0.0f, fNewPlayerPosY + 1.0f, c2);

            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f) == c)
                map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 0.0f, c2);

            if (map.GetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f) == c)
                map.SetTile(fNewPlayerPosX + 1.0f, fNewPlayerPosY + 1.0f, c2);
        }

    }
}
