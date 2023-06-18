using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillFactory : ISkillFactory
{
    public abstract ISkill CreateSkill();

}
