using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player player;

    private List<Sprite> gameSprites = new List<Sprite>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        Texture2D p1tex = Content.Load<Texture2D>("player1");
        player = new Player(p1tex);
        gameSprites.Add(player);

        player.setSpriteScale(new Vector2(10,10));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // pointClamp makes upscaled sprites look sharp
        foreach (Sprite sprite in gameSprites) {
            sprite.Draw(gameTime, _spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
