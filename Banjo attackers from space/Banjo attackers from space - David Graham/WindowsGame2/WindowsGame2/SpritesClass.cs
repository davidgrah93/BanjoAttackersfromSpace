using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    public class SpritesClass
    {
        public int SpriteSpeed;
        public int SpriteLife;
        public Texture2D SpriteTexture;
        public Rectangle SpritePosition;
        public SpritesClass(int x, int y, int width, int height, Texture2D Texture, int Speed, int Life) //Constructor
        {
            Initialise(new Rectangle(x, y, width, height), Texture, Speed, Life);
        }

        public void Initialise(Rectangle Position, Texture2D Texture, int Speed, int Life)
        {
            SpritePosition = Position;
            SpriteTexture = Texture;
            SpriteSpeed = Speed;
            SpriteLife = Life;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (SpriteLife > 0)
            {
                spriteBatch.Draw(SpriteTexture, SpritePosition, Color.White);
            }
        }
        public virtual void save(System.IO.TextWriter textout)
        {
            textout.WriteLine(SpriteLife);
            textout.WriteLine(SpritePosition.X);
            textout.WriteLine(SpritePosition.Y);
        }

        public virtual void load(System.IO.TextReader textin)
        {
            SpriteLife= int.Parse(textin.ReadLine());
            SpritePosition.X = int.Parse(textin.ReadLine());
            SpritePosition.Y = int.Parse(textin.ReadLine());
        }
    }
}
