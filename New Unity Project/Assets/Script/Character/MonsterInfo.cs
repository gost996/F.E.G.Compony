using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : CharacterInfo
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") SetTarget(collision.gameObject.GetComponent<CharacterInfo>());
    }

    public override void Run()
    {
        base.Run();
        transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
    }
}
