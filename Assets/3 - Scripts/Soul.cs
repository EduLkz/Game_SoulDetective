using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour {
    
    public ParticleSystem rest;

    private string soulName;
    private string relation = "dad";

    bool firstInteract;

    public Item item;
    string myDialog;
    string getback;
    string doyouhave;
    int dialogNum;

    public void Initialize(Item _item) {
        item = _item;

        //randomize the soul relationship
        relation = relations[Random.Range(0, relations.Length)];


        //randomize soul dialog lines
        dialogNum = Random.Range(0, possibleDialogs.Length);
        myDialog = possibleDialogs[dialogNum];

        //replace initial dialog with item name, relation and item position
        myDialog = myDialog.Replace("1", item.name);
        myDialog = myDialog.Replace("2", relation);
        myDialog = myDialog.Replace("3", item.place);

        getback = GettingBack[dialogNum];
        //replace "get back" dialog with item name, relation and item position
        getback = getback.Replace("1", item.name);
        getback = getback.Replace("2", relation);
        getback = getback.Replace("3", item.place);

        doyouhave = DoYouHave[dialogNum];

        //replace "do you have" dialog with item name, relation and item position
        doyouhave = doyouhave.Replace("1", item.name);
        doyouhave = doyouhave.Replace("2", relation);
        doyouhave = doyouhave.Replace("3", item.place);
        item.gameObject.SetActive(false);

    }
    public void Interact() {

        //Show this dialog if its the player's first interaction with the soul
        //Start mission
        if(firstInteract) {
            Dialog.Instance.ShowText(doyouhave, DialogType.Text);
            return;
        }


        //if its isn't the fist, show this dialog
        firstInteract = true;
        Dialog.Instance.ShowText(myDialog, DialogType.Text);
        item.gameObject.SetActive(true);
    }

    public void GiveItem() {
        //End mission
        Instantiate(rest, transform.position + Vector3.up, Quaternion.identity);
        Dialog.Instance.ShowText(getback, DialogType.Text);
        Destroy(item, 3f);
        Destroy(gameObject, 2.5f);
        SpawnItens.Instance.Found();
    }

    //Initial dialogs
    string[] possibleDialogs = new string[]
    { "I lost my 2's 1, last time i see was near the 3, but i can't find it.",
    "Can you find the 1, it belong to my 2. Its probably near 3",
    "OH MY GOD, WHERE'S THE 1 ??? OH!! THE DETECTIVE!! PLEASE I HAVE LOST MY 1 THAT MY LOVELY 2 GAVE TO ME!!! I USED TO HANG OUT IN 3 WITH IT!!",
    "Hello sir, could you please help me? My 2 gave me a 1 long time ago. Back then i used to spend so much time in 3, such lovely memories",
    "HEY YO DUDE, someone took my 1. Aw man, i really like that 1, my 2 gave to me. Last time i saw? Probably around 3 if i remember correctly.",
    "Oh hi, didn't saw you there, what's up? If i need help? Actually yes, i don't know where is my 1, it was a gift. Don't remember last time i saw, but i used to hang out a lot near 3.",
    "All i want is to my find my 1, last time i had it was when i was around 3.",
    "Shit, my 2 is gonna be kill me cause i lost the 1 somewhere near 3 (even tho I'm already dead). Can you help me found it?",
    "Can you pick up my 1? I left near 3, but i don't want go there anymore.",
    "Did you saw those fire-flamy-things? I was trying to find my 1 when i was attacked by one of those things, can you help me? I was going to the 3, see if i find it there.",
    };

    string[] relations = new string[]
    {"father","mother", "aunt", "uncle",
    "brother","sister", "nephew", "niece",
    "son","daughter", "granddaughter", "grandson",
    "husband", "wife",
    "mother-in-law", "father-in-law", "brother-in-law", "sister-in-law",
    "girlfriend", "boyfriend", "ex-girlfriend", "ex-boyfriend",
    "best friend", "favorite teacher",
    "boss", "neighbour",
    };

    string[] GettingBack = new string[]
    {"Thanks for finding my 2's 1. I can now finally rest.",
    "You are really good following directions detective, i'm really grateful.",
    "DIDYOUFINDIT? OH MY GOD YOU DID, THANKS THANKS THANKS THANKS FOR REAL!!!! I NEED TO RUN!!1!BYE!!!",
    "Hi again good sir, you found my lost 1. I'm so happy, at this age we usually don't have much, the little things become so much important for us. Thanks again.",
    "IT'S THE BIG GUY. Hey man, thanks for getting this, it is really important to me, brings me good memories.",
    "You actually found it. Thanks, it brings memories from my 2 when i receive back then.",
    "Oh, thanks. I thought i wasn't going to se this again. My 2 game to me years ago, i can't lose this.",
    "I can't believe what my eyes see (even tho i don't have eyes), you found the 1.",
    "You got my 1, thanks for that. I was afraid from those things, but i really needed to get this, it was a gift from my 2.",
    "Did you fight those fire-flamy-things? You're really brave, thank you for helping me.",
    };


    string[] DoYouHave = new string[] { 
    "You don't have the thing yet?? If i remember it's near the 3",
    "Take your time, i can wait, dont worry. If you lost, probrably look near 3",
    "DIDIDIDIDIDYOU GET IT? oh, you didin't. AAAN I LOST WHEN I NEAR 3 I GUESS",
    "It's ok you didn't found yet, the fact you are searching it's enought. But search near the 3.",
    "COME ON DUDE, don't give me hope if you can't do it. Last time a saw was near 3.",
    "Hey there. What? Did you forgot where i said was? It was near 3.",
    "Just want my 1, i think i left around the 3.",
    "When i saw you comming, gave me chills on my spine, even tho i don't have a spine. But you don't have it yet. It was around 3 area.",
    "That 3 place scares me.",
    "Don't you want to go to the 3 anymore? Are you afraid from those fire-flamy-things?",
    };
}