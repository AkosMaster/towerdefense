using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Enemy : IActor
{

    private readonly Sprite sprite;
    public Transform transform = new Transform();
    public Sprite getSprite() {
        return sprite;
    }

    private Lane lane;
    public Enemy(Texture2D texture, Lane _lane){
        lane = _lane;
        sprite = new Sprite(texture);
        sprite.transform.parent = transform;
        transform.localScale = new Vector2(10,10);
    }
    
    private int currentPointIndex = -1;
    private Vector2 currentPoint;
    private bool reachedGoal = true;

    private float speed = 0.05f;
    public void Update(GameTime gameTime) {
        if (reachedGoal) {
            currentPointIndex++;
            if (currentPointIndex < lane.getPointCount()) {
                currentPoint = lane.getPoint(currentPointIndex);
                reachedGoal = false;
            }
        }

        if ((currentPoint - transform.getPosition()).LengthSquared() <= 10) {
            reachedGoal = true;
        }
        Vector2 directionVector = currentPoint - transform.getPosition();
        directionVector.Normalize();
        transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
    }
}