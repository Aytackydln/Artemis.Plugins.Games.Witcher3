using System.Collections.Generic;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.Witcher3.DataModels;
using Witcher3Gsi;

namespace Artemis.Plugins.Games.Witcher3;

public class Witcher3Module : Module<Witcher3DataModel>
{
    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

    private readonly Witcher3GameStateListener _witcher3Listener = new();

    private void Witcher3ListenerOnGameStateChanged(object? sender, Witcher3StateEventArgs e)
    {
        DataModel.Player = e.GameState.Player;
    }

    public override void ModuleActivated(bool isOverride)
    {
        _witcher3Listener.GameStateChanged += Witcher3ListenerOnGameStateChanged;
        _witcher3Listener.StartReading();
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        _witcher3Listener.StopListening();
        _witcher3Listener.GameStateChanged -= Witcher3ListenerOnGameStateChanged;
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }

    public override void Update(double deltaTime)
    {
    }
}