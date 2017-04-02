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
    public class BaseBanjo : SpritesClass
    {
        bool DirectionChange;
        int YPosition;
        public BaseBanjo(int x, int y, int width, int height, Texture2D Texture, int Speed, int Life, bool direction)
            : base(x, y, width, height, Texture, Speed, Life)
        {
            DirectionChange = direction;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public virtual void Update(GameTime gameTime, int ScreenWidth)
        {
            if (SpritePosition.X > ScreenWidth - SpritePosition.Width)
                {
                    if (DirectionChange == false)
                    {
                        DirectionChange = true;
                        YPosition = SpritePosition.Y;
                    }
   
                    if (DirectionChange == true)
                    {
                    SpritePosition.Y = SpritePosition.Y + SpriteSpeed;
                    }

                    if (SpritePosition.Y >= YPosition + SpritePosition.Height)
                    {
                        DirectionChange = false;
                        SpritePosition.X = ScreenWidth - SpritePosition.Width;
                        SpriteSpeed = 0 - SpriteSpeed;
                        SpritePosition.X += SpriteSpeed;
                    }
                }      
                else
                    if (SpritePosition.X < 0)
                    {
                        if (DirectionChange == false)
                        {
                            DirectionChange = true;
                            YPosition = SpritePosition.Y;
                        }

                        if (DirectionChange == true)
                        {
                            SpritePosition.Y = SpritePosition.Y - SpriteSpeed;
                        }

                        if (SpritePosition.Y >= YPosition + SpritePosition.Height)
                        {
                            DirectionChange = false;
                            SpritePosition.X = 0;
                            SpriteSpeed = 0 - SpriteSpeed;
                            SpritePosition.X += SpriteSpeed;
                        }
                    }
                    else
                    {
                        SpritePosition.X += SpriteSpeed;
                    }
        }
        public override void save(System.IO.TextWriter textout)
        {
            base.save(textout);
            if (DirectionChange)
                textout.WriteLine("True");
            else
                textout.WriteLine("False");
            textout.WriteLine(YPosition);
        }

        public override void load(System.IO.TextReader textin)
        {
            base.load(textin);
            string dirText = textin.ReadLine();
            if(dirText == "True")
                DirectionChange = true;
            else
                DirectionChange = false;
            YPosition = int.Parse(textin.ReadLine());
        }
    }
}
