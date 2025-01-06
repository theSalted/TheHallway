Shader "Custom/ToonShadingImageEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeThreshold ("Edge Threshold", Range(0.1, 1.0)) = 0.2
        _ColorLevels ("Color Levels", Range(1, 8)) = 4
        _FillTintColor ("Fill Tint Color", Color) = (1, 1, 1, 1)
        _EdgeTintColor ("Edge Tint Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _EdgeThreshold;
            float _ColorLevels;
            fixed4 _FillTintColor;
            fixed4 _EdgeTintColor;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                // Sample neighboring pixels for edge detection
                float2 texelSize = _MainTex_TexelSize.xy;
                float3 sample[9];
                int index = 0;
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        sample[index++] = tex2D(_MainTex, uv + texelSize * float2(x, y)).rgb;
                    }
                }

                // Compute edge detection using Sobel operator
                float3 gx = sample[2] + 2.0 * sample[5] + sample[8] - (sample[0] + 2.0 * sample[3] + sample[6]);
                float3 gy = sample[0] + 2.0 * sample[1] + sample[2] - (sample[6] + 2.0 * sample[7] + sample[8]);
                float edgeIntensity = length(gx + gy);

                // Original color
                float3 color = tex2D(_MainTex, uv).rgb;

                // Convert to grayscale
                float gray = dot(color, float3(0.299, 0.587, 0.114));

                // Apply fill tint color
                float3 tintedFillColor = gray * _FillTintColor.rgb;

                // Quantize colors
                tintedFillColor = floor(tintedFillColor * _ColorLevels) / _ColorLevels;

                // Combine edge and quantized color
                if (edgeIntensity > _EdgeThreshold)
                {
                    // Edge color (tinted edge color)
                    return fixed4(_EdgeTintColor.rgb, 1.0);
                }
                else
                {
                    // Quantized and tinted grayscale color
                    return fixed4(tintedFillColor, 1.0);
                }
            }
            ENDCG
        }
    }
    FallBack Off
}