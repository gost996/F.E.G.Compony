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
    public float animSpeed = 1f;

    [Header("InitSpec")]
    public Vector3 initPos;
    public int initHp;
    public int initDamage;
    public float initWalkSpeed;
    public float initAttackSpeed;
    public float initAnimSpeed;

    public Animator anim;
    public List<CharacterInfo> targetList = new List<CharacterInfo>();
    public CharacterInfo currentTarget = null;
    
    private void Start()
    {
        anim.SetFloat("AttackSpd", attackSpeed);
        anim.SetFloat("AnimSpd", animSpeed);
      // StartCoroutine(Run());
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
            AnimationIdle();
        }
        else
        {
            currentTarget = targetList[0];
            Attack();
        }
    }

    public void CalculateDamage()
    {
        if (currentTarget == null) return;

        currentTarget.hp -= damage;

        if(currentTarget.hp <= 0)
        {
            currentTarget.anim.SetBool("IsDead", true);
            currentTarget.StopAllCoroutines();

            currentTarget = null;
            targetList.RemoveAt(0);

            FindTarget();
        }
    }

    public void Attack()
    {
        SetAnimParameter("IsAttack");
        anim.SetFloat("AttackSpd", attackSpeed);
    }

    public virtual IEnumerator Run()
    {
        yield return null;
    }
    
    public virtual void SetInitInfo()
    {
        initPos = transform.position;
        initHp = hp;
        initDamage = damage;
        initWalkSpeed = walkSpeed;
        initAttackSpeed = attackSpeed;
        initAnimSpeed = animSpeed;
    }

    public void AnimationIdle()
    {
        if(targetList.Count == 0)
        {
            SetAnimParameter("IsIdle");
        }
        else
        {
            SetAnimParameter("IsCombatIdle");
        }
    }

    public void SetAnimParameter(string prmtName)
    {
        foreach(AnimatorControllerParameter prmt in anim.parameters)
        {
            if (prmt.name.Contains("Is")) anim.SetBool(prmt.name, false);
        }
        anim.SetBool(prmtName, true);
    }

}
