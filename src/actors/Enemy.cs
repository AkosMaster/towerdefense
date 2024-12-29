using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class Enemy : GameObject
{

    static Texture2D texture = Game1.contentManager.Load<Texture2D>("basic_ant");
    private Lane lane;
    public int health = 100;
    public Enemy(Lane _lane) : base("enemy"){
        lane = _lane;
        SetSprite(new Sprite(texture));

        collider.shapes.Add(new CRectangle(new Vector2(0,0), new Vector2(150/4,250/4)));
        transform.localScale = new Vector2(0.3f, 0.3f);
        //collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    }

    private int currentPointIndex = -1;
    private Vector2 currentPoint;
    private bool reachedGoal = true;

    private float timeSinceDamaged = 99999f;
    private float timeSinceDeath = 0;

    private float speed = 0.05f;

    public override void Update(GameTime gameTime)
    {
        // for a multitude of reasons, it is better to die in our own Update loop (for example animation)
        if (!CheckAlive(gameTime)) {
            return;
        }

        if (!DamageAnimation(gameTime)) {
            FollowPath(gameTime);
            WalkAnimation(gameTime);
        }

        timeSinceDamaged += gameTime.ElapsedGameTime.Milliseconds;
    }

    private bool CheckAlive(GameTime gameTime) {
        if (health <= 0) {
            if (timeSinceDeath > 500) {
                this.Delete();
            } else {
                timeSinceDeath += gameTime.ElapsedGameTime.Milliseconds;
                sprite.color = Color.MediumVioletRed;
                sprite.transform.localScale = new Vector2(1f, (500-timeSinceDeath)/500 );
            }
            return false;
        } else {
            return true;
        }
    }

    private bool DamageAnimation(GameTime gameTime) {
        if (timeSinceDamaged < 200) {
            sprite.color = Color.Red;
            sprite.transform.localScale = new Vector2(timeSinceDamaged/200, 1f);
            return true;
        } else {
            sprite.color = Color.White;
            return false;
        }
    }

    private void FollowPath(GameTime gameTime)
    {
        //check if reached end of line segment
        if ((currentPoint - transform.GetPosition()).LengthSquared() <= 10)
        {
            reachedGoal = true;
        }

        //switch to next path segment if reached end
        if (reachedGoal)
        {
            currentPointIndex++;
            if (currentPointIndex < lane.getPointCount())
            {
                currentPoint = lane.getPoint(currentPointIndex);
                reachedGoal = false;
            }
        }

        //face and move toward to end of next path segment
        Vector2 directionVector = currentPoint - transform.GetPosition();
        directionVector.Normalize();
        transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
    }

    private void WalkAnimation(GameTime gameTime)
    {
        sprite.transform.localScale.X = 1 + (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 700f + this.GetHashCode()) / 50;
    }

    public void Damage(int value) {
        timeSinceDamaged = 0;
        health -= value;
    }
}