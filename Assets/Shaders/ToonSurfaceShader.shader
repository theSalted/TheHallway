Shader "Custom/ToonSurfaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorLevels ("Color Levels", Range(1, 8)) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _ColorLevels;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            // Quantize color
            c.rgb = floor(c.rgb * _ColorLevels) / _ColorLevels;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}