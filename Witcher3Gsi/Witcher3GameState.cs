namespace Witcher3Gsi;

public class Witcher3GameState
{
    public Witcher3Player Player { get; internal set; }
}

public class Witcher3Player
{
    public int MaximumHealth { get; internal set; }
    public int CurrentHealth { get; internal set; }
    public float Stamina { get; internal set; }
    public float Toxicity { get; internal set; }
    public WitcherSign ActiveSign { get; internal set; } = WitcherSign.None;
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