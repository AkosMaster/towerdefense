using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Lane : IUpdateable
{
    public Transform transform = new Transform();
    private List<Vector2> points;

    public Lane(List<Vector2> _points, float delay = 0) {
        points = _points;
        elapsed = -delay;
    }

    public int getPointCount() {
        return points.Count;
    }
    public Vector2 getPoint(int index) {
        return points[index] + transform.GetPosition();
    }

    float elapsed = 0;
    float spawnInterval = 2000;

    public void Update(GameTime gameTime)
    {
        AttemptSpawn(gameTime);
    }

    private void AttemptSpawn(GameTime gameTime)
    {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;

        //spawn enemy
        if (elapsed > spawnInterval && getPointCount() > 0)
        {
            elapsed -= spawnInterval;
            Enemy enemy;
            if (Game1.random.Next(1,100) < 5) {
                enemy = new GiAnt(this);
            } else {
                enemy = new BasicAnt(this);
            }
            enemy.transform.localPosition = getPoint(0);
        }
    }
}