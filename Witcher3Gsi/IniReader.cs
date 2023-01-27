namespace Witcher3Gsi;

internal static class IniReader
{
    internal static string[] GetSection(string file, String section)
    {
        try
        {
            var start = file.IndexOf(section);//finds the beginning of the artemis string
            var end = file.IndexOf("[", start + section.Length);//finds the first [ after the artemis section header
            return file.Substring(start, end - start)//obtains just the useful part of the config
                .Replace("\n", "")//removes all \n
                .Split('\r')//splits into lines
                .Where(s => s != string.Empty && !s.Contains("Artemis"))//removes last empty line and header
                .ToArray();
        }
        catch//if we reach the catch block, we either didnt find anything, or what we found wasnt formatted correctly
        {//most likely the mod isnt installed properly
            return Array.Empty<string>();
        }
    }

    internal static int GetInt(string[] data, string name)
    {
        try
        {
            var first = data.FirstOrDefault(d => d.Contains(name))
                .Split('=')[1]
                .Split('.')[0];
            return int.Parse(first);
        }
        catch
        {
            return -1;
        }
    }

    internal static T GetEnum<T>(string[] artemisSection, string activeSign, T defaultValue = default) where T : struct, IConvertible
    {
        var enumText = artemisSection.FirstOrDefault(d => d.Contains(activeSign))?.Split('_').Last() ?? "";
        return Enum.TryParse<T>(enumText, true, out var res) ? res : defaultValue;
    }
}