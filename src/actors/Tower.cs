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
    public Tower(Texture2D texture) : base("tower") 
    {
        SetSprite(new Sprite(texture));
        transform.localScale = new Vector2(0.1f,0.1f);
    }
    
    protected float elapsed = 0;
    protected float shotInterval = 500;
    protected float range = 300;

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

    protected Enemy GetNearestEnemyInRange() {
        Enemy closestEnemy = null;
        float smallestDistance = 999999999;
        foreach (Enemy enemy in GameObject.GetGameObjectsByTag("enemy")) {
            float distance = (enemy.transform.GetPosition() - transform.GetPosition()).Length();
            if (distance <= range && distance < smallestDistance) {
                closestEnemy = enemy;
                smallestDistance = distance;
            }
        }
        return closestEnemy;
    }
    protected virtual void Shoot(GameTime gameTime) {
        
    }
}