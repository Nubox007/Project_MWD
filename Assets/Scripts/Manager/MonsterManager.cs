using MySql.Data.MySqlClient;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct MonsterData
{
    public string name;
    public int hp;
    public int round;
    public float moveSpeed;
    public int armor;
}

public class MonsterManager : MonoBehaviour
{
    public MonsterData[] monsterDataArray;
    private string connectionString = "Data Source=projectmwd.pro; Initial Catalog=mywaydefence;User ID=root;Password=Gnrhkdtkfkd!2";
    [SerializeField] protected float hp = 10f;
    protected float maxhp = 10f;
    [SerializeField] protected float armor = 0;
    protected bool revive = true; // ��Ÿ ����
    private bool marvelSpeedCooldown = false; // ��������
    private bool isInteruptted = false; // ����
    private float chance;
    MonsterData monster = new MonsterData();

    private MonsterMovement monsterMovement;


    float time = 0f;
    public string monsterName = "Orc";
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Transform cam;


    protected virtual void Awake()
    {
        hpBar = GetComponentInChildren<Slider>();
        monsterMovement = GetComponent<MonsterMovement>();
        waveManager = GetComponentInParent<WaveManager>();
        cam = Camera.main.transform;
        chance = Random.Range(0f, 1f);
        LoadMonsterData();
    }

    private void OnEnable()
    {
        InputMonsterData(monsterName, waveManager.round);
        if (monsterDataArray == null || monsterDataArray.Length == 0)
        {
            return;
        }
    }
    private void Update()
    {
        hpBar.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        hpBar.value = Mathf.Lerp(hpBar.value, hp, Time.deltaTime * 5f);
    }

    public void LoadMonsterData()
    {
        List<MonsterData> monsterDataList = new List<MonsterData>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT MonsterName, Round, Health, Movespeed, Armor FROM monster_data";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monster.name = reader.GetString(0);
                        monster.round = reader.GetInt32(1);
                        monster.hp = reader.GetInt32(2);
                        monster.moveSpeed = reader.GetFloat(3);
                        monster.armor = reader.GetInt32(4);
                        monsterDataList.Add(monster);
                    }
                }
            }
        }
        monsterDataArray = monsterDataList.ToArray();
        OnMonsterDataLoaded();
    }

    // �����͸� �ε��� �� ȣ��Ǵ� �޼���
    protected virtual void OnMonsterDataLoaded()
    {
        if (gameObject.activeInHierarchy)
        {
            InputMonsterData(monsterName, waveManager.round);
        }
    }

    public void DamageByTower(float _attack)
    {
        if (gameObject.name.Contains("C_Clone"))
        {
            if (chance <= 0.1f)
            {
                Debug.Log("Damage ignored!");
                chance = Random.Range(0f, 1f);
                return;
            }
        }

        if (gameObject.name.Contains("Hertia"))
        {
            if (armor > 0)
            {
                float damageToArmor = Mathf.Min(armor, _attack);
                armor -= damageToArmor;
                _attack -= damageToArmor;
            }
            hp -= _attack;
        }
        else
        {
            hp -= _attack;
        }

        if (gameObject.name.Contains("Marvel") && !marvelSpeedCooldown)
        {
            StartCoroutine(TemporarilyIncreaseSpeed());
        }

        if (hp <= 0)
        {
            if (!gameObject.name.Contains("Delta"))
            {
                hp = 0;
                gameObject.SetActive(false);
                //Debug.Log("Monster name: " + gameObject.name + " is Dead");
            }

            if (gameObject.name.Contains("Delta") && revive)
            {
                // ��Ȱ ���� �߰�
                ReviveAsDelta();
                revive = false;
            }
            else if (gameObject.name.Contains("Delta") && !revive)
            {
                hp = 0;
                gameObject.SetActive(false);
            }

        }


    }

    public void InputMonsterData(string _monsterName, int _round)
    {
        if (monsterDataArray == null)
        {
            Debug.Log("NULL");
            return;
        }
        foreach (var monster in monsterDataArray)
        {
            if (_monsterName == monster.name && _round == monster.round)
            {
                hp = monster.hp;
                maxhp = hp;
                armor = monster.armor;

                hpBar.maxValue = maxhp;
                hpBar.value = maxhp;
                Debug.Log("Monster Name: " + monster.name + ", HP: " + monster.hp + ", Armor: " + monster.armor);
            }
        }
    }

    public void ReviveAsDelta()
    {
        gameObject.SetActive(true);
        // ü���� 30%�� ��Ȱ
        hp = maxhp * 0.3f;
        // �������� 50% �۰� ����
        transform.localScale = transform.localScale * 0.5f;
        Debug.Log("Delta monster revived with 30% HP and 50% scale.");
    }
    public void Interrupt()
    {
        isInteruptted = true;
    }
    //���� ����
    private IEnumerator TemporarilyIncreaseSpeed()
    {
        marvelSpeedCooldown = true;
        float originalSpeed = monsterMovement.mvSpeed;
        monsterMovement.mvSpeed = monsterMovement.mvSpeed * 2;
        yield return new WaitForSeconds(2f);
        monsterMovement.mvSpeed = originalSpeed;
        yield return new WaitForSeconds(8f);
        marvelSpeedCooldown = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Fire"))
        {
            Debug.Log("Hitted");
        }
    }

    public void ReduceHP(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            // ���Ͱ� �׾��� ���� �߰� ������ ���⿡ �߰��� �� �ֽ��ϴ�.
        }
    }
}



