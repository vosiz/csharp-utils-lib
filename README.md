# C# Utils library
Created by Vosiz

## Motivation
Started as a personal grab-bag of small utilities, wrappers and helpers shared across private
C# projects. Being stabilized into a proper reusable library — usable from Unity as well as
.NET Framework 4.8 projects — and published as a NuGet package. MIT — use it however you like.

## Requirements
- `netstandard2.0` or `net48`

## Bug tracker
No tracked issues.

## Features
See [Roadmap](#roadmap) below for the full list of what's implemented and what's planned.

## Roadmap

### Root
- [x] Basic enums

### Assembly
- [x] Version (parsing, formatting, compatibility comparison)

### Commons
- [x] Assertion
- [x] Exceptions
- [x] Retval (return value wrapper)
- [x] Limited / LimitedNumber (min/max bounded value)
- [x] Unit / Quantity (with SI prefix conversion)
- [x] Flagword
- [x] Singleton
- [x] Duration (weeks/days/hours/minutes/seconds breakdown formatting)
- [ ] Unit conversion between related units (e.g. Kelvin <-> Celsius)

### Presets
- [x] Unit (Kelvin, Ampere, Volt, Meter, Second)
- [x] Quantity (Temperature, Current, Voltage, Length, Time)

### Extensions
- [x] Binary (byte array)
- [x] Collections (various)
- [x] Double
- [x] Enums
- [x] Integer
- [x] Object
- [x] String

### Helpers
- [x] Enums
- [x] Files
- [x] Ini
- [x] Paths
- [x] Time
- [x] Xml

### Utilities
- [ ] Archive (zip)
- [x] Async (Run / RunLocked / Repeat)
- [x] Randomizer
- [x] Timer
- [x] Generator (Lipsum)

## Projects
This repository is a multi-project solution:
- `CsharpLib` — the library itself, published as the `Vosiz.UtilsLib` NuGet package.
- `Tests` — reflection-based test runner exercising `CsharpLib`, not published.

## Setup
Install from NuGet:
```
dotnet add package Vosiz.UtilsLib
```
