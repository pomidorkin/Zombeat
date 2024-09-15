using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyType enemyType;
    private AllEnemiesManager allEnemiesManager;
    public EnemySoundPlayer enemySoundPlayer;
    public EnemySpawner enemySpawner;
    [SerializeField] public float baseAttackDamage = 10;
    [SerializeField] float bossAttackRange;
    public bool noticedThePlayer = false;
    public bool vocalEnemy = false; // ����� ������ ������������ �������, ����� �� ������� ��������� � �������, � �� ���� ����� ������������ ������ �� �� �����
    public bool canTeleport = false;
    public bool canShoot = false;
    [SerializeField] public ParticleSystem shardParticlePrefab;
    [SerializeField] public ParticleSystem explosionParticlePrefab;
    private ParticleSystem newShardEffect;
    private ParticleSystem newExplosionEffect;
    [SerializeField] SkinnedMeshRenderer[] meshes;
    [SerializeField] public EnemyWeapon enemyWeapon;
    [SerializeField] public GameObject attackCollider;
    [SerializeField] public HumanoidBossManager humanoidBossManager;
    public bool isBoss = false;
    public bool isDead = false;

    private void OnEnable()
    {
        allEnemiesManager = FindFirstObjectByType<AllEnemiesManager>();
    }

    private void Start()
    {
        if (humanoidBossManager != null)
        {
            isBoss = true;
        }
        allEnemiesManager.AddMyselfToList(this);
        /*if (enemySpawner)
        {
            enemySpawner.activeEnemies.Add(this);
        }*/
    }

    public void RemoveSelf()
    {
        allEnemiesManager.RemoveMyselfFromList(this);
        if (enemyWeapon != null)
        {
            enemyWeapon.AbortShoot();
            enemyWeapon.DropWeapon();
        }
    }

    public ParticleSystem InstantiateParticleEffect(Vector3 position, Vector3 rotation)
    {
        ParticleSystem newShardEffect = Instantiate(shardParticlePrefab, position, Quaternion.identity);
        newShardEffect.transform.LookAt(rotation);
        newShardEffect.Play();
        this.newShardEffect = newShardEffect;
        StartCoroutine(DestroyParticleSystem(newShardEffect.gameObject));
        return newShardEffect;
    }

    public void InstantiateAppearEffect()
    {
        newExplosionEffect = Instantiate(explosionParticlePrefab, new Vector3(transform.position.x, 0 , transform.position.z), Quaternion.identity); // bug will appear when level will be different in height
        newExplosionEffect.Play();
        StartCoroutine(DestroyParticleSystem(newExplosionEffect.gameObject));
    }

    private IEnumerator DestroyParticleSystem(GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(objectToDestroy);
    }

    public void SetMeshesVisible(bool val)
    {
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = val;
        }
    }

    /*public AiStateId CustomAttack()
    {
        // ������ ����, ��� ������������ ��������� �����/�����������
        // ��� ������ ���������� �����������, ����� ������� ���1 ��������� � ������� ��� ������
        // � ������� AiChasePlayer ����� ������, ������� ����� ����������� ����� �� ���� ����� 
        // ����� AiChasePlayer ����� ���������� � ����� ������� � � ����� ����� ��������
        // Id ���������, ������� ����� ��� ����������� ���� ����� � ���������� �����
        switch (enemyType)
        {
            case EnemyType.Default:
                return AiStateId.Stagger;
                break;
            case EnemyType.NormalZombie:
                return AiStateId.Stagger;
                break;
            case EnemyType.WallCrawler:
                return AiStateId.Stagger;
                break;
            case EnemyType.Boss:
                return AiStateId.Stagger;
                break;
            default:
                return AiStateId.Stagger;
                break;
        }
    }*/
}
