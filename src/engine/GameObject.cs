using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public abstract class GameObject : IUpdateable
{

    public Sprite sprite;
    public Transform transform = new Transform();
    public Collider collider = new Collider();
    string tag;
    public GameObject(string _tag){
        tag = _tag;
        UpdateManager.actorManager.registerActor(this);
        collider.transform.parent = transform;

        if(!gameObjectsByTag.ContainsKey(tag)) {
            gameObjectsByTag.Add(tag, new List<GameObject>());
        }
        gameObjectsByTag[tag].Add(this);
    }

    public void Delete() {
        UpdateManager.actorManager.unregisterActor(this);
        sprite.Delete();
        gameObjectsByTag[tag].Remove(this);
    }

    public void SetSprite(Sprite _sprite) {
        sprite = _sprite;
        sprite.transform.parent = transform;
    }

    public abstract void Update(GameTime gameTime);

    private static Dictionary<string, List<GameObject>> gameObjectsByTag = new Dictionary<string, List<GameObject>>();
    public static List<GameObject> getGameObjectsByTag(string tag) {
        if (!gameObjectsByTag.ContainsKey(tag)) {
            return new List<GameObject>();
        }
        return gameObjectsByTag[tag];
    }
}