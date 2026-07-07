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
