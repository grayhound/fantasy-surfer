using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour {
    public Material[] skybox_materials;

    private Material skybox;
    private float rotation  = 0.0f;
    private float speed = 0.0f;
    
	void Start ()
    {
        skybox = RenderSettings.skybox;
        ReloadSkybox();
    }
	
	void Update ()
    {
        Rotate(Time.deltaTime);
	}

    public void ReloadSkybox()
    {
        SetRandomRotationSpeed();
        SetRandomSkyboxMaterial();
    }

    private void Rotate(float deltaTime)
    {
        rotation += speed * deltaTime;        
        if (rotation > 360.0f) {
            rotation = rotation - 360.0f;
        }        
        skybox.SetFloat("_Rotation", rotation);
    }

    public void SetRotationSpeed(float new_speed)
    {
        speed = new_speed;
    }

    private void SetRandomRotationSpeed()
    {
        SetRotationSpeed(GetRandomRotationSpeed());
    }

    private float GetRandomRotationSpeed()
    {
        return Random.Range(2.0f, 6.0f);
    }

    public void SetSkyboxMaterial(Material new_skybox_material)
    {
        RenderSettings.skybox = new_skybox_material;
        skybox = RenderSettings.skybox;
    }

    private void SetRandomSkyboxMaterial()
    {
        SetSkyboxMaterial(GetRandomSkyboxMaterial());
    }

    private Material GetRandomSkyboxMaterial()
    {
        int random_skybox_index = Random.Range(0, skybox_materials.Length);
        return skybox_materials[random_skybox_index];
    }
}
