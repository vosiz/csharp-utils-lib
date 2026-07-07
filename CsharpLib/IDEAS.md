# Ideas / notes

## Unit conversion between related units

`Presets.Unit`/`Presets.Quantity` currently give one fixed unit per quantity (e.g. Temperature -> Kelvin).
Idea: extend quantities that have multiple common units (Temperature: Kelvin/Celsius, maybe others
later) with a way to pick which unit a value is created in or converted to.

Not just a symbol swap — some conversions need an offset/formula, not just SI-prefix scaling
(e.g. Celsius = Kelvin - 273.15). This needs a proper conversion mechanism (per-unit-pair formula,
not the `SiPrefix` scaling model), not decided yet how it fits architecturally. Raised while adding
`Presets.Quantity.Temperature` — wanted an enum param to pick Kelvin vs Celsius, deferred until
conversion is designed.

## Issues found while writing the Tests project

Found while writing tests as-is against current code — not fixed yet, fix in a separate branch
together with new/updated tests for whatever changes:

- `Commons/Assert.cs` only has `OnNull` and `OnType`. Too limited for real logic testing (no
  `OnTrue`/`OnFalse`/`OnEqual`/etc). `OnType(null, ...)` also throws a raw `NullReferenceException`
  instead of a clean `AssertException`, since it calls `obj.GetType()` before checking for null.
- `Utils/Randomizer.cs`: calling any `Next*` method before `Randomizer.Init()` throws
  `NullReferenceException` (the static `RandGen` field is never lazily initialized). Tests project
  works around this by calling `Randomizer.Init()` once at startup in `Program.cs`.
- `Extends/CollectionExt.cs` `AsArray<T>`: the extra `items` parameter is silently ignored —
  `list.ToList().AddRange(items)` builds a new list and immediately discards it instead of using it
  for the returned array. Currently behaves the same as a plain `.ToArray()`.
- `Helpers/XmlHlper.cs` — class name is missing an "e" (should be `XmlHelper`).
