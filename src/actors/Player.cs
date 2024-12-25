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
    public Player(Texture2D texture){
        sprite = new Sprite(texture);
        sprite.transform.parent = transform;
    }

    public void Update(GameTime gameTime) {
        KeyboardState keyState = Keyboard.GetState();
        Vector2 move = Vector2.Zero;
        float speed = 1;
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