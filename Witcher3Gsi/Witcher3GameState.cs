namespace Witcher3Gsi;

public class Witcher3GameState
{
    public Witcher3Player Player { get; }
    
    internal Witcher3GameState(Witcher3Player player)
    {
        Player = player;
    }
}

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

public enum WitcherSign
{
    None,
    Axii,
    Aard,
    Igni,
    Quen,
    Yrden
}