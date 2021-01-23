using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Texture3DPreview : MonoBehaviour
{
    public Image image;
    public bool lol;
    public Texture3DGenerator generator;

    public void Update()
    {
        var sprite = Sprite.Create(generator.GenerateTexture2D(), new Rect(0, 0, 128, 128), Vector2.zero);
        image.sprite = sprite;
    }
}
