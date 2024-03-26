using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : Entity
{
    Ability _fAbility1;
    Ability _fAbility2;
    Ability _fAbility3;
    int _pattoBuff = 0;

    GameObject player;
    Coord currentCell;
    GridElement selectedGridCell;
    Vector3 position;
    float width;
    float height;
    CombatGrid grid;
    GridElement[,] elements;
    Cell[] map;

    public void Start()
    {
        grid = GameObject.FindWithTag("CombatGrid").GetComponent<CombatGrid>();
        currentCell = new Coord(0, 0);
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;

        _currentHP = MaxHP.GetValue();
        _currentAP = MaxAP.GetValue();
        AbilitiesInitialization();
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
                                selectedGridCell.SetGameObjectMaterial(grid.GetGridMat());

                            elements = grid.GetGridElements();
                            map = new Cell[elements.Length];
                            int index = 0;
                            foreach (var gridElement in elements)
                            {
                                Cell newCell = new Cell { coord = gridElement.GetCoord() };
                                map[index] = newCell;
                                index++;

                                if (touchedObject == gridElement.GetGameObject())
                                {
                                    selectedGridCell = gridElement;
                                    selectedGridCell.SetGameObjectMaterial(grid.GetSelectedGridMat());
                                }
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

    public void Move()
    {
        foreach (var gridElement in elements)
        {
            gridElement.SetGameObjectMaterial(grid.GetGridMat());
        }
        List<Cell> path = AStar.FindPath(currentCell, selectedGridCell.GetCoord(), map);
        foreach (var cell in path)
        {
            foreach (var gridElement in elements)
            {
                if (gridElement.GetCoord().Equals(cell.coord))
                    gridElement.SetGameObjectMaterial(grid.GetPathGridMat());
            }
        }
        selectedGridCell.SetGameObjectMaterial(grid.GetSelectedGridMat());
        Move(selectedGridCell.GetCoord(), true);
        currentCell = selectedGridCell.GetCoord();
    }
    public override void Death()
    {
        // GameOver
    }
}