using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;
public class SpriteManager {

    private readonly List<IDrawable> gameSprites = new List<IDrawable>();

    public List<IDrawable> getSprites() {
        return gameSprites;
    }

    public void RegisterSprite(IDrawable sprite) {
        gameSprites.Add(sprite);
    }

    public void UnRegisterSprite(IDrawable sprite) {
        gameSprites.Remove(sprite);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

        spriteBatch.Begin(samplerState: SamplerState.PointClamp); // pointClamp makes upscaled sprites look sharp
        
        foreach (IDrawable sprite in gameSprites) {
            sprite.Draw(gameTime, spriteBatch);
        }
        spriteBatch.End();
    }

    public static readonly SpriteManager spriteManager = new SpriteManager();
}

