// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7672,x:33382,y:32953,varname:node_7672,prsc:2|diff-7652-OUT,normal-6587-OUT,emission-1656-OUT,alpha-3322-OUT;n:type:ShaderForge.SFN_Tex2d,id:85,x:30672,y:32670,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_85,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2311-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2311,x:30495,y:32670,varname:node_2311,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:5682,x:29598,y:32525,varname:node_5682,prsc:2;n:type:ShaderForge.SFN_Vector1,id:6036,x:30145,y:32463,varname:node_6036,prsc:2,v1:2;n:type:ShaderForge.SFN_Frac,id:2157,x:30145,y:32525,varname:node_2157,prsc:2|IN-6215-OUT;n:type:ShaderForge.SFN_Vector1,id:9440,x:29598,y:32470,varname:node_9440,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:6302,x:29784,y:32525,varname:node_6302,prsc:2|A-9440-OUT,B-5682-TSL;n:type:ShaderForge.SFN_Add,id:6215,x:29959,y:32525,varname:node_6215,prsc:2|A-3871-OUT,B-6302-OUT;n:type:ShaderForge.SFN_Multiply,id:6591,x:30321,y:32525,varname:node_6591,prsc:2|A-6036-OUT,B-2157-OUT;n:type:ShaderForge.SFN_Vector1,id:1587,x:29598,y:32264,varname:node_1587,prsc:2,v1:2;n:type:ShaderForge.SFN_Abs,id:8690,x:29598,y:32329,varname:node_8690,prsc:2|IN-2220-Y;n:type:ShaderForge.SFN_ObjectPosition,id:2220,x:29403,y:32329,varname:node_2220,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3871,x:29784,y:32329,varname:node_3871,prsc:2|A-1587-OUT,B-8690-OUT;n:type:ShaderForge.SFN_Subtract,id:1941,x:30495,y:32525,varname:node_1941,prsc:2|A-9499-OUT,B-6591-OUT;n:type:ShaderForge.SFN_Vector1,id:9499,x:30321,y:32463,varname:node_9499,prsc:2,v1:1;n:type:ShaderForge.SFN_Abs,id:9091,x:30669,y:32525,varname:node_9091,prsc:2|IN-1941-OUT;n:type:ShaderForge.SFN_Add,id:9716,x:30852,y:32525,varname:node_9716,prsc:2|A-155-OUT,B-9091-OUT;n:type:ShaderForge.SFN_Vector1,id:155,x:30669,y:32466,varname:node_155,prsc:2,v1:-0.7;n:type:ShaderForge.SFN_Multiply,id:8341,x:31212,y:32525,varname:node_8341,prsc:2|A-8861-OUT,B-7580-OUT,C-85-R;n:type:ShaderForge.SFN_Vector1,id:8861,x:31036,y:32461,varname:node_8861,prsc:2,v1:6;n:type:ShaderForge.SFN_Sin,id:3396,x:31199,y:32828,varname:node_3396,prsc:2|IN-6209-OUT;n:type:ShaderForge.SFN_Time,id:514,x:30472,y:32829,varname:node_514,prsc:2;n:type:ShaderForge.SFN_Add,id:215,x:30852,y:32828,varname:node_215,prsc:2|A-85-B,B-6850-OUT;n:type:ShaderForge.SFN_Multiply,id:6209,x:31021,y:32828,varname:node_6209,prsc:2|A-3702-OUT,B-215-OUT;n:type:ShaderForge.SFN_Vector1,id:3702,x:30852,y:32772,varname:node_3702,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:8439,x:31376,y:32828,varname:node_8439,prsc:2|A-4799-OUT,B-3396-OUT;n:type:ShaderForge.SFN_Vector1,id:4799,x:31199,y:32774,varname:node_4799,prsc:2,v1:2;n:type:ShaderForge.SFN_Get,id:7424,x:31178,y:32953,varname:node_7424,prsc:2|IN-5091-OUT;n:type:ShaderForge.SFN_Multiply,id:7560,x:31574,y:32828,varname:node_7560,prsc:2|A-8439-OUT,B-4673-OUT;n:type:ShaderForge.SFN_Vector1,id:7506,x:33255,y:32393,varname:node_7506,prsc:2,v1:0;n:type:ShaderForge.SFN_Dot,id:6142,x:30587,y:31861,varname:node_6142,prsc:2,dt:3|A-6155-OUT,B-923-OUT;n:type:ShaderForge.SFN_NormalVector,id:6155,x:30417,y:31713,prsc:2,pt:False;n:type:ShaderForge.SFN_ViewVector,id:5169,x:30251,y:31861,varname:node_5169,prsc:2;n:type:ShaderForge.SFN_Normalize,id:923,x:30417,y:31861,varname:node_923,prsc:2|IN-5169-OUT;n:type:ShaderForge.SFN_Set,id:5091,x:31131,y:31841,varname:rim,prsc:2|IN-9454-OUT;n:type:ShaderForge.SFN_Set,id:3342,x:33701,y:32241,varname:intersect,prsc:2|IN-6064-OUT;n:type:ShaderForge.SFN_Multiply,id:9431,x:31810,y:32624,varname:node_9431,prsc:2|A-8341-OUT,B-290-RGB;n:type:ShaderForge.SFN_Multiply,id:1279,x:31810,y:32745,varname:node_1279,prsc:2|A-290-RGB,B-7560-OUT;n:type:ShaderForge.SFN_Add,id:2962,x:32006,y:32685,varname:node_2962,prsc:2|A-9431-OUT,B-1279-OUT;n:type:ShaderForge.SFN_Set,id:6317,x:32196,y:32685,varname:hexes,prsc:2|IN-2962-OUT;n:type:ShaderForge.SFN_Max,id:9372,x:31084,y:33418,varname:node_9372,prsc:2|A-7855-OUT,B-4335-OUT;n:type:ShaderForge.SFN_Set,id:659,x:31402,y:33319,varname:glow,prsc:2|IN-5329-OUT;n:type:ShaderForge.SFN_Get,id:7855,x:30896,y:33404,varname:node_7855,prsc:2|IN-3342-OUT;n:type:ShaderForge.SFN_Get,id:4335,x:30896,y:33469,varname:node_4335,prsc:2|IN-5091-OUT;n:type:ShaderForge.SFN_Set,id:2971,x:31429,y:33559,varname:glowColor,prsc:2|IN-490-OUT;n:type:ShaderForge.SFN_Set,id:4671,x:31809,y:32524,varname:mainColor,prsc:2|IN-290-RGB;n:type:ShaderForge.SFN_Get,id:5239,x:30600,y:33563,varname:node_5239,prsc:2|IN-4671-OUT;n:type:ShaderForge.SFN_Lerp,id:1701,x:30903,y:33558,varname:node_1701,prsc:2|A-5239-OUT,B-290-RGB,T-5645-OUT;n:type:ShaderForge.SFN_Get,id:8009,x:30408,y:33618,varname:node_8009,prsc:2|IN-659-OUT;n:type:ShaderForge.SFN_Power,id:5645,x:30621,y:33618,varname:node_5645,prsc:2|VAL-8009-OUT,EXP-5718-OUT;n:type:ShaderForge.SFN_Vector1,id:5718,x:30429,y:33669,varname:node_5718,prsc:2,v1:2;n:type:ShaderForge.SFN_Append,id:490,x:31266,y:33559,varname:node_490,prsc:2|A-5496-OUT,B-3479-OUT;n:type:ShaderForge.SFN_Vector1,id:3479,x:31084,y:33705,varname:node_3479,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Get,id:5269,x:32365,y:32687,varname:node_5269,prsc:2|IN-4671-OUT;n:type:ShaderForge.SFN_Multiply,id:6666,x:32595,y:32687,varname:node_6666,prsc:2|A-5269-OUT,B-2166-OUT;n:type:ShaderForge.SFN_Get,id:2166,x:32365,y:32752,varname:node_2166,prsc:2|IN-8293-OUT;n:type:ShaderForge.SFN_Add,id:3208,x:32790,y:32687,varname:node_3208,prsc:2|A-3586-OUT,B-6666-OUT;n:type:ShaderForge.SFN_Get,id:3586,x:32574,y:32626,varname:node_3586,prsc:2|IN-2971-OUT;n:type:ShaderForge.SFN_Multiply,id:2894,x:32972,y:32687,varname:node_2894,prsc:2|A-8902-OUT,B-3208-OUT;n:type:ShaderForge.SFN_Get,id:8902,x:32769,y:32626,varname:node_8902,prsc:2|IN-659-OUT;n:type:ShaderForge.SFN_Add,id:7652,x:33151,y:32687,varname:node_7652,prsc:2|A-1868-OUT,B-2894-OUT;n:type:ShaderForge.SFN_Get,id:1868,x:32951,y:32626,varname:node_1868,prsc:2|IN-6317-OUT;n:type:ShaderForge.SFN_Set,id:8293,x:31810,y:32885,varname:colorAlpha,prsc:2|IN-290-A;n:type:ShaderForge.SFN_Smoothstep,id:1364,x:33075,y:32261,varname:node_1364,prsc:2|A-2152-OUT,B-7179-OUT,V-5693-OUT;n:type:ShaderForge.SFN_Vector1,id:2152,x:32877,y:32202,varname:node_2152,prsc:2,v1:0;n:type:ShaderForge.SFN_ProjectionParameters,id:8817,x:32666,y:32261,varname:node_8817,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7179,x:32877,y:32261,varname:node_7179,prsc:2|A-8147-OUT,B-8817-RFAR;n:type:ShaderForge.SFN_Vector1,id:8147,x:32666,y:32206,varname:node_8147,prsc:2,v1:0.5;n:type:ShaderForge.SFN_OneMinus,id:9911,x:33255,y:32261,varname:node_9911,prsc:2|IN-1364-OUT;n:type:ShaderForge.SFN_If,id:6064,x:33494,y:32261,varname:node_6064,prsc:2|A-5693-OUT,B-7506-OUT,GT-9911-OUT,EQ-7506-OUT,LT-7506-OUT;n:type:ShaderForge.SFN_Depth,id:3538,x:32666,y:32056,varname:node_3538,prsc:2;n:type:ShaderForge.SFN_Slider,id:3322,x:32731,y:33375,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3322,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.776699,max:1;n:type:ShaderForge.SFN_Clamp01,id:7580,x:31036,y:32525,varname:node_7580,prsc:2|IN-9716-OUT;n:type:ShaderForge.SFN_OneMinus,id:5517,x:30778,y:31861,varname:node_5517,prsc:2|IN-6142-OUT;n:type:ShaderForge.SFN_Multiply,id:9454,x:30972,y:31841,varname:node_9454,prsc:2|A-5114-OUT,B-5517-OUT;n:type:ShaderForge.SFN_Vector1,id:5114,x:30778,y:31778,varname:node_5114,prsc:2,v1:1;n:type:ShaderForge.SFN_SceneDepth,id:9518,x:32666,y:31938,varname:node_9518,prsc:2;n:type:ShaderForge.SFN_Subtract,id:5693,x:32877,y:32056,varname:node_5693,prsc:2|A-9518-OUT,B-3538-OUT;n:type:ShaderForge.SFN_Tex2d,id:290,x:31574,y:32646,ptovrint:False,ptlb:SurfaceTex,ptin:_SurfaceTex,varname:node_290,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:6913,x:30903,y:33695,varname:node_6913,prsc:2|A-5239-OUT,B-8678-RGB,T-5645-OUT;n:type:ShaderForge.SFN_Add,id:5496,x:31084,y:33559,varname:node_5496,prsc:2|A-1701-OUT,B-6913-OUT;n:type:ShaderForge.SFN_Color,id:8678,x:30621,y:33778,ptovrint:False,ptlb:_RimColor,ptin:__RimColor,varname:node_8678,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:3207,x:32888,y:33089,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_3207,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ConstantClamp,id:5329,x:31280,y:33418,varname:node_5329,prsc:2,min:0.2,max:1|IN-9372-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:4673,x:31376,y:32953,varname:node_4673,prsc:2,min:0,max:1|IN-7424-OUT;n:type:ShaderForge.SFN_Slider,id:2806,x:32731,y:33271,ptovrint:False,ptlb:Normal Intensity,ptin:_NormalIntensity,varname:node_2806,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:6587,x:33096,y:33139,varname:node_6587,prsc:2|A-3207-RGB,B-2806-OUT;n:type:ShaderForge.SFN_Slider,id:7695,x:32731,y:32988,ptovrint:False,ptlb:Emission Intensity,ptin:_EmissionIntensity,varname:node_7695,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:1656,x:33096,y:32968,varname:node_1656,prsc:2|A-5269-OUT,B-7695-OUT;n:type:ShaderForge.SFN_ValueProperty,id:180,x:30472,y:32989,ptovrint:False,ptlb:Flash Speed,ptin:_FlashSpeed,varname:node_180,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6850,x:30672,y:32843,varname:node_6850,prsc:2|A-514-TDB,B-180-OUT;proporder:290-85-3207-2806-3322-8678-7695-180;pass:END;sub:END;*/

Shader "Cameron/HologramPlanet" {
    Properties {
        _SurfaceTex ("SurfaceTex", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _Normal ("Normal", 2D) = "white" {}
        _NormalIntensity ("Normal Intensity", Range(0, 1)) = 1
        _Opacity ("Opacity", Range(0, 1)) = 0.776699
        __RimColor ("_RimColor", Color) = (1,1,1,1)
        _EmissionIntensity ("Emission Intensity", Range(0, 1)) = 0
        _FlashSpeed ("Flash Speed", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Opacity;
            uniform sampler2D _SurfaceTex; uniform float4 _SurfaceTex_ST;
            uniform float4 __RimColor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _NormalIntensity;
            uniform float _EmissionIntensity;
            uniform float _FlashSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = (_Normal_var.rgb*_NormalIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_5682 = _Time + _TimeEditor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _SurfaceTex_var = tex2D(_SurfaceTex,TRANSFORM_TEX(i.uv0, _SurfaceTex));
                float4 node_514 = _Time + _TimeEditor;
                float rim = (1.0*(1.0 - abs(dot(i.normalDir,normalize(viewDirection)))));
                float3 hexes = (((6.0*saturate(((-0.7)+abs((1.0-(2.0*frac(((2.0*abs(objPos.g))+(5.0*node_5682.r))))))))*_MainTex_var.r)*_SurfaceTex_var.rgb)+(_SurfaceTex_var.rgb*((2.0+sin((1.0*(_MainTex_var.b+(node_514.b*_FlashSpeed)))))*clamp(rim,0,1))));
                float node_5693 = (sceneZ-partZ);
                float node_7506 = 0.0;
                float node_6064_if_leA = step(node_5693,node_7506);
                float node_6064_if_leB = step(node_7506,node_5693);
                float intersect = lerp((node_6064_if_leA*node_7506)+(node_6064_if_leB*(1.0 - smoothstep( 0.0, (0.5*_ProjectionParams.a), node_5693 ))),node_7506,node_6064_if_leA*node_6064_if_leB);
                float glow = clamp(max(intersect,rim),0.2,1);
                float3 mainColor = _SurfaceTex_var.rgb;
                float3 node_5239 = mainColor;
                float node_5645 = pow(glow,2.0);
                float4 glowColor = float4((lerp(node_5239,_SurfaceTex_var.rgb,node_5645)+lerp(node_5239,__RimColor.rgb,node_5645)),0.5);
                float3 node_5269 = mainColor;
                float colorAlpha = _SurfaceTex_var.a;
                float3 diffuseColor = (hexes+(glow*(glowColor+(node_5269*colorAlpha)))).rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_5269*_EmissionIntensity);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,_Opacity);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Opacity;
            uniform sampler2D _SurfaceTex; uniform float4 _SurfaceTex_ST;
            uniform float4 __RimColor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _NormalIntensity;
            uniform float _EmissionIntensity;
            uniform float _FlashSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _Normal_var = tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal));
                float3 normalLocal = (_Normal_var.rgb*_NormalIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_5682 = _Time + _TimeEditor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _SurfaceTex_var = tex2D(_SurfaceTex,TRANSFORM_TEX(i.uv0, _SurfaceTex));
                float4 node_514 = _Time + _TimeEditor;
                float rim = (1.0*(1.0 - abs(dot(i.normalDir,normalize(viewDirection)))));
                float3 hexes = (((6.0*saturate(((-0.7)+abs((1.0-(2.0*frac(((2.0*abs(objPos.g))+(5.0*node_5682.r))))))))*_MainTex_var.r)*_SurfaceTex_var.rgb)+(_SurfaceTex_var.rgb*((2.0+sin((1.0*(_MainTex_var.b+(node_514.b*_FlashSpeed)))))*clamp(rim,0,1))));
                float node_5693 = (sceneZ-partZ);
                float node_7506 = 0.0;
                float node_6064_if_leA = step(node_5693,node_7506);
                float node_6064_if_leB = step(node_7506,node_5693);
                float intersect = lerp((node_6064_if_leA*node_7506)+(node_6064_if_leB*(1.0 - smoothstep( 0.0, (0.5*_ProjectionParams.a), node_5693 ))),node_7506,node_6064_if_leA*node_6064_if_leB);
                float glow = clamp(max(intersect,rim),0.2,1);
                float3 mainColor = _SurfaceTex_var.rgb;
                float3 node_5239 = mainColor;
                float node_5645 = pow(glow,2.0);
                float4 glowColor = float4((lerp(node_5239,_SurfaceTex_var.rgb,node_5645)+lerp(node_5239,__RimColor.rgb,node_5645)),0.5);
                float3 node_5269 = mainColor;
                float colorAlpha = _SurfaceTex_var.a;
                float3 diffuseColor = (hexes+(glow*(glowColor+(node_5269*colorAlpha)))).rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * _Opacity,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
