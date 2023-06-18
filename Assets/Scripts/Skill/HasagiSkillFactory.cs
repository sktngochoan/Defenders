using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasagiSkillFactory : SkillFactory
{
    public override ISkill CreateSkill()
    {
        return new HasagiSkill();
    }
}
