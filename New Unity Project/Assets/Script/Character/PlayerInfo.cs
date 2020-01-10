using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster") SetTarget(collision.gameObject.GetComponent<CharacterInfo>());
    }
    
    public override void Run()
    {
        base.Run();
        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
    }
}
