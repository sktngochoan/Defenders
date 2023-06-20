using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsControllers : MonoBehaviour
{
    public GameObject hasagiPrefabs;
    public float shootForce = 10f;
    public HasagiSkill hasagiSkill;

    void Update()
    {
        //4. Sử dụng skill Hasagi
        if (Input.GetKey(KeyCode.E))
        {
            ActivateHasagi();
        }
    }

    void ActivateHasagi()
    {
        hasagiSkill.ActivateHasagiSkill();
    }
}
