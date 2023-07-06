using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSystem : MonoBehaviour
{
    public void UpdateHasagi()
    {
        Debug.Log(1);
        GameManager.Instance.DeActiveUpdateSystem();
    }

    public void UpdateHeal()
    {
        Debug.Log(2);
        GameManager.Instance.DeActiveUpdateSystem();
    }

    public void UpdateFrezz()
    {
        Debug.Log(3);
        GameManager.Instance.DeActiveUpdateSystem();
    }
}
