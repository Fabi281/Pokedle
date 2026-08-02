namespace Pokedle.Components.Models;

public class GuessResult
{
    public Guid Id = Guid.NewGuid();
    public Pokemon Guess { get; set; } = default!;
    public MatchState Name { get; set; }
    public MatchState PrimaryType { get; set; }
    public MatchState SecondaryType { get; set; }
    public MatchState HighestStatNames { get; set; }
    public MatchState EvolutionStage { get; set; }
    public MatchState Color { get; set; }
    public MatchState Height { get; set; }
    public MatchState Weight { get; set; }
    public MatchState Generation { get; set; }

    public int EvolutionStageDirection { get; set; } // -1 lower, 0 equal, 1 higher
    public int HeightDirection { get; set; } 
    public int WeightDirection { get; set; }
    public int GenerationDirection { get; set; }

    public static GuessResult Compare(Pokemon TargetPokemon, Pokemon Guess)
    {
        return new GuessResult
        {
            Guess = Guess,

            Name = Guess.Name == TargetPokemon.Name
                ? MatchState.Correct
                : MatchState.Wrong,

            PrimaryType = Guess.PrimaryType == TargetPokemon.PrimaryType
                ? MatchState.Correct
                : Guess.PrimaryType == TargetPokemon.SecondaryType
                    ? MatchState.Partial
                    : MatchState.Wrong,

            SecondaryType = Guess.SecondaryType == TargetPokemon.SecondaryType
                ? MatchState.Correct
                : Guess.SecondaryType == TargetPokemon.PrimaryType
                    ? MatchState.Partial
                    : MatchState.Wrong,

            HighestStatNames = Guess.HighestStatNames.Intersect(TargetPokemon.HighestStatNames).Any()
                ? Guess.HighestStatNames.SequenceEqual(TargetPokemon.HighestStatNames)
                    ? MatchState.Correct
                    : MatchState.Partial
                : MatchState.Wrong,

            EvolutionStage = Guess.EvolutionStage == TargetPokemon.EvolutionStage
                ? MatchState.Correct
                : MatchState.Wrong,

            Color = Guess.Color == TargetPokemon.Color
                ? MatchState.Correct
                : MatchState.Wrong,

            Height = Guess.Height == TargetPokemon.Height
                ? MatchState.Correct
                : Math.Abs(Guess.Height - TargetPokemon.Height) <= 5
                    ? MatchState.Partial
                    : MatchState.Wrong,

            Weight = Guess.Weight == TargetPokemon.Weight
                ? MatchState.Correct
                : Math.Abs(Guess.Weight - TargetPokemon.Weight) <= 5
                    ? MatchState.Partial
                    : MatchState.Wrong,

            Generation = Guess.Generation == TargetPokemon.Generation
                ? MatchState.Correct
                : MatchState.Wrong,



            HeightDirection = Math.Sign(TargetPokemon.Height - Guess.Height),
            EvolutionStageDirection = Math.Sign(TargetPokemon.EvolutionStage - Guess.EvolutionStage),
            WeightDirection = Math.Sign(TargetPokemon.Weight - Guess.Weight),
            GenerationDirection = Math.Sign(TargetPokemon.Generation - Guess.Generation)
        };
    }
}

public enum MatchState
{
    Wrong,
    Partial,
    Correct
}