using Godot;
using System.Collections.Generic;

public class State : Node
{
    /// <summary>
    /// # Reference à la `StateMachine` pour appeler sa méthode `transition_to()` directement.
    /// C'est notre triche pour l'implémentation du DP État, car cela ajoute une dépendance entre
    /// l'état et l'objet `StateMachine`, mais une méthode efficace pour nos besoins
    /// La machine à état qui l'assignera.
    /// </summary>
    public StateMachine _stateMachine = null;

    /// <summary>
    /// Fonction virtuelle. Reçoit les événements de `_unhandled_input()`.
    /// </summary>
    /// <param name="inputEvent"></param>
    public virtual void HandleInputs(InputEvent inputEvent)
    {
        return;
    }

    /// <summary>
    /// Fonction virtuel correspondant à `_process()`.
    /// </summary>
    /// <param name="delta"></param>
    public virtual void Update(float delta)
    {
        return;
    }

    /// <summary>
    /// Fonction virtuel correspondant à `_PhysicsProcess()`
    /// </summary>
    /// <param name="delta"></param>
    public virtual void PhysicsUpdate(float delta)
    {
        return;
    }

    /// <summary>
    /// Fonction virtuelle. Appelée par la machine à état pour modifier l'état courant. Le paramètre `msg`
    /// est un dictionnaire avec des données arbitraires que l'état peut utiliser pour initialiser.
    /// </summary>
    /// <param name="message"></param>
    public virtual void Enter(Dictionary<string, bool> message = null)
    {
        return;
    }

    /// <summary>
    /// Fonction virtuelle. Appelée par la machine à état avant de changer l'état courant. Utilisez cette
    /// fonction pour nettoyer les ressources utilisées par l'état.
    /// </summary>
    public virtual void Exit()
    {
        return;
    }
}
