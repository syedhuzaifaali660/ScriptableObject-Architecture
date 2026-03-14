# ScriptableObject-Architecture
[![openupm](https://img.shields.io/npm/v/com.danieleverland.scriptableobjectarchitecture?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.danieleverland.scriptableobjectarchitecture/)

Makes using Scriptable Objects as a fundamental part of your architecture in Unity super easy

Based on Ryan Hipple's 2017 Unite talk https://www.youtube.com/watch?v=raQ3iHhE_Kk

Now on the [Asset Store!](https://assetstore.unity.com/packages/tools/utilities/scriptableobject-architecture-131520)

Reading the [Quick Start Page](https://github.com/DanielEverland/ScriptableObject-Architecture/wiki/Quick-Start) is recommended!

# Features
- Automatic Script Generation
- Variables - All C# primitives
- Clamped Variables
- Variable References
- Typed Events
- Runtime Sets
- Custom Icons

Visual debugging of events

![](https://i.imgur.com/GPP3aVR.gif)

Full stacktrace and editor invocation for events

![](https://i.imgur.com/S90VUWI.png)

Custom icons

![](https://i.imgur.com/simB0mK.png)

Easy and automatic script generation

![](https://i.imgur.com/xm2gNmo.png)

# Installation

For a more detailed explanation, please read the [Quick Start Page](https://github.com/DanielEverland/ScriptableObject-Architecture/wiki/Quick-Start)

There are four ways you can install this package
- [Unity Asset Store](https://assetstore.unity.com/packages/tools/utilities/scriptableobject-architecture-131520)
- .unitypackage from [Releases](https://github.com/DanielEverland/ScriptableObject-Architecture/releases)
- Unity package manager introduced in 2017.2
- [OpenUPM](https://openupm.com/packages/com.danieleverland.scriptableobjectarchitecture/)

## Package Manager Installation

Simply modify your `manifest.json` file found at `/PROJECTNAME/Packages/manifest.json` by including the following line

```json
{
	"dependencies": {
		...
		"com.danieleverland.scriptableobjectarchitecture": "https://github.com/DanielEverland/ScriptableObject-Architecture.git#release/stable",
		...
	}
}
```

## My Fork Installation

This repository also includes a custom fork with additional improvements by Syed Ali.

If you want to use this fork instead of the original package, add the following entry to your Unity project's `manifest.json`:

```json
{
	"dependencies": {
		...
		"com.syedali.scriptableobjectarchitecture": "https://github.com/syedhuzaifaali660/ScriptableObject-Architecture.git?path=/Assets/SO%20Architecture#v1.6.5",
		...
	}
}
```

You can also install it directly from Unity:

1. Open **Window > Package Manager**
2. Click **+**
3. Select **Install package from git URL...**
4. Paste:

```text
https://github.com/syedhuzaifaali660/ScriptableObject-Architecture.git?path=/Assets/SO%20Architecture#v1.6.5
```

### Notes for this fork
- Package name: `com.syedali.scriptableobjectarchitecture`
- Version: `1.6.5`
- This fork contains custom improvements by **Syed Ali**
- The package is installed from the `Assets/SO Architecture` folder in this repository
