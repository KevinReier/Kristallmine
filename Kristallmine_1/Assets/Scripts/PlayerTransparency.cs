using UnityEngine;
using System.Collections;

public class PlayerTransparency : MonoBehaviour
{
    public static PlayerTransparency Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerTransparency>();
            }
            return instance;
        }
    }
    private static PlayerTransparency instance;

    private float duration; //How fast is the Player blinking if he gets hit
    private float time; //How much Time has gone by since the Player got hit
    internal bool hit = false; //was the Player hit?
    
    // Use this for initialization
    void Start()
    {
        duration = GameSettings.Instance.hitDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit && GameSettings.Instance.health > 0)
        {
            PlayerHit();
        }
        
    }
    //If the Player got hit, play the blinking-animation for a given Time
    internal void PlayerHit()
    {

        SetupMaterialWithBlendMode(gameObject.GetComponent<Renderer>().material, BlendMode.Transparent);
        Color textureColor = gameObject.GetComponent<Renderer>().material.color;
        textureColor.a = Mathf.PingPong(Time.time, duration) / duration;
        gameObject.GetComponent<Renderer>().material.color = textureColor;
        time += Time.deltaTime;
        if (time > GameSettings.Instance.hitAnimationTime)
        {
            hit = false;
            SetupMaterialWithBlendMode(gameObject.GetComponent<Renderer>().material, BlendMode.Opaque);
            time = 0;
            duration = GameSettings.Instance.hitDuration;
        }

    }

    //Functions to change the Blendmode of the Player-Model
    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }
    public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }


}
