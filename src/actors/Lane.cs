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

    public Lane(List<Vector2> _points) {
        points = _points;

    }

    public int getPointCount() {
        return points.Count;
    }
    public Vector2 getPoint(int index) {
        return points[index] + transform.GetPosition();
    }

    float elapsed = 0;
    float spawnInterval = 500;

    public void Update(GameTime gameTime)
    {
        AttemptSpawn(gameTime);
    }

    private void AttemptSpawn(GameTime gameTime)
    {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;

        //spawn enemy
        if (elapsed > spawnInterval)
        {
            elapsed -= spawnInterval;

            if (Game1.random.Next(1,100) < 5) {
                GiAnt enemy = new GiAnt(this);
            } else {
                BasicAnt enemy = new BasicAnt(this);
            }
        }
    }
}