using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class ItemSpawner : GameObject
{
    
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("placeholder");
    public ItemSpawner() : base("itemspawner") {
        SetSprite(new Sprite(texture));
        transform.localScale = new Vector2(5f,5f);
    }
    
    float elapsed = 0;
    float spawnInterval = 10000;

    private void Spawn(GameTime gameTime) {
        if (elapsed > spawnInterval) {
            elapsed -= spawnInterval;

            double rotation = gameTime.TotalGameTime.TotalMilliseconds/1000f + GetHashCode();

            Item item = new TestItem();
            item.transform.localPosition = transform.GetPosition() 
                + new Vector2((float)Math.Sin(rotation),(float)Math.Cos(rotation)) * 60;
        }
        elapsed += gameTime.ElapsedGameTime.Milliseconds;
    }

    
    public override void Update(GameTime gameTime) {
        
        Spawn(gameTime);
        sprite.transform.localScale.Y = 1f + (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds/700f)/50;
    }
}