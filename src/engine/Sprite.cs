using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public interface IDrawable {
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}

public class Sprite : IDrawable
{
    private readonly Texture2D spriteTexture;
    public Color color = Color.White;
    public Transform transform = new Transform();

    public float layer = 0f;
    
    public Sprite(Texture2D _texture) {
        spriteTexture = _texture;

        SpriteManager.spriteManager.RegisterSprite(this);
    }

    public void Delete() {
        SpriteManager.spriteManager.UnRegisterSprite(this);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) { 
        spriteBatch.Draw(spriteTexture, transform.GetPosition() - new Vector2(spriteTexture.Width, spriteTexture.Height)*transform.GetScale()/2, null,
        color, 0f, Vector2.Zero, transform.GetScale(), SpriteEffects.None, layer);
    }
}