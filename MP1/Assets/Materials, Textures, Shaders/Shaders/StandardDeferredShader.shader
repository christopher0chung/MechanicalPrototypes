// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "StandardDeferredShader"
{
	Properties
	{
		_NormalTexture1("Normal Texture 1", 2D) = "bump" {}
		_AlbedoColor("Albedo Color", Color) = (0,0,0,0)
		_EmissiveColor("Emissive Color", Color) = (0,0,0,0)
		_NormalTexture0("Normal Texture 0", 2D) = "bump" {}
		_EmissiveBoost("EmissiveBoost", Range( 1 , 10)) = 1
		_NormalBlend("NormalBlend", Range( 0 , 1)) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_NormalStrength("NormalStrength", Range( 0 , 2)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:forward 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalStrength;
		uniform sampler2D _NormalTexture0;
		uniform float4 _NormalTexture0_ST;
		uniform sampler2D _NormalTexture1;
		uniform float4 _NormalTexture1_ST;
		uniform float _NormalBlend;
		uniform float4 _AlbedoColor;
		uniform float4 _EmissiveColor;
		uniform float _EmissiveBoost;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalTexture0 = i.uv_texcoord * _NormalTexture0_ST.xy + _NormalTexture0_ST.zw;
			float2 uv_NormalTexture1 = i.uv_texcoord * _NormalTexture1_ST.xy + _NormalTexture1_ST.zw;
			float3 lerpResult3 = lerp( UnpackScaleNormal( tex2D( _NormalTexture0, uv_NormalTexture0 ), _NormalStrength ) , UnpackScaleNormal( tex2D( _NormalTexture1, uv_NormalTexture1 ), _NormalStrength ) , _NormalBlend);
			o.Normal = lerpResult3;
			o.Albedo = _AlbedoColor.rgb;
			o.Emission = ( _EmissiveColor * _EmissiveBoost ).rgb;
			o.Metallic = 0.0;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
1927;29;1906;1004;1412.389;400.5859;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-1215.389,-95.58591;Float;False;Property;_NormalStrength;NormalStrength;7;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-768.9889,133.85;Float;False;Property;_NormalBlend;NormalBlend;5;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-721.3439,205.5877;Float;False;Property;_EmissiveColor;Emissive Color;2;0;Create;True;0;0;False;0;0,0,0,0;1,0.6042768,0.4292453,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-773.389,376.4141;Float;False;Property;_EmissiveBoost;EmissiveBoost;4;0;Create;True;0;0;False;0;1;1.29;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-883.3248,-54.78236;Float;True;Property;_NormalTexture1;Normal Texture 1;0;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-883.3248,-244.7823;Float;True;Property;_NormalTexture0;Normal Texture 0;3;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-253.389,96.41409;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;3;-527,22.5;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;2;-295,-139.5;Float;False;Property;_AlbedoColor;Albedo Color;1;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-357.3439,255.5877;Float;False;Constant;_Metalness;Metalness;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-357.3439,331.5877;Float;False;Property;_Smoothness;Smoothness;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;StandardDeferredShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;DeferredOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;5;10;0
WireConnection;1;5;10;0
WireConnection;11;0;6;0
WireConnection;11;1;12;0
WireConnection;3;0;1;0
WireConnection;3;1;4;0
WireConnection;3;2;5;0
WireConnection;0;0;2;0
WireConnection;0;1;3;0
WireConnection;0;2;11;0
WireConnection;0;3;7;0
WireConnection;0;4;8;0
ASEEND*/
//CHKSM=6891AA413E28A417D62134B1436FC5F9482CD4AB