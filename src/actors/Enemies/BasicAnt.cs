using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace towerdefense;

public class BasicAnt : Enemy
{

    static Texture2D texture = Game1.contentManager.Load<Texture2D>("basic_ant");
    public BasicAnt(Lane _lane) : base(_lane, texture){
        
    }

}