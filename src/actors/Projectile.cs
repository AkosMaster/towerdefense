using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Projectile : GameObject
{
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("bullet1");
    bool targetMode = false;
    public float speed = 0.1f;
    GameObject target;
    Vector2 direction;

    public Projectile(GameObject _target) : base("projectile"){
        target = _target;
        SetSprite(new Sprite(texture));
        targetMode = true;
        collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    }

    public Projectile(Vector2 _direction) : base("projectile"){
        direction = _direction;
        direction.Normalize();
        SetSprite(new Sprite(texture));
        
        collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    } 

    public override void Update(GameTime gameTime)
    {
        if (targetMode)
        {
            Vector2 directionVector = target.transform.GetPosition() - transform.GetPosition();
            directionVector.Normalize();
            transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
        }
        else
        {
            transform.localPosition += direction * speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        CheckForHit();
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds() {
        if (transform.GetPosition().X < 0 || transform.GetPosition().X > Game1.virtualWidth) {
            this.Delete();
        }
        if (transform.GetPosition().Y < 0 || transform.GetPosition().Y > Game1.virtualHeight) {
            this.Delete();
        }
    }

    private void CheckForHit()
    {
        foreach (Enemy enemy in GameObject.getGameObjectsByTag("enemy"))
        {
            if (enemy.collider.CheckIntersection(collider))
            {
                enemy.Damage(40);
                this.Delete();
                return;
            }
        }
    }
}