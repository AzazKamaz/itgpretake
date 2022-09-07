Shader "AzazKamaz/LitSpecular"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Exp ("Exponent", Float) = 50
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline"}

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 view : TEXCOORD2;
            };

            float _Exp;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = TransformObjectToWorldNormal(v.normal);
                o.view = GetWorldSpaceViewDir(TransformObjectToWorld(v.vertex));
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // return float4(i.view, 1);
                
                Light l = GetMainLight();
                float n_dot_l = dot(i.normal, l.direction);
                float3 spec = 0;
                if (n_dot_l > 0)
                {
                    spec = l.color * pow(max(0, dot(reflect(-l.direction, normalize(i.normal)), normalize(i.view))), _Exp);
                }
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * n_dot_l * float4(l.color, 1) + float4(spec, 1);
            }
            ENDHLSL
        }
    }
}
