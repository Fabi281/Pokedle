namespace Pokedle.Components.Models;

public record Pokemon(int PokedexNumber, string Name, string PrimaryType, string SecondaryType, int HP, int Attack, int Defense, int SpecialAttack, int SpecialDefense, int Speed, int EvolutionStage, string Color, double Height, double Weight, int Generation, string DexEntry)
{
    public List<string> HighestStatNames => GetHighestStatNames();

    private List<string> GetHighestStatNames()
    {
        var stats = new Dictionary<string, int>
        {
            { "HP", HP },
            { "Attack", Attack },
            { "Defense", Defense },
            { "Sp. Attack", SpecialAttack },
            { "Sp. Defense", SpecialDefense },
            { "Speed", Speed }
        };
        var maxStatValue = stats.Values.Max();
        return [.. stats.Where(s => s.Value == maxStatValue).Select(s => s.Key).ToList()];
    }
}

