using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int hp;
    public int damage;
    public float walkSpeed;
    public float attackSpeed;
    
    protected virtual void Attack(GameObject _target)
    {
        
    }

    protected virtual void Run()
    {

    }
}
