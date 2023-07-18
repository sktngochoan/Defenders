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
    private float currentHp;
    public float CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
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
    private float currentExp;
    public float CurrentExp
    {
        get { return currentExp; }
        set { currentExp = value; }
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
        UpdateEntity();
        UpdateSkill();
    }

    public void UpdateEntity()
    {
        HP = HP + 10;
        CurrentHp = HP;
        Damage = Damage + 1;
        exp = exp + lvl * 5;
        CurrentExp = 0;
        lvl = lvl + 1;
        UpdateStatusBar();
    }

    public void UpdateSkill()
    {
        GameManager.Instance.ActiveUpdateSystem();
    }

    public void UpdateEntityWithLv(int lv, float HpSave, float ExpSave)
    {
        HP = HP + 10 * lv;
        Damage = Damage + 1 * lv;
        exp = exp + lv * 5;
        lvl = lv;
        currentExp = ExpSave;
        currentHp = HpSave;
    }

    public void UpdateStatusBar()
    {
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        playerController.changeHp();
        playerController.changeExp();
    }
}