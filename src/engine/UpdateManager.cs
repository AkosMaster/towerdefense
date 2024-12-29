using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace towerdefense;

public class UpdateManager {

    private readonly List<IUpdateable> gameActors = new List<IUpdateable>();
    private readonly List<IUpdateable> newGameActors = new List<IUpdateable>();
    private readonly List<IUpdateable> oldGameActors = new List<IUpdateable>();
    public void registerActor(IUpdateable actor) {
        newGameActors.Add(actor);
    }
    public void unregisterActor(IUpdateable actor) {
        oldGameActors.Add(actor);
    }

    public List<IUpdateable> getActors() {
        return gameActors;
    }
    public void Update(GameTime gameTime) {
        foreach(IUpdateable actor in oldGameActors) {
            gameActors.Remove(actor);
        }
        oldGameActors.Clear();
        foreach(IUpdateable actor in newGameActors) {
            gameActors.Add(actor);
        }
        newGameActors.Clear();
        
        foreach(IUpdateable actor in gameActors) {
            actor.Update(gameTime);
        }
    }

    public static readonly UpdateManager actorManager = new UpdateManager();
}