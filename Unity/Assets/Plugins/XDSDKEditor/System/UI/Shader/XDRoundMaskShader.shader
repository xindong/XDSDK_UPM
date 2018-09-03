// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/XDRoundMaskShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
        

        _TopLeftRoundedRadius("Top Left Rounded Radius", Range(0, 256)) = 64
        _TopRightRoundedRadius("Top Right Rounded Radius", Range(0, 256)) = 64
        _BottomLeftRoundedRadius("Botton Left Rounded Radius", Range(0, 256)) = 64
        _BottomRightRoundedRadius("Bottom Right Rounded Radius", Range(0, 256)) = 64
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask[_ColorMask]

        Pass
        {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "UnityUI.cginc"

#pragma multi_compile __ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
float4 vertex   :
                POSITION;
float4 color    :
                COLOR;
float2 texcoord :
                TEXCOORD0;
            };

            struct v2f
            {
float4 vertex   :
                SV_POSITION;
fixed4 color :
                COLOR;
half2 texcoord  :
                TEXCOORD0;
float4 worldPosition :
                TEXCOORD1;
            };

            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            
            float _RoundedRadius;

            float _TopLeftRoundedRadius;
            float _TopRightRoundedRadius;
            float _BottomLeftRoundedRadius;
            float _BottomRightRoundedRadius;

            float4 _MainTex_TexelSize;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.worldPosition = IN.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = IN.texcoord;

#ifdef UNITY_HALF_TEXEL_OFFSET
                OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
#endif

                OUT.color = IN.color * _Color;
                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

#ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
#endif

                float width = _MainTex_TexelSize.z;
                float height = _MainTex_TexelSize.w;

                float x = IN.texcoord.x * width;
                float y = IN.texcoord.y * height;

                float tlr = _TopLeftRoundedRadius;
                float trr = _TopRightRoundedRadius;
                float blr = _BottomLeftRoundedRadius;
                float brr = _BottomRightRoundedRadius;
                
                //左下角
                if (x < blr && y < blr)
                {
                    if ((x - blr) * (x - blr) + (y - blr) * (y - blr) > blr * blr)
                        color.a = 0;
                }

                //左上角
                if (x < tlr && y > (height - tlr))
                {
                    if ((x - tlr) * (x - tlr) + (y - (height - tlr)) * (y - (height - tlr)) > tlr * tlr)
                        color.a = 0;
                }

                //右下角
                if (x > (width - brr) && y < brr)
                {
                    if ((x - (width - brr)) * (x - (width - brr)) + (y - brr) * (y - brr) > brr * brr)
                        color.a = 0;
                }

                //右上角
                if (x > (width - trr) && y > (height - trr))
                {
                    if ((x - (width - trr)) * (x - (width - trr)) + (y - (height - trr)) * (y - (height - trr)) > trr * trr)
                        color.a = 0;
                }

                return color;
            }
            ENDCG
        }
    }
}
