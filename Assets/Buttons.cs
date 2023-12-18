using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{
   

    int healthPoints;
    int coins;
    int greenHerb;
    int redHerb;
    int greenGreenMixture;
    int greenRedMixture;
    int redRedMixture;
    int normalGunpowder;
    int highGradeGunpowder;
    int handGrenade;
    int flashGrenade;
    int assualtRifle;
    int shotGun;
    int goldBar;
    int ruby;
    int emerald;
    int twelvepistolAmmo;
    int thirtyRifleAmmo;
    int eightShotgunAmmo;
    int sixRevolverAmmo;
    public Text coinnn;

    // Start is called before the first frame update
    void Start()

    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void getGreenHerb()
    {
        if (coins >= 20)
        {
            greenHerb++;
            coins = coins - 20;
            coinnn.text = coins+"";

        }
    }
    public void sellGreenHerb()
    {
        if (greenHerb >= 1)
        {
            greenHerb--;
            coins = coins + 15;
            coinnn.text = coins + "";
        }
    }

    public void getRedHerb()
    {
        if (coins >= 10)
        {
            redHerb++;
            coins = coins - 10;
            coinnn.text = coins + "";
        }
    }
    public void sellRedHerb()
    {
        if (redHerb >= 1)
        {
            redHerb--;
            coins = coins + 5;
            coinnn.text = coins + "";
        }
    }
    public void sellgreenGreenMixture()
    {
        if (greenGreenMixture >= 1)
        {
            greenGreenMixture--;
            coins = coins + 30;
            coinnn.text = coins + "";
        }
    }

    public void sellgreenRedMixture()
    {
        if (greenRedMixture >= 1)
        {
            greenRedMixture--;
            coins = coins + 20;
            coinnn.text = coins + "";
        }
    }
    public void sellredRedMixture()
    {
        if (redRedMixture >= 1)
        {
            redRedMixture--;
            coins = coins + 10;
            coinnn.text = coins + "";
        }
    }
    public void getnormalGunpowder()
    {
        if (coins >= 10)
        {
            normalGunpowder++;
            coins = coins - 10;
            coinnn.text = coins + "";
        }
    }
    public void sellnormalGunpowder()
    {
        if (normalGunpowder >= 1)
        {
            normalGunpowder--;
            coins = coins + 5;
            coinnn.text = coins + "";
        }
    }

    public void gethighGradeGunpowder()
    {
        if (coins >= 20)
        {
            highGradeGunpowder++;
            coins = coins - 20;
            coinnn.text = coins + "";
        }
    }
    public void sellhighGradeGunpowder()
    {
        if (normalGunpowder >= 1)
        {
            highGradeGunpowder--;
            coins = coins + 15;
            coinnn.text = coins + "";
        }
    }

    public void getHandGrenade()
    {
        if (coins >= 15)
        {
            handGrenade++;
            coins = coins - 15;
            coinnn.text = coins + "";
        }
    }
    public void sellHandGrenade()
    {
        if (normalGunpowder >= 1)
        {
            handGrenade--;
            coins = coins + 10;
            coinnn.text = coins + "";
        }
    }

    public void getFlashGrenade()
    {
        if (coins >= 15)
        {
            handGrenade++;
            coins = coins - 15;
            coinnn.text = coins + "";
        }
    }
    public void sellFlashGrenade()
    {
        if (normalGunpowder >= 1)
        {
            handGrenade--;
            coins = coins + 10;
            coinnn.text = coins + "";
        }
    }

    public void getAssualtPistol()
    {
        if (coins >= 150)
        {
            assualtRifle++;
            coins = coins - 150;
            coinnn.text = coins + "";
        }
    }
    public void getShotgun()
    {
        if (coins >= 140)
        {
            shotGun++;
            coins = coins - 140;
            coinnn.text = coins + "";
        }
    }
    public void gettwelvePistolAmmo()
    {
        if (coins >= 30)
        {
            twelvepistolAmmo++;
            coins = coins - 30;
            coinnn.text = coins + "";
        }
    }
    public void getthirtyRifleAmmo()
    {
        if (coins >= 50)
        {
            twelvepistolAmmo++;
            coins = coins - 50;
            coinnn.text = coins + "";
        }
    }
    public void geteightShotgunAmmo()
    {
        if (coins >= 40)
        {
            eightShotgunAmmo++;
            coins = coins - 40;
            coinnn.text = coins + "";
        }
    }
    public void getsixRevolverAmmo()
    {
        if (coins >= 70)
        {
            sixRevolverAmmo++;
            coins = coins - 70;

        }
    }
    public void sellgoldBar()
    {
        if (normalGunpowder >= 1)
        {
            normalGunpowder--;
            coins = coins + 100;
            coinnn.text = coins + "";
        }
    }
    public void sellruby()
    {
        if (normalGunpowder >= 1)
        {
            ruby--;
            coins = coins + 200;
            coinnn.text = coins + "";
        }
    }
    public void sellemerald()
    {
        if (normalGunpowder >= 1)
        {
            ruby--;
            coins = coins + 500;
            coinnn.text = coins + "";
        }
    }

    public void Storage()
    {
       
    }


}
