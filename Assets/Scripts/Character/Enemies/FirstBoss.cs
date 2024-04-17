using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FirstBoss : Enemy
{
    [SerializeField] GameObject mitePrefab;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1.Damage = 2;
    }

    public override void Attacking()
    {
        Debug.Log("Attackcing");
        Player _player = FindObjectOfType<Player>();
        if (_ability2.RoundsBeforeReuse == 0)
        {
            CastAbility2(_player);
        }
        List<Coord> attackCoords = new List<Coord>();
        for (int i = 0; i < Size.X; i++)
        {
            for (int j = 0; j < Size.Y; j++)
            {
                attackCoords.Add(new Coord(CurrentPos.X + i - 2, CurrentPos.Y + j));
                attackCoords.Add(new Coord(CurrentPos.X + i + 2, CurrentPos.Y + j));
                attackCoords.Add(new Coord(CurrentPos.X + i, CurrentPos.Y + j - 2));
                attackCoords.Add(new Coord(CurrentPos.X + i, CurrentPos.Y + j + 2));
            }
        }

        if (attackCoords.Contains(_player.CurrentPos))
        {
            Vector3 directeur = (_player.transform.position - transform.position - new Vector3(0.5f, 0, 0.5f) * grid.gridCellScale);
            if (Mathf.Abs(directeur.x) > Mathf.Abs(directeur.z))
            {
                transform.localRotation = directeur.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = directeur.z < 0 ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);
            }

            if (_ability1.RoundsBeforeReuse == 0)
            {
                CastAbility1(_player);
            }
        }

        _ability1.RoundsBeforeReuse = Mathf.Clamp(_ability1.RoundsBeforeReuse - 1, 0, 10);
        _ability2.RoundsBeforeReuse = Mathf.Clamp(_ability2.RoundsBeforeReuse - 1, 0, 10);
    }
    public override void CastAbility1(Entity target)
    {
        base.CastAbility1(target);
        _ability1.RoundsBeforeReuse = 1;
    }
    public override void CastAbility2(Entity target)
    {
        Vector3 rotation = new Vector3(0, 180, 0);
        var x = 0; // Random or something
        var y = 0; // Random or something
        if (grid.AddEnemy(new Coord(x, y), mitePrefab, rotation, new Coord(1, 1), false))
        {
            Debug.Log("Spawn Mite");
            _ability2.RoundsBeforeReuse = 3;
        }
    }
}
