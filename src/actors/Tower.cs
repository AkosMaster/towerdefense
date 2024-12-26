using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Tower : GameObject
{

    public Tower(){
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("archer")));
    }
    
    public override void Update(GameTime gameTime) {
        
    }
}