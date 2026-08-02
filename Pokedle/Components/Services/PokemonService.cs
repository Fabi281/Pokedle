using CsvHelper;
using Pokedle.Components.Models;
using System.Globalization;

namespace Pokedle.Components.Services;

public class PokemonService
{
    public IReadOnlyList<Pokemon> All { get; }

    public IReadOnlyDictionary<int, Pokemon> ByDexNumber { get; }

    public PokemonService(IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, "Components", "Data", "pokemon.csv");

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var pokemon = csv.GetRecords<Pokemon>().ToList();

        All = pokemon;
        ByDexNumber = pokemon.ToDictionary(p => p.PokedexNumber);
    }
}
