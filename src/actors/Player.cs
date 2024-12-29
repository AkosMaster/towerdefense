using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Player : GameObject
{
    Collider hammerCollider = new Collider();
    public Player() : base("player") {
        SetSprite(new Sprite(Game1.contentManager.Load<Texture2D>("archer")));

        hammerCollider.shapes.Add(new CCircle(new Vector2(0,0), 15));
        hammerCollider.transform.parent = transform;
    }

    float speed = 0.15f;

    Vector2 moveInput;
    bool interactInput;

    public Vector2 facing;

    public override void Update(GameTime gameTime)
    {

        float elapsed = gameTime.ElapsedGameTime.Milliseconds;

        ReadInput();
        transform.localPosition += moveInput * speed * elapsed;

        if (interactInput)
            UseHammer();


        // if moving, update facing
        UpdateFacing();

        // put hammer collider in front of player.
        hammerCollider.transform.localPosition = facing * 10;
    }

    void UseHammer() {

        // whack enemies
        foreach (Enemy enemy in GameObject.getGameObjectsByTag("enemy")) {
            if (enemy.collider.CheckIntersection(hammerCollider)) {
                enemy.Damage(100);
            }
        }
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

        interactInput = keyState.IsKeyDown(Keys.E);

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