Shader "FresnelEdgeGlow"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _FresnelPower("Fresnel Power", Range(0.1, 5)) = 2.0
        _GlowIntensity("Glow Intensity", Range(0, 5)) = 1.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Pass
        {
            Blend SrcAlpha One // Additive blending for glow effect
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float fresnel : TEXCOORD0;
            };

            float4 _Color;
            float _FresnelPower;
            float _GlowIntensity;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                
                // Calculate Fresnel effect
                float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
                float fresnel = pow(1.0 - dot(viewDir, v.normal), _FresnelPower);
                o.fresnel = saturate(fresnel * _GlowIntensity);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return float4(_Color.rgb * i.fresnel, i.fresnel);
            }
            ENDCG
        }
    }
}
