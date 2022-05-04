using KillerIguana.CardManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPreviewer : MonoBehaviour
{
    public Image previewImage;
    public Text previewText;
    private BaseCard previewData;

    public void InitializePreview(BaseCard data)
    {
        previewData = data;
        previewImage.material.SetTexture("CardArt", previewData.graphic);
        previewText.text = previewData.cardDescription;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
