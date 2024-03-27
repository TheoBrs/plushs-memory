public class Ability
{
    private string _name;
    private string _description;

    private int _damage = 0;
    private int _cost = 0;
    private int _roundsBeforeReuse = 0;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

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
}
