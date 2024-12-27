using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Collider
{
    public Transform transform = new Transform();
    
    public bool isPointInside(Vector2 point) {
        return Math.Abs((point - transform.getPosition()).Length()) < transform.getScale().X;
    }
}