using Coords;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{

    private List<float> _treePhis = new List<float>();

    public void generateTrees(int num)
    {
        while (num-- > 0)
        {
            float? phi = generateValidPhi();
            if (phi != null)
            {
                print(phi.Value);
                Vector3 pos = new PolarCoord(0.54f + 0.17f, phi.Value).ToCartesian().ToVector3();
                Instantiate(Resources.Load("Prefaby/Tree"), pos, Quaternion.identity);
                _treePhis.Add(phi.Value);
            }
            else
            {
                print("Smula");
            }
        }
        _treePhis.Clear();
    }

    private float? generateValidPhi()
    {
        int maxIter = 100;
        while (maxIter-- > 0)
        {
            float phi = Random.Range(1.0f, 9.9f);

            if (!_treePhis.Any(x => (x - 1f) < phi && phi < (x + 1f)))
            {
                return phi;
            }
        }

        return null;
    }
}
