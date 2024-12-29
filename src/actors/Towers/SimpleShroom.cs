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
}