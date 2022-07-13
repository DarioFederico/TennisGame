using FluentValidation;

namespace TennisGame.Application.Features.Tournaments.Commands;

public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
{
    public CreateTournamentCommandValidator()
    {
        RuleFor(t => t.Players)
            .NotEmpty()
            .WithMessage("Player cannot be empty");
        
        RuleFor(t => t.Players)
            .Must(IsValidCount)
            .WithMessage("La cantidad de jugadores debe ser potencia de 2");

        RuleFor(t => t.PlayerType)
            .IsInEnum()
            .WithMessage("Invalid player type");
    }
    
    private bool IsValidCount(IEnumerable<PlayerDto> playerDtos)
    {
        var total = playerDtos.Count();
        return total >= 2 && IsPowerOfTwo(total);
    }

    private bool IsPowerOfTwo(int number) => (number & (number - 1)) == 0;
}