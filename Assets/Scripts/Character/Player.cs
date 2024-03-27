using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Player : Entity
{
    Ability _fAbility1;
    Ability _fAbility2;
    Ability _fAbility3;
    int _pattoBuff = 0;

    GameObject player;
    Coord currentCell;
    Cell selectedGridCell;
    Vector3 position;
    float width;
    float height;
    CombatGrid grid;
    Cell[,] elements;
    List<Cell> path;

    protected override void Start()
    {
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        currentCell = new Coord(0, 0);
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;

        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();
        AbilitiesInitialization();
        base.Start();
    }

    protected override void AbilitiesInitialization()
    {
        /* Not instanciated
        _ability1.Damage = 2;
        _ability1.Cost = 1;

        _ability2.Damage = 4;
        _ability2.Cost = 2;

        _fAbility1.RoundsBeforeReuse = 2;
        _fAbility2.RoundsBeforeReuse = 3;
        _fAbility3.RoundsBeforeReuse = 4;
        */
    }

    protected override void CastAbility1(Entity target)
    {
        _currentAP -= _ability1.Cost;
        if(_pattoBuff > 0)
        {
            target.TakeDamage((_ability1.Damage + Attack.GetValue()) *2);
            _pattoBuff -= 1;
        }
        else
        {
            target.TakeDamage(_ability1.Damage + Attack.GetValue());
        }
    }

    protected override void CastAbility2(Entity target)
    {
        _currentAP -= _ability2.Cost;
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

    protected void CastFriendAbility1()
    {
        if(_fAbility1.RoundsBeforeReuse == 0)
        {
            _currentHP += 5;
            _fAbility1.RoundsBeforeReuse = 2;
        }
        else
        {
            // Make the button grey and prevent it for being clicked
            // (there shouldn't be a "else", it's to not forget to do this)
        }
    }

    protected void CastFriendAbility2()
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

    protected void CastFriendAbility3()
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
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        GameObject touchedObject = hit.transform.gameObject;
                        if (touchedObject.transform.name == "GridCell(Clone)")
                        {
                            if (selectedGridCell != null)
                                selectedGridCell.SetGameObjectMaterial(grid.GetDefaultGridMat());

                            elements = grid.GetGridElements();
                            foreach (var gridElement in elements)
                            {
                                if (touchedObject == gridElement.GameObject)
                                {
                                    if (gridElement.IsObstacle)
                                    {
                                        foreach (var tempcell in elements)
                                        {
                                            if (tempcell.IsObstacle)
                                                tempcell.SetGameObjectMaterial(grid.GetNotWalkableGridMat());
                                            else if (tempcell.HasEnemy)
                                                tempcell.SetGameObjectMaterial(grid.GetEnemyGridMat());
                                            else
                                                tempcell.SetGameObjectMaterial(grid.GetDefaultGridMat());
                                        }
                                        if (path != null)
                                            path.Clear();

                                        return;
                                    }

                                    if (gridElement.HasEnemy)
                                    {
                                        foreach (var tempcell in elements)
                                        {
                                            if (tempcell.IsObstacle)
                                                tempcell.SetGameObjectMaterial(grid.GetNotWalkableGridMat());
                                            else if (tempcell.HasEnemy)
                                                tempcell.SetGameObjectMaterial(grid.GetEnemyGridMat());
                                            else
                                                tempcell.SetGameObjectMaterial(grid.GetDefaultGridMat());
                                        }
                                    }

                                    selectedGridCell = gridElement;
                                    selectedGridCell.SetGameObjectMaterial(grid.GetSelectedGridMat());
                                }
                            }

                            foreach (var gridElement in elements)
                            {
                                if (gridElement.IsObstacle)
                                    gridElement.SetGameObjectMaterial(grid.GetNotWalkableGridMat());
                                else if (gridElement.HasEnemy)
                                    gridElement.SetGameObjectMaterial(grid.GetEnemyGridMat());
                                else
                                    gridElement.SetGameObjectMaterial(grid.GetDefaultGridMat());
                            }
                            path = AStar.FindPath(currentCell, selectedGridCell.Coord, elements);

                            if (selectedGridCell.HasEnemy)
                            {
                                // path[path.Count - 1] Contain the cell with the enemy
                                // path[path.Count - 1].Entity.TakeDamage(3);
                                selectedGridCell = path[path.Count - 2];
                                path.RemoveAt(path.Count - 1);
                            }

                            int steps = 0;
                            foreach (var cell in path)
                            {
                                Cell gridElement = elements[cell.Coord.X + grid.GetMaxX() / 2, cell.Coord.Y + grid.GetMaxY() / 2];
                                // This is assuming that the current AP doesn't change while selecting a movement
                                if (steps <= _currentAP)
                                {
                                    gridElement.SetGameObjectMaterial(grid.GetPathGridMat());
                                }
                                else
                                {
                                    gridElement.SetGameObjectMaterial(grid.GetRedPathGridMat());
                                }
                                steps++;
                            }
                            if (steps <= _currentAP + 1)
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

    void Move()
    {
        if (path == null || path.Count == 0)
            return;

        foreach (var gridElement in elements)
        {
            if (gridElement.IsObstacle)
                gridElement.SetGameObjectMaterial(grid.GetNotWalkableGridMat());
            else
                gridElement.SetGameObjectMaterial(grid.GetDefaultGridMat());
        }
        Move(selectedGridCell.Coord, true);
        currentCell = selectedGridCell.Coord;
        path.Clear();
    }
    public override void Death()
    {
        // GameOver
    }
}