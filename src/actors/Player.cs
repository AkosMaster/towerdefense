using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Player : GameObject
{
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("archer");
    Collider hammerCollider = new Collider();

    public Item carriedItem = null;
    public Player() : base("player") {
        SetSprite(new Sprite(texture));

        hammerCollider.shapes.Add(new CCircle(new Vector2(0,0), 15));
        hammerCollider.transform.parent = transform;
    }

    float speed = 0.15f;

    Vector2 moveInput;
    bool interactInput;
    bool hammerInput;

    public Vector2 facing;

    public override void Update(GameTime gameTime)
    {

        float elapsed = gameTime.ElapsedGameTime.Milliseconds;

        ReadInput();
        transform.localPosition += moveInput * speed * elapsed;

        // if moving, update facing
        UpdateFacing();

        // put hammer collider in front of player.
        hammerCollider.transform.localPosition = facing * 30;

        if (hammerInput) {
            if (carriedItem == null) {
                UseHammer();
            }
        } else if (interactInput) {
            if (carriedItem == null) {
                PickupItem();
            } else {
                // drop item
                carriedItem.Drop(transform.GetPosition() + facing * 30);
                carriedItem = null;
                
            }
        }
    }

    void PickupItem() { // pickup nearest item
        Item nearestItem = null;
        float nearestDistance = 99999f;
        foreach (Item item in GameObject.GetGameObjectsByTag("item")) {
            float distance = (item.transform.GetPosition() - transform.GetPosition()).Length();
            if (distance < 30 && distance < nearestDistance) {
                nearestDistance = distance;
                nearestItem = item;
            }
        }

        if (nearestItem != null) {
            carriedItem = nearestItem;
            nearestItem.SetCarrier(this);
        }
    }

    void UseHammer() {

        // whack enemies
        foreach (Enemy enemy in GameObject.GetGameObjectsByTag("enemy")) {
            if (enemy.collider.CheckIntersection(hammerCollider)) {
                enemy.Damage(100);
            }
        }
    }

    KeyboardState lastKeyState = Keyboard.GetState();
    bool IsKeyPressed(KeyboardState state, Keys key) { // if key was only just pressed
        return state.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
    }
    void ReadInput()
    {
        KeyboardState keyState = Keyboard.GetState();

        moveInput = Vector2.Zero;

        if (keyState.IsKeyDown(Keys.W))
            moveInput += new Vector2(0, -1);
        if (keyState.IsKeyDown(Keys.S))
            moveInput += new Vector2(0, 1);
        if (keyState.IsKeyDown(Keys.A))
            moveInput += new Vector2(-1, 0);
        if (keyState.IsKeyDown(Keys.D))
            moveInput += new Vector2(1, 0);

        hammerInput = IsKeyPressed(keyState, Keys.Space);
        interactInput = IsKeyPressed(keyState, Keys.E);

        lastKeyState = keyState;

    }

    private void UpdateFacing()
    {
        if (moveInput.LengthSquared() > float.Epsilon)
        {
            facing = moveInput;
            moveInput.Normalize();
        }
    }
}