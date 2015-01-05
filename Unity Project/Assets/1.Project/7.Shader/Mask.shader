Shader "Unlit/Mask" {
  Properties {
    _MainTex ("Main Texture", 2D) = "white" {}
    _AlphaTex ("Alpha Texture", 2D) = "white" {}
  }
 
  SubShader{  
    Pass {		
      Blend SrcAlpha OneMinusSrcAlpha 
      
      SetTexture [_MainTex] {
        Combine Texture
      }
       
      SetTexture [_AlphaTex] {
        Combine previous, texture
      }
    }
  }
}
