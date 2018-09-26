// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PostPosterize"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_PosterizePower("PosterizePower", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_PosterizeEffectScale("PosterizeEffectScale", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			

			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float _PosterizePower;
			uniform sampler2D _TextureSample0;
			uniform float4 _TextureSample0_ST;
			uniform float _PosterizeEffectScale;

			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos ( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_TextureSample0 = i.uv.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode7 = tex2D( _TextureSample0, uv_TextureSample0 );
				float4 temp_cast_1 = (( ( tex2DNode7.r + tex2DNode7.g + tex2DNode7.b ) / 3.0 )).xxxx;
				float div5=256.0/float((int)_PosterizePower);
				float4 posterize5 = ( floor( temp_cast_1 * div5 ) / div5 );
				float4 lerpResult8 = lerp( posterize5 , tex2DNode7 , _PosterizeEffectScale);
				

				finalColor = lerpResult8;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15600
499;92;938;805;579.5784;245.9514;1.3;False;False
Node;AmplifyShaderEditor.SamplerNode;7;-708.52,-7.593163;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;3c4086a5450cfe14d8bef33a43ad0a23;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-375.4787,58.2483;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-60.84415,-66.10669;Float;False;Property;_PosterizePower;PosterizePower;0;0;Create;True;0;0;False;0;0;84.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-231.1782,55.64825;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-311.3769,442.7065;Float;False;Property;_PosterizeEffectScale;PosterizeEffectScale;2;0;Create;True;0;0;False;0;0;0.72;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;5;-47.74828,-161.9694;Float;False;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;8;120.0943,91.38595;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4980392;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;419.0668,-18.6;Float;False;True;2;Float;ASEMaterialInspector;0;2;PostPosterize;c71b220b631b6344493ea3cf87110c93;0;0;SubShader 0 Pass 0;1;False;False;True;2;False;-1;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;1;0;FLOAT4;0,0,0,0;False;0
WireConnection;15;0;7;1
WireConnection;15;1;7;2
WireConnection;15;2;7;3
WireConnection;16;0;15;0
WireConnection;5;1;16;0
WireConnection;5;0;6;0
WireConnection;8;0;5;0
WireConnection;8;1;7;0
WireConnection;8;2;9;0
WireConnection;1;0;8;0
ASEEND*/
//CHKSM=2446D727192CB6638260D6AB1864232B64D96F70