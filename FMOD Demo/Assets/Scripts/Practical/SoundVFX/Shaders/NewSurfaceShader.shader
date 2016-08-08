Shader "Custom/NewSurfaceShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("Height Adjustment", Float) = 0.0
		_Alpha ("Alpha", Range(0,1)) = 0.0
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
		float _Amount;
		float _Alpha;

		void vert (inout appdata_full v)
		{
			if (v.vertex.y < 0)
			{
				if (_Amount < 0.2)
					_Amount *= 1.5;
				v.vertex.y *= (_Amount * 100);
			}
			v.color.rgb = -v.vertex.yyy;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
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
