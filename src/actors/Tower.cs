using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Tower : GameObject
{

    public Tower() : base("tower") {
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("tower1")));
        transform.localScale = new Vector2(0.5f,0.5f);
    }
    
    float elapsed = 0;
    float shotInterval = 500;
    public override void Update(GameTime gameTime) {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsed > shotInterval) {
            elapsed -= shotInterval;
            
            Projectile proj = new Projectile(new Vector2(-1,0));
            proj.transform.localPosition = transform.localPosition;
        }
    }
}