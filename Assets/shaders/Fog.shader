Shader "Custom/SmokeMultiGradientShader"
{
    Properties
    {
        _RampTex ("Gradient Texture (Color Ramp)", 2D) = "white" {} 
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        _Scale ("Noise Scale", Range(0.1, 5)) = 1.0
        _GradientStrength ("Gradient Strength", Range(0, 2)) = 1.0
    }
    
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha  // Enables transparency
            ZWrite Off  // Prevents depth conflicts
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float gradientFactor : TEXCOORD1;
            };

            sampler2D _RampTex;  // Gradient texture
            sampler2D _NoiseTex; 
            float _Opacity;
            float _Scale;
            float _GradientStrength;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Scale;  // Scale the noise texture
                
                // Use UV.y for gradient sampling
                o.gradientFactor = saturate(v.uv.y * _GradientStrength); 
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float noise = tex2D(_NoiseTex, i.uv).r; // Sample noise texture
                float alpha = _Opacity * noise; // Adjust transparency with noise

                // Sample the color ramp texture using the gradient factor
                float4 gradientColor = tex2D(_RampTex, float2(i.gradientFactor, 0.5));

                return float4(gradientColor.rgb, alpha); // RGB + alpha transparency
            }
            ENDCG
        }
    }
}
