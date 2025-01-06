Shader "Custom/OilPaintingImageEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MainTex_TexelSize;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 mask = tex2D(_MaskTex, uv);
                if (mask.r > 0.5)
                {
                    // Apply oil painting effect
                    float2 offset = _MainTex_TexelSize.xy * 4.0;

                    fixed4 col = 0;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            col += tex2D(_MainTex, uv + offset * float2(x, y));
                        }
                    }
                    col /= 9.0;
                    return col;
                }
                else
                {
                    // Return original color
                    return tex2D(_MainTex, uv);
                }
            }
            ENDCG
        }
    }
    FallBack Off
}