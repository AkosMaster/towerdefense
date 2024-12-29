using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class GiAnt : Enemy
{

    static Texture2D texture = Game1.contentManager.Load<Texture2D>("basic_ant");
    public GiAnt(Lane _lane) : base(_lane, texture){
        transform.localScale = new Vector2(0.6f,0.6f);
        health = 300;
    }

}