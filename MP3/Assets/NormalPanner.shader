// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NormalPanner"
{
	Properties
	{
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Color1("Color 1", Color) = (1,1,1,0)
		_Color0("Color 0", Color) = (1,1,1,0)
		_NoisePower("Noise Power", Range( 0 , 1)) = 0
		_NormalPower("Normal Power", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:forward 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _TextureSample0;
		uniform sampler2D _TextureSample1;
		uniform float _NoisePower;
		uniform float _NormalPower;
		uniform float4 _Color0;
		uniform float4 _Color1;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float4 appendResult4 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 temp_output_12_0 = ( appendResult4 * float4( 1.15,1.15,0,0 ) );
			float2 panner2 = ( 1.0 * _Time.y * float2( 0.1,0.1 ) + temp_output_12_0.xy);
			float mulTime50 = _Time.y * 0.3;
			float4 appendResult52 = (float4(ase_worldPos.x , ase_worldPos.z , mulTime50 , 0.0));
			float simplePerlin3D51 = snoise( appendResult52.xyz );
			float temp_output_53_0 = ( simplePerlin3D51 * 0.01 );
			float4 appendResult8 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 panner9 = ( 1.0 * _Time.y * float2( 0.2,-0.05 ) + ( appendResult8 * float4( 1,1,0,0 ) ).xy);
			float4 appendResult58 = (float4(ase_worldPos.x , ase_worldPos.z , mulTime50 , 0.0));
			float simplePerlin3D56 = snoise( appendResult58.xyz );
			float temp_output_74_0 = ( 0.5 - ( _NoisePower * 0.5 ) );
			float4 appendResult64 = (float4(ase_worldPos.z , ase_worldPos.x , mulTime50 , 0.0));
			float simplePerlin3D65 = snoise( appendResult64.xyz );
			float4 appendResult62 = (float4(( ( simplePerlin3D56 * _NoisePower ) + temp_output_74_0 ) , ( ( simplePerlin3D65 * _NoisePower ) + temp_output_74_0 ) , 1.0 , 0.0));
			float4 normalizeResult66 = normalize( appendResult62 );
			float4 temp_output_11_0 = ( tex2D( _TextureSample0, ( panner2 + temp_output_53_0 ) ) + tex2D( _TextureSample1, ( panner9 + temp_output_53_0 ) ) + normalizeResult66 );
			float4 appendResult85 = (float4(( (temp_output_11_0).r * _NormalPower ) , ( (temp_output_11_0).g * _NormalPower ) , (temp_output_11_0).b , 0.0));
			float4 normalizeResult67 = normalize( appendResult85 );
			o.Normal = normalizeResult67.xyz;
			float4 appendResult45 = (float4(temp_output_12_0.xy , ( _Time.y * 0.5 ) , 0.0));
			float simplePerlin3D41 = snoise( appendResult45.xyz );
			float4 lerpResult42 = lerp( _Color0 , _Color1 , simplePerlin3D41);
			o.Albedo = lerpResult42.rgb;
			o.Metallic = 0.0;
			o.Smoothness = 1.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15900
2567;-477;1906;1130;1883.396;407.9242;1.210394;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-2928.186,33.66432;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;50;-3390.928,1030.509;Float;False;1;0;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;7;-2929.186,228.1642;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;58;-2510.142,685.7426;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-2806.033,1387.095;Float;False;Property;_NoisePower;Noise Power;4;0;Create;True;0;0;False;0;0;0.343;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;64;-2508.567,919.5419;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-2386.902,1290.346;Float;False;Constant;_Float3;Float 3;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-2686.186,259.1642;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;65;-2338.739,921.4775;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-2370.902,1379.346;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-2685.186,64.66431;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;56;-2340.315,687.6783;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-3187.933,701.1758;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;51;-3020.651,699.2137;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-2447.186,270.6642;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;1,1,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-2448.186,86.66432;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;1.15,1.15,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;74;-2203.902,1295.346;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2106.476,703.1068;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-2113.057,893.5223;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-1967.703,874.9303;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.45;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;9;-2214.788,279.7645;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,-0.05;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;70;-1950.801,705.9114;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.45;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-2792.73,712.3762;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;2;-2213.788,85.26432;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-1959.133,96.37613;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-1967.132,272.3756;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;62;-1809.079,710.7368;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;10;-1775.89,256.1327;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;None;2d50c3626a69de246b07a8977f53e22a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1774.89,61.63279;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;2d50c3626a69de246b07a8977f53e22a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;66;-1587.27,726.8964;Float;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;44;-1442.987,-291.1297;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1417.89,138.9035;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-1229.086,-282.2446;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;81;-953.4205,126.5778;Float;False;True;False;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;82;-948.5778,208.9101;Float;False;False;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1090.853,550.2206;Float;False;Property;_NormalPower;Normal Power;5;0;Create;True;0;0;False;0;0;0.3637695;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;83;-953.4206,291.2424;Float;False;False;False;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-645.8859,227.0716;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;-1009.382,-315.3399;Float;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-647.3068,127.3255;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;40;-616.7451,-608.0685;Float;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;1,1,1,0;0.1090246,0.1307471,0.1320755,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;43;-618.1617,-432.2534;Float;False;Property;_Color1;Color 1;2;0;Create;True;0;0;False;0;1,1,1,0;0.136926,0.1556871,0.1603774,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;41;-606.0265,-259.8537;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;85;-472.0771,125.8596;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NormalizeNode;67;-285.8515,-29.90821;Float;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-346.4754,426.0914;Float;False;Constant;_Smoothness;Smoothness;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-338.4754,351.0915;Float;False;Constant;_Metalness;Metalness;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;42;-248.6403,-293.6834;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;NormalPanner;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;DeferredOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;58;0;3;1
WireConnection;58;1;3;3
WireConnection;58;2;50;0
WireConnection;64;0;3;3
WireConnection;64;1;3;1
WireConnection;64;2;50;0
WireConnection;8;0;7;1
WireConnection;8;1;7;3
WireConnection;65;0;64;0
WireConnection;75;0;72;0
WireConnection;4;0;3;1
WireConnection;4;1;3;3
WireConnection;56;0;58;0
WireConnection;52;0;7;1
WireConnection;52;1;7;3
WireConnection;52;2;50;0
WireConnection;51;0;52;0
WireConnection;13;0;8;0
WireConnection;12;0;4;0
WireConnection;74;0;73;0
WireConnection;74;1;75;0
WireConnection;68;0;56;0
WireConnection;68;1;72;0
WireConnection;69;0;65;0
WireConnection;69;1;72;0
WireConnection;71;0;69;0
WireConnection;71;1;74;0
WireConnection;9;0;13;0
WireConnection;70;0;68;0
WireConnection;70;1;74;0
WireConnection;53;0;51;0
WireConnection;2;0;12;0
WireConnection;54;0;2;0
WireConnection;54;1;53;0
WireConnection;55;0;9;0
WireConnection;55;1;53;0
WireConnection;62;0;70;0
WireConnection;62;1;71;0
WireConnection;10;1;55;0
WireConnection;1;1;54;0
WireConnection;66;0;62;0
WireConnection;11;0;1;0
WireConnection;11;1;10;0
WireConnection;11;2;66;0
WireConnection;46;0;44;0
WireConnection;81;0;11;0
WireConnection;82;0;11;0
WireConnection;83;0;11;0
WireConnection;84;0;82;0
WireConnection;84;1;78;0
WireConnection;45;0;12;0
WireConnection;45;2;46;0
WireConnection;77;0;81;0
WireConnection;77;1;78;0
WireConnection;41;0;45;0
WireConnection;85;0;77;0
WireConnection;85;1;84;0
WireConnection;85;2;83;0
WireConnection;67;0;85;0
WireConnection;42;0;40;0
WireConnection;42;1;43;0
WireConnection;42;2;41;0
WireConnection;0;0;42;0
WireConnection;0;1;67;0
WireConnection;0;3;5;0
WireConnection;0;4;6;0
ASEEND*/
//CHKSM=B870B34DD1740622C1A7EE08CD519E7EB37BC66D