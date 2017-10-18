Shader "Custom/ShaderLightning" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _LightSaberFactor ("LightSaberFactor", Range(0.0, 1.0)) = 0.9
    }
    SubShader {
        Tags { "RenderType"="Geometry" "Queue" = "Transparent" }
        LOD 200
 
        Pass {
 
            Cull Off
            ZWrite Off
            ZTest LEqual
            //Blend One One
            Blend SrcAlpha OneMinusSrcAlpha
            //Lighting Off
            Lighting On
 
            CGPROGRAM
            #pragma glsl_no_auto_normalization
            #pragma vertex vert
            #pragma fragment frag
            
 
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _LightSaberFactor;
 
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 color : COLOR;
            };
 
            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            v2f vert (a2v v)
            {
	            v2f o0;
	            o0.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	            o0.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
	            return o0;
            }
 
            float4 frag(v2f i) : COLOR
            {
                float4 tx = tex2D (_MainTex, i.uv);
                               
                if (tx.a > _LightSaberFactor)
                {
                    return float4(1.0, 1.0, 1.0, tx.a * _Color.a);
                }
                else
                {
                    return tx * _Color;//i.color; 
                }
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}