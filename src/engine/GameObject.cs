using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public abstract class GameObject : IActor
{

    public Sprite sprite;
    public Transform transform = new Transform();

    public GameObject(){
        ActorManager.actorManager.registerActor(this);
    }

    public void setSprite(Sprite _sprite) {
        sprite = _sprite;
        sprite.transform.parent = transform;
    }

    public abstract void Update(GameTime gameTime);
}