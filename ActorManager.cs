using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class ActorManager {

    private readonly List<IActor> gameActors = new List<IActor>();
    public void registerActor(IActor actor) {
        gameActors.Add(actor);
    }

    public void Update(GameTime gameTime) {
        foreach(IActor actor in gameActors) {
            actor.Update(gameTime);
        }
    }

    public static readonly ActorManager actorManager = new ActorManager();
}