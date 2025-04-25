Shader "Custom/WaterSmallRipples"
{
    Properties
    {
        _Color ("Base Color", Color) = (0.0, 0.3, 0.6, 0.5) // Semi-transparent blue
        _WaveStrength ("Wave Strength", Range(0, 0.01)) = 0.002
        _WaveSpeed ("Wave Speed", Range(0, 5)) = 1.5
        _NoiseTex ("Noise Texture", 2D) = "white" {} // Perlin noise texture
        _NormalTex ("Normal Map", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Range(0, 2)) = 1.0
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        _Shininess ("Shininess", Range(0, 1)) = 0.8
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha   // Enables transparency
        ZWrite Off   // Prevents depth issues with transparency

        CGPROGRAM
        #pragma surface surf Standard alpha:fade vertex:vert fullforwardshadows
        #pragma target 3.0

        sampler2D _NoiseTex;
        sampler2D _NormalTex;
        float4 _Color;
        float _WaveStrength;
        float _WaveSpeed;
        float _NormalStrength;
        float _Opacity;
        float _Shininess;

        void vert(inout appdata_full v)
        {
            float2 uv = v.vertex.xz * 10.0; // Higher frequency noise
            float timeFactor = _Time.y * _WaveSpeed;

            // Sample noise texture for displacement (small ripples)
            float noiseValue = tex2Dlod(_NoiseTex, float4(uv + timeFactor, 0, 0)).r;
            v.vertex.y += (noiseValue - 0.5) * _WaveStrength;  // Small displacement
        }

        struct Input
        {
            float2 uv_NormalTex;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 normalTex = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
            o.Normal = normalize(lerp(float3(0,0,1), normalTex, _NormalStrength));

            o.Albedo = _Color.rgb;
            o.Metallic = 0;
            o.Smoothness = _Shininess;
            o.Alpha = _Opacity; // Transparency based on property
        }
        ENDCG
    }
    FallBack "Transparent"
}
