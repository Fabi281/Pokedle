using Pokedle.Components.Models;

namespace Pokedle.Components.Services;

public class FlyingPokemonService : IDisposable
{
    private readonly Random _random = new();

    private readonly List<AnimatedPokemon> _activePokemon = [];

    public IReadOnlyList<AnimatedPokemon> ActivePokemon => _activePokemon;

    public event Action? Changed;

    private readonly AnimatedPokemonDefinition[] _definitions =
    [
        new() { CssClass = "pidgey", Weight = 50, Duration = TimeSpan.FromSeconds(30), Scale = 2 },
        new() { CssClass = "zubat", Weight = 40, Duration = TimeSpan.FromSeconds(30), Scale = 2 },
        new() { CssClass = "butterfree", Weight = 30, Duration = TimeSpan.FromSeconds(30), Scale = 2 },
        new() { CssClass = "spearow", Weight = 25, Duration = TimeSpan.FromSeconds(30), Scale = 2 },
        new() { CssClass = "articuno", Weight = 5, Duration = TimeSpan.FromSeconds(30), Scale = 3 },
        new() { CssClass = "zapdos", Weight = 5, Duration = TimeSpan.FromSeconds(30), Scale = 3 },
        new() { CssClass = "moltres", Weight = 5, Duration = TimeSpan.FromSeconds(30), Scale = 3 }
    ];

    public FlyingPokemonService()
    {
        _ = RunAsync();
    }

    private async Task RunAsync()
    {
        while (true)
        {
            Spawn();
            Changed?.Invoke();
            await Task.Delay(_random.Next(5000, 7000));
        }
    }

    private void Spawn()
    {
        var randomPokemonDefintion = PickWeighted();

        var pokemon = new AnimatedPokemon
        {
            Definition = randomPokemonDefintion,
            Direction = _random.Next(2) == 0
                ? FlightDirection.Left
                : FlightDirection.Right,

            Top = _random.NextDouble() * 80,

            RemoveAt = DateTime.UtcNow + randomPokemonDefintion.Duration
        };

        _activePokemon.Add(pokemon);
        _ = RemoveLater(pokemon);
    }

    private AnimatedPokemonDefinition PickWeighted()
    {
        var total = _definitions.Sum(x => x.Weight);
        var value = _random.Next(total);

        foreach (var pokemon in _definitions)
        {
            if (value < pokemon.Weight)
                return pokemon;

            value -= pokemon.Weight;
        }

        return _definitions.Last();
    }

    private async Task RemoveLater(AnimatedPokemon pokemon)
    {
        await Task.Delay(pokemon.Definition.Duration);

        _activePokemon.Remove(pokemon);
        Changed?.Invoke();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}