namespace TennisGame.Domain.Entities;

public class Tournament: Entity
{
    public string Name { get; set; }
    public PlayerType PlayerType { get; set; }
    public IList<Match> Matches { get; }
    public Player Winner { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime EndDate { get; set; }

    public Tournament()
    {
        Matches = new List<Match>();
    }

    public static Tournament Create(string name, PlayerType playerType, DateTime initialDate) =>
        new()
        {
            Name = name,
            InitialDate = initialDate,
            PlayerType = playerType
        };

    public void AddMatch(Player playerOne, Player playerTwo)
    {
        if (playerOne.PlayerType != PlayerType || playerTwo.PlayerType != PlayerType)
            throw new ArgumentException("Players type are not equals");

        var match = new Match(this);
        match.AddPlayers(playerOne, playerTwo);
        Matches.Add(match);
    }

    public void Start()
    {
        var hasWinner = false;
        
        while (!hasWinner)
        {
            var matches = Matches
                .Where(m => !m.HasFinish)
                .ToList();

            matches.AsParallel()
                .ForAll(m => m.PlayMatch());
            
            var winners = matches.Select(m => m.Winner);
            
            if (winners.Count() == 1)
            {
                Winner = winners.First();
                EndDate = DateTime.Now;
                hasWinner = true;
            }
            else
            {
                var nextPlayers = winners.Chunk(2);

                foreach (var players in nextPlayers)
                {
                    AddMatch(players[0], players[1]);
                }
            }
        }
    }
}