using UnityEngine;

public class Targets : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float minTorque = -10;
    private float maxTorque = 10;
    private float xRangeSpawn = 4;
    private float yPosSpawn = -6;
    public int pointValue;

    public GameManager gameManager;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPositionSpawn();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(minTorque, maxTorque);
    }
    Vector3 RandomPositionSpawn()
    {
        return new Vector3(Random.Range(-xRangeSpawn, xRangeSpawn), yPosSpawn);
    }
    // Update is called once per frame
    void Update()
    {

    }
    /*  private void OnMouseDown()
      {
          if(gameManager.isGameActive)
          {
              Destroy(gameObject);
              Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
              gameManager.UpdateScore(pointValue);
          }
      }*/
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.LiveUpdate(-1);
        }
    }
    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}
