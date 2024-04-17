using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Entity
{
    public int _currentAlly;
    int _previousAlly;
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
    GameObject buttonFriendlyAbility;
    GameObject MoveButton;
    GameObject EndTurnButton;
    Text playerAPText;
    bool movingButtonDisabled = false;
    public HealthBar PlayerHP;
    public Image allyImage;
    public Sprite keroImage;
    public Sprite boonImage;
    public Sprite pattoImage;

    void Awake()
    {
        buttonAbility1 = GameObject.FindWithTag("Ability1");
        buttonAbility2 = GameObject.FindWithTag("Ability2");
        buttonFriendlyAbility = GameObject.FindWithTag("FriendlyAbility");
        MoveButton = GameObject.FindWithTag("MoveButton");
        EndTurnButton = GameObject.FindWithTag("EndTurnButton");
        playerAPText = GameObject.FindWithTag("PlayerAPText").GetComponent<Text>();
    }

    protected override void Start()
    {
        base.Start();
        CurrentPos = new Coord(posX, posY);
        transform.position = new Vector3(posX * grid.gridCellScale, 0.01f, posY * grid.gridCellScale);
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;
        elements = grid.GetGridCells();
        CheckEntity();
        SetupAllyPassives();
        healthBar = PlayerHP;
        if (healthBar)
            healthBar.SetMaxHP(MaxHP.GetValue());
        // !!!!!!!! Need Equipment Manager !!!!!!!!
        // EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    public void SetupAllyPassives()
    {
        if (_previousAlly == 1)
        {
            MaxHP.RemoveModifier(3);
            CurrentHP = MaxHP.GetValue();
        }
        else if (_previousAlly == 2)
        {
            Defense.RemoveModifier(1);
        }
        else if (_previousAlly == 3)
        {
            Attack.RemoveModifier(1);
        }
        else
        {
            buttonFriendlyAbility.SetActive(true);
        }
        _previousAlly = _currentAlly;

        if (_currentAlly == 1)
        {
            allyImage.sprite = keroImage;
            MaxHP.AddModifier(3);
            CurrentHP = MaxHP.GetValue();
        }
        else if (_currentAlly == 2)
        {
            allyImage.sprite = boonImage;
            Defense.AddModifier(1);
        }
        else if (_currentAlly == 3)
        {
            allyImage.sprite = pattoImage;
            Attack.AddModifier(1);
        }
        else
        {
            buttonFriendlyAbility.SetActive(false);
        }
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

        _ability1.Damage = 1;
        _ability1.Cost = 1;

        _ability2.Damage = 3;
        _ability2.Cost = 2;

        _fAbility1.RoundsBeforeReuse = 0;
        _fAbility2.RoundsBeforeReuse = 0;
        _fAbility3.RoundsBeforeReuse = 0;
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
                target.TakeDamage((int)Mathf.Ceil((_ability1.Damage + Attack.GetValue()) * 1.5f));
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
                target.TakeDamage((int)Mathf.Ceil((_ability2.Damage + Attack.GetValue()) * 1.5f));
                _pattoBuff -= 1;
            }
            else
            {
                target.TakeDamage(_ability2.Damage + Attack.GetValue());
            }
        }
    }

    public void FriendAbilityButton()
    {
        if(_currentAlly == 1)
        {
            CastFriendAbility1();
        }
        else if(_currentAlly == 2)
        {
            CastFriendAbility2();
        }
        else if (_currentAlly == 3)
        {
            CastFriendAbility3();
        }
        else
        {
            Debug.Log("No ally chose");
        }
    }

    public void CastFriendAbility1()
    {
        if (_fAbility1.RoundsBeforeReuse == 0)
        {
            CurrentHP = Mathf.Clamp(CurrentHP + 5, 0, MaxHP.GetValue());
            healthBar.SetHP(CurrentHP);
            _fAbility1.RoundsBeforeReuse = 2;
            buttonFriendlyAbility.SetActive(false);
        }
    }

    public void CastFriendAbility2()
    {
        if (_fAbility2.RoundsBeforeReuse == 0)
        {
            _invincible = true;
            _fAbility2.RoundsBeforeReuse = 3;
            buttonFriendlyAbility.SetActive(false);
        }
    }

    public void CastFriendAbility3()
    {
        if (_fAbility3.RoundsBeforeReuse == 0)
        {
            _pattoBuff = 2;
            _fAbility3.RoundsBeforeReuse = 4;
            buttonFriendlyAbility.SetActive(false);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (!movingButtonDisabled)
            {
                MoveButton.GetComponent<Image>().color = MoveButton.GetComponent<Button>().colors.disabledColor;
                MoveButton.GetComponent<Button>().enabled = false;
                EndTurnButton.GetComponent<Image>().color = EndTurnButton.GetComponent<Button>().colors.disabledColor;
                EndTurnButton.GetComponent<Button>().enabled = false;
                movingButtonDisabled = true;
            }
            MoveOverTime();
        }
        else
        {
            if (movingButtonDisabled)
            {
                if (CurrentAP > 0)
                {
                    MoveButton.GetComponent<Image>().color = MoveButton.GetComponent<Button>().colors.normalColor;
                    MoveButton.GetComponent<Button>().enabled = true;
                }
                EndTurnButton.GetComponent<Image>().color = EndTurnButton.GetComponent<Button>().colors.normalColor;
                EndTurnButton.GetComponent<Button>().enabled = true;
                movingButtonDisabled = false;
            }
            if (Input.touchCount == 1)
            {
                HandleOneTouch();
            }
            else if (Input.touchCount == 2)
            {
                HandleTwoTouch();
            }
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

            if (EventSystem.current.currentSelectedGameObject == null && hits.Length > 0 || EventSystem.current.currentSelectedGameObject.layer != 5)
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
                if (selectedGridCell.HasEnemy)
                {
                    if (CurrentPos.DistanceTo(selectedGridCell.Coord) == 1)
                    {
                        selectedEnemyGridCell = selectedGridCell;
                        entity = selectedEnemyGridCell.Entity;
                    }
                    else
                    {
                        selectedGridCell.IsSelected = false;
                        entity = null;
                    }
                    CheckEntity();
                    selectedGridCell = null;
                    grid.RefreshGridMat();
                    path?.Clear();
                    return;
                }
                else
                {
                    if (selectedEnemyGridCell != null)
                    {
                        selectedEnemyGridCell.IsSelected = false;
                        selectedEnemyGridCell = null;
                    }
                    entity = null;
                    CheckEntity();
                }

                path = AStar.FindPath(CurrentPos, selectedGridCell.Coord);

                if (path.Count <= 1)
                {
                    if (selectedGridCell != null)
                        selectedGridCell.IsSelected = false;
                    selectedGridCell = path[0];
                    path.Clear();
                    grid.RefreshGridMat();
                    return;
                }

                grid.RefreshGridMat();
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
            if (steps < CurrentAP)
            {
                gridElement.SetGameObjectMaterial(grid.GetPathGridMat());
            }
            else if (steps == CurrentAP)
            {
                gridElement.SetGameObjectMaterial(grid.GetSelectedGridMat());
            }
            else
            {
                gridElement.Direction = 'u';
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
            selectedGridCell.Direction = 'u';
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

    public void CheckEntity()
    {
        if (entity)
        {
            buttonAbility1.SetActive(true);
            buttonAbility2.SetActive(true);
            CheckAP();
        }
        else
        {
            buttonAbility1.SetActive(false);
            buttonAbility2.SetActive(false);
        }
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

        if (CurrentAP <= 0)
        {
            MoveButton.GetComponent<Image>().color = MoveButton.GetComponent<Button>().colors.disabledColor;
            MoveButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            MoveButton.GetComponent<Image>().color = MoveButton.GetComponent<Button>().colors.normalColor;
            MoveButton.GetComponent<Button>().enabled = true;
        }

        playerAPText.text = "AP :   " + CurrentAP.ToString() + " / " + MaxAP.GetValue().ToString();
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
        entity = null;
        path?.Clear();
        CheckEntity();
        grid.RefreshGridMat();

        // Update RoundsBeforeReuse for friend abilities
        _fAbility1.RoundsBeforeReuse = Mathf.Clamp(_fAbility1.RoundsBeforeReuse - 1, 0, 10);
        _fAbility2.RoundsBeforeReuse = Mathf.Clamp(_fAbility2.RoundsBeforeReuse - 1, 0, 10);
        _fAbility3.RoundsBeforeReuse = Mathf.Clamp(_fAbility3.RoundsBeforeReuse - 1, 0, 10);

        if ((_currentAlly == 1 && _fAbility1.RoundsBeforeReuse == 0) || 
            (_currentAlly == 2 && _fAbility2.RoundsBeforeReuse == 0) || 
            (_currentAlly == 3 && _fAbility3.RoundsBeforeReuse == 0))
        {
            buttonFriendlyAbility.SetActive(true);
        }
    }

    public Entity GetEnemy()
    {
        return entity;
    }
    
    public override void Death()
    {
        Debug.Log("Player Dead");
        TurnSystem turnSystem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();
        turnSystem.OnPlayerDeath();
        // GameOver
    }
}