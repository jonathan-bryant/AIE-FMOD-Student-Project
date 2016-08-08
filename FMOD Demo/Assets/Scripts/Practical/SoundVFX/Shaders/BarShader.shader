Shader "Unlit/BarShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SoundImage ("SoundImage", 2D) = "white" {}
		_Emission ("Emission", Color) = (0.0,0.0,0.0,1)
		iResolutionX ("Resolution X", Float) = 1.0
		iResolutionY ("Resolution Y", Float) = 1.0
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

			// https://www.shadertoy.com/view/MdVSWG

			float3 B2_spline(float3 x)	// Returns 3 B-spline functions of degree 2
			{
				float3 t = 3.0 * x;
				float3 b0 = step(0.0, t)		* step(0.0, 1.0 - t);
				float3 b1 = step(0.0, t - 1.0)	* step(0.0, 2.0 - t);
				float3 b2 = step(0.0, t - 2.0)	* step(0.0, 3.0 - t);
				float3 two = float3(2.0, 2.0, 2.0);
				return 0.5 * (
					b0 * pow(t, two) +
					b1 * (-2.0*pow(t, two + 6.0*t - 3.0)) +
					b2 * pow(3.0 - t, two));
			}

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
			sampler2D _SoundImage;
			float4 _MainTex_ST;
			float iResolutionX;
			float iResolutionY;
			float4 _Emission;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv / float2(iResolutionX, iResolutionY);

				float fVBars = 50.0;
				float fVSpacing = 1.0;

				float fVFreq = (uv.x * 3.14);
				float squareWave = sign(sin(fVFreq * fVBars)+1.0 - fVSpacing);

				float x = floor(uv.x * fVBars)/fVBars;
				float fSample = tex2D(_SoundImage, float2(abs(2.0 * x - 1.0), 0.25)).x ;

				float fft = squareWave * fSample;

				float fHBars = 50.0;
				float fHSpacing = 0.180;
				float fHFreq = (uv.y * 3.14);
				fHFreq = sign(sin(fHFreq * fHBars) + 1.0 - fHSpacing);

				float2 centered = float2(1.0, 1.0) * uv - float2(1.0, 1.0);
				float t = _Time.y / 100.0;
				float polychrome = 1.0;
				float3 spline_var = float3(polychrome * uv.x - t,polychrome * uv.x - t,polychrome * uv.x - t);
				float3 spline_args = frac(spline_var + float3(0.0, -1.0/3.0, -2.0/3.0));
				float3 spline = B2_spline(spline_args);

				float f = abs(centered.y);
				float3 base_color = float3(1.0, 1.0, 1.0) - f * spline;
				float3 flame_color = pow(base_color, float3(3.0, 3.0, 3.0));

				float tt = 0.3 - uv.y;
				float df = sign(tt);
				df = (df + 1.0)/0.5;
				float3 color = flame_color * float3(
					1.0 - step(fft, abs(0.3 - uv.y)), 
					1.0 - step(fft, abs(0.3 - uv.y)), 
					1.0 - step(fft, abs(0.3 - uv.y))) * float3(fHFreq, fHFreq, fHFreq);
				color -= color * df * 0.180;

				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb = color.xyz;
				col.rgb *= _Emission.rgb;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
