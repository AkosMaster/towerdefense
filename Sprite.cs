using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Sprite
{
    private Texture2D spriteTexture;

    private Vector2 spritePosition;
    private Vector2 spriteScale;
    public Sprite(Texture2D _texture) {
        spriteTexture = _texture;
        spritePosition = Vector2.Zero;
        spriteScale = new Vector2(1,1);
    }

    public void setSpriteScale(Vector2 scale) {
        spriteScale = scale;
    }

    public void setSpritePosition(Vector2 position) {
        spritePosition = position;
    }

    public Vector2 getSpriteScale() {
        return spriteScale;
    }
    public Vector2 getSpritePosition() {
        return spritePosition;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(spriteTexture, spritePosition, null,
        Color.White, 0f, Vector2.Zero, spriteScale, SpriteEffects.None, 0f);
    }
}