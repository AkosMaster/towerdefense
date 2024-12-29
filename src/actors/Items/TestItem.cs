
using Microsoft.Xna.Framework.Graphics;

namespace towerdefense;

public class TestItem: Item
{
    static Texture2D texture = Game1.contentManager.Load<Texture2D>("placeholder");

    public TestItem() : base(texture)
    {
        SetSprite(new Sprite(texture));
        transform.localScale = defaultScale;
    }
}