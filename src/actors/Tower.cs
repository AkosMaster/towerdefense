using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Tower : GameObject
{
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("basic_mushroom");
    public Tower() : base("tower") {
        SetSprite(new Sprite(texture));
        transform.localScale = new Vector2(0.1f,0.1f);
    }
    
    float elapsed = 0;
    float shotInterval = 100;
    public override void Update(GameTime gameTime) {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsed > shotInterval) {
            elapsed -= shotInterval;
            double rotation = gameTime.TotalGameTime.TotalMilliseconds/1000f;
            Projectile proj = new Projectile(new Vector2((float)Math.Sin(rotation),(float)Math.Cos(rotation)));
            proj.transform.localPosition = transform.localPosition;
        }

        sprite.transform.localScale.Y = 1f + (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds/700f)/50;
    }
}