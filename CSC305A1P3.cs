using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSC305A1P3 : MonoBehaviour
{
    Texture2D RayTracingResult;
    public Texture2D textureOnSquare;

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
      Vector3 RayOrigin = Vector3.zero;
      Vector3 viewPointCenter = Vector3.forward;

      Color BackgroundColor = Color.grey;

      //Initializing vertices
      List<Vector3> vertices = new List<Vector3>();
      vertices.Add(new Vector3(-4, -2.8f, 10)); //0
      vertices.Add(new Vector3(-4, 2.8f, 10));  //1
      vertices.Add(new Vector3(0, -2.8f, 9));   //2
      vertices.Add(new Vector3(0, 2.8f, 9));    //3
      vertices.Add(new Vector3(4, -2.8f, 10));  //4
      vertices.Add(new Vector3(4, 2.8f, 10));   //5

      //Initializing UV texture mapping
      List<Vector2> UVs = new List<Vector2>();
      UVs.Add(new Vector2(0, 0)); //0
      UVs.Add(new Vector2(0, 1)); //1
      UVs.Add(new Vector2(1, 0)); //2
      UVs.Add(new Vector2(1, 1)); //3
      UVs.Add(new Vector2(0, 0)); //4
      UVs.Add(new Vector2(0, 1)); //5

      //For each pixel
      for (int i = 0; i < pixel_width; i++){
        for (int j = 0; j < pixel_height; j++){

          //ray casting / base colour setting
          Vector3 rayDirection = viewPointCenter;
          rayDirection.x = (i - (pixel_width/2)) / ((float)pixel_width/2) * (ViewportWidth/2);
          rayDirection.y = (j - (pixel_height/2)) / ((float)pixel_height/2) * (ViewportHeight/2);
          rayDirection.Normalize();
          Color pixelColour = BackgroundColor;

          for (int tNum = 0; tNum < 4; tNum++){
              //Triangle 1 at (0,1,2)
              if(tNum == 0){
                  int v1 = 0;
                  int v2 = 1;
                  int v3 = 2;

                  //tracing the array
                  Vector3 N = Vector3.Cross((vertices[v2] - vertices[v1]), (vertices[v3] - vertices[v1]));
                  float D = Vector3.Dot(vertices[v1], N);
                  float T = (Vector3.Dot(N,RayOrigin) + D) / Vector3.Dot(N, rayDirection);
                  Vector3 P = RayOrigin + (T * rayDirection);

                  //Calculating Sides
                  Vector3 E1 = vertices[v2] - vertices[v1];
                  Vector3 VP1 = P - vertices[v1];
                  Vector3 C1 = Vector3.Cross(E1, VP1);

                  Vector3 E2 = vertices[v3] - vertices[v2];
                  Vector3 VP2 = P - vertices[v2];
                  Vector3 C2 = Vector3.Cross(E2, VP2);

                  Vector3 E3 = vertices[v1] - vertices[v3];
                  Vector3 VP3 = P - vertices[v3];
                  Vector3 C3 = Vector3.Cross(E3, VP3);

                  //Triangle intersection
                  if(Vector3.Dot(N,C1)>0 && Vector3.Dot(N,C2)>0 && Vector3.Dot(N,C3)>0){
                    //Difference between points
                    float val1 = C2.magnitude/N.magnitude;
                    float val2 = C3.magnitude/N.magnitude;
                    float val3 = C1.magnitude/N.magnitude;

                    //Set texture values to mapped coordinates
                    int xCol = textureOnSquare.width;
                    int yCol = textureOnSquare.height;
                    Vector2 UVMap = (val1 * UVs[v1]) + (val2 * UVs[v2]) + (val3 * UVs[v3]);
                    Color currentPixelColour = textureOnSquare.GetPixel((int)(UVMap.x * xCol), (int)(UVMap.y * yCol));
                    pixelColour = currentPixelColour;
                  }
              }

              //Triangle 2 at (2,1,3)
              if (tNum == 1){
                  int v1 = 2;
                  int v2 = 1;
                  int v3 = 3;

                  //tracing the array
                  Vector3 N = Vector3.Cross((vertices[v2] - vertices[v1]), (vertices[v3] - vertices[v1]));
                  float D = Vector3.Dot(vertices[v1], N);
                  float T = (Vector3.Dot(N, RayOrigin) + D) / Vector3.Dot(N, rayDirection);
                  Vector3 P = RayOrigin + (T * rayDirection);

                  //Calculating Sides
                  Vector3 E1 = vertices[v2] - vertices[v1];
                  Vector3 VP1 = P - vertices[v1];
                  Vector3 C1 = Vector3.Cross(E1, VP1);

                  Vector3 E2 = vertices[v3] - vertices[v2];
                  Vector3 VP2 = P - vertices[v2];
                  Vector3 C2 = Vector3.Cross(E2, VP2);

                  Vector3 E3 = vertices[v1] - vertices[v3];
                  Vector3 VP3 = P - vertices[v3];
                  Vector3 C3 = Vector3.Cross(E3, VP3);

                  //Triangle intersection
                  if (Vector3.Dot(N, C1)>0 && Vector3.Dot(N, C2)>0 && Vector3.Dot(N, C3)>0){
                    //Difference between points
                    float val1 = C2.magnitude/N.magnitude;
                    float val2 = C3.magnitude/N.magnitude;
                    float val3 = C1.magnitude/N.magnitude;

                    //Set texture values to mapped coordinates
                    int xCol = textureOnSquare.width;
                    int yCol = textureOnSquare.height;
                    Vector2 UVMap = (val1 * UVs[v1]) + (val2 * UVs[v2]) + (val3 * UVs[v3]);
                    Color currentPixelColour = textureOnSquare.GetPixel((int)(UVMap.x * xCol), (int)(UVMap.y * yCol));
                    pixelColour = currentPixelColour;
                  }
              }

              //Triangle 3 at (2,3,5)
              if (tNum == 2){
                  int v1 = 2;
                  int v2 = 3;
                  int v3 = 5;

                  //tracing the array
                  Vector3 N = Vector3.Cross((vertices[v2] - vertices[v1]), (vertices[v3] - vertices[v1]));
                  float D = Vector3.Dot(vertices[v1], N);
                  float T = (Vector3.Dot(N, RayOrigin) + D) / Vector3.Dot(N, rayDirection);
                  Vector3 P = RayOrigin + (T * rayDirection);

                  //Calculating Sides
                  Vector3 E1 = vertices[v2] - vertices[v1];
                  Vector3 VP1 = P - vertices[v1];
                  Vector3 C1 = Vector3.Cross(E1, VP1);

                  Vector3 E2 = vertices[v3] - vertices[v2];
                  Vector3 VP2 = P - vertices[v2];
                  Vector3 C2 = Vector3.Cross(E2, VP2);

                  Vector3 E3 = vertices[v1] - vertices[v3];
                  Vector3 VP3 = P - vertices[v3];
                  Vector3 C3 = Vector3.Cross(E3, VP3);

                  //Triangle intersection
                  if (Vector3.Dot(N, C1)>0 && Vector3.Dot(N, C2)>0 && Vector3.Dot(N, C3)>0){
                    //Difference between points
                    float val1 = C2.magnitude/N.magnitude;
                    float val2 = C3.magnitude/N.magnitude;
                    float val3 = C1.magnitude/N.magnitude;

                    //Set texture values to mapped coordinates
                    int xCol = textureOnSquare.width;
                    int yCol = textureOnSquare.height;
                    Vector2 UVMap = (val1 * UVs[v1]) + (val2 * UVs[v2]) + (val3 * UVs[v3]);
                    Color currentPixelColour = textureOnSquare.GetPixel((int)(UVMap.x * xCol), (int)(UVMap.y * yCol));
                    pixelColour = currentPixelColour;
                  }
              }

              //Triangle 4 at (2,5,4)
              if (tNum == 3){
                  int v1 = 2;
                  int v2 = 5;
                  int v3 = 4;

                  //tracing the array
                  Vector3 N = Vector3.Cross((vertices[v2] - vertices[v1]), (vertices[v3] - vertices[v1]));
                  float D = Vector3.Dot(vertices[v1], N);
                  float T = (Vector3.Dot(N, RayOrigin) + D) / Vector3.Dot(N, rayDirection);
                  Vector3 P = RayOrigin + (T * rayDirection);

                  //Calculating Sides
                  Vector3 E1 = vertices[v2] - vertices[v1];
                  Vector3 VP1 = P - vertices[v1];
                  Vector3 C1 = Vector3.Cross(E1, VP1);

                  Vector3 E2 = vertices[v3] - vertices[v2];
                  Vector3 VP2 = P - vertices[v2];
                  Vector3 C2 = Vector3.Cross(E2, VP2);

                  Vector3 E3 = vertices[v1] - vertices[v3];
                  Vector3 VP3 = P - vertices[v3];
                  Vector3 C3 = Vector3.Cross(E3, VP3);

                  //Triangle intersection
                  if (Vector3.Dot(N,C1)>0 && Vector3.Dot(N,C2)>0 && Vector3.Dot(N,C3)>0){
                    //Difference between points
                    float val1 = C2.magnitude/N.magnitude;
                    float val2 = C3.magnitude/N.magnitude;
                    float val3 = C1.magnitude/N.magnitude;

                    //Set texture values to mapped coordinates
                    int xCol = textureOnSquare.width;
                    int yCol = textureOnSquare.height;
                    Vector2 UVMap = (val1 * UVs[v1]) + (val2 * UVs[v2]) + (val3 * UVs[v3]);
                    Color currentPixelColour = textureOnSquare.GetPixel((int)(UVMap.x * xCol), (int)(UVMap.y * yCol));
                    pixelColour = currentPixelColour;
                  }
              }
            }
            RayTracingResult.SetPixel(i, j, pixelColour);
          }
        }
        RayTracingResult.Apply();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Show the generated ray tracing image on screen
        Graphics.Blit(RayTracingResult, destination);
    }
}
