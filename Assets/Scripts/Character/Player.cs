using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Ally")]
    public int currentAlly;
    public Sprite keroImage;
    public Sprite boonImage;
    public Sprite pattoImage;


    private int _previousAlly;
    private Ability _fAbility1;
    private Ability _fAbility2;
    private Ability _fAbility3;
    private int _pattoBuff = 0;
    private Cell _selectedGridCell;
    private Cell _selectedEnemyGridCell;
    private float _width;
    private float _height;
    private Cell[,] _elements;
    private List<Cell> _path;
    private GameObject _buttonAbility1;
    private GameObject _buttonAbility2;
    private GameObject _buttonFriendlyAbility;
    private GameObject _MoveButton;
    private GameObject _EndTurnButton;
    private bool _movingButtonDisabled = false;
    private GameObject _playerAP;
    private Image _allyImage;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public Entity entity;


    protected override void Awake()
    {
        base.Awake();
        _buttonAbility1 = GameObject.FindWithTag("Ability1");
        _buttonAbility2 = GameObject.FindWithTag("Ability2");
        _buttonFriendlyAbility = GameObject.FindWithTag("FriendlyAbility");
        _allyImage = GameObject.FindWithTag("FriendlyAbilityImage").GetComponent<Image>();
        _MoveButton = GameObject.FindWithTag("MoveButton");
        _EndTurnButton = GameObject.FindWithTag("EndTurnButton");
        _playerAP = GameObject.FindWithTag("PlayerAPText");
        _width = Screen.width / 2.0f;
        _height = Screen.height / 2.0f;
        _elements = _grid.GetGridCells();
        CheckEntity();
        SetupAllyPassives();
        _healthBar = ToolBox.GetChildWithTag(GameObject.FindWithTag("PlayerHPText").transform, "HealthBar").GetComponent<HealthBar>();
        if (_healthBar)
            _healthBar.SetMaxHP(MaxHP.GetValue());
        if (EquipmentManager.Instance)
            EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
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
            _buttonFriendlyAbility.GetComponent<Image>().enabled = true;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        _previousAlly = currentAlly;

        if (currentAlly == 1)
        {
            _allyImage.sprite = keroImage;
            MaxHP.AddModifier(3);
            CurrentHP = MaxHP.GetValue();
            if (_healthBar)
                _healthBar.SetMaxHP(MaxHP.GetValue());
        }
        else if (currentAlly == 2)
        {
            _allyImage.sprite = boonImage;
            Defense.AddModifier(1);
        }
        else if (currentAlly == 3)
        {
            _allyImage.sprite = pattoImage;
            Attack.AddModifier(1);
        }
        else
        {
            _buttonFriendlyAbility.GetComponent<Image>().enabled = false;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = false;
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
        bool canReach = false;
        foreach (Cell cell in target.occupiedCells)
        {
            if (CurrentPos.DistanceTo(cell.Coord) == 1)
            {
                canReach = true;
                TurnTowardTarget(cell);
                break;
            }
        }

        if (CurrentAP - _ability1.Cost >= 0 && canReach)
        {
            _animator.SetTrigger("Attack");
            CurrentAP -= _ability1.Cost;
            CheckAP(true);
            _lastAbilityAttack = 1;
            _currentTarget = target;
        }
    }

    public override void CastAbility2(Entity target)
    {
        bool canReach = false;
        foreach (Cell cell in target.occupiedCells)
        {
            if (CurrentPos.DistanceTo(cell.Coord) == 1)
            {
                canReach = true;
                TurnTowardTarget(cell);
                break;
            }
        }

        if (CurrentAP - _ability2.Cost >= 0 && canReach)
        {
            _animator.SetTrigger("Attack");
            CurrentAP -= _ability2.Cost;
            CheckAP(true);
            _lastAbilityAttack = 2;
            _currentTarget = target;
        }
    }

    public override void AttackEvent()
    {
        if (_lastAbilityAttack == 1)
        {
            _SFXplayer.Play(false);
            if (_pattoBuff > 0)
            {
                _currentTarget.TakeDamage((int)Mathf.Ceil((_ability1.Damage + Attack.GetValue()) * 1.5f));
                _pattoBuff -= 1;
            }
            else
            {
                _currentTarget.TakeDamage(_ability1.Damage + Attack.GetValue());
            }
        }

        if (_lastAbilityAttack == 2)
        {
            _SFXplayer.Play(true);
            if (_pattoBuff > 0)
            {
                _currentTarget.TakeDamage((int)Mathf.Ceil((_ability2.Damage + Attack.GetValue()) * 1.5f));
                _pattoBuff -= 1;
            }
            else
            {
                _currentTarget.TakeDamage(_ability2.Damage + Attack.GetValue());
            }
        }
        CheckAP(false);
        isAttacking = false;
    }

    public void FriendAbilityButton()
    {
        if(currentAlly == 1)
        {
            CastFriendAbility1();
        }
        else if(currentAlly == 2)
        {
            CastFriendAbility2();
        }
        else if (currentAlly == 3)
        {
            CastFriendAbility3();
        }

    }

    public void CastFriendAbility1()
    {
        if (_fAbility1.RoundsBeforeReuse == 0)
        {
            CurrentHP = Mathf.Clamp(CurrentHP + 5, 0, MaxHP.GetValue());
            _healthBar.SetHP(CurrentHP);
            _fAbility1.RoundsBeforeReuse = 2;
            _buttonFriendlyAbility.GetComponent<Image>().enabled = false;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public void CastFriendAbility2()
    {
        if (_fAbility2.RoundsBeforeReuse == 0)
        {
            _invincible = true;
            _fAbility2.RoundsBeforeReuse = 3;
            _buttonFriendlyAbility.GetComponent<Image>().enabled = false;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    public void CastFriendAbility3()
    {
        if (_fAbility3.RoundsBeforeReuse == 0)
        {
            _pattoBuff = 2;
            _fAbility3.RoundsBeforeReuse = 4;
            _buttonFriendlyAbility.GetComponent<Image>().enabled = false;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (!_movingButtonDisabled)
            {
                _MoveButton.GetComponent<Image>().color = _MoveButton.GetComponent<Button>().colors.disabledColor;
                _MoveButton.GetComponent<Button>().enabled = false;
                _EndTurnButton.GetComponent<Image>().color = _EndTurnButton.GetComponent<Button>().colors.disabledColor;
                _EndTurnButton.GetComponent<Button>().enabled = false;
                _movingButtonDisabled = true;
            }
            MoveOverTime();
        }
        else
        {
            if (_movingButtonDisabled)
            {
                if (CurrentAP > 0)
                {
                    _MoveButton.GetComponent<Image>().color = _MoveButton.GetComponent<Button>().colors.normalColor;
                    _MoveButton.GetComponent<Button>().enabled = true;
                }
                _EndTurnButton.GetComponent<Image>().color = _EndTurnButton.GetComponent<Button>().colors.normalColor;
                _EndTurnButton.GetComponent<Button>().enabled = true;
                _movingButtonDisabled = false;
            }
            if (Input.touchCount == 1)
            {
                if (IsTurn && !isAttacking && !_grid._dialogueBox.activeSelf)
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
                if (_selectedGridCell != null)
                    _selectedGridCell.IsSelected = false;
                if (_selectedEnemyGridCell != null)
                    _selectedEnemyGridCell.IsSelected = false;

                _elements = _grid.GetGridCells();
                foreach (var gridElement in _elements)
                {
                    if (touchedObject == gridElement.GameObject)
                    {
                        if (gridElement.HasObstacle)
                        {
                            _grid.RefreshGridMat();
                            _path?.Clear();
                            return;
                        }

                        _selectedGridCell = gridElement;
                        _selectedGridCell.IsSelected = true;
                        break;
                    }
                }
                if (_selectedGridCell.HasEnemy)
                {
                    if (CurrentPos.DistanceTo(_selectedGridCell.Coord) == 1)
                    {
                        _selectedEnemyGridCell = _selectedGridCell;
                        entity = _selectedEnemyGridCell.Entity;
                    }
                    else
                    {
                        _selectedGridCell.IsSelected = false;
                        entity = null;
                    }
                    CheckEntity();
                    _selectedGridCell = null;
                    _grid.RefreshGridMat();
                    _path?.Clear();
                    return;
                }
                else
                {
                    if (_selectedEnemyGridCell != null)
                    {
                        _selectedEnemyGridCell.IsSelected = false;
                        _selectedEnemyGridCell = null;
                    }
                    entity = null;
                    CheckEntity();
                }

                _path = AStar.FindPath(CurrentPos, _selectedGridCell.Coord);

                if (_path.Count <= 1)
                {
                    if (_selectedGridCell != null)
                        _selectedGridCell.IsSelected = false;
                    _selectedGridCell = _path[0];
                    _path.Clear();
                    _grid.RefreshGridMat();
                    return;
                }

                _grid.RefreshGridMat();
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
            newCamPos.x = (newCamPos.x + _width) / _width;
            newCamPos.y = (newCamPos.y + _height) / _height;
            Camera.main.transform.localPosition = newCamPos;
        }
    }

    void DrawPath()
    {
        int steps = 0;
        foreach (var cell in _path)
        {
            Cell gridElement = _elements[cell.Coord.X + _grid.GetMaxX() / 2, cell.Coord.Y + _grid.GetMaxY() / 2];
            if (steps < CurrentAP)
            {
                gridElement.SetGameObjectMaterial(_grid.GetPathGridMat());
            }
            else if (steps == CurrentAP)
            {
                gridElement.SetGameObjectMaterial(_grid.GetSelectedGridMat());
            }
            else
            {
                gridElement.Direction = 'u';
                gridElement.SetGameObjectMaterial(_grid.GetRedPathGridMat());
            }
            steps++;
        }
        if (steps <= CurrentAP + 1 && CurrentAP > 0)
        {
            _selectedGridCell.SetGameObjectMaterial(_grid.GetSelectedGridMat());
        }
        else
        {
            _selectedGridCell.Direction = 'u';
            _selectedGridCell.SetGameObjectMaterial(_grid.GetRedPathGridMat());
            _path.Clear();
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

    void TurnTowardTarget(Cell cell)
    {
        Vector3 directeur = (cell.GameObject.transform.position - transform.position);
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
            _buttonAbility1.GetComponent<Image>().enabled = true;
            _buttonAbility2.GetComponent<Image>().enabled = true;
            CheckAP(false);
        }
        else
        {
            _buttonAbility1.GetComponent<Image>().enabled = false;
            _buttonAbility2.GetComponent<Image>().enabled = false;
        }
    }

    public void CheckAP(bool hide)
    {
        if (CurrentAP < _ability1.Cost || hide)
        {
            _buttonAbility1.GetComponent<Image>().color = _buttonAbility1.GetComponent<Button>().colors.disabledColor;
            _buttonAbility1.GetComponent<Button>().enabled = false;
        }
        else
        {
            _buttonAbility1.GetComponent<Image>().color = _buttonAbility1.GetComponent<Button>().colors.normalColor;
            _buttonAbility1.GetComponent<Button>().enabled = true;
        }

        if (CurrentAP < _ability2.Cost || hide)
        {
            _buttonAbility2.GetComponent<Image>().color = _buttonAbility2.GetComponent<Button>().colors.disabledColor;
            _buttonAbility2.GetComponent<Button>().enabled = false;
        }
        else
        {
            _buttonAbility2.GetComponent<Image>().color = _buttonAbility2.GetComponent<Button>().colors.normalColor;
            _buttonAbility2.GetComponent<Button>().enabled = true;
        }

        _playerAP.GetComponent<TextMeshProUGUI>().text = "PA :   " + CurrentAP.ToString() + " / " + MaxAP.GetValue().ToString();
    }

    public void Move()
    {
        if (_path == null || _path.Count == 0)
            return;

        CurrentAP -= _path.Count - 1;
        CheckAP(true);
        _grid.RefreshGridMat();
        Move(_path);
        CurrentPos = _selectedGridCell.Coord;
    }

    public void StartOfTurn()
    {
        if ((currentAlly == 1 && _fAbility1.RoundsBeforeReuse == 0) ||
            (currentAlly == 2 && _fAbility2.RoundsBeforeReuse == 0) ||
            (currentAlly == 3 && _fAbility3.RoundsBeforeReuse == 0))
        {
            _buttonFriendlyAbility.GetComponent<Image>().enabled = true;
            _buttonFriendlyAbility.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        _MoveButton.GetComponent<Image>().color = _MoveButton.GetComponent<Button>().colors.normalColor;
        _MoveButton.GetComponent<Button>().enabled = true;
        _EndTurnButton.GetComponent<Image>().color = _EndTurnButton.GetComponent<Button>().colors.normalColor;
        _EndTurnButton.GetComponent<Button>().enabled = true;
        _buttonFriendlyAbility.GetComponent<Image>().color = _buttonFriendlyAbility.GetComponent<Button>().colors.normalColor;
        _allyImage.GetComponent<Image>().color = _buttonFriendlyAbility.GetComponent<Button>().colors.normalColor;
        _buttonFriendlyAbility.GetComponent<Button>().enabled = true;
    }

    public void EndOfTurn()
    {
        if (_selectedGridCell != null)
            _selectedGridCell.IsSelected = false;
        if (_selectedEnemyGridCell != null)
            _selectedEnemyGridCell.IsSelected = false;
        IsTurn = false;
        entity = null;
        _path?.Clear();
        CheckEntity();
        _grid.RefreshGridMat();

        // Update RoundsBeforeReuse for friend abilities
        _fAbility1.RoundsBeforeReuse = Mathf.Clamp(_fAbility1.RoundsBeforeReuse - 1, 0, 10);
        _fAbility2.RoundsBeforeReuse = Mathf.Clamp(_fAbility2.RoundsBeforeReuse - 1, 0, 10);
        _fAbility3.RoundsBeforeReuse = Mathf.Clamp(_fAbility3.RoundsBeforeReuse - 1, 0, 10);

        _MoveButton.GetComponent<Image>().color = _MoveButton.GetComponent<Button>().colors.disabledColor;
        _MoveButton.GetComponent<Button>().enabled = false;
        _EndTurnButton.GetComponent<Image>().color = _EndTurnButton.GetComponent<Button>().colors.disabledColor;
        _EndTurnButton.GetComponent<Button>().enabled = false;
        _buttonFriendlyAbility.GetComponent<Image>().color = _buttonFriendlyAbility.GetComponent<Button>().colors.disabledColor;
        _allyImage.GetComponent<Image>().color = _buttonFriendlyAbility.GetComponent<Button>().colors.disabledColor;
        _buttonFriendlyAbility.GetComponent<Button>().enabled = false;
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