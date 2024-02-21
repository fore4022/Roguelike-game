using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Map_Scroller : MonoBehaviour
{
    private Renderer render;
    private void Start()
    {
        render = Util.getOrAddComponent<Renderer>(gameObject);
        render.material = Managers.Resource.load<Material>($"Material/{Managers.Game.map.mapThemeName}");
        transform.localScale = new Vector3(Managers.Game.camera_h, Managers.Game.camera_v);
        transform.position = Managers.Game.player.gameObject.transform.position;
        //render.material.SetFloat("Smoothness", 0f);
        Managers.Resource.instantiate("Prefab/DirectionalLight", gameObject.transform);
    }
    private void Update()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        render.material.mainTextureOffset += new Vector2(Managers.Game.player.h, Managers.Game.player.v) * Time.deltaTime / 2;
    }
}
