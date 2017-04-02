using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    public class HunterBanjo : BaseBanjo
    {
        double timer = 0;
        public HunterBanjo(int x, int y, int width, int height, Texture2D Texture, int life, bool direction, double time)
            : base(x, y, width, height, Texture, 3, life, direction)
        {
            timer = time;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime, int ScreenWidth)
        {
            if (timer < 5)
            {
                base.Update(gameTime, ScreenWidth);
            }
            else //if (timer >= 5)
            {
                //chase code
                Rectangle playerRect = BanjoGame.Accordian.getPos();
                SpritePosition.Y -= SpriteSpeed;
                if (playerRect.X < SpritePosition.X)
                {
                    SpritePosition.X += SpriteSpeed;
                }
                else if (playerRect.X > SpritePosition.X)
                {
                    SpritePosition.X -= SpriteSpeed;
                }
            }

            timer = timer + gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override void save(System.IO.TextWriter textout)
        {
            base.save(textout);
            textout.WriteLine(timer);
        }
        
    }
}

