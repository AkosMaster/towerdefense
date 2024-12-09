using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;
public class SpriteManager {

    private readonly List<Sprite> gameSprites = new List<Sprite>();
    public void registerSprite(Sprite sprite) {
        gameSprites.Add(sprite);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

        spriteBatch.Begin(samplerState: SamplerState.PointClamp); // pointClamp makes upscaled sprites look sharp
        foreach (Sprite sprite in gameSprites) {
            sprite.Draw(gameTime, spriteBatch);
        }
        spriteBatch.End();
    }

    public static readonly SpriteManager spriteManager = new SpriteManager();
}

