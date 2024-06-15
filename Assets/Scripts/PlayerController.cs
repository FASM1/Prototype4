using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed =5.0f;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

    }

    // Update is called once per frame
    public GameObject powerupIndicator;
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    public bool haspowerup;
    private void OnTriggerEnter(Collider other){
     if (other.CompareTag("Powerup")) {
            haspowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
     
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        haspowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private float powerupStrength = 15.0f;
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && haspowerup)
            {
                 Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                 Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

                Debug.Log("Collided with" + collision.gameObject.name + "with powerup set to" + haspowerup);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
        }
}
