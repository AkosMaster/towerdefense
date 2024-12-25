using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Player : IActor
{

    private readonly Sprite sprite;
    public Transform transform = new Transform();
    public Sprite getSprite() {
        return sprite;
    }
    public Player(){
        sprite = new Sprite(Game1.contentManager.Load<Texture2D>("player1"));
        sprite.transform.parent = transform;
        transform.localScale = new Vector2(10,10);
    }

    public void Update(GameTime gameTime) {
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

        transform.localPosition += move;
    }
}