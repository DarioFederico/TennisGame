using MediatR;
using Microsoft.AspNetCore.Mvc;
using TennisGame.Api.Middlewares;
using TennisGame.Application.Features.Tournaments.Commands;
using TennisGame.Application.Features.Tournaments.Dtos;
using TennisGame.Application.Features.Tournaments.Queries;

namespace TennisGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TournamentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TournamentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetTournamentsByCriteria")]
    [ProducesResponseType(typeof(IEnumerable<TournamentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] GetTournamentByFiltersQuery query) => Ok(await _mediator.Send(query));

    [HttpPost(Name = "CreateTournament")]
    [ProducesResponseType(typeof(CreateTournamentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateTournamentCommand command) => Ok(await _mediator.Send(command));
}