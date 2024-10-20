﻿Shader "Koenigz/Demo/Unlit Transparent Color" 
{
    Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader 
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        
        ZWrite Off
        Lighting Off
        Fog { Mode Off }

        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            Color [_Color]
        }
    }
}