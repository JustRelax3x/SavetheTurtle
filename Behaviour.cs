using UnityEngine;

public class Behaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {

        if (triggerCollider.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
        else if(triggerCollider.CompareTag("PickUper"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.localScale = new Vector3(5f, 5f, gameObject.transform.localScale.z);
           
        }
    }
}
