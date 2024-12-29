using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class Enemy : GameObject
{

    //static Texture2D texture = Game1.contentManager.Load<Texture2D>("placeholder");
    private Lane lane;
    public int health = 100;
    public bool alive = true;
    public Enemy(Lane _lane, Texture2D texture) : base("enemy"){
        lane = _lane;
        SetSprite(new Sprite(texture));

        collider.shapes.Add(new CRectangle(new Vector2(0,0), new Vector2(150/4,250/4)));
        transform.localScale = new Vector2(0.3f, 0.3f);
        //collider.shapes.Add(new CCircle(new Vector2(0,0), 1));
    }

    int currentPointIndex = -1;
    Vector2 currentPoint;
    bool reachedGoal = true;

    float timeSinceDamaged = 99999f;
    float timeSinceDeath = 0;

    float speed = 0.05f;

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

    protected virtual void OnDeath() {

    }

     bool CheckAlive(GameTime gameTime) {
        if (health <= 0) {
            alive = false;
            if (timeSinceDeath > 500) {
                OnDeath();
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

    bool DamageAnimation(GameTime gameTime) {
        if (timeSinceDamaged < 200) {
            sprite.color = Color.Red;
            sprite.transform.localScale = new Vector2(timeSinceDamaged/200, 1f);
            return true;
        } else {
            sprite.color = Color.White;
            return false;
        }
    }

    void FollowPath(GameTime gameTime)
    {

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

        //check if reached end of line segment
        if ((currentPoint - transform.GetPosition()).LengthSquared() <= 10)
        {
            reachedGoal = true;
            return; // it works this way. Dont question it
        }

        //face and move toward to end of next path segment
        Vector2 directionVector = currentPoint - transform.GetPosition();
        directionVector.Normalize();
        transform.localPosition += directionVector * speed * gameTime.ElapsedGameTime.Milliseconds;
    }

    void WalkAnimation(GameTime gameTime)
    {
        sprite.transform.localScale.X = 1 + (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 700f + this.GetHashCode()) / 50;
    }

    public void Damage(int value) {
        timeSinceDamaged = 0;
        health -= value;
    }
}