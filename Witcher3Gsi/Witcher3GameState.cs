using JetBrains.Annotations;

namespace Witcher3Gsi;

[PublicAPI]
public class Witcher3GameState
{
    public Witcher3Player Player { get; }
    
    public Witcher3World World { get; }
    
    internal Witcher3GameState(Witcher3Player player, Witcher3World world)
    {
        Player = player;
        World = world;
    }
}

[PublicAPI]
public class Witcher3Player
{
    public Witcher3Player(){}

    public Witcher3Player(float toxicity, float stamina, int maximumHealth, int currentHealth, WitcherSign activeSign)
    {
        Toxicity = toxicity;
        Stamina = stamina;
        MaximumHealth = maximumHealth;
        CurrentHealth = currentHealth;
        ActiveSign = activeSign;
    }

    public int MaximumHealth { get; }
    public int CurrentHealth { get; }
    public float Stamina { get; }
    public float Toxicity { get; }
    public WitcherSign ActiveSign { get; } = WitcherSign.None;
}

[UsedImplicitly]
[PublicAPI]
public enum WitcherSign
{
    None,
    Axii,
    Aard,
    Igni,
    Quen,
    Yrden
}

[PublicAPI]
public class Witcher3World
{
    public bool IsNight { get; }
    public long DayNightTimeLeftHours { get; }
    public long DayNightTimeLeftMinutes { get; }
    public long DayNightTimeLeftSeconds { get; }

    public Witcher3World() { }

    public Witcher3World(bool isNight, long dayNightTimeLeftHours, long dayNightTimeLeftMinutes, long dayNightTimeLeftSeconds)
    {
        IsNight = isNight;
        DayNightTimeLeftHours = dayNightTimeLeftHours;
        DayNightTimeLeftMinutes = dayNightTimeLeftMinutes;
        DayNightTimeLeftSeconds = dayNightTimeLeftSeconds;
    }
}
