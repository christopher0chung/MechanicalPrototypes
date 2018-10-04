// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NoiseTexShader"
{
	Properties
	{
		_Vector0("Vector 0", Vector) = (10,10,0,0)
		_Vector1("Vector 1", Vector) = (10,10,0,0)
		_Scale("Scale", Vector) = (10,10,0,0)
		_Offset("Offset", Range( -10 , 10)) = 2.744634
		_PD2("PD2", Float) = 1
		_PD1("PD1", Float) = 0.5
		_SPD2("SPD2", Float) = -2
		_SPD1("SPD1", Float) = 1.5
		_Amp("Amp", Float) = 0.3
		_OpacityOffset("OpacityOffset", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float _SPD1;
		uniform float _PD1;
		uniform float _SPD2;
		uniform float _PD2;
		uniform float _Amp;
		uniform float _Offset;
		uniform float2 _Scale;
		uniform float2 _Vector0;
		uniform float2 _Vector1;
		uniform float _OpacityOffset;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float mulTime44 = _Time.y * _SPD1;
			float mulTime51 = _Time.y * _SPD2;
			float lerpResult49 = lerp( ( ( ase_vertex3Pos.x + mulTime44 + ase_vertex3Pos.z ) * _PD1 ) , ( ( ase_vertex3Pos.x + mulTime51 + ( ase_vertex3Pos.z * -1.0 ) ) * _PD2 ) , 0.5);
			float4 appendResult36 = (float4(0.0 , ( sin( lerpResult49 ) * _Amp ) , 0.0 , 0.0));
			v.vertex.xyz += appendResult36.xyz;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float2 panner26 = ( 1.0 * _Time.y * float2( 0.1,0.1 ) + float2( 0,0 ));
			float2 uv_TexCoord8 = i.uv_texcoord * _Scale + panner26;
			float simplePerlin2D2 = snoise( uv_TexCoord8 );
			float2 panner27 = ( 1.0 * _Time.y * float2( -0.1,0.15 ) + float2( 0,0 ));
			float2 uv_TexCoord24 = i.uv_texcoord * _Vector0 + panner27;
			float simplePerlin2D25 = snoise( uv_TexCoord24 );
			float2 panner29 = ( 1.0 * _Time.y * float2( 0.2,-0.13 ) + float2( 0,0 ));
			float2 uv_TexCoord30 = i.uv_texcoord * _Vector1 + panner29;
			float simplePerlin2D31 = snoise( uv_TexCoord30 );
			float temp_output_21_0 = ( _Offset + simplePerlin2D2 + simplePerlin2D25 + simplePerlin2D31 );
			float temp_output_22_0 = asin( temp_output_21_0 );
			float clampResult63 = clamp( ( temp_output_22_0 + _OpacityOffset ) , 0.0 , 1.0 );
			c.rgb = 0;
			c.a = clampResult63;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 panner26 = ( 1.0 * _Time.y * float2( 0.1,0.1 ) + float2( 0,0 ));
			float2 uv_TexCoord8 = i.uv_texcoord * _Scale + panner26;
			float simplePerlin2D2 = snoise( uv_TexCoord8 );
			float2 panner27 = ( 1.0 * _Time.y * float2( -0.1,0.15 ) + float2( 0,0 ));
			float2 uv_TexCoord24 = i.uv_texcoord * _Vector0 + panner27;
			float simplePerlin2D25 = snoise( uv_TexCoord24 );
			float2 panner29 = ( 1.0 * _Time.y * float2( 0.2,-0.13 ) + float2( 0,0 ));
			float2 uv_TexCoord30 = i.uv_texcoord * _Vector1 + panner29;
			float simplePerlin2D31 = snoise( uv_TexCoord30 );
			float temp_output_21_0 = ( _Offset + simplePerlin2D2 + simplePerlin2D25 + simplePerlin2D31 );
			float temp_output_22_0 = asin( temp_output_21_0 );
			float3 temp_cast_0 = (temp_output_22_0).xxx;
			o.Albedo = temp_cast_0;
			float3 temp_cast_1 = (temp_output_22_0).xxx;
			o.Emission = temp_cast_1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT( UnityGI, gi );
				o.Alpha = LightingStandardCustomLighting( o, worldViewDir, gi ).a;
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
2569;-518;1837;1070;2608.027;-658.8499;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;58;-2301.007,1358.396;Float;False;Property;_SPD2;SPD2;6;0;Create;True;0;0;False;0;-2;0.005;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;50;-2025.602,1216.646;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;56;-2010.516,1472.018;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-2255.007,1048.396;Float;False;Property;_SPD1;SPD1;7;0;Create;True;0;0;False;0;1.5;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;29;-1406.46,609.9165;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,-0.13;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;26;-1402.672,114.5674;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;28;-1401.794,486.5832;Float;False;Property;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;10,10;5,5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;23;-1401.006,233.234;Float;False;Property;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;10,10;10,10;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PosVertexDataNode;38;-2007.427,919.4956;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-1807.497,1300.774;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;44;-1985.228,1068.495;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;27;-1405.672,356.5674;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,0.15;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;51;-2003.403,1365.646;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;9;-1399.306,-3.332193;Float;False;Property;_Scale;Scale;2;0;Create;True;0;0;False;0;10,10;80,80;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1223.306,-6.332177;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-1801.228,919.4956;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-1225.794,483.5832;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-1225.006,230.234;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-1804.243,1066.778;Float;False;Property;_PD1;PD1;5;0;Create;True;0;0;False;0;0.5;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1639.419,1380.928;Float;False;Property;_PD2;PD2;4;0;Create;True;0;0;False;0;1;43.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-1635.404,1233.646;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1061.891,-111.21;Float;False;Property;_Offset;Offset;3;0;Create;True;0;0;False;0;2.744634;0.1;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;25;-991.0042,228.234;Float;True;Simplex2D;1;0;FLOAT2;128,128;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-989.3046,-8.332185;Float;True;Simplex2D;1;0;FLOAT2;128,128;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1469.4,1235.684;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;31;-991.7922,481.5832;Float;True;Simplex2D;1;0;FLOAT2;128,128;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-1635.224,920.5331;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-695.1555,22.77272;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;49;-1479.183,921.9471;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ASinOpNode;22;-555.1975,236.03;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-576.0022,455.2456;Float;False;Property;_OpacityOffset;OpacityOffset;9;0;Create;True;0;0;False;0;0;0.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1272.007,1145.396;Float;False;Property;_Amp;Amp;8;0;Create;True;0;0;False;0;0.3;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;43;-1292.766,919.7582;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-295.0022,258.2456;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1082.007,957.3956;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ACosOpNode;18;-554.1557,22.77272;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;63;-166.0022,251.2456;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-899.4368,912.6556;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;NoiseTexShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;55;0;50;3
WireConnection;55;1;56;0
WireConnection;44;0;57;0
WireConnection;51;0;58;0
WireConnection;8;0;9;0
WireConnection;8;1;26;0
WireConnection;40;0;38;1
WireConnection;40;1;44;0
WireConnection;40;2;38;3
WireConnection;30;0;28;0
WireConnection;30;1;29;0
WireConnection;24;0;23;0
WireConnection;24;1;27;0
WireConnection;52;0;50;1
WireConnection;52;1;51;0
WireConnection;52;2;55;0
WireConnection;25;0;24;0
WireConnection;2;0;8;0
WireConnection;54;0;52;0
WireConnection;54;1;53;0
WireConnection;31;0;30;0
WireConnection;46;0;40;0
WireConnection;46;1;47;0
WireConnection;21;0;20;0
WireConnection;21;1;2;0
WireConnection;21;2;25;0
WireConnection;21;3;31;0
WireConnection;49;0;46;0
WireConnection;49;1;54;0
WireConnection;22;0;21;0
WireConnection;43;0;49;0
WireConnection;61;0;22;0
WireConnection;61;1;62;0
WireConnection;59;0;43;0
WireConnection;59;1;60;0
WireConnection;18;0;21;0
WireConnection;63;0;61;0
WireConnection;36;1;59;0
WireConnection;0;0;22;0
WireConnection;0;2;22;0
WireConnection;0;9;63;0
WireConnection;0;11;36;0
ASEEND*/
//CHKSM=D556628169B01657A1B3840F0A24B307958F3901