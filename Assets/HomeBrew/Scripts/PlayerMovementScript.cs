using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : TacticsMovement
{
    bool canClick = true;
    GameObject characterObject;
    Vector3 characterPosition;

    Vector3[] squaresInRange;
    List<GameObject> localMapTileList = new List<GameObject>();
    // stuff for youtube related stuff: https://www.youtube.com/watch?v=2NVEqBeXdBk&t=22s








    MapTile parent = null;


    // publisher suscriber pattern
    public event System.Action OnPlayerClicked;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        EventBroker.MapInstantiated += LoadMapFromFullMap;
        walkingRange = gameObject.GetComponent<Fox>().speed;


    }
    private void OnDisable()
    {
        EventBroker.MapInstantiated -= LoadMapFromFullMap;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            canClick = true;
        }
        if (!isMyTurn)
        {
            //Debug.Log("not my turn:");
            //Debug.Log(this.transform.position);

            return;
        }
        //FindSelectableTiles();
        if (!moving)
        {
            CheckMouse();
        }
        else
        {
            Move();
        }
    }
    void OnMouseDown()
    {
        if (canClick && isMyTurn)
        {
            FindSelectableTiles();
            EventBroker.CallCharacterSelected(this.gameObject);
            canClick = false;
        }
    }


    void LoadMapFromFullMap()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // youtube video stuff





    void CheckMouse()
    {
        if (canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Tilebois"));
                if (hit)
                {
                    if (hit.collider.tag == "Tile")
                    {
                        MapTile mapTile = hit.collider.GetComponent<MapTile>();
                        if (mapTile.selectable)
                        {
                            MoveToTile(mapTile);
                        }
                    }
                }
            //Debug.Log("clicked");
            canClick = false;
            //Debug.Log("canClick now false");
            }
        }
        
    }







}
