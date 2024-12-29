using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class SimpleShroom : Tower
{
    public SimpleShroom() : base(Game1.contentManager.Load<Texture2D>("basic_mushroom")) {

    }

    protected override void Shoot(GameTime gameTime) {
        if (elapsed > shotInterval) 
        {
            elapsed -= shotInterval;
            
            Enemy target = GetNearestEnemyInRange();

            if (target != null) {
                Projectile proj = new Projectile(target);
                proj.transform.localPosition = transform.localPosition;
            }
        }
    }
}