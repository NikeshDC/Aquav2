public struct Damage
{
    private int _value;
    public int value => _value;

    public enum Target { Men = 1, Ship = 2};
    private Target _damageTarget;
    public Target target => _damageTarget;

    public Damage(int value, Target damageTarget) 
    { 
        this._value = value; 
        this._damageTarget = damageTarget;
    }

    public bool HasTarget(Target damageTarget)
    {
        return ((this.target & damageTarget) == damageTarget);
    }
}
