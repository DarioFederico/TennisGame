namespace TennisGame.Domain.Entities;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}