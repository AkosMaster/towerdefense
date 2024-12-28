using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Collider : IDrawable
{
    public Transform transform = new Transform();
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

    }
    public bool isPointInsideCircle(Vector2 point) {
        return Math.Abs((point - transform.getPosition()).Length()) < transform.getScale().X;
    }

    public bool isPointInsideRectangle(Vector2 point) {
        float upperX = transform.getPosition().X + transform.getScale().X/2;
        float lowerX = transform.getPosition().X - transform.getScale().X/2;

        float upperY = transform.getPosition().Y + transform.getScale().Y/2;
        float lowerY = transform.getPosition().Y - transform.getScale().Y/2;

        return point.X <= upperX && point.X >= lowerX && point.Y <= upperY && point.Y >= lowerY;
    }
}