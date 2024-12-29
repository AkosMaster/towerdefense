using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class Item : GameObject
{
    protected Vector2 defaultScale = new Vector2(3, 3);
    
    public Item(Texture2D texture) : base("item")
    {
        SetSprite(new Sprite(texture));
        transform.localScale = defaultScale;
    }
    
    public Player carryingPlayer = null;
    public override void Update(GameTime gameTime)
    {
        if (carryingPlayer == null) {
            // float animation
            sprite.transform.localPosition.Y = (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds/1000f)*10;
            sprite.transform.localScale.X = Math.Abs((float)Math.Cos(gameTime.TotalGameTime.TotalMilliseconds/1000f));
        }

    }

    public void SetCarrier(Player player) {
        transform.parent = player.transform;
        transform.localPosition = new Vector2(0,0);
        sprite.transform.localScale = new Vector2(1, 1);
        carryingPlayer = player;
    }
    public void Drop(Vector2 position) {
        transform.parent = null;
        transform.localPosition = position;
        carryingPlayer = null;
    }
}