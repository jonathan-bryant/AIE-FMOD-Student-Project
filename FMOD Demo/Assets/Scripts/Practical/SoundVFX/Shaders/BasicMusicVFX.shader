Shader "Unlit/BasicMusicVFX"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		iResolutionX("ResolutionX", Float) = 1.0
		iResolutionY("ResolutionY", Float) = 1.0
		_SoundImage("SoundImage", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
	{
		CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

	float audio_freq(in sampler2D channel, in float f) { return tex2D(channel, float2(f, 0.25)).x; }
	float audio_ampl(in sampler2D channel, in float t) { return tex2D(channel, float2(t, 0.75)).x; }

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
	float4 _MainTex_ST;
	float iResolutionX;
	float iResolutionY;
	sampler2D _SoundImage;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);		

		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// https://www.shadertoy.com/view/lsdGR8#
		float2 uv = i.uv / float2(iResolutionX, iResolutionY);
		float2 centered = 2.0 * uv - 1.0;
		centered.x *= iResolutionX / iResolutionY;

		float dist2 = dot(centered, centered);
		float clamped_dist = smoothstep(0.0, 1.0, dist2);
		float arcLength = abs(atan2(centered.y, centered.x) / radians(360.0)) + 0.01;

		// Color variation functions
		float t = _Time.x / 1000.0;
		float polychrome = (1.0 + sin(t*10.0)) / 2.0;	// 0 --> uniform color, 1 --> full spectrum
		float spline_val1 = float3(polychrome * uv.x - t, polychrome * uv.x - t, polychrome * uv.x - t);
		float3 spline_args = frac(spline_val1 + float3(0.0, -1 / 3.0, -2.0 / 3.0));
		float3 spline = B2_spline(spline_args);

		float f = abs(centered.y);
		float3 base_color = float3 (1.0, 1.0, 1.0) - f * spline;
		float3 flame_color = pow(base_color, float3(3.0, 3.0, 3.0));
		float3 disc_color = 0.20 * base_color;
		float3 wave_color = 0.10 * base_color;
		float3 flash_color = 0.05 * base_color;

		float sample1 = audio_freq(_SoundImage, abs(2.0 * uv.x - 1.0) + 0.01);
		float sample2 = audio_ampl(_SoundImage, clamped_dist);
		float sample3 = audio_ampl(_SoundImage, arcLength);

		float disp_dist = smoothstep(-0.2, -0.1, sample3 - dist2);

		float3 color = float3(0.0, 0.0, 0.0);

		float vert = abs(uv.y - 0.5);
		color += flame_color * smoothstep(vert, vert * 8.0, sample1);
		//color += disc_color * smoothstep(0.5, 1.0, sample2) * (1.0 - clamped_dist);
		//color += flash_color * smoothstep(0.5, 1.0, sample3) * clamped_dist;
		//color += wave_color * disp_dist;
		color = pow(color, float3(0.4545, 0.4545, 0.4545));

		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
		col.rgb = color.rrr;
		// apply fog
		UNITY_APPLY_FOG(i.fogCoord, col);
		return col;
	}
		ENDCG
	}
	}
}
