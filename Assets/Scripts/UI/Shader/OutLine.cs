
using System;
using UnityEngine; 
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[SerializeField]
[PostProcess(typeof(PostProcessOutlineRenderer), PostProcessEvent.AfterStack, "Outline")]

public class OutLine : PostProcessEffectSettings
{

    [Range(1f, 5f),  Tooltip("OutLinethickress")]
    public IntParameter thinkness = new IntParameter {value = 2};

    [Range(0f, 5f),  Tooltip("OutLine edge start")]
    public FloatParameter edge = new FloatParameter {value = 0.2f};

    [Range(0f, 1f),  Tooltip("OutLine smoothess transition on close objects")]
    public FloatParameter transition = new FloatParameter {value = 0.2f};

    [Tooltip("OutLine color")]
    public ColorParameter color = new ColorParameter {value = Color.black};
}

public  class PostProcessOutlineRenderer : PostProcessEffectRenderer<PostProcessOutLine>
{
    public override void Render(PostProcessEffectRendererContext context)
    {
        var sheet = context.propertySheet.Get(Shader.Find("Hidden/Outline")) ;
        sheet.property.SetInt("_Thinknes", settings.thinkness);
        sheet.property.SetInt("_Edge", settings.edge);
        sheet.property.SetInt("_TransitionSmootheness", settings.transition);
        sheet.property.SetInt("_Color", settings.color);
        context.comand.BlitFullscreenTrinangle(context.source, context.destination, sheet, 0);
    }
}
