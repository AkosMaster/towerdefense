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
        transform.localScale = new Vector2(0.45f,0.45f);
        health = 300;
    }

    protected override void OnDeath() {
        TestItem droppedItem = new TestItem();
        droppedItem.transform.localPosition = transform.GetPosition();
    }
}