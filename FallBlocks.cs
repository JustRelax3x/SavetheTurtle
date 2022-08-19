
using UnityEngine;


public class FallBlocks : MonoBehaviour
{
    public Vector2 speedMinMax;
    float visible;
    float speed;
    Vector2 screenHalfsize;
    private void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
        visible = -Camera.main.orthographicSize - transform.localScale.y;
        screenHalfsize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }
    void Update()
    {
        
        if (transform.position.y < visible || transform.position.x <= -screenHalfsize.x *1.5f || transform.position.x >= screenHalfsize.x * 1.5f || transform.position.y <= -screenHalfsize.y *1.5f )
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
