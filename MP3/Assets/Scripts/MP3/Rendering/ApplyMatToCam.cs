using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ApplyMatToCam : MonoBehaviour {

    public Material matToApply;

    void OnRenderImage(RenderTexture sourceImage, RenderTexture outputTexture)
    {
        Graphics.Blit(sourceImage, outputTexture, matToApply);
    }
}
