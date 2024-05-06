Shader "Custom/CellShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (0,0,0,0)
        _Threshold("Threshold", Range(0.01, 1)) = 0.3
        _Smoothness("Smoothness", Range(0.01, 1)) = 0.1
        _AmbientColor("AmbientColor", Color) = (0.2, 0.2, 0.2, 1)
        _LightIntensity("LightIntensity", Range(0.01, 10)) = 1.0
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
            float4 _AmbientColor;
            float _LightIntensity;

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
                // 주변광 계산
                float4 ambientLight = _AmbientColor * _DiffuseColor;

                // 주변광 및 주변광 포함된 디퓨즈 라이팅
                float3 lightDir = normalize(_LightDirection);
                float lightIntensity = max(dot(i.normal, lightDir), 0) * _LightIntensity;
                
                float3 halfDir = normalize(lightDir + i.viewDir);
                float specIntensity = pow(max(dot(i.normal, halfDir), 0), 10.0f);
                
                float4 col = _DiffuseColor * lightIntensity + float4(1, 1, 1, 1) * specIntensity * _Smoothness;
                
                // 주변광을 디퓨즈 라이팅 결과에 더해줌
                col += ambientLight;

                // Threshold 및 Banding 처리
                float4 banding = floor(col / _Threshold);
                float4 finalIntensity = banding * _Threshold;

                return finalIntensity;
            }

            ENDCG
        }
    }
}