namespace Pokedle.Components.Models;

public class AnimatedPokemon
{
    public Guid Id { get; } = Guid.NewGuid();

    public required AnimatedPokemonDefinition Definition { get; init; }

    public required FlightDirection Direction { get; init; }

    public double Top { get; init; }

    public DateTime RemoveAt { get; init; }
}

public class AnimatedPokemonDefinition
{
    public required string CssClass { get; init; }

    public int Weight { get; init; }

    public TimeSpan Duration { get; init; }

    public double Scale { get; init; } = 3;
}

public enum FlightDirection
{
    Left,
    Right
}
