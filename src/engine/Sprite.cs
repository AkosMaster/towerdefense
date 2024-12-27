using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Sprite
{
    private readonly Texture2D spriteTexture;

    public Transform transform = new Transform();
    public Sprite(Texture2D _texture) {
        spriteTexture = _texture;

        SpriteManager.spriteManager.registerSprite(this);
    }

    public void Delete() {
        SpriteManager.spriteManager.unregisterSprite(this);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(spriteTexture, transform.getPosition(), null,
        Color.White, 0f, Vector2.Zero, transform.getScale(), SpriteEffects.None, 0f);
    }
}