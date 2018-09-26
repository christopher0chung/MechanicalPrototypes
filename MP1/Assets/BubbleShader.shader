// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.03921569,fgcg:0.09019608,fgcb:0.07843138,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:0,x:34414,y:32442,varname:node_0,prsc:2|diff-5610-OUT,spec-75-OUT,gloss-76-OUT,emission-9416-RGB,transm-29-OUT,lwrap-29-OUT,alpha-9416-A,refract-14-OUT,olwid-7039-OUT,olcol-7135-RGB,voffset-6413-OUT;n:type:ShaderForge.SFN_Slider,id:13,x:33580,y:32752,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:_RefractionIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1854701,max:1;n:type:ShaderForge.SFN_Multiply,id:14,x:34145,y:32725,varname:node_14,prsc:2|A-16-OUT,B-220-OUT;n:type:ShaderForge.SFN_ComponentMask,id:16,x:33975,y:32652,varname:node_16,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-27-OUT;n:type:ShaderForge.SFN_TexCoord,id:26,x:33379,y:32505,varname:node_26,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:27,x:33550,y:32566,varname:node_27,prsc:2|A-26-UVOUT,B-28-OUT;n:type:ShaderForge.SFN_Vector1,id:28,x:33379,y:32662,varname:node_28,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:29,x:34145,y:32578,varname:node_29,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:75,x:34145,y:32432,varname:node_75,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:76,x:34145,y:32487,varname:node_76,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Multiply,id:220,x:33975,y:32804,varname:node_220,prsc:2|A-13-OUT,B-221-OUT;n:type:ShaderForge.SFN_Vector1,id:221,x:33737,y:32831,varname:node_221,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:5610,x:34145,y:32368,varname:node_5610,prsc:2,v1:0.2;n:type:ShaderForge.SFN_FragmentPosition,id:9627,x:32615,y:33335,varname:node_9627,prsc:2;n:type:ShaderForge.SFN_Time,id:5122,x:32949,y:33333,varname:node_5122,prsc:2;n:type:ShaderForge.SFN_Vector1,id:7860,x:33265,y:33004,cmnt:Vert Pd,varname:node_7860,prsc:2,v1:6;n:type:ShaderForge.SFN_Multiply,id:4446,x:33265,y:33069,varname:node_4446,prsc:2|A-7860-OUT,B-9627-Y;n:type:ShaderForge.SFN_Multiply,id:9947,x:33265,y:33194,varname:node_9947,prsc:2|A-5122-T,B-5127-OUT;n:type:ShaderForge.SFN_Vector1,id:5127,x:33265,y:33333,cmnt:Vert Freq,varname:node_5127,prsc:2,v1:5;n:type:ShaderForge.SFN_Vector1,id:3866,x:33271,y:33489,cmnt:Horz Pd,varname:node_3866,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:3079,x:33270,y:33551,varname:node_3079,prsc:2|A-3866-OUT,B-5818-OUT;n:type:ShaderForge.SFN_Multiply,id:6043,x:33269,y:33676,varname:node_6043,prsc:2|A-5122-T,B-8142-OUT;n:type:ShaderForge.SFN_Vector1,id:8142,x:33270,y:33818,cmnt:Horz Freq,varname:node_8142,prsc:2,v1:20;n:type:ShaderForge.SFN_Add,id:6941,x:33444,y:33536,varname:node_6941,prsc:2|A-3079-OUT,B-6043-OUT;n:type:ShaderForge.SFN_Sin,id:9084,x:33608,y:33536,varname:node_9084,prsc:2|IN-6941-OUT;n:type:ShaderForge.SFN_Multiply,id:9498,x:33783,y:33536,varname:node_9498,prsc:2|A-9084-OUT,B-3495-OUT;n:type:ShaderForge.SFN_Vector1,id:3495,x:33782,y:33672,cmnt:Horz Amp,varname:node_3495,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Divide,id:3742,x:33955,y:33536,varname:node_3742,prsc:2|A-9498-OUT,B-7333-X;n:type:ShaderForge.SFN_Vector1,id:3474,x:34275,y:33376,varname:node_3474,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:6413,x:34192,y:32912,varname:node_6413,prsc:2|A-3742-OUT,B-4631-OUT,C-3474-OUT;n:type:ShaderForge.SFN_ObjectScale,id:7333,x:33879,y:33337,varname:node_7333,prsc:2,rcp:False;n:type:ShaderForge.SFN_Divide,id:4631,x:33975,y:33051,varname:node_4631,prsc:2|A-5125-OUT,B-7333-Y;n:type:ShaderForge.SFN_Multiply,id:5125,x:33783,y:33051,varname:node_5125,prsc:2|A-1631-OUT,B-5946-OUT;n:type:ShaderForge.SFN_Vector1,id:5946,x:33768,y:33197,cmnt:Vert Amp,varname:node_5946,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Sin,id:1631,x:33607,y:33052,varname:node_1631,prsc:2|IN-154-OUT;n:type:ShaderForge.SFN_Add,id:154,x:33441,y:33052,varname:node_154,prsc:2|A-4446-OUT,B-9947-OUT;n:type:ShaderForge.SFN_Add,id:5818,x:32917,y:33519,varname:node_5818,prsc:2|A-9627-X,B-9732-OUT;n:type:ShaderForge.SFN_Color,id:9416,x:33935,y:32107,ptovrint:False,ptlb:Emissive Color,ptin:_EmissiveColor,varname:node_9416,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.397,c2:0.397,c3:0.397,c4:1;n:type:ShaderForge.SFN_Slider,id:7039,x:34192,y:33066,ptovrint:False,ptlb:Line Thickness,ptin:_LineThickness,varname:node_7039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.015,max:1;n:type:ShaderForge.SFN_Color,id:7135,x:34192,y:33168,ptovrint:False,ptlb:Line Color,ptin:_LineColor,varname:node_7135,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.107116,c2:0.122,c3:0.1133775,c4:1;n:type:ShaderForge.SFN_Multiply,id:9732,x:32615,y:33482,varname:node_9732,prsc:2|A-9627-Z,B-3183-OUT,C-9627-Y;n:type:ShaderForge.SFN_Slider,id:3183,x:32458,y:33654,ptovrint:False,ptlb:Z_OffsetMultiplier,ptin:_Z_OffsetMultiplier,varname:node_3183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:100,max:100;proporder:13-9416-7039-7135-3183;pass:END;sub:END;*/

Shader "Shader Forge/BubbleVertDisp" {
    Properties {
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 0.1854701
        _EmissiveColor ("Emissive Color", Color) = (0.397,0.397,0.397,1)
        _LineThickness ("Line Thickness", Range(0, 1)) = 0.015
        _LineColor ("Line Color", Color) = (0.107116,0.122,0.1133775,1)
        _Z_OffsetMultiplier ("Z_OffsetMultiplier", Range(0, 100)) = 100
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _LineThickness;
            uniform float4 _LineColor;
            uniform float _Z_OffsetMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_5122 = _Time + _TimeEditor;
                v.vertex.xyz += float3(((sin(((10.0*(mul(unity_ObjectToWorld, v.vertex).r+(mul(unity_ObjectToWorld, v.vertex).b*_Z_OffsetMultiplier*mul(unity_ObjectToWorld, v.vertex).g)))+(node_5122.g*20.0)))*0.01)/objScale.r),((sin(((6.0*mul(unity_ObjectToWorld, v.vertex).g)+(node_5122.g*5.0)))*0.1)/objScale.g),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal*_LineThickness,1) );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                return fixed4(_LineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float _RefractionIntensity;
            uniform float4 _EmissiveColor;
            uniform float _Z_OffsetMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_5122 = _Time + _TimeEditor;
                v.vertex.xyz += float3(((sin(((10.0*(mul(unity_ObjectToWorld, v.vertex).r+(mul(unity_ObjectToWorld, v.vertex).b*_Z_OffsetMultiplier*mul(unity_ObjectToWorld, v.vertex).g)))+(node_5122.g*20.0)))*0.01)/objScale.r),((sin(((6.0*mul(unity_ObjectToWorld, v.vertex).g)+(node_5122.g*5.0)))*0.1)/objScale.g),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_14 = ((i.uv0*1.0).r*(_RefractionIntensity*0.2));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(node_14,node_14);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.8;
                float perceptualRoughness = 1.0 - 0.8;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_5610 = 0.2;
                float3 diffuseColor = float3(node_5610,node_5610,node_5610); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_29 = 1.0;
                float3 w = float3(node_29,node_29,node_29)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_29,node_29,node_29);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
                float3 emissive = _EmissiveColor.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,_EmissiveColor.a),1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float _RefractionIntensity;
            uniform float4 _EmissiveColor;
            uniform float _Z_OffsetMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_5122 = _Time + _TimeEditor;
                v.vertex.xyz += float3(((sin(((10.0*(mul(unity_ObjectToWorld, v.vertex).r+(mul(unity_ObjectToWorld, v.vertex).b*_Z_OffsetMultiplier*mul(unity_ObjectToWorld, v.vertex).g)))+(node_5122.g*20.0)))*0.01)/objScale.r),((sin(((6.0*mul(unity_ObjectToWorld, v.vertex).g)+(node_5122.g*5.0)))*0.1)/objScale.g),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float node_14 = ((i.uv0*1.0).r*(_RefractionIntensity*0.2));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(node_14,node_14);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.8;
                float perceptualRoughness = 1.0 - 0.8;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_5610 = 0.2;
                float3 diffuseColor = float3(node_5610,node_5610,node_5610); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_29 = 1.0;
                float3 w = float3(node_29,node_29,node_29)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_29,node_29,node_29);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * _EmissiveColor.a,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Z_OffsetMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_5122 = _Time + _TimeEditor;
                v.vertex.xyz += float3(((sin(((10.0*(mul(unity_ObjectToWorld, v.vertex).r+(mul(unity_ObjectToWorld, v.vertex).b*_Z_OffsetMultiplier*mul(unity_ObjectToWorld, v.vertex).g)))+(node_5122.g*20.0)))*0.01)/objScale.r),((sin(((6.0*mul(unity_ObjectToWorld, v.vertex).g)+(node_5122.g*5.0)))*0.1)/objScale.g),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
