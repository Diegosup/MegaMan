using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll
{
    public class Enemy
    {
        Sprite mainSprite;
        public float fEnemyPosX = 0.0f;
        public float fEnemyPosY = 5.0f;

        private float fEnemyVelX = 0.0f;
        private float fEnemyVelY = 0.0f;

        public PointF Position { get; set; }
        public float Health { get; set; }
        public bool IsAlive { get { return Health > 0; } }

        public Enemy(PointF position, float health, Size original,Size display,Bitmap resource)
        {
            mainSprite = new Sprite(original, display, new Point(), resource,resource);
            Position = position;
            Health = health;
        }

        public void Frame(int x)
        {
            mainSprite.Frame(x);
        }
        public void Stop()
        {
            mainSprite.Frame(0);
        }

        

        public void Update(float fElapsedTime, Player player)
        {
            // Implementa aquí la lógica de movimiento y ataque de los enemigos
            // Si el enemigo muere, asegúrate de ajustar su salud a cero
            // Puedes verificar si el jugador se encuentra dentro del rango de ataque del enemigo, y si es así, aplicar el daño correspondiente
          
                
            
        }
    }

}
