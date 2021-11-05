using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateCar : MonoBehaviour
{
    public GameObject carPrefab;

    public void changeCar(GameObject desiredCar)
    {
        if (desiredCar!=null)
        {
            MeshFilter meshFilder_model = carPrefab.transform.Find("model").GetComponent<MeshFilter>();
            meshFilder_model.sharedMesh = desiredCar.GetComponent<MeshFilter>().sharedMesh;

            Transform model = carPrefab.transform.Find("model");
            Transform prefab_wheel_anteriore_r = model.Find("wc_fr");
            Transform prefab_wheel_anteriore_l = model.Find("wc_fl");
            Transform prefab_wheel_posteriore_r = model.Find("wc_br");
            Transform prefab_wheel_posteriore_l = model.Find("wc_bl");

            foreach (Transform wheel in desiredCar.transform)
            {
                if (wheel.localPosition.x > 0)// se si trova davamnti
                    if (wheel.localPosition.z > 0)//se si trova a sinistra
                        prefab_wheel_anteriore_l.localPosition = wheel.localPosition;
                    else//se si trova a destra
                        prefab_wheel_anteriore_r.localPosition = wheel.localPosition;
                else// se si trova dietro
                    if (wheel.localPosition.z > 0)//se si trova a sinistra
                    prefab_wheel_posteriore_l.localPosition = wheel.localPosition;
                else//se si trova a destra
                    prefab_wheel_posteriore_r.localPosition = wheel.localPosition;
            }

            carPrefab.transform.Find("model").GetComponent<MeshCollider>().sharedMesh = desiredCar.GetComponent<MeshCollider>().sharedMesh;
            
            MeshRenderer renderer = carPrefab.transform.Find("model").GetComponent<MeshRenderer>();

            for (int i = 0; i < renderer.materials.Length; i++)
            { renderer.materials[i].mainTexture = desiredCar.GetComponent<MeshRenderer>().sharedMaterials[i].mainTexture; }

        }
    }
}
