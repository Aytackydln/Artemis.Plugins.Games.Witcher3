using Artemis.Core.Modules;
using Witcher3Gsi;

namespace Artemis.Plugins.Games.Witcher3.DataModels;

public class Witcher3DataModel : DataModel
{
    public Witcher3Player Player { get; set; } = new();
}