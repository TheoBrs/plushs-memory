using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    Ability _fAbility1;
    Ability _fAbility2;
    Ability _fAbility3;
    int _pattoBuff = 0;

    [SerializeField] int posX;
    [SerializeField] int posY;
    GameObject player;
    Cell selectedGridCell;
    float width;
    float height;
    CombatGrid grid;
    Cell[,] elements;
    List<Cell> path;
    Entity entity;

    protected override void Start()
    {
        base.Start();
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        CurrentPos = new Coord(posX, posY);
        transform.position = new Vector3(posX, 0.01f, posY);
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;
    }

    protected override void AbilitiesInitialization()
    {
        _ability1 = new Ability();
        _ability2 = new Ability();
        _fAbility1 = new Ability();
        _fAbility2 = new Ability();
        _fAbility3 = new Ability();

        _ability1.Damage = 2;
        _ability1.Cost = 1;

        _ability2.Damage = 4;
        _ability2.Cost = 2;

        _fAbility1.RoundsBeforeReuse = 2;
        _fAbility2.RoundsBeforeReuse = 3;
        _fAbility3.RoundsBeforeReuse = 4;
    }

    public override void CastAbility1(Entity target) //corps à corps
    {
        CurrentAP -= _ability1.Cost;
        if(_pattoBuff > 0)
        {
            target.TakeDamage((_ability1.Damage + Attack.GetValue()) *2);
            _pattoBuff -= 1;
        }
        else
        {
            Debug.Log(_ability1.Damage);
            Debug.Log(Attack.GetValue());

            target.TakeDamage(_ability1.Damage + Attack.GetValue());
        }
    }

    public override void CastAbility2(Entity target)
    {
        CurrentAP -= _ability2.Cost;
        if (_pattoBuff > 0)
        {
            target.TakeDamage((_ability2.Damage + Attack.GetValue()) *2);
            _pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(_ability2.Damage + Attack.GetValue());
        }
    }

    public void CastFriendAbility1()
    {
        if(_fAbility1.RoundsBeforeReuse == 0)
        {
            CurrentHP += 5;
            _fAbility1.RoundsBeforeReuse = 2;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    public void CastFriendAbility2()
    {
        if (_fAbility2.RoundsBeforeReuse == 0)
        {
            _invincible = true;
            _fAbility2.RoundsBeforeReuse = 3;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    public void CastFriendAbility3()
    {
        if (_fAbility3.RoundsBeforeReuse == 0)
        {
            _pattoBuff = 2;
            _fAbility3.RoundsBeforeReuse = 4;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    void Update()
    {
        if (_isMoving)
        {
            MoveOverTime();
        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
                RaycastHit hitCell = new RaycastHit();
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

                if (hits.Length > 0)
                {

                    foreach (var hit in hits)
                    {
                        // Need to use a different check
                        if (hit.transform.CompareTag("GridCell"))
                        {
                            hitCell = hit;
                            break;
                        }
                    }
                    if (hitCell.collider != null)
                    {
                        GameObject touchedObject = hitCell.transform.gameObject;
                        if (selectedGridCell != null)
                            selectedGridCell.SetGameObjectMaterial(grid.GetDefaultGridMat());

                        elements = grid.GetGridCells();
                        foreach (var gridElement in elements)
                        {
                            if (touchedObject == gridElement.GameObject)
                            {
                                if (gridElement.HasObstacle)
                                {
                                    RefreshGridMat();
                                    if (path != null)
                                        path.Clear();
                                    return;
                                }

                                if (gridElement.HasEnemy)
                                {
                                    RefreshGridMat();
                                }

                                selectedGridCell = gridElement;
                                selectedGridCell.SetGameObjectMaterial(grid.GetSelectedGridMat());
                                break;
                            }
                        }

                        RefreshGridMat();
                        path = AStar.FindPath(CurrentPos, selectedGridCell.Coord, elements, grid.GetMaxX(), grid.GetMaxY());

                        if (selectedGridCell.HasEnemy)
                        {
                            // path[path.Count - 1].Entity Contain the cell with the enemy
                            entity = path[path.Count - 1].Entity;
                            selectedGridCell = path[path.Count - 2];
                            path.RemoveAt(path.Count - 1);
                        }
                        else
                            entity = null;

                        if (path.Count <= 1)
                        {
                            selectedGridCell = path[0];
                            path.Clear();
                            return;
                        }

                        int steps = 0;
                        foreach (var cell in path)
                        {
                            Cell gridElement = elements[cell.Coord.X + grid.GetMaxX() / 2, cell.Coord.Y + grid.GetMaxY() / 2];
                            // This is assuming that the current AP doesn't change while selecting a movement
                            if (steps <= CurrentAP)
                            {
                                gridElement.SetGameObjectMaterial(grid.GetPathGridMat());
                            }
                            else
                            {
                                gridElement.SetGameObjectMaterial(grid.GetRedPathGridMat());
                            }
                            steps++;
                        }
                        if (steps <= CurrentAP + 1)
                        {
                            selectedGridCell.SetGameObjectMaterial(grid.GetSelectedGridMat());
                        }
                        else
                        {
                            selectedGridCell.SetGameObjectMaterial(grid.GetRedPathGridMat());
                            // can add more stuff that prevent to move
                            path.Clear();
                        }
                    }
                }
            }
        }
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;

                Vector3 newCamPos = new Vector3(-touchPosition.x, -touchPosition.y, -10);
                newCamPos.x = (newCamPos.x + width) / width;
                newCamPos.y = (newCamPos.y + height) / height;
                Camera.main.transform.localPosition = newCamPos;
            }
        }
    }

    public void RefreshGridMat()
    {
        foreach (var cell in elements)
        {
            if (cell.HasObstacle)
                cell.SetGameObjectMaterial(grid.GetNotWalkableGridMat());
            else if (cell.HasEnemy)
                cell.SetGameObjectMaterial(grid.GetEnemyGridMat());
            else
                cell.SetGameObjectMaterial(grid.GetDefaultGridMat());
        }
    }

    public void Move()
    {
        if (path == null || path.Count == 0)
            return;

        CurrentAP -= path.Count - 1;

        RefreshGridMat();
        Move(path);
        CurrentPos = selectedGridCell.Coord;
    }

    public Entity GetEnemy()
    {
        return entity;
    }

    public void DebugEnemyStr()
    {
        Entity entity = GetEnemy();
        if(entity  == null)
            Debug.Log("No Entity Selected");
        else
            Debug.Log(entity.name);
    }
    public override void Death()
    {
        // GameOver
    }
}