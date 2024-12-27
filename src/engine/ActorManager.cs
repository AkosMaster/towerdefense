using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class ActorManager {

    private readonly List<IActor> gameActors = new List<IActor>();
    private readonly List<IActor> newGameActors = new List<IActor>();
    private readonly List<IActor> oldGameActors = new List<IActor>();
    public void registerActor(IActor actor) {
        newGameActors.Add(actor);
    }
    public void unregisterActor(IActor actor) {
        oldGameActors.Add(actor);
    }

    public List<IActor> getActors() {
        return gameActors;
    }
    public void Update(GameTime gameTime) {
        foreach(IActor actor in oldGameActors) {
            gameActors.Remove(actor);
        }
        oldGameActors.Clear();
        foreach(IActor actor in newGameActors) {
            gameActors.Add(actor);
        }
        newGameActors.Clear();
        
        foreach(IActor actor in gameActors) {
            actor.Update(gameTime);
        }
    }

    public static readonly ActorManager actorManager = new ActorManager();
}