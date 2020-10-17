using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMovement>> units = new Dictionary<string, List<TacticsMovement>>();
    //first element o fthe queue determines what team's turn it is
    static Queue<string> turnKey = new Queue<string>();
    //team whose turn it currently is
    static Queue<TacticsMovement> turnTeam = new Queue<TacticsMovement>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(turnTeam.Count);
        //basically only used at the start, might replace with sub-pub pattern
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
        
    }
    static void InitTeamTurnQueue(){
        Debug.Log("initTeamTurnQueue");
        List<TacticsMovement> teamList = units[turnKey.Peek()];
        foreach (TacticsMovement unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartTurn();
    }

    public static void StartTurn(){
        Debug.Log("startTurn");
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }
    public static void EndTurn(){
        Debug.Log("end turn");
        TacticsMovement unit = turnTeam.Dequeue();
        unit.EndTurn();
        // if unit that just ended turn was not the last unit of the team, start turn for next unit, else start turn for new team
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }
    //method for units to add themselves to the turnmanager, could use action pub sub instead
    public static void AddUnit(TacticsMovement unit){
        Debug.Log("We adding?");
        List<TacticsMovement> list;
        //if no team with this tag exists
        if (!units.ContainsKey(unit.tag))
        {
            //make a new entry into the units dictionary with the tag being the key and and empty list
            list = new List<TacticsMovement>();
            units[unit.tag] = list;
            //if this team hasn't been added to the order in which teams get to play: 
            if (!turnKey.Contains(unit.tag))
            {
                //add the tag to that queue
                turnKey.Enqueue(unit.tag);
            }
        }
        //if the team already exists
        else
        {
            //get the list of units for that team
            list = units[unit.tag];
        }
        //add the unit that wants to add itself to the list of the team
        list.Add(unit);
    }
    //deal with units getting killed and teams disappearing
}
