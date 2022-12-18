Shader "Unlit/451ShaderWithTextureIllum"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off


		// Blend SrcAlpha OneMinusSrcAlpha

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
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float4x4 MyXformMat;
            fixed4   MyColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(MyXformMat, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                col += MyColor;
				return col;
			}
			ENDCG
		}
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert
		
		struct Input {
			float2 uv_MainTex;
		};
		
		sampler2D _MainTex;
		
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}
		ENDCG
	}
}
