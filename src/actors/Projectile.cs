using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Projectile : GameObject
{
    bool targetMode = false;
    public float speed = 0.1f;
    GameObject target;
    Vector2 direction;
    public Projectile(GameObject _target) : base("projectile"){
        target = _target;
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("bullet1")));
        targetMode = true;
        collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    }
    public Projectile(Vector2 _direction) : base("projectile"){
        direction = _direction;
        direction.Normalize();
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("bullet1")));
        
        collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    } 
    public override void Update(GameTime gameTime) {
        if (targetMode) {
            Vector2 directionVector = target.transform.getPosition() - transform.getPosition();
            directionVector.Normalize();
            transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
        } else {
            transform.localPosition += direction * speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        foreach(GameObject enemy in GameObject.getGameObjectsByTag("enemy")) {
            if (enemy.collider.doesIntersect(collider)) {
                enemy.Delete();
                this.Delete();
                return;
            }
        }
    }
}