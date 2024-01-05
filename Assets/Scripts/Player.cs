public class Player
{
    private int _playerId;
    public int playerId => _playerId;

    private int _points;
    public int points => _points;
    public void IncrementPoints(int amount) { _points += amount; }
    public bool TryDecrementPoints(int amount) 
    { 
        if(points - amount < 0)
        {  return false; }
        else
        { 
            _points -= amount; 
            return true; 
        }
    }

    public Player(int playerId, int points)
    {
        _playerId = playerId;
        _points = points;   
    }
}
