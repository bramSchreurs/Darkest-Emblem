using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHomeBrew : MonoBehaviour
{
    // Start is called before the first frame update
    bool turnEnded = false;
    int maxNumberOfTiles = 5;
    int currentTilesMovedThisTurn = 0;
    float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask obstacles;

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTilesMovedThisTurn < maxNumberOfTiles)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.position) == 0f)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, obstacles))
                    {
                        Vector3 startingPosition = movePoint.position;
                        //Debug.Log("no overlap");
                        Debug.Log(movePoint.position);
                        Debug.Log(transform.position);
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        Debug.Log(movePoint.position);
                        Debug.Log(transform.position);
                        if (Vector3.Distance(movePoint.position, transform.position) == 1)
                        {
                            currentTilesMovedThisTurn += 1;
                            Debug.Log(currentTilesMovedThisTurn);
                            if (currentTilesMovedThisTurn == maxNumberOfTiles)
                            {
                                turnEnded = true;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("overlap");
                    }


                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, obstacles))
                    {
                        Vector3 startingPosition = movePoint.position;
                        //Debug.Log("no overlap");
                        Debug.Log(movePoint.position);
                        Debug.Log(transform.position);
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        Debug.Log(movePoint.position);
                        Debug.Log(transform.position);
                        if (Vector3.Distance(movePoint.position, transform.position) == 1)
                        {
                            currentTilesMovedThisTurn += 1;
                            Debug.Log(currentTilesMovedThisTurn);
                            if (currentTilesMovedThisTurn == maxNumberOfTiles)
                            {
                                turnEnded = true;
                            }
                        }

                    }
                    else
                    {
                        Debug.Log("overlap");
                    }

                }
            }
        }



    }
    void OnGUI()
    {
        if (turnEnded)
        {
            GUI.Window(50, new Rect(movePoint.position.x, movePoint.position.y, 100f, 100f), QuitWindowFunction, "Turn ended");
        }
    }
    void QuitWindowFunction(int id)
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Ok"))
        {
            turnEnded = false;
            currentTilesMovedThisTurn = 0;
        }


    }
}
