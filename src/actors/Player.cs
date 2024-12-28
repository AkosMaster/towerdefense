using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Player : GameObject
{
    public Player() : base("player") {
        setSprite(new Sprite(Game1.contentManager.Load<Texture2D>("archer")));
    }

    public Vector2 facing;

    void useHammer() {

    }


    public override void Update(GameTime gameTime) {
        KeyboardState keyState = Keyboard.GetState();
        Vector2 move = Vector2.Zero;
        float speed = 0.15f;
        float elapsed = gameTime.ElapsedGameTime.Milliseconds;

        if (keyState.IsKeyDown(Keys.W))
            move += new Vector2(0, -speed*elapsed);
        if (keyState.IsKeyDown(Keys.S))
            move += new Vector2(0, speed*elapsed);
        if (keyState.IsKeyDown(Keys.A))
            move += new Vector2(-speed*elapsed, 0);
        if (keyState.IsKeyDown(Keys.D))
            move += new Vector2(speed*elapsed, 0);
        
        if (keyState.IsKeyDown(Keys.E))
            useHammer();
        
        // if moving, update facing
        if (move.LengthSquared() > float.Epsilon) {
            facing = move;
            move.Normalize();
        }

        transform.localPosition += move;
    }
}