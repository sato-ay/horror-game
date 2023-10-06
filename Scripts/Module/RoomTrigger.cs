using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : BaseBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            MainUI.GetInstance().RoomEvent();
            Destroy(this.gameObject);
        }
    }
}
