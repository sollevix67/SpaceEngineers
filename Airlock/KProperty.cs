﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;
using System.Text.RegularExpressions;

namespace IngameScript
{
    partial class Program
    {
        public class KProperty
        {
            protected MyIni MyIni = new MyIni();
            protected Program program;
            public string color_default = "255,130,0,255";

            public string filter = "*";
            public int timer;
            public string pressurised_color;
            public string running_color;
            public string depressurised_color;

            public KProperty(Program program)
            {
                this.program = program;
            }

            public void Load()
            {
                MyIniParseResult result;
                if (!MyIni.TryParse(program.Me.CustomData, out result))
                    throw new Exception(result.ToString());
                filter = MyIni.Get("Airlock", "filter").ToString("CG:SAS1");
                timer = MyIni.Get("Airlock", "timer").ToInt32(30);
                pressurised_color = MyIni.Get("Airlock", "pressurised_color").ToString("0,255,0,255");
                running_color = MyIni.Get("Airlock", "running_color").ToString("255,130,0,255");
                depressurised_color = MyIni.Get("Airlock", "depressurised_color").ToString("255,0,0,255");

                if (program.Me.CustomData.Equals(""))
                {
                    Save();
                }
            }
            public void Save()
            {
                MyIniParseResult result;
                if (!MyIni.TryParse(program.Me.CustomData, out result))
                    throw new Exception(result.ToString());
                MyIni.Set("Airlock", "filter", filter);
                MyIni.Set("Airlock", "timer", timer);
                MyIni.Set("Airlock", "pressurised_color", pressurised_color);
                MyIni.Set("Airlock", "running_color", running_color);
                MyIni.Set("Airlock", "depressurised_color", depressurised_color);

                program.Me.CustomData = MyIni.ToString();
            }
            public string Get(string section, string key, string default_value = "")
            {
                return MyIni.Get(section, key).ToString(default_value);
            }

            public int GetInt(string section, string key, int default_value = 0)
            {
                return MyIni.Get(section, key).ToInt32(default_value);
            }

            public Color GetColor(string section, string key, string default_value = null)
            {
                if (default_value == null) default_value = color_default;
                string colorValue = MyIni.Get(section, key).ToString(default_value);
                Color color = Color.Gray;
                // Find matches.
                //program.drawingSurface.WriteText($"{section}/{key}={colorValue}", true);
                if (!colorValue.Equals(""))
                {
                    string[] colorSplit = colorValue.Split(',');
                    color = new Color(int.Parse(colorSplit[0]), int.Parse(colorSplit[1]), int.Parse(colorSplit[2]), int.Parse(colorSplit[3]));
                }
                return color;
            }
            
        }
    }
}
