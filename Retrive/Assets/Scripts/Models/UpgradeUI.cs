using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeUI 
{
    [SerializeField] public TipoAtributo Tipo;
    [SerializeField] public TextMeshProUGUI TextoLevelUpgrade;
    [SerializeField] public TextMeshProUGUI TextoValorUpgrade;
    [SerializeField] public Button BotaoUpgrade;
}
