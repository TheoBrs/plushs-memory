using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatGrid : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] int _maxX;
    [SerializeField] int _maxY;
    public float gridCellScale = 1;

    [Header("Materials")]
    [SerializeField] private Material _defaultGridMat;
    [SerializeField] private Material _notWalkableGridMat;
    [SerializeField] private Material _enemyGridMat;
    [SerializeField] private Material _selectedEnemyGridMat;
    [SerializeField] private Material _selectedGridMat;
    [SerializeField] private Material _pathGridMat;
    [SerializeField] private Material _redPathGridMat;


    [Header("Dialogues")]
    public GameObject _dialogueBox;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private Dialogue _dialogue0;
    [SerializeField] private Dialogue _dialogue1;
    [SerializeField] private Dialogue _dialogue2;
    [SerializeField] private Dialogue _dialogue3;
    [SerializeField] private Dialogue _dialogue4;
    [SerializeField] private Dialogue _dialogue5;
    [SerializeField] private Dialogue _dialogue6;
    [SerializeField] private Dialogue _dialogue7;

    [Header("Misc")]
    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private GameObject _mainCamPivot;
    [SerializeField] private List<GameObject> _combatUI = new List<GameObject>();
    [HideInInspector] public BattleSceneActions battleSceneActions;
    [HideInInspector] public int dialogueIndex;

    private Cell[,] _elements;
    private TurnSystem _turnSystem;
    private Player _player;
    private Vector3 _offset;

    //creation de la grille de Combat
    void Awake()
    {
        // Select chapter somehow
        if (SceneManager.GetActiveScene().name == "Chapter1")
        {
            battleSceneActions.SetupChapter1();
            dialogueIndex = 2;
        }
        if (SceneManager.GetActiveScene().name == "Chapter2")
        {
            battleSceneActions.SetupChapter2();
            dialogueIndex = 7;
        }
        if (SceneManager.GetActiveScene().name == "Chapter3")
        {
            battleSceneActions.SetupChapter3();
            dialogueIndex = 11;
        }

        _turnSystem = GameObject.FindWithTag("TurnSystem").GetComponent<TurnSystem>();
        _dialogueBox.SetActive(true);
        _offset = transform.position;
        _mainCamPivot.transform.position += _offset;

        SetupGrid();

    }

    private void Start()
    {
        RunDialogue();
    }
    void EnableDialogue()
    {
        _dialogueBox.SetActive(true);
        foreach (var go in _combatUI)
        {
            go.transform.localPosition += new Vector3(0, 10000, 0);
        }
    }
    void DisableDialogue()
    {
        _dialogueBox.SetActive(false);
        foreach (var go in _combatUI)
        {
            go.transform.localPosition -= new Vector3(0, 10000, 0);
        }
    }

    public void RunDialogue()
    {
        switch (dialogueIndex)
        {
            case 2:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue0);
                break;
            case 3:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue1);
                break;
            case 4:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue2);
                break;
            case 7:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue3);
                break;
            case 8:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue4);
                break;
            case 11:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue5);
                break;
            case 12:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue6);
                break;
            case 13:
                EnableDialogue();
                _dialogueChannel.RaiseRequestDialogue(_dialogue7);
                break;

            default:
                break;
        }
    }

    public void OnDialogueEnd()
    {
        if (true) // DialogueSequencer.CanStartDialogue()
        {
            //_dialogue0.FirstNode.
            switch (dialogueIndex)
            {
                case 2:
                    DisableDialogue();
                    break;
                case 3:
                    DisableDialogue();
                    break;
                case 4:
                    // Start transition
                    DisableDialogue();
                    _turnSystem.animator.SetTrigger("StartFadeIn");
                    break;
                case 7:
                    DisableDialogue();
                    break;
                case 8:
                    // Start transition
                    DisableDialogue();
                    _turnSystem.animator.SetTrigger("StartFadeIn");
                    break;
                case 11:
                    DisableDialogue();
                    break;
                case 12:
                    // Souris thingy
                    DisableDialogue();
                    break;
                case 13:
                    // Start transition
                    DisableDialogue();
                    _turnSystem.animator.SetTrigger("StartFadeIn");
                    break;

                default:
                    break;
            }
            dialogueIndex++;
        }
    }

    public void SetupGrid()
    {

        _elements = new Cell[_maxX, _maxY];
        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                Coord coords = Coord.ToWorldCoord(x, y, _maxX, _maxY);
                GameObject newCell = Instantiate(_gridPrefab, new Vector3(coords.X * gridCellScale, 0.01f, coords.Y * gridCellScale) + _offset, Quaternion.identity);
                newCell.transform.localScale *= gridCellScale;
                newCell.tag = "GridCell";
                Cell gridElement = new Cell { Coord = coords, GameObject = newCell };
                gridElement.GameObject.transform.parent = gameObject.transform;
                _elements[x, y] = gridElement;
            }
        }

        if (battleSceneActions != null && battleSceneActions.nextBattlePlacement != null)
        {
            AddMoomoo(battleSceneActions.nextBattlePlacement.moomooCell.Item1, battleSceneActions.nextBattlePlacement.moomooCell.Item2);

            foreach (var tuple in battleSceneActions.nextBattlePlacement.enemyCellList)
            {
                AddEnemy(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
            }

            foreach (var tuple in battleSceneActions.nextBattlePlacement.obstacleCellList)
            {
                AddObstacle(tuple.Item1, tuple.Item2);
            }
        }
    }

    public void DestroyGrid()
    {
        foreach (Cell cell in _elements)
        {
            Destroy(cell.GameObject);
        }
        _elements = null;
    }
    public void RefreshGridMat()
    {
        foreach (var cell in _elements)
        {
            if (cell.HasObstacle)
                cell.SetGameObjectMaterial(GetNotWalkableGridMat());
            else if (cell.HasEnemy)
                if (cell.IsSelected)
                    cell.SetGameObjectMaterial(GetSelectedEnemyGridMat());
                else
                    cell.SetGameObjectMaterial(GetEnemyGridMat());
            else
                cell.SetGameObjectMaterial(GetDefaultGridMat());
        }
    }
    
    public bool AddObstacle(Coord coord, GameObject obstacle)
    {
        int x = coord.X;
        int y = coord.Y;
        if (_elements[x, y].HasObstacle)
            return false;

        _elements[x, y].HasObstacle = true;
        _elements[x, y].GameObject = obstacle;
        _elements[x, y].SetGameObjectMaterial(_notWalkableGridMat);
        return true;
    }

    public void AddMoomoo(Coord coord, GameObject moomooPrefabs) 
    {
        Vector3 rotation = new Vector3(0, 0, 0);
        Player moomoo = Instantiate(moomooPrefabs, new Vector3(coord.X * gridCellScale, 0.01f, coord.Y * gridCellScale) + _offset, Quaternion.Euler(rotation)).GetComponent<Player>();
        moomoo.CurrentPos = coord;
        moomoo.speed = moomooPrefabs.GetComponent<Player>().speed;
        _player = moomoo;
        _turnSystem.AddMoomoo(_player);
    }

    public bool AddEnemy(Coord coord, GameObject enemyPrefabs, Vector3 rotation, Coord size, bool causeEndOfBattle = false, bool canPlayAfterSpawn = true)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                int x = Coord.ToListCoord(coord, _maxX, _maxY).X + i;
                int y = Coord.ToListCoord(coord, _maxX, _maxY).Y + j;

                if (_elements[x, y].HasObstacle || _elements[x, y].HasEnemy || _player.CurrentPos.Equals(coord))
                    return false;
            }
        }

        GameObject enemy = Instantiate(enemyPrefabs, new Vector3(coord.X * gridCellScale + (size.X - 1) * gridCellScale / 2
            , 0.01f, coord.Y * gridCellScale + (size.Y - 1) * gridCellScale / 2) + _offset, Quaternion.Euler(rotation));

        enemy.transform.localScale *= Mathf.Sqrt(size.X * size.Y);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.CurrentPos = coord;
        enemyScript.justSpawned = !canPlayAfterSpawn;
        enemyScript.speed = enemyPrefabs.GetComponent<Enemy>().speed;
        enemyScript.Size = size;
        enemyScript.causeEndOfBattle = causeEndOfBattle;
        _turnSystem.AddEnemy(enemyScript);

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                int x = Coord.ToListCoord(coord, _maxX, _maxY).X + i;
                int y = Coord.ToListCoord(coord, _maxX, _maxY).Y + j;

                _elements[x, y].HasEnemy = true;
                _elements[x, y].Entity = enemyScript;
                _elements[x, y].SetGameObjectMaterial(_enemyGridMat);
                enemyScript.occupiedCells.Add(_elements[x, y]);
            }
        }
        return true;
    }

    public Cell GetGridCell(int x, int y)
    {
        Coord coord = Coord.ToListCoord(x, y, _maxX, _maxY);
        return _elements[coord.X, coord.Y];
    }

    public Cell GetGridCell(Coord coord)
    {
        coord = Coord.ToListCoord(coord.X, coord.Y, _maxX, _maxY);
        return _elements[coord.X, coord.Y];
    }

    public Cell[,] GetGridCells() => _elements;

    public Material GetDefaultGridMat() => _defaultGridMat;

    public Material GetNotWalkableGridMat() => _notWalkableGridMat;

    public Material GetEnemyGridMat() => _enemyGridMat;

    public Material GetSelectedEnemyGridMat() => _selectedEnemyGridMat;

    public Material GetSelectedGridMat() => _selectedGridMat;

    public Material GetPathGridMat() => _pathGridMat;

    public Material GetRedPathGridMat() => _redPathGridMat;

    public int GetMaxX() => _maxX;

    public int GetMaxY() => _maxY;
}

