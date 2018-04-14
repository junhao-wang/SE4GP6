using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour {


	private BattleState savedHeroStats {get; set;}
	private HeroStats displayedHeroStats {get; set;}
	public string SelectedHero {get;set;}
	public ItemInfo selectedItem {get;set;}

	// Use this for initialization
	void Start () {
		SelectedHero = "Alexei";
		savedHeroStats = SavedLoad.Read();
		displayedHeroStats = savedHeroStats.Alexei;
		updatedInfoPanelStats();
		selectedItem = null;
		Image image = GetComponentsInChildren<Image>()[1];
		//image.sprite = Resources.Load<Sprite>("Icons/ImageBackground");
	}
	
	// Update is called once per frame
	void Update () {
		if(SelectedHero == "Kroner"){
			savedHeroStats = SavedLoad.Read();
			displayedHeroStats = savedHeroStats.Kroner;
		}
		if(SelectedHero == "Alexei"){
			savedHeroStats = SavedLoad.Read();
			displayedHeroStats = savedHeroStats.Alexei;
		}
		if(SelectedHero == "Lee"){
			savedHeroStats = SavedLoad.Read();
			displayedHeroStats = savedHeroStats.Lee;
		}
		updatedInfoPanelStats();
	}
	public void setPassiveItem(ItemInfo itemInfo){
		selectedItem = itemInfo;
	}
	public void setSprite(Sprite sprite){
		Image image = GetComponentsInChildren<Image>()[1];
		image.sprite = sprite;
	}
	public void updatedInfoPanelStats(){
		Text[] stats = GetComponentsInChildren<Text>();
		/*if(selectedItem == null){
		stats[3].text = displayedHeroStats.getHP().ToString();
		stats[4].text = displayedHeroStats.getArmour().ToString();
		stats[5].text = displayedHeroStats.getGunAttack().ToString();
		stats[6].text = displayedHeroStats.getAttack().ToString();
		stats[7].text = displayedHeroStats.getRange().ToString();
		stats[8].text = displayedHeroStats.getSpeed().ToString();
		stats[9].text = displayedHeroStats.getMoveRange().ToString();
		}else{

		stats[0].text = selectedItem.description;
		stats[1].text = selectedItem.name;
		stats[3].text = (displayedHeroStats.getHP()+selectedItem.stats[0]).ToString();
		stats[4].text = (displayedHeroStats.getArmour()+selectedItem.stats[1]).ToString();
		stats[5].text = (displayedHeroStats.getGunAttack()+selectedItem.stats[3]).ToString();
		stats[6].text = (displayedHeroStats.getAttack()+selectedItem.stats[2]).ToString();
		stats[7].text = (displayedHeroStats.getRange()+selectedItem.stats[5]).ToString();
		stats[8].text = (displayedHeroStats.getSpeed()+selectedItem.stats[4]).ToString();
		stats[9].text = (displayedHeroStats.getMoveRange()+selectedItem.stats[6]).ToString();
		}*/
	}

}
