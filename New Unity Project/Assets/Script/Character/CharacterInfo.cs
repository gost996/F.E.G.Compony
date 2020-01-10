using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int hp;
    public int damage;
    public float walkSpeed;
    public float attackSpeed;

    Transform initTransform;
    int initHp;
    int initDamage;
    float initWalkSpeed;
    float initAttackSpeed;

    Animator anim;

    protected List<CharacterInfo> targetList = new List<CharacterInfo>();
    CharacterInfo currentTarget = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Run();
    }
    
    public void SetTarget(CharacterInfo _target)
    {
        targetList.Add(_target);

        if (currentTarget == null)
        {
            currentTarget = targetList[0];
            StartCoroutine(Attack());
        }
    }

    void FindTarget()
    {
        if (targetList.Count == 0) return;
        else
        {
            currentTarget = targetList[0];
            Attack();
        }
    }

    public IEnumerator Attack()
    {
        anim.SetTrigger("Attack");

        while(currentTarget.hp > 0)
        {
            currentTarget.hp -= damage;
            yield return new WaitForSeconds(attackSpeed);
        }

        currentTarget.anim.SetTrigger("Death");
        StopCoroutine(currentTarget.Attack());

        currentTarget = null;
        targetList.RemoveAt(0);
        Debug.Log(targetList[0]);

        FindTarget();
    }

    public virtual void Run()
    {
        anim.SetBool("Run", true);
        anim.SetBool("Idle", false);
        anim.SetBool("CombatIdle", false);
    }

    public virtual void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
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
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
            anim.SetBool("CombatIdle", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            anim.SetBool("CombatIdle", true);
        }
    }
}
