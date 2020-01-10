using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [Header("Spec")]
    public int hp;
    public int damage;
    public float walkSpeed;
    public float attackSpeed;

    [Header("InitSpec")]
    Transform initTransform;
    int initHp;
    int initDamage;
    float initWalkSpeed;
    float initAttackSpeed;
    
    public Animator anim;
    public List<CharacterInfo> targetList = new List<CharacterInfo>();
    CharacterInfo currentTarget = null;
    
    private void Update()
    {
        if(targetList.Count == 0) Run();
    }

    public void SetTarget(CharacterInfo _target)
    {
        targetList.Add(_target);

        if (currentTarget == null)
        {
            currentTarget = targetList[0];
            Attack();
        }
    }

    void FindTarget()
    {
        if (targetList.Count == 0)
        {
            return;
        }
        else
        {
            currentTarget = targetList[0];
            Attack();
        }
    }

    public void CalculateDamage()
    {
        currentTarget.hp -= damage;

        if(currentTarget.hp <= 0)
        {
            currentTarget.anim.SetTrigger("Death");
            currentTarget.StopAllCoroutines();

            currentTarget = null;
            targetList.RemoveAt(0);

            FindTarget();
        }
    }

    public void Attack()
    {
        SetAnimParameter("Attack");
        anim.SetFloat("AttackSpd", attackSpeed);
    }

    public virtual void Run()
    {
        SetAnimParameter("Run");
    }
    
    public virtual void SetInitInfo()
    {
        initTransform = transform;
        initHp = hp;
        initDamage = damage;
        initWalkSpeed = walkSpeed;
        initAttackSpeed = attackSpeed;
    }
    
    void AnimationIdle()
    {
        if(targetList.Count == 0)
        {
            SetAnimParameter("Idle");
        }
        else
        {
            SetAnimParameter("CombatIdle");
        }
    }

    void SetAnimParameter(string prmtName)
    {
        foreach(AnimatorControllerParameter prmt in anim.parameters)
        {
            if (prmt.name != "Death") anim.SetBool(prmt.name, false);
        }
        anim.SetBool(prmtName, true);
    }
}
