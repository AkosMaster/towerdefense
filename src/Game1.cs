using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private Player player;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }



    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    private SpriteBatch targetBatch;
    private RenderTarget2D renderTarget;

    int virtualWidth = 320; // relative width of game
    int virtualHeight = 240; // relative height of game
    protected override void LoadContent()
    {
        targetBatch = new SpriteBatch(GraphicsDevice);
        renderTarget = new RenderTarget2D(GraphicsDevice, virtualWidth, virtualHeight);
        // TODO: use this.Content to load your game content here

        Texture2D p1tex = Content.Load<Texture2D>("player1");
        player = new Player(p1tex);
        ActorManager.actorManager.registerActor(player);

        player.getSprite().setSpriteScale(new Vector2(10,10));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        ActorManager.actorManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {   
        GraphicsDevice.SetRenderTarget(renderTarget);

        // normal draw calls
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // draw game sprites
        SpriteManager.spriteManager.Draw(gameTime, targetBatch);
        
        // set rendering back to the back buffer
        GraphicsDevice.SetRenderTarget(null);

        // render target to back buffer
        targetBatch.Begin();

        // calculate the size of the actual game rectangle so that aspect ratio is kept (simple coordinate math)
        Rectangle gameRect;
        float virtualAspectRatio = (float)virtualHeight / (float)virtualWidth;
        if (Window.ClientBounds.Width * virtualAspectRatio <= Window.ClientBounds.Height) {
            int maxHeight = (int)(Window.ClientBounds.Width * virtualAspectRatio);
            gameRect = new Rectangle(0, (Window.ClientBounds.Height - maxHeight)/2, Window.ClientBounds.Width, maxHeight);
        } else {
            int maxWidth = (int)(Window.ClientBounds.Height / virtualAspectRatio);
            gameRect = new Rectangle((Window.ClientBounds.Width - maxWidth)/2, 0, maxWidth, Window.ClientBounds.Height);
        }

        // draw game rectangle to window
        targetBatch.Draw(renderTarget, gameRect, Color.White);
        targetBatch.End();

        base.Draw(gameTime);
    }
}
