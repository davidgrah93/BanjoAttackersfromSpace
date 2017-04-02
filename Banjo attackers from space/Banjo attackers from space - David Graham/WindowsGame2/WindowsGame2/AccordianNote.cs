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
    public class AccordianNote : SpritesClass
    {
        public AccordianNote(int x, int y, int width, int height, Texture2D Texture)
            : base(x, y, width, height, Texture, 8, 1)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
                base.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime, int ScreenWidth)
        {
            if (SpriteLife > 0)
            {
                SpritePosition.Y -= SpriteSpeed;
            }
        }
        public override void save(System.IO.TextWriter textout)
        {
            base.save(textout);
        }
    }
}
