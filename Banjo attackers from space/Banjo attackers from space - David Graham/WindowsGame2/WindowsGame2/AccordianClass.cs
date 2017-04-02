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
    public class AccordianClass : SpritesClass
    {
        public AccordianClass(int x, int y, int width, int height, Texture2D Texture)
            : base(x, y, width, height, Texture, 8, 3)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public void AccordianMove(string Direction, int ScreenWidth)
        {
            if (Direction == "Right")
            {
                SpritePosition.X += SpriteSpeed;
            }
            if (Direction == "Left")
            {
                SpritePosition.X -= SpriteSpeed;
            }
            if (SpritePosition.X > ScreenWidth - SpritePosition.Width)
            {
                SpritePosition.X = ScreenWidth - SpritePosition.Width;
            }
            if (SpritePosition.X < 0)
            {
                SpritePosition.X = 0;
            }
        }
        public Rectangle getPos()
        {
            return SpritePosition;
        }
        public override void save(System.IO.TextWriter textout)
        {
            base.save(textout);
        }
    }
}
