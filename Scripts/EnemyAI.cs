using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f; // takip aralığı
    [SerializeField] float turnSpeed = 5f; // oyuncuya doğru dönüş hızı

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity; // hedefe olan mesafe
    bool isProvoked = false; // düşman kışkırtıldı mı?
    EnemyHealth enemyHealth;
    Transform target; // hedef yani oyuncu

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform; // Oyuncu sağlığına sahip olacak olan sadece oyuncudur.
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead()) // eğer düşman öldüyse
        {
            enabled = false; // EnemyAI bileşenini devre dışı bırak
            navMeshAgent.enabled = false; // Nav Mesh Agent bileşenini devre dışı bırak
            return;
        }

        // hedef yani oyuncuya düşman ne kadar uzakta, oyuncu ile düşman arasındaki mesafe
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked) // düşmana ateş edildiyse
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange) // oyuncu düşmanın takip aralığında mı?
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        // düşman hasar aldığında kışkırtılır böylece oyuncuya saldırmaya gider
        isProvoked = true;
        // düşmanın hasar aldığını sağlığının azalmasından anlayabilirim (EnemyHealth)
    }

    private void EngageTarget()
    {
        FaceTarget(); // düşman oyuncuya baksın

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            // düşman stoppingDistance kadar mesafe kalana kadar yaklaşır ve durur
            ChaseTarget(); // Hedefi Takip Et
        }
        else

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            // eğer oyuncu düşmanın saldırı menziline girdiyse
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        // saldırı animasyonundan çık
        GetComponent<Animator>().SetBool("attack", false);
        // hareket animasyonuna geçiş yapsın
        GetComponent<Animator>().SetTrigger("move");
        // düşman oyuncuya doğru hareket edecek
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        // saldırı animasyonuna geç
        GetComponent<Animator>().SetBool("attack", true);
    }

    // düşman hedefe doğru belirli bir hızda dönecek
    private void FaceTarget()
    {
        // Normalleştirilmiş vektörün büyüklüğü 1'dir ve geçerli vektörle aynı yöndedir.
        Vector3 direction = (target.position - transform.position).normalized; // oyuncuya doğru
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // Slerp: iki vektör arasında güzel ve yumuşak bir şekilde dönmemizi sağlar.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
