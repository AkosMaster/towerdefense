using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class Game1 : Game
{
    GraphicsDeviceManager _graphics;
    Player player;

    public static ContentManager contentManager;
    public static Random random = new Random();
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

    SpriteBatch targetBatch;
    RenderTarget2D renderTarget;

    public static int virtualWidth = 1920; // relative width of game
    public static int virtualHeight = 1080; // relative height of game

    protected override void LoadContent()
    {
        contentManager = Content;
        targetBatch = new SpriteBatch(GraphicsDevice);
        renderTarget = new RenderTarget2D(GraphicsDevice, virtualWidth, virtualHeight);
        // TODO: use this.Content to load your game content here

        //Sprite bg = new Sprite(contentManager.Load<Texture2D>("bg1"));
        //bg.transform.localScale = new Vector2(2,2);

        InitializeMap();
    }

    protected void InitializeMap()
    {
        Sprite pathSprite = new Sprite(contentManager.Load<Texture2D>("path1"));
        pathSprite.transform.localPosition = new Vector2(900,500);
        pathSprite.layer = 1;
        pathSprite.color = Color.White * 0.8f;

        player = new Player();

        Lane lane1 = new Lane(new List<Vector2> { new Vector2(72, 333), new Vector2(657, 465), new Vector2(895, 218),
            new Vector2(1396, 321), new Vector2(1557, 573), new Vector2(1711, 811) });

        Lane lane2 = new Lane(new List<Vector2> { new Vector2(72, 333), new Vector2(657, 465), new Vector2(763, 676),
            new Vector2(1264, 753), new Vector2(1557, 573), new Vector2(1711, 811) }, 1000);
        
        UpdateManager.actorManager.registerActor(lane1);
        UpdateManager.actorManager.registerActor(lane2);

        Tower t1 = new SimpleShroom();
        t1.transform.localPosition = new Vector2(500, 200);

        Tower t2 = new SimpleShroom();
        t2.transform.localPosition = new Vector2(200, 500);

        Tower t3 = new SimpleShroom();
        t3.transform.localPosition = new Vector2(1000, 600);

        ItemSpawner i1 = new ItemSpawner();
        i1.transform.localPosition = new Vector2(300,600);

        ItemSpawner i2 = new ItemSpawner();
        i2.transform.localPosition = new Vector2(600,200);
    }

    Vector2 GetVirtualMousePosition() { // get mouse position using game units
        if (gameRect.Width == 0 || gameRect.Height == 0) {
            return Vector2.Zero;
        }
        MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        Vector2 vMouse = new Vector2(mouseState.X, mouseState.Y) - new Vector2(gameRect.X, gameRect.Y);
        vMouse *= new Vector2((float)virtualWidth/(float)gameRect.Width,(float)virtualHeight/(float)gameRect.Height );
        return vMouse;
    }
    protected override void Update(GameTime gameTime)
    {
        CheckExitInput();

        UpdateManager.actorManager.Update(gameTime);

        base.Update(gameTime);

        Vector2 vMouse = GetVirtualMousePosition();
        Debug.Print("MouseX: " + vMouse.X.ToString() + " MouseY: " + vMouse.Y.ToString()
            + " [actor count]: " + UpdateManager.actorManager.getActors().Count.ToString()
            + " [sprite count]: " + SpriteManager.spriteManager.getSprites().Count.ToString());
    }

    private void CheckExitInput()
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
    }
    Rectangle gameRect; // game rectangle inside window. Has fixed aspect ratio

    protected override void Draw(GameTime gameTime)
    {   
        GraphicsDevice.SetRenderTarget(renderTarget);

        // normal draw calls
        GraphicsDevice.Clear(Color.DarkGreen);

        // draw game sprites
        SpriteManager.spriteManager.Draw(gameTime, targetBatch);
        
        // set rendering back to the back buffer
        GraphicsDevice.SetRenderTarget(null);

        // render target to back buffer
        targetBatch.Begin();

        // calculate the size of the actual game rectangle so that aspect ratio is kept (simple coordinate math)
        float virtualAspectRatio = (float)virtualHeight / (float)virtualWidth;
        if (Window.ClientBounds.Width * virtualAspectRatio <= Window.ClientBounds.Height)
         {
            int maxHeight = (int)(Window.ClientBounds.Width * virtualAspectRatio);
            gameRect = new Rectangle(0, (Window.ClientBounds.Height - maxHeight)/2, Window.ClientBounds.Width, maxHeight);
        } 
        else 
        {
            int maxWidth = (int)(Window.ClientBounds.Height / virtualAspectRatio);
            gameRect = new Rectangle((Window.ClientBounds.Width - maxWidth)/2, 0, maxWidth, Window.ClientBounds.Height);
        }

        // draw game rectangle to window
        targetBatch.Draw(renderTarget, gameRect, Color.White);
        targetBatch.End();

        base.Draw(gameTime);
    }
}
