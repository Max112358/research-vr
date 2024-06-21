using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class StartMR : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {


        VarjoChromaKeyParams chromaKeyParams = new VarjoChromaKeyParams
        {
            hue = 0.3f, // Sample hue value for green
            hsvTolerance = new Vector3(0.1f, 0.2f, 0.3f) // Sample HSV tolerance values
        };


        VarjoChromaKey.UnlockChromaKeyConfigs();
        VarjoMixedReality.StartRender();
        VarjoChromaKey.EnableChromaKey(true, false);
        VarjoChromaKey.SetChromaKeyParameters(230, chromaKeyParams);
        VarjoChromaKey.LockChromaKeyConfigs();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
