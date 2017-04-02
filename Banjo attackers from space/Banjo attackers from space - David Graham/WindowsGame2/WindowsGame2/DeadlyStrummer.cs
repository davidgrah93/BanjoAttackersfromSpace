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
    public class DeadlyStrummer : BaseBanjo 
    {
        public DeadlyStrummer(int x, int y, int width, int height, Texture2D Texture, int life, bool direction) :
            base(x, y, width, height, Texture, 4, life, direction)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime, int ScreenWidth)
        {
            Rectangle playerRect = BanjoGame.Accordian.getPos();
            SpritePosition.Y += SpriteSpeed;
            if (playerRect.X < SpritePosition.X)
            {
                SpritePosition.X -= SpriteSpeed;
            }
            else if (playerRect.X > SpritePosition.X)
            {
                SpritePosition.X += SpriteSpeed;
            }
        }
        public override void save(System.IO.TextWriter textout)
        {
            base.save(textout);
        }
    }
}