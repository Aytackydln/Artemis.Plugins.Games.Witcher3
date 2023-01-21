namespace Witcher3Gsi;

public sealed class Witcher3GameStateListener : IDisposable
{
    public event EventHandler<Witcher3StateEventArgs> GameStateChanged;

    private const string ArtemisString = "[Artemis]";

    private static readonly string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\The Witcher 3";
    private static readonly string ConfigFile = Path.Combine(ConfigFolder, "user.settings");
    private static readonly string Dx12ConfigFile = Path.Combine(ConfigFolder, "dx12user.settings");

    private FileSystemWatcher? _watcher;
    private FileSystemWatcher? _dx12Watcher;

    public void StartReading()
    {
        if (Directory.Exists(ConfigFolder))
        {
            _watcher = new FileSystemWatcher
            {
                Path = ConfigFolder,
                Filter = "user.settings",
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            _watcher.Changed += (_, _) => ReadGameState(ConfigFile);
        }
        if (Directory.Exists(ConfigFolder))
        {
            _dx12Watcher = new FileSystemWatcher
            {
                Path = ConfigFolder,
                Filter = "dx12user.settings",
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            _dx12Watcher.Changed += (_, _) => ReadGameState(Dx12ConfigFile);
        }

        ReadGameState(ConfigFile);
        ReadGameState(Dx12ConfigFile);
    }

    public void StopListening()
    {
        _watcher?.Dispose();
        _dx12Watcher?.Dispose();
    }

    private void ReadGameState(string configFile)
    {
        if (!File.Exists(configFile))
            return;
        
        var content = ReadFile(configFile);
        var player = ParseContent(content);
            
        GameStateChanged.Invoke(this, new Witcher3StateEventArgs(player));
    }

    private static string ReadFile(string configFile)
    {
        using var reader = new StreamReader(File.Open(configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        return reader.ReadToEnd();
    }

    private Witcher3GameState ParseContent(string content)
    {

        var artemisSection = IniReader.GetSection(content, ArtemisString);
        var player = new Witcher3Player
        {
            Toxicity = IniReader.GetInt(artemisSection, "Toxicity"),
            Stamina = IniReader.GetInt(artemisSection, "Stamina"),
            MaximumHealth = IniReader.GetInt(artemisSection, "MaxHealth"),
            CurrentHealth = IniReader.GetInt(artemisSection, "CurrHealth")
        };

        var enumText = artemisSection.FirstOrDefault(d => d.Contains("ActiveSign"))?.Split('_').Last() ?? "";
        player.ActiveSign = TryParseOr(enumText, true, WitcherSign.None);
        
        return new Witcher3GameState
        {
            Player = player
        };
    }

    private static T TryParseOr<T>(string value, bool ignoreCase = true, T defaultValue = default) where T : struct, IConvertible {
        return Enum.TryParse<T>(value, ignoreCase, out var res) ? res : defaultValue;
    }

    public void Dispose()
    {
        StopListening();
        _watcher = null;
        _dx12Watcher = null;
    }
}

public class Witcher3StateEventArgs : EventArgs
{
    public readonly Witcher3GameState GameState;

    public Witcher3StateEventArgs(Witcher3GameState gameState)
    {
        GameState = gameState;
    }
}