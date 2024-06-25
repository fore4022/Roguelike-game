using UnityEngine;
public class Map_Scroller : MonoBehaviour
{
    private Renderer _render;

    private void Start()
    {
        _render = Util.GetOrAddComponent<Renderer>(gameObject);

        _render.material = Managers.Resource.Load<Material>($"Material/{Managers.Game.map.mapThemeName}");
        transform.localScale = new Vector3(Managers.Game.camera_w, Managers.Game.camera_h, 1);
        transform.position = Managers.Game.player.gameObject.transform.position;
        _render.material.SetFloat("_Glossiness", 0f);

        Managers.Resource.Instantiate("Prefab/DirectionalLight", gameObject.transform);

        Managers.Input.keyAction -= Adjustment;
        Managers.Input.keyAction += Adjustment;
    }
    private void Adjustment()
    {
        if (!Managers.Game.player || !Input.anyKey) { return; }

        transform.position = Managers.Game.player.gameObject.transform.position;
        _render.material.mainTextureOffset += new Vector2(Managers.Game.player.h* 0.16f * Managers.Game.player.MoveSpeed, Managers.Game.player.v* 0.09f * Managers.Game.player.MoveSpeed) * Time.deltaTime / 2;
    }
    private void OnDestroy() { Managers.Input.keyAction -= Adjustment; }
}
