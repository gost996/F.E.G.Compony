using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CharacterInfo player;
    public GameObject monsterList;

    CharacterInfo[] monsters;
    List<CharacterInfo> characters = new List<CharacterInfo>();

    int spdCount = 1;
    bool _pause = false;

    public Sprite[] playImages = new Sprite[2];
    public Button playBtn;

    public Button doubleSpdBtn;

    private static GameManager _instance = null;
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (_instance == null)
                {
                    Debug.Log("There's no active ManagerClass object");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        { 
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        monsters = new CharacterInfo[monsterList.transform.childCount];
        for (int i = 0; i < monsterList.transform.childCount; i++)
        {
            monsters[i] = monsterList.transform.GetChild(i).GetComponent<CharacterInfo>();
        }

        SetInitCharacters();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoubleSpeed()
    {
        if (spdCount == 1) spdCount = 2;
        else if (spdCount == 2) spdCount = 4;
        else spdCount = 1;

        SpeedManage(spdCount);      
    }

    public void PlayScene()
    {
        SpeedManage(spdCount);
        if (playBtn.GetComponent<Image>().sprite.Equals(playImages[0]))
        {
            if (_pause) SpeedManage(spdCount);
            else
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    StartCoroutine(characters[i].Run());
                }
            }
            _pause = false;
            playBtn.GetComponent<Image>().sprite = playImages[1];
        }
        else
        {
            SpeedManage(0);
            _pause = true;
            playBtn.GetComponent<Image>().sprite = playImages[0];
        }
    }
    
    public void Reset()
    {
        StopAllCoroutines();

        spdCount = 1;
        SpeedManage(spdCount);
        playBtn.GetComponent<Image>().sprite = playImages[0];

        foreach(CharacterInfo character in characters)
        {
            character.targetList.Clear();

            character.AnimationIdle();

            character.transform.position = character.initPos;
            character.hp = character.initHp;
            character.damage = character.initDamage;
            character.walkSpeed = character.initWalkSpeed;
            character.attackSpeed = character.initAttackSpeed;
            character.animSpeed = character.initAnimSpeed;
        }
        _pause = false;
    }

    public void SetInitCharacters()
    {
        player.SetInitInfo();
        characters.Add(player);
        foreach (CharacterInfo monster in monsters)
        {
            monster.SetInitInfo();
            characters.Add(monster);
        }
    }
    
    public void SpeedManage(int rate)
    {
        foreach (CharacterInfo character in characters)
        {
            character.walkSpeed = character.initWalkSpeed * rate;
            character.attackSpeed = character.initAttackSpeed * rate;
            character.animSpeed = character.initAnimSpeed * rate;

            character.anim.SetFloat("AttackSpd", character.attackSpeed);
            character.anim.SetFloat("AnimSpd", character.animSpeed);
        }
        doubleSpdBtn.transform.GetChild(0).GetComponent<Text>().text = "x" + System.Convert.ToString(spdCount);
    }
}
