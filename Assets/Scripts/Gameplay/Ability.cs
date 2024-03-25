public class Ability
{
    private int _damage = 0;
    private int _cost = 0;
    private int _roundsBeforeReuse = 0;

    public int Damage 
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public int Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }
    public int RoundsBeforeReuse
    {
        get { return _roundsBeforeReuse; }
        set { _roundsBeforeReuse = value; }
    }

    public Ability() {}
}
