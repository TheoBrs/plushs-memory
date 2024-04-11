using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    Ability _fAbility1;
    Ability _fAbility2;
    Ability _fAbility3;
    int _pattoBuff = 0;

    [SerializeField] int posX;
    [SerializeField] int posY;
    Cell selectedGridCell;
    Cell selectedEnemyGridCell;
    float width;
    float height;
    Cell[,] elements;
    List<Cell> path;
    Entity entity;
    GameObject buttonAbility1;
    GameObject buttonAbility2;
    

    protected override void Start()
    {
        base.Start();
        CurrentPos = new Coord(posX, posY);
        transform.position = new Vector3(posX, 0.01f, posY);
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;
        elements = grid.GetGridCells();
        buttonAbility1 = GameObject.FindWithTag("Ability1");
        buttonAbility2 = GameObject.FindWithTag("Ability2");

        // !!!!!!!! Need Equipment Manager !!!!!!!!
        // EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged (Equipment newEquipment, Equipment oldEquipment)
    {
        if(newEquipment != null)
        {
            Defense.AddModifier(newEquipment.defenseModifier);
            Attack.AddModifier(newEquipment.attackModifier);
            MaxAP.AddModifier(newEquipment.apModifier);
        }

        if (oldEquipment != null)
        {
            Defense.RemoveModifier(oldEquipment.defenseModifier);
            Attack.RemoveModifier(oldEquipment.attackModifier);
            MaxAP.RemoveModifier(oldEquipment.apModifier);
        }
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

    public override void CastAbility1(Entity target)
    {
        if (CurrentAP - _ability1.Cost >= 0 && CurrentPos.DistanceTo(target.CurrentPos) == 1)
        {
            CurrentAP -= _ability1.Cost;
            CheckAP();
            TurnTowardTarget(target);
            if (_pattoBuff > 0)
            {
                target.TakeDamage((_ability1.Damage + Attack.GetValue()) * 2);
                _pattoBuff -= 1;
            }
            else
            {
                target.TakeDamage(_ability1.Damage + Attack.GetValue());
            }
        }
    }

    public override void CastAbility2(Entity target)
    {
        if (CurrentAP - _ability2.Cost >= 0 && CurrentPos.DistanceTo(target.CurrentPos) == 1)
        {
            CurrentAP -= _ability2.Cost;
            CheckAP();
            TurnTowardTarget(target);
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
    }

    public void CastFriendAbility1()
    {
        if (_fAbility1.RoundsBeforeReuse == 0)
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
            HandleOneTouch();
        }
        else if (Input.touchCount == 2)
        {
            HandleTwoTouch();
        }
    }
    void HandleOneTouch()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
            RaycastHit hitCell = new RaycastHit();

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.CompareTag("GridCell"))
                    {
                        hitCell = hit;
                        break;
                    }
                }
                if (hitCell.collider == null)
                    return;

                GameObject touchedObject = hitCell.transform.gameObject;
                if (selectedGridCell != null)
                    selectedGridCell.IsSelected = false;
                if (selectedEnemyGridCell != null)
                    selectedEnemyGridCell.IsSelected = false;

                elements = grid.GetGridCells();
                foreach (var gridElement in elements)
                {
                    if (touchedObject == gridElement.GameObject)
                    {
                        if (gridElement.HasObstacle)
                        {
                            grid.RefreshGridMat();
                            path?.Clear();
                            return;
                        }

                        selectedGridCell = gridElement;
                        selectedGridCell.IsSelected = true;
                        break;
                    }
                }

                path = AStar.FindPath(CurrentPos, selectedGridCell.Coord);


                if (path.Count <= 1)
                {
                    if (selectedGridCell != null)
                        selectedGridCell.IsSelected = false;
                    if (selectedEnemyGridCell != null)
                        selectedEnemyGridCell.IsSelected = false;
                    selectedGridCell = path[0];
                    entity = null;
                    path.Clear();
                    grid.RefreshGridMat();
                    return;
                }

                grid.RefreshGridMat();
                if (selectedGridCell.HasEnemy)
                {
                    // path[path.Count - 1].Entity Contain the cell with the enemy
                    entity = path[path.Count - 1].Entity;
                    selectedEnemyGridCell = path[path.Count - 1];
                    selectedGridCell = path[path.Count - 2];
                    path.RemoveAt(path.Count - 1);
                }
                else
                    entity = null;

                DrawPath();
            }
        }
    }

    void HandleTwoTouch()
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

    void DrawPath()
    {
        int steps = 0;
        foreach (var cell in path)
        {
            Cell gridElement = elements[cell.Coord.X + grid.GetMaxX() / 2, cell.Coord.Y + grid.GetMaxY() / 2];
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
        if (steps <= CurrentAP + 1 && CurrentAP > 0)
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

    void TurnTowardTarget(Entity target)
    {
        Vector3 directeur = (target.transform.position - transform.position);
        if (directeur.x > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (directeur.x < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (directeur.z > 0)
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        if (directeur.z < 0)
            transform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    public void CheckAP()
    {
        if (CurrentAP < _ability1.Cost)
        {
            buttonAbility1.GetComponent<Image>().color = buttonAbility1.GetComponent<Button>().colors.disabledColor;
            buttonAbility1.GetComponent<Button>().enabled = false;
        }
        else
        {
            buttonAbility1.GetComponent<Image>().color = buttonAbility1.GetComponent<Button>().colors.normalColor;
            buttonAbility1.GetComponent<Button>().enabled = true;
        }

        if (CurrentAP < _ability2.Cost)
        {
            buttonAbility2.GetComponent<Image>().color = buttonAbility2.GetComponent<Button>().colors.disabledColor;
            buttonAbility2.GetComponent<Button>().enabled = false;
        }
        else
        {
            buttonAbility2.GetComponent<Image>().color = buttonAbility2.GetComponent<Button>().colors.normalColor;
            buttonAbility2.GetComponent<Button>().enabled = true;
        }
    }

    public void Move()
    {
        if (path == null || path.Count == 0)
            return;

        CurrentAP -= path.Count - 1;
        CheckAP();
        grid.RefreshGridMat();
        Move(path);
        CurrentPos = selectedGridCell.Coord;
    }

    public void EndOfTurn()
    {
        if (selectedGridCell != null)
            selectedGridCell.IsSelected = false;
        if (selectedEnemyGridCell != null)
            selectedEnemyGridCell.IsSelected = false;
    }

    public Entity GetEnemy()
    {
        return entity;
    }
    
    public override void Death()
    {
        Debug.Log("Player Dead");
        TurnSystem turnSystyem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();
        turnSystyem.OnPlayerDeath();
        // GameOver
    }
}