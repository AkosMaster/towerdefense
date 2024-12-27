using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Collider
{
    public Transform transform = new Transform();
    
    public bool isNear(Transform other) {
        return Math.Abs((other.getPosition() - transform.getPosition()).Length()) < transform.getScale().X;
    }
}