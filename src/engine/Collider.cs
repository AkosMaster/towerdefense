using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public abstract class CShape 
{
}

public class CCircle : CShape 
{
    public Vector2 center;
    public float radius;

    public CCircle(Vector2 _center, float _radius)
    {
        center = _center;
        radius = _radius;
    }
}

public class CRectangle : CShape 
{
    public Vector2 center;
    public Vector2 scale;

    public CRectangle(Vector2 _center, Vector2 _scale)
    {
        center = _center;
        scale = _scale;
    }
}

public class Collider : IDrawable
{
    public Transform transform = new Transform();
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

    }
    /*public bool isPointInsideCircle(Vector2 point) {
        return Math.Abs((point - transform.getPosition()).Length()) < transform.getScale().X;
    }

    public bool isPointInsideRectangle(Vector2 point) {
        float upperX = transform.getPosition().X + transform.getScale().X/2;
        float lowerX = transform.getPosition().X - transform.getScale().X/2;

        float upperY = transform.getPosition().Y + transform.getScale().Y/2;
        float lowerY = transform.getPosition().Y - transform.getScale().Y/2;

        return point.X <= upperX && point.X >= lowerX && point.Y <= upperY && point.Y >= lowerY;
    }*/
    public List<CShape> shapes = new List<CShape>();

    public Collider() {
        
    }

    static bool CheckCircleCircle(CCircle c1, Transform t1, CCircle c2, Transform t2)
    {
        Vector2 c1_center = c1.center + t1.GetPosition();
        Vector2 c2_center = c2.center + t2.GetPosition();

        float distance = (c1_center - c2_center).Length();
        return distance <= c1.radius + c2.radius;
    }

    static bool CheckRectRect(CRectangle r1, Transform t1, CRectangle r2, Transform t2) 
    {
        Vector2 r1_center = r1.center + t1.GetPosition();
        Vector2 r2_center = r2.center + t2.GetPosition();

        return r1_center.X + r1.scale.X / 2 > r2_center.X - r2.scale.X / 2 &&
        r1_center.X - r1.scale.X / 2 < r2_center.X + r2.scale.X / 2 &&
        r1_center.Y + r1.scale.Y / 2 > r2_center.Y - r2.scale.Y / 2 &&
        r1_center.Y - r1.scale.Y / 2 < r2_center.Y + r2.scale.Y / 2;
    }

    static bool CheckCircleRect(CCircle circle, Transform t1, CRectangle rect, Transform t2) 
    {

        Vector2 circle_center = circle.center + t1.GetPosition();
        Vector2 rect_center = rect.center + t2.GetPosition();

        float circleDistance_x = Math.Abs(circle_center.X - rect_center.X);
        float circleDistance_y = Math.Abs(circle_center.Y - rect_center.Y);

        if (circleDistance_x > (rect.scale.X/2 + circle.radius)) { return false; }
        if (circleDistance_y > (rect.scale.Y/2 + circle.radius)) { return false; }

        if (circleDistance_x <= (rect.scale.X/2)) { return true; } 
        if (circleDistance_y <= (rect.scale.Y/2)) { return true; }

        double cornerDistance_sq = Math.Pow((double)circleDistance_x - rect.scale.X/2, (double)2) +
                         Math.Pow((double)circleDistance_y - rect.scale.Y/2, (double)2);

        return (cornerDistance_sq <= (circle.radius*circle.radius));
    }

    //checks collision between any types of colliders
    static bool CheckColliders(CShape c1, Transform t1, CShape c2, Transform t2)
    {
        if (c1.GetType() == typeof(CCircle) && c2.GetType() == typeof(CCircle))
            return CheckCircleCircle((CCircle)c1, t1, (CCircle)c2, t2);
            
        if (c1.GetType() == typeof(CCircle) && c2.GetType() == typeof(CRectangle))
            return CheckCircleRect((CCircle)c1, t1, (CRectangle)c2, t2);  
        
        if (c1.GetType() == typeof(CRectangle) && c2.GetType() == typeof(CCircle))
            return CheckCircleRect((CCircle)c2, t2, (CRectangle)c1, t1);
        
        if (c1.GetType() == typeof(CRectangle) && c2.GetType() == typeof(CRectangle))
            return CheckRectRect((CRectangle)c1, t1, (CRectangle)c2, t2);
        
        return false;
    }

    public bool CheckIntersection(Collider other) 
    {
        foreach (CShape shape in shapes) 
        {
            foreach(CShape otherShape in other.shapes)
            {
                return CheckColliders(shape, transform, otherShape, other.transform);
            }
        }
        return false;
    }
}