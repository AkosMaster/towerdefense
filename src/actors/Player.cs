using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Player : GameObject
{
    public Player() : base("player") {
        SetSprite(new Sprite(Game1.contentManager.Load<Texture2D>("archer")));
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

    }

    void UseHammer() {

    }

    void ReadInput()
    {
        KeyboardState keyState = Keyboard.GetState();

        Vector2 moveInput = Vector2.Zero;

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