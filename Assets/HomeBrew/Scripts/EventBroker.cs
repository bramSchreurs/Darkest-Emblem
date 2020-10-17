using System;
using UnityEngine;
public class EventBroker
{
    public static event Action<GameObject> CharacterSelected;
    public static event Action MapInstantiated;

    public static void CallCharacterSelected(GameObject character){
        if(CharacterSelected != null)
            {
                CharacterSelected(character);
            }
    }

    public static void CallMapInstantiated(){
        if(MapInstantiated != null)
            {
                MapInstantiated();
            }
    }


}
