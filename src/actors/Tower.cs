using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Tower : GameObject
{

    public Tower(){
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("archer")));
    }
    
    float elapsed = 0;
    float shotInterval = 200;
    public override void Update(GameTime gameTime) {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsed > shotInterval) {
            elapsed -= shotInterval;
            
            Projectile proj = new Projectile(new Vector2(0,0));
        }
    }
}