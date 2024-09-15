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
    public bool vocalEnemy = false; // потом селать поэлегантнее решение, чтобы не булевым значением в префабе, а по типу врага определ€лось издает ли он звуки
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
        // ѕример того, как использовать кастомные атаки/способности
        // ƒл€ каждой уникальной способности, нужно создать сво1 состо€ние и описать там логику
        // ¬ скрипте AiChasePlayer нужен таймер, который будет отслеживать врем€ до спец атаки 
        // ƒалее AiChasePlayer будет обращатьс€ к этому скрипту и в ответ будет получать
        // Id состо€ние, которое нужно дл€ конкретного типа врага и конкретной атаки
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
