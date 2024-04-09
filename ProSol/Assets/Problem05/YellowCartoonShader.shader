Shader "Unlit/YellowCartoonShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (1,-1,-1,0)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth("Outline Width", Range(0.001, 0.03)) = 0.01
        _MainTex("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;
            float4 _OutlineColor;
            float _OutlineWidth;
            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float lightDir = normalize(_LightDirection);
                float lightIntensity = max(dot(i.normal, lightDir), 0);

                float4 col = _DiffuseColor * lightIntensity;

                // Cartoon effect
                float edge = tex2D(_MainTex, i.uv).a;
                float alpha = fwidth(edge);
                float2 gradient = 0.5 * fwidth(i.screenPos.xy) / alpha;
                float border = smoothstep(0.5 - alpha, 0.5 + alpha, min(gradient.x, gradient.y));

                // Apply outline
                float outline = smoothstep(0.5 - _OutlineWidth * 0.5, 0.5 + _OutlineWidth * 0.5, border);
                col = lerp(_OutlineColor, col, outline);

                return col;
            }
            ENDCG
        }
    }
}