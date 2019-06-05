using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSC305A1P1 : MonoBehaviour
{
    Texture2D RayTracingResult;

    //Initialize Colour variables
    Color BackgroundColor = Color.grey;
    Color LightColor = Color.cyan;
    Color PixelColor = Color.black;
    Color AmbientColor = new Color(0.1f, 0.1f, 0.03f);
    float diffuseStrength = 0.5f;
    float specularStrength = 0.2f;
    float specularPower = 10;

    // Start is called before the first frame update
    void Start()
    {
      //Initialization
      Camera this_camera = gameObject.GetComponent<Camera>();
      Debug.Assert(this_camera);
      int pixel_width = this_camera.pixelWidth;
      int pixel_height = this_camera.pixelHeight;
      RayTracingResult = new Texture2D(pixel_width, pixel_height);
      float ViewportWidth = 3;
      float ViewportHeight = ViewportWidth / pixel_width * pixel_height;

      Vector3 SphereCenter = new Vector3(0,0,10);
      Vector3 RayOrigin = Vector3.zero;
      float radius = 1;

      for (int i = 0; i < pixel_width; ++i){
        for (int j = 0; j < pixel_height; ++j){

          Vector3 rayDirection = Vector3.forward;
          rayDirection.x = (i - pixel_width/2)/(float)pixel_width/2 * ViewportWidth/2;
          rayDirection.y = (j - pixel_height/2)/(float)pixel_height/2* ViewportHeight/2;
          rayDirection.Normalize();

          RayTracingResult.SetPixel(i, j, BackgroundColor);

          Vector3 OC = SphereCenter - RayOrigin;
          float OG = Vector3.Dot(OC, rayDirection);
          float rad = radius*radius;
          float OCSquare = Vector3.Dot(OC,OC);
          float CG = Mathf.Sqrt(OCSquare - OG*OG);

          float PG = Mathf.Sqrt(rad - CG*CG);
          float OP = OG - PG;
          Vector3 P = RayOrigin+(OP*rayDirection);

          Vector3 lightDirection = new Vector3(1,1,-1);

          //shading
          if(radius>CG){
            Vector3 normal = P - SphereCenter;
            normal.Normalize();

            PixelColor = AmbientColor;
            float diffuse = Vector3.Dot(normal, lightDirection) * diffuseStrength;
            PixelColor += LightColor * diffuse;

            Vector3 V = rayDirection * -1;
            Vector3 H = V + lightDirection;
            H.Normalize();

            float blinn = Vector3.Dot(H, normal);
            float specular = Mathf.Pow(blinn, specularPower) * specularStrength;
            PixelColor += LightColor * specular;

            RayTracingResult.SetPixel(i, j, PixelColor);

          }
        }
      }
      RayTracingResult.Apply();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Show the generated ray tracing image on screen
        Graphics.Blit(RayTracingResult, destination);
    }
}
