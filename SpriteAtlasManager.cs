using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteAtlasManager : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas _spriteAtlas;
    [SerializeField]
    private string _spriteName;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _spriteAtlas.GetSprite(_spriteName);
    }
}
