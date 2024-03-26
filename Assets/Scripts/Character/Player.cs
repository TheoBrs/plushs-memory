using System.Drawing;
using UnityEngine;

public class Player : Entity
{
    private Ability _fAbility1;
    private Ability _fAbility2;
    private Ability _fAbility3;
    private int _pattoBuff = 0;

    public int gridWidth;
    public int gridHeight;
    private Material gridMat;
    private Material selectedGridMat;

    private GameObject player;
    private GridElement selectedGridCell;
    private Vector3 position;
    private float width;
    private float height;

    public void Start()
    {
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;

        _currentHP = GetMaxHP().GetValue();
        _currentAP = GetMaxAP().GetValue();
        AbilitiesInitialization();
    }

    protected override void AbilitiesInitialization()
    {
        _ability1 = new Ability
        {
            Damage = 2,
            Cost = 1
        };

        _ability2 = new Ability
        {
            Damage = 4,
            Cost = 2
        };
        
        _fAbility1 = new Ability
        {
            RoundsBeforeReuse = 2
        };

        _fAbility2 = new Ability
        {
            RoundsBeforeReuse = 3
        };

        _fAbility3 = new Ability
        {
            RoundsBeforeReuse = 4
        };
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
                                selectedGridCell.SetGameObjectMaterial(gridMat);

                            //touchedObject.transform.p SetGameObjectMaterial(selectedGridMat);
                            //selectedGridCell = touchedObject;
                            Move(selectedGridCell.GetCoord());
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

    public override void Death()
    {
        // GameOver
    }
}