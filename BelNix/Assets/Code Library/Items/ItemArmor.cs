﻿using UnityEngine;
using System.Collections;

public enum ArmorType  {Head, Shoulder, Chest, Gloves, Pants, Boots}


public class ItemArmor : EditorItem  {


	public ArmorType armorType;
	public int AC;
	//Armor armor;

	public override Item getItem()  {
		return getArmor();
	}

	public Armor getArmor()  {
	//	if (armor == null)
		return new Armor(itemName, itemType, canPlaceInShoulder, gold, silver, copper, isKeyItem, inventoryTextureSpritePrefabName, layerAdd, armorType, AC);
	//	return armor;
	}
}
