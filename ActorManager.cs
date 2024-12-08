using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace towerdefense;

public class ActorManager {

    private List<Actor> gameActors = new List<Actor>();
    public void registerActor(Actor actor) {
        gameActors.Add(actor);
    }

    public void Update(GameTime gameTime) {
        foreach(Actor actor in gameActors) {
            actor.Update(gameTime);
        }
    }

    public static readonly ActorManager actorManager = new ActorManager();
}