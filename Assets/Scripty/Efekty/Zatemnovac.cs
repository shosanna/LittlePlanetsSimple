using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class Zatemnovac : MonoBehaviour {
    public Material Material;

    private void Start()
    {
        Material.SetFloat("_Magnitude", 1);
        DOTween.To(() => Material.GetFloat("_Magnitude"), x => Material.SetFloat("_Magnitude", x), 0.2f, 30).SetLoops(-1, LoopType.Restart);
    }

    void Update() {
        //if (Den.Noc())
        //{
        //    Material.SetFloat("_Magnitude", 0.1f);
        //}
        //else if (Den.Vecer())
        //{
        //    Material.SetFloat("_Magnitude", 0.45f);
        //}
        //else if (Den.Odpoledne())
        //{
        //    Material.SetFloat("_Magnitude", 0.75f);
        //}
        //else
        //{
        //    Material.SetFloat("_Magnitude", 1f);
        //}

        // Konec dne!
        if (Den.Pulnoc())
        {
            GameState.Instance.NastavKonecDne();
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        if (Material != null)
            Graphics.Blit(src, dst, Material);
    }
}