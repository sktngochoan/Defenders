using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSystem : MonoBehaviour
{
    public HasagiSkill hasagiSkill;
    public FreezeSkill freezeSkill;
    public HealSkill healSkill;
    public void UpdateHasagi()
    {
        hasagiSkill.UpdateHasagiSkill();
        GameManager.Instance.DeActiveUpdateSystem();
    }

    public void UpdateHeal()
    {
        healSkill.UpdateHealSkill();

        GameManager.Instance.DeActiveUpdateSystem();
    }

    public void UpdateFrezz()
    {
        freezeSkill.UpdateFreezeSkill();
        GameManager.Instance.DeActiveUpdateSystem();
    }
}
