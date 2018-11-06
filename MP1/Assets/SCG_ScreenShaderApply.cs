using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SCG_ScreenShaderApply : MonoBehaviour {

    public Material PostProcessingMat;

    void OnRenderImage(RenderTexture sourceImage, RenderTexture outputTexture)
    {
        Graphics.Blit(sourceImage, outputTexture, PostProcessingMat);
    }
}
