using MSCLoader;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace UI_Mover
{
    public class UI_Mover : Mod
    {
        public override string ID => "UI_Mover"; //Your mod ID (unique)
        public override string Name => "UI Mover"; //You mod name
        public override string Author => "Krutonium"; //Your Username
        public override string Version => "1.1"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;
        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }


        static class originals
        {
            public static Vector3 Day;
            public static Vector3 Hunger;
            public static Vector3 Thrist;
            public static Vector3 Stress;
            public static Vector3 Fatigue;
            public static Vector3 Urine;
            public static Vector3 Dirty;
            public static Vector3 Money;
            public static Vector3 FPS;
        }
        static bool fps = true;

        public override void OnLoad()
        {
            ModConsole.Print("Moving UI...");
            // Called once, when mod is loading after game is fully loaded
            GetInitialValues();
            Adjustment();
        }
        private void GetInitialValues()
        {
            originals.Day = GameObject.Find("Day").transform.position;
            originals.Hunger = GameObject.Find("Hunger").transform.position;
            originals.Thrist = GameObject.Find("Thrist").transform.position;
            originals.Stress = GameObject.Find("Stress").transform.position;
            originals.Fatigue = GameObject.Find("Fatigue").transform.position;
            originals.Urine = GameObject.Find("Urine").transform.position;
            originals.Dirty = GameObject.Find("Dirty").transform.position;
            originals.Money = GameObject.Find("Money").transform.position;
            try
            {
                originals.FPS = GameObject.Find("FPS").transform.position;
            } catch (Exception ex)
            {
                fps = false;
            }
            
        }
        private static void RestoreInitialValues()
        {
            GameObject.Find("Day").transform.position = originals.Day;
            GameObject.Find("Hunger").transform.position = originals.Hunger;
            GameObject.Find("Thrist").transform.position = originals.Thrist;
            GameObject.Find("Stress").transform.position = originals.Stress;
            GameObject.Find("Fatigue").transform.position = originals.Fatigue;
            GameObject.Find("Urine").transform.position = originals.Urine;
            GameObject.Find("Dirty").transform.position = originals.Dirty;
            GameObject.Find("Money").transform.position = originals.Money;
            if (fps)
            {
                GameObject.Find("FPS").transform.position = originals.FPS;
            }
        }

        private static void Move(string thingToMove, bool inverted = false, int distance = 5) //5 is ideal for 16:9
        {
            try
            {
                GameObject selected = GameObject.Find(thingToMove);
                if (!inverted)
                {
                    distance *= -1;
                }
                var pos = selected.transform.position + new Vector3(distance, 0, 0);
                selected.transform.position = pos;
                ModConsole.Print("UI Mover Moved: " + thingToMove);
            }
            catch
            {
                ModConsole.Print( "UI Mover Moved: "+ thingToMove + " does not exist!");
            }
        }

        static Settings UI_Slider = new Settings("slider", "Adjustment", 5, Adjustment);
        //5 is ideal for 16:9
        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
            Settings.AddHeader(this, "Adjust Slider");
            Settings.AddSlider(this, UI_Slider, -100, 100);
        }
        public static void Adjustment()
        {
            int V = int.Parse(UI_Slider.GetValue().ToString());
            RestoreInitialValues();
            Move("Day", false, V);
            Move("Hunger",false,V);
            Move("Thrist", false, V);
            Move("Stress", false, V);
            Move("Fatigue", false, V);
            Move("Urine", false, V);
            Move("Dirty", false, V);
            Move("Money", false, V);
            if (fps)
            {
                Move("FPS", true, V);
            }
        }
        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.

            //Unused
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
            //Unused
        }

        public override void Update()
        {
            // Update is called once per frame
            //Unused
        }

    }
}
