using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Enemy : GameObject
{

    private Lane lane;
    public Enemy(Lane _lane) : base("enemy"){
        lane = _lane;
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("ufo1")));

        collider.shapes.Add(new CRectangle(new Vector2(0,0), new Vector2(150/4,250/4)));
        //collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    }
    private int currentPointIndex = -1;
    private Vector2 currentPoint;
    private bool reachedGoal = true;

    private float speed = 0.05f;

    public override void Update(GameTime gameTime) {
        //check if reached end of line segment
        if ((currentPoint - transform.getPosition()).LengthSquared() <= 10) {
            reachedGoal = true;
        }

        //switch to next path segment if reached end
        if (reachedGoal) 
        {
            currentPointIndex++;
            if (currentPointIndex < lane.getPointCount()) {
                currentPoint = lane.getPoint(currentPointIndex);
                reachedGoal = false;
            }
        }

        //face and move toward to end of next path segment
        Vector2 directionVector = currentPoint - transform.getPosition();
        directionVector.Normalize();
        transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
    }
}