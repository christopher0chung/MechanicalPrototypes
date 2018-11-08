// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PostPosterize"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_PosterizeColor("Posterize Color", Float) = 0
		_PosterizeBW("Posterize B/W", Float) = 0
		_ColorBWLerp("Color - B/W Lerp", Range( 0 , 1)) = 0
		_RealismPower("RealismPower", Range( 0 , 1)) = 0
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

			uniform float _PosterizeColor;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _PosterizeBW;
			uniform float _ColorBWLerp;
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
				float div36=256.0/float((int)_PosterizeColor);
				float4 posterize36 = ( floor( tex2DNode7 * div36 ) / div36 );
				float4 temp_cast_2 = (( ( tex2DNode7.r + tex2DNode7.g + tex2DNode7.b ) / 3.0 )).xxxx;
				float div5=256.0/float((int)_PosterizeBW);
				float4 posterize5 = ( floor( temp_cast_2 * div5 ) / div5 );
				float4 lerpResult38 = lerp( posterize36 , posterize5 , _ColorBWLerp);
				float4 lerpResult19 = lerp( lerpResult38 , tex2DNode7 , _RealismPower);
				
				
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
1269;92;668;655;118.5551;84.98596;2.483914;True;False
Node;AmplifyShaderEditor.SamplerNode;7;-443.89,0.4415331;Float;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;None;3c4086a5450cfe14d8bef33a43ad0a23;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;25;-12.76775,-19.29896;Float;False;581.7306;266.845;Posterize Light/Dark;4;15;6;5;40;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;37.23235,30.70125;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;35;-12.73186,-303.7675;Float;False;581.7306;266.845;Posterize Color;2;36;37;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;37;154.9382,-137.049;Float;False;Property;_PosterizeColor;Posterize Color;1;0;Create;True;0;0;False;0;0;10.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;175.0667,132.5464;Float;False;Property;_PosterizeBW;Posterize B/W;2;0;Create;True;0;0;False;0;0;43;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;40;207.8252,30.25024;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;36;355.8488,-240.0985;Float;False;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;39;317.4375,276.582;Float;False;Property;_ColorBWLerp;Color - B/W Lerp;3;0;Create;True;0;0;False;0;0;0.23;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;33;720.9861,245.3386;Float;False;626.8711;308.3206;Processed to Literal Lerp;2;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosterizeNode;5;375.9774,29.49723;Float;False;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;38;678.0145,5.509968;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;770.9861,438.6586;Float;False;Property;_RealismPower;RealismPower;4;0;Create;True;0;0;False;0;0;0.57;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;1082.857,295.3382;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.4980392;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;34;1373.196,294.716;Float;False;True;2;Float;ASEMaterialInspector;0;1;PostPosterize;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;15;0;7;1
WireConnection;15;1;7;2
WireConnection;15;2;7;3
WireConnection;40;0;15;0
WireConnection;36;1;7;0
WireConnection;36;0;37;0
WireConnection;5;1;40;0
WireConnection;5;0;6;0
WireConnection;38;0;36;0
WireConnection;38;1;5;0
WireConnection;38;2;39;0
WireConnection;19;0;38;0
WireConnection;19;1;7;0
WireConnection;19;2;20;0
WireConnection;34;0;19;0
ASEEND*/
//CHKSM=D35172E08805A746D4F1806245ACFB0FAD30A1DA