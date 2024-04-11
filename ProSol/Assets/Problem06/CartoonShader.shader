Shader "CartoonShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (0,0,0,0)
        _Threshold("Threshold", Range(0.01, 1)) = 0.3
        _Smoothness("Smoothness", Range(0.01, 1)) = 0.1
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD0;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;
            float _Threshold;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(UnityObjectToWorldNormal(v.normal));
                o.viewDir = normalize(UnityWorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightDir = normalize(_LightDirection);
                float lightIntensity = max(dot(i.normal, lightDir), 0);
                
                float3 halfDir = normalize(lightDir + i.viewDir);
                float specIntensity = pow(max(dot(i.normal, halfDir), 0), 10.0f);
                
                float4 col = _DiffuseColor * lightIntensity + float4(1, 1, 1, 1) * specIntensity * _Smoothness;

                float4 banding = floor(col / _Threshold);
                float4 finalIntensity = banding * _Threshold;

                return finalIntensity;
            }
            ENDCG
        }
    }
}
