using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public abstract class CShape {

}
public class CCircle : CShape {
    public Vector2 center;
    public float radius;

    public CCircle(Vector2 _center, float _radius) {
        center = _center;
        radius = _radius;
    }
}
public class CRectangle : CShape {
    public Vector2 center;
    public Vector2 scale;

    public CRectangle(Vector2 _center, Vector2 _scale) {
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

    bool intersects_Circle_Circle(CCircle c1, CCircle c2, Transform other_transform) {
        Vector2 c1_center = c1.center + transform.getPosition();
        Vector2 c2_center = c2.center + other_transform.getPosition();

        float distance = (c1_center - c2_center).Length();
        return distance <= c1.radius + c2.radius;
    }

    bool intersects_Rect_Rect(CRectangle r1, CRectangle r2, Transform other_transform) {
        Vector2 r1_center = r1.center + transform.getPosition();
        Vector2 r2_center = r2.center + other_transform.getPosition();

        return r1_center.X + r1.scale.X / 2 > r2_center.X - r2.scale.X / 2 &&
        r1_center.X - r1.scale.X / 2 < r2_center.X + r2.scale.X / 2 &&
        r1_center.Y + r1.scale.Y / 2 > r2_center.Y - r2.scale.Y / 2 &&
        r1_center.Y - r1.scale.Y / 2 < r2_center.Y + r2.scale.Y / 2;
    }
    public bool doesIntersect(Collider other) {
        foreach (CShape shape in shapes) {
            foreach(CShape other_shape in other.shapes) {

                if (shape.GetType() == typeof(CCircle)) {
                    if (other_shape.GetType() == typeof(CCircle)) {
                        if (intersects_Circle_Circle((CCircle)shape, (CCircle)other_shape, other.transform)) {
                            //Debug.Print(((CCircle)shape).center.X);
                            return true;
                        }
                    } 
                    else {
                        // NO

                    }
                } 

                else {
                    if (other_shape.GetType() == typeof(CCircle)) {
                        // NO
                    } else {
                        if (intersects_Rect_Rect((CRectangle)shape, (CRectangle)other_shape, other.transform)) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}