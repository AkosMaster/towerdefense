using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Tower : GameObject
{
    public const float pickupRange = 30;
    public bool isBluePrint = true;
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("basic_mushroom");
    public Tower() : base("tower") 
    {
        SetSprite(new Sprite(texture));
        transform.localScale = new Vector2(0.1f,0.1f);
    }
    
    float elapsed = 0;
    float shotInterval = 100;

    public override void Update(GameTime gameTime)
    {
        //blueprint behaviour
        if (isBluePrint)
        {
            BluePrintBehaviour();
        }
        //normal behaviour
        else
        {
            TowerBehaviour(gameTime);
        }

        IdleAnimation(gameTime);
    }

    void IdleAnimation(GameTime gameTime)
    {
        sprite.transform.localScale.Y = 1f + (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds / 700f) / 50;
    }

    void TowerBehaviour(GameTime gameTime)
    {
        elapsed += gameTime.ElapsedGameTime.Milliseconds;
        sprite.color = Color.White;
        Shoot(gameTime);
    }

    void BluePrintBehaviour()
    {
        sprite.color = Color.Aqua * 0.5f;
        CollectItems();
    }

    void CollectItems() 
    {
        List<Item> itemsToDelete = new List<Item>();
        foreach(Item item in GameObject.GetGameObjectsByTag("item")) 
        {
            // later here we would check item type

            float distance = (item.transform.GetPosition() - transform.GetPosition()).Length();

            if (distance < pickupRange && item.carryingPlayer == null) 
            {
                itemsToDelete.Add(item);

                // this can be done after multiple correct-type items are fed of course
                isBluePrint = false;
            }
        }

        // we cant do this in the loop above since we cant iterate a list whilst modifying it
        foreach(Item item in itemsToDelete) 
        {
            item.Delete();
        }
    }

    void Shoot(GameTime gameTime) {
        if (elapsed > shotInterval) 
        {
            elapsed -= shotInterval;
            double rotation = gameTime.TotalGameTime.TotalMilliseconds/1000f;
            Projectile proj = new Projectile(new Vector2((float)Math.Sin(rotation),(float)Math.Cos(rotation)));
            proj.transform.localPosition = transform.localPosition;
        }
    }
}