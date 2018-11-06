// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PostPosterize"
{
	Properties
	{
		_ColorPosterizePower("ColorPosterizePower", Float) = 0
		_PosterizePower("PosterizePower", Float) = 0
		_MainTex("MainTex", 2D) = "white" {}
		_RealismPower("RealismPower", Range( 0 , 1)) = 0
		_NormalizePower("NormalizePower", Range( 0 , 1)) = 0
		_ColorPostToBWPost("ColorPost To BWPost", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform float _ColorPosterizePower;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _NormalizePower;
			uniform float _PosterizePower;
			uniform float _ColorPostToBWPost;
			uniform float _RealismPower;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float2 uv_MainTex = i.ase_texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode7 = tex2D( _MainTex, uv_MainTex );
				float4 normalizeResult18 = normalize( tex2DNode7 );
				float4 lerpResult22 = lerp( tex2DNode7 , normalizeResult18 , _NormalizePower);
				float div30=256.0/float((int)_ColorPosterizePower);
				float4 posterize30 = ( floor( lerpResult22 * div30 ) / div30 );
				float4 temp_cast_2 = (( ( tex2DNode7.r + tex2DNode7.g + tex2DNode7.b ) / 3.0 )).xxxx;
				float div5=256.0/float((int)_PosterizePower);
				float4 posterize5 = ( floor( temp_cast_2 * div5 ) / div5 );
				float4 lerpResult8 = lerp( posterize30 , posterize5 , _ColorPostToBWPost);
				float4 lerpResult19 = lerp( lerpResult8 , tex2DNode7 , _RealismPower);
				
				
				finalColor = lerpResult19;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15600
716;63;1837;1010;1803.228;1025.781;1.9;True;False
Node;AmplifyShaderEditor.SamplerNode;7;-838.5197,-82.99317;Float;True;Property;_MainTex;MainTex;2;0;Create;True;0;0;False;0;None;3c4086a5450cfe14d8bef33a43ad0a23;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;24;-828.5415,-526.3624;Float;False;793.4;365.4;Color Flattening and Posterize;5;30;31;21;18;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;25;-547.6788,-103.5518;Float;False;581.7306;266.845;Posterize Light/Dark;4;15;16;5;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-804.4133,-372.0401;Float;False;Property;_NormalizePower;NormalizePower;4;0;Create;True;0;0;False;0;0;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-497.6788,-53.55169;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;18;-804.478,-442.4016;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-496.0601,-246.5135;Float;False;Property;_ColorPosterizePower;ColorPosterizePower;0;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;16;-296.1782,-53.55175;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;32;78.02316,-109.414;Float;False;624.2712;279.7205;Color to BW Lerp;2;9;8;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;22;-500.342,-465.9621;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4980392;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-359.8443,48.29324;Float;False;Property;_PosterizePower;PosterizePower;1;0;Create;True;0;0;False;0;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;115.0232,73.50641;Float;False;Property;_ColorPostToBWPost;ColorPost To BWPost;5;0;Create;True;0;0;False;0;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;30;-207.3638,-467.1764;Float;False;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;33;720.9861,245.3386;Float;False;626.8711;308.3206;Processed to Literal Lerp;2;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosterizeNode;5;-156.9482,-52.76945;Float;False;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;770.9861,438.6586;Float;False;Property;_RealismPower;RealismPower;3;0;Create;True;0;0;False;0;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;450.2945,-59.41405;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4980392;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;19;1082.857,295.3382;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4980392;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;34;1393.067,297.1999;Float;False;True;2;Float;ASEMaterialInspector;0;1;PostPosterize;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;15;0;7;1
WireConnection;15;1;7;2
WireConnection;15;2;7;3
WireConnection;18;0;7;0
WireConnection;16;0;15;0
WireConnection;22;0;7;0
WireConnection;22;1;18;0
WireConnection;22;2;21;0
WireConnection;30;1;22;0
WireConnection;30;0;31;0
WireConnection;5;1;16;0
WireConnection;5;0;6;0
WireConnection;8;0;30;0
WireConnection;8;1;5;0
WireConnection;8;2;9;0
WireConnection;19;0;8;0
WireConnection;19;1;7;0
WireConnection;19;2;20;0
WireConnection;34;0;19;0
ASEEND*/
//CHKSM=AD1683AA1A875D80EFA4880B69B974A1DD8AEF19