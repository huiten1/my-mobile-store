Shader "Unlit/UVFlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowMap("flow map",2D) = "black" {}
        _FlowSpeed("flow speed",Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _FlowMap;
            float4 _FlowMap_ST;
            float _FlowSpeed;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float3 FlowUVW(float2 uv,float2 flowVector,float time,bool flowB)
            {
                float phaseOffset = flowB ? 0.5 : 0;
	            float progress = frac(time + phaseOffset);
	            float3 uvw;
	            uvw.xy = uv - flowVector * progress;
	            uvw.z = 1 - abs(1 - 2 * progress);
	            return uvw;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 flowVector = tex2D(_FlowMap,TRANSFORM_TEX( i.uv,_FlowMap));
                
                float time= _Time.y * _FlowSpeed;
                float3 uvwA = FlowUVW(i.uv, flowVector, time, false);
			    float3 uvwB = FlowUVW(i.uv, flowVector, time, true);

			    fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
			    fixed4 texB = tex2D(_MainTex, uvwB.xy) * uvwB.z;

			    fixed4 c = (texA + texB) ;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return c;
            }
            ENDCG
        }
    }
}
