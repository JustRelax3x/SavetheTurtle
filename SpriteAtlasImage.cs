using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteAtlasImage : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas _spriteAtlas;
    [SerializeField]
    private string _spriteName;

    private void Start()
    {
        GetComponent<Image>().sprite = _spriteAtlas.GetSprite(_spriteName);
    }
}
