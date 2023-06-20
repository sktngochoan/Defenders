using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField]
    private int lvl;
    public int Level
    {
        get { return lvl; }
        set { lvl = value; }
    }
    [SerializeField]
    private float hp;
    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }
    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField]
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField]
    private float exp;
    public float Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    [SerializeField]
    private float attackRange;
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public void UpdateLv()
    {
        HP = HP + 10;
        Damage = Damage + lvl;
        exp = exp + lvl * 5;
        lvl = lvl + 1;
    }
}
