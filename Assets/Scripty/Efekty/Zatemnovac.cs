using UnityEngine;

[ExecuteInEditMode]
public class Zatemnovac : MonoBehaviour {
    public Material Material;

    void Update() {
        if (Den.Noc()) {
            Material.SetFloat("_Magnitude", 0.1f);
        } else if (Den.Vecer()) {
            Material.SetFloat("_Magnitude", 0.45f);
        } else if (Den.Odpoledne()) {
            Material.SetFloat("_Magnitude", 0.75f);
        } else {
            Material.SetFloat("_Magnitude", 1f);
        }

        // Konec dne!
        if (Den.Pulnoc()) {
            GameState.Instance.NastavKonecDne();
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        if (Material != null)
            Graphics.Blit(src, dst, Material);
    }
}