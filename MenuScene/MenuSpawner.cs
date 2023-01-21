using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public GameObject[] Garbage;
    GameObject Block;
    FallBlocks ob;
    Vector2 screenHalfsize;
    float NextSpawnTime = 1f;

    void Update()
    {
        if (Time.timeSinceLevelLoad > NextSpawnTime)
        {
            NextSpawnTime = Time.timeSinceLevelLoad + 5f;
            screenHalfsize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
            Vector2 SpawnPos = new Vector2(Random.Range(-screenHalfsize.x, screenHalfsize.x), screenHalfsize.y *1.25f);
            Block = (GameObject) Instantiate(Garbage[Random.Range(0, 21)], SpawnPos, Quaternion.Euler(Vector3.one * Random.Range(-12f,12f)));
            Block.AddComponent<FallBlocks>();
            Block.transform.position =  new Vector3(Block.transform.position.x, Block.transform.position.y, 15f);
            
            ob = Block.GetComponent<FallBlocks>();
            ob.speedMinMax.x = 0.4f;
            ob.speedMinMax.y = 0.5f;
            
            if (Block.CompareTag("Botl"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.46f);
                Block.transform.localScale = new Vector3(0.075630f, 0.075630f, 0.075630f);
            }
            else if (Block.CompareTag("Can"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.55f);
                Block.transform.localScale = new Vector3(0.287524f, 0.287524f, 0.287524f);
            }
            else if (Block.CompareTag("Nail"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.455f);
                Block.transform.localScale = new Vector3(0.176843f, 0.176843f, 0.176843f);
            }
            else if (Block.CompareTag("Jar"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.43f);
                Block.transform.localScale = new Vector3(0.078139f, 0.078139f, 0.078139f);
            }
            else if (Block.CompareTag("Mask"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.52f);
                Block.transform.localScale = new Vector3(0.078512f, 0.082426f, 0.086841f);
            }
            else if (Block.CompareTag("Paket"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.59f);
                Block.transform.localScale = new Vector3(0.125519f, 0.134520f, 0.144733f);
            }
            else if(Block.CompareTag("Bung"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.53f);
                Block.transform.localScale = new Vector3(0.016853f, 0.016853f, 0.016853f);
            }
            else if(Block.CompareTag("Bo4ka"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.57f);
                Block.transform.localScale = new Vector3(0.073646f, 0.073646f, 0.064500f);
            }
            else if(Block.CompareTag("Pila"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.494f);
                Block.transform.localScale = new Vector3(0.129930f, 0.129930f, 0.129930f);
            }
            else if(Block.CompareTag("Kanistra"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
                Block.transform.localScale = new Vector3(0.111652f, 0.111652f, 0.111652f);
            }
            else if(Block.CompareTag("Box"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.53f);
                Block.transform.localScale = new Vector3(0.046615f, 0.046615f, 0.046615f);
            }
            else if(Block.CompareTag("Idro"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.58f);
                Block.transform.localScale = new Vector3(0.202150f, 0.202150f, 0.202150f);
            }
            else if(Block.CompareTag("Anhor"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.55f);
                Block.transform.localScale = new Vector3(0.057697f, 0.057697f, 0.057697f);
            }
            else if(Block.CompareTag("Food"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 237f, 29f, 0.61f);
                Block.transform.localScale = new Vector3(0.087810f, 0.042407f, 0.58022f);
                Block.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(Block.CompareTag("Shield"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.70f);
                Block.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
                Block.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(Block.CompareTag("Rocket"))
            {
                Block.GetComponent<SpriteRenderer>().color = new Color(255f, 237f, 29f, 0.70f);
                Block.transform.localScale = new Vector3(0.018967f, 0.018967f, 0.018967f);
                Block.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
