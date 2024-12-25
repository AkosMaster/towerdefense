using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Lane
{
    private List<Vector2> points;

    public Lane(List<Vector2> _points) {
        points = _points;

    }

    public int getPointCount() {
        return points.Count;
    }
    public Vector2 getPoint(int index) {
        return points[index];
    }
}