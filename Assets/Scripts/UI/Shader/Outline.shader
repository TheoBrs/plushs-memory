Shader "Learning/Unlit/TO RENAME"
{
    HLSLINCLUIDE  
        
    #include "Packages/com.unity.postprocessing/PostProssessing/Shaders/StdLib.hlsl"
    TEXTURE2D_SAMPLER2D(_MainText, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_CarerDephtTexture, sampler_CarerDephtTexture);
    
    int  _Thinkness;
    float __TransitionSmootheness;
    float _Edge;
    float4 _Color;

    float4 Frag(VaryingsDefault i ) : SV_Target
    {
        float2 offset = _Thinkness/_ScreenParams;
        float left = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CarerDephtTexture, i.texcoord +float2(-offset.x,0)).x);
        float rigth = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CarerDephtTexture, i.texcoord +float2(offset.x,0)).x);
        float up = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CarerDephtTexture, i.texcoord +float2(-offset.x,0)).x);
        float down = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CarerDephtTexture, i.texcoord +float2(-offset.x,0)).x);

        float delta = sqrt (pow(rigth - left, 2 ) + pow(up-down, 2));

        float = smoothstep(_Edge, _Edge + __TransitionSmootheness, delta);

        float 4 mainText = SAMPLE_TEXTURE2D(_MainText, sampler_MainTex, i.texcoord);

        float4 color = lerp(mainTex , color, t);

        float4 output = lerp(mainTex, color, t);

        return output;
    }
    ENDHLSL

    SubShader
    {
        Cull Off ZWr ite ZTest Always
        Pass
        {
            HSLSPROGRAM
            #pragma vertex VertDefault
            #pragmafragment Frag
            ENDHLSL
        }
    }
}
