using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Enemy
{
    [SerializeField] GameObject mitePrefab;
    protected override void Awake()
    {
        base.Awake();
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
        Player _player = FindObjectOfType<Player>();
        if (_ability2.RoundsBeforeReuse == 0)
        {
            CastAbility2(_player);
        }
        List<Coord> attackCoords = new List<Coord>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                attackCoords.Add(new Coord(CurrentPos.X - 3 + i, CurrentPos.Y + j));
                attackCoords.Add(new Coord(CurrentPos.X + j, CurrentPos.Y + 4 - i));
                attackCoords.Add(new Coord(CurrentPos.X + 4 - i, CurrentPos.Y + j));
                attackCoords.Add(new Coord(CurrentPos.X + j, CurrentPos.Y - 1 - i));
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
        List<Coord> possiblesCells = new List<Coord>
        {
            new Coord(+2, +2),
            new Coord(+2, -1),
            new Coord(+2, -2)
        };
        int index = Mathf.FloorToInt(Random.Range(0, 3));
        Vector3 rotation = new Vector3(0, 180, 0);
        if (grid.AddEnemy(possiblesCells[index], mitePrefab, rotation, new Coord(1, 1), false, false))
        {
            _ability2.RoundsBeforeReuse = 3;
        }
    }
}
