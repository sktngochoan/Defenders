using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    private ISkillFactory skillFactory;

    // Inject Factory cụ thể (ví dụ: FireballSkillFactory) thông qua Inspector trong Unity
    public HasagiSkillFactory hasagiSkill;

    // Start is called before the first frame update
    void Start()
    {
        skillFactory = hasagiSkill;
    }

    // Update is called once per frame
    void Update()
    {
        //Dùng skill Hasagi bằng phím E
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateAndUseSkillHasagi();
        }
    }

    private void CreateAndUseSkillHasagi()
    {
        ISkill skill = skillFactory.CreateSkill();
        skill.Use();
    }
}
