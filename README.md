
<img width="300" height="300" alt="qcmv3" src="https://raw.githubusercontent.com/novaissilly/ServerData/main/qcmv3.png" />

# Qolossal Cheat Menu (https://novax.lol)[https://novax.lol]
This is the full source code to Qolossal Cheat Menu V3 (Qolossal.lol), I am releasing this publically because copys are boring asf and i have no motavation, yes you read that right lmfao. Please continue the Qolossal legacy, if you decide to release your own fork of the menu please make sure you keep my name somewhere! (Please do it means alot)

# How to make this project (almost) uncrackable (for anyone in gtag copy com atleast)
Qolossal Cheat Menu (V2 + V3) never got cracked but heres what i did to make it good.

### File Checks
```
            string decodedAnti = DecodeString(anti1);

            if (anti != decodedAnti)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (!Directory.Exists(modspath))
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }

            if (Directory.GetFiles(modspath, "*QolossalCheatMenuV3*.dll").Length == 0)
            {
                shouldbeallowed = false;
                QG();
                locked = true;
                return;
            }
```

# How does this work? / Security Practices
Qolossal Cheat Menu V3 has lots a big security things aswell as smaller things to be a inconvenience to any threat actors, do not remove any of these (everything is there for a reason). I will not be listing everything, but here are a couple things that may confuse you.

### Main DLL
The Qolossal Cheat Menu V3 project is the main menu with the menu features and main stuff.
### Process Ending
In Qolossal Cheat Menu I use 
```
GameObject.DestroyImmediate(GorillaTagger.Instance);
GameObject.DestroyImmediate(GorillaTagger.Instance);
foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
{
    GameObject.DestroyImmediate(go);
}
Application.Quit();
Application.ForceCrash(1);
Application.CallLowMemory();
System.Environment.Exit(0);
```

### Manual String Obfuscation
You will see many things like this, this is just a annoying thing for decompilers/deobfuscation
```
"Dfg8afb3AsiHDfg8afb3AsioDfg8afb3AsilDfg8afb3AsidDfg8afb3AsieDfg8afb3AsirDfg8afb3AsiQDfg8afb3AsiCDfg8afb3AsiMDfg8afb3AsiVDfg8afb3Asi3Dfg8afb3Asi".Replace("Dfg8afb3Asi", "")
```
### Anti Emulation/Debugger
This checks if a specific method is in GorillaTagger, if not (you arent running in gorilla tag obviously lmao) and it kills itself.
```
            if (typeof(GorillaTagger).GetMethod("L3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEAU3THASFKAdsfds4tewEAp3THASFKAdsfds4tewEAd3THASFKAdsfds4tewEAa3THASFKAdsfds4tewEAt3THASFKAdsfds4tewEAe3THASFKAdsfds4tewEA".Replace("3THASFKAdsfds4tewEA", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
            {
                Plugin.QG();
                Application.Quit();
                Application.ForceCrash(1);
                Application.CallLowMemory();
                Environment.Exit(0);
            }
```

# Thank you
Sorry if the code quality is not very good, I have been using the same project for almost 1 year and have just been re-writing parts lmfao.

All Credits to Colossus otherwise none of this would of happened <3

Credits to Lars/LHAX, Colossus, Mios, Starry, WM/64Will64 for the base code and menu, Marsilacks, Saturn, X0 being there for me and motovating me.
Its been 3-4 years since I started copy modding and its been starting to get really really dead since 2025 onwards, I just dont know what to do so heres the source code <3
Credits: [[https://qolossallol.vercel.app/credits
](https://novax.lol)](https://novax.lol)
