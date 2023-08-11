using UnityEngine;

public class AliveAnchor : MonoBehaviour
{
    public Room room;

    private void OnDestroy()
    {
        if(GetComponent<Health>().hp <= 0)
            room.OnUnitDeath();
    }
}
