Shader "Custom/NewSurfaceShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_UVx ("UVx", float) = 0
		_UVy ("UVy", float) = 0
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float4 color : COLOR0;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _UVx;
		float _UVy;

		void vert (inout appdata_full v)
		{
			float4 worldpos = mul(_Object2World, v.vertex);
			float time = _Time * 20.0;
			float value = sin(time + worldpos.x * 3) * 0.2;
			if (v.vertex.y < 0)
			{
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y + value, v.vertex.z);
			}
			v.color.rgb = -v.vertex.yyy;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color

			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a * IN.color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
