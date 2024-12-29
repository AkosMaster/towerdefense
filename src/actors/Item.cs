using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class Item : GameObject
{

    static Texture2D texture = Game1.contentManager.Load<Texture2D>("placeholder");
    
    public Item() : base("item"){
        SetSprite(new Sprite(texture));
        transform.localScale = new Vector2(3,3);
    }

    public override void Update(GameTime gameTime)
    {
        sprite.transform.localPosition.Y = (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds/1000f)*10;
        sprite.transform.localScale.X = Math.Abs((float)Math.Cos(gameTime.TotalGameTime.TotalMilliseconds/1000f));


    }
}