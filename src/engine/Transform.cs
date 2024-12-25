using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Transform
{
    public Vector2 localPosition = new Vector2(0,0);
    public Vector2 localScale = new Vector2(1,1);
    
    public Transform parent = null;

    public Vector2 getPosition() {
        if (parent == null) {
            return localPosition;
        } else {
            return localPosition + parent.getPosition();
        }
    }

    public Vector2 getScale() {
        if (parent == null) {
            return localScale;
        } else {
            return localScale * parent.getScale();
        }
    }
}