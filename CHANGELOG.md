# Change log

## Current version

### 2.3.0 - String casing extensions
- add: StringExt.ToPascalCase
- add: StringExt.ToCamelCase
- add: StringExt.ToSnakeCase
- add: StringExt.ToDashed
- add: StringExt.ToScreamingSnakeCase
- add: StringExt.ToModuleCase
- add: StringExt.ToCapsLock
- add: StringExt.ToCapital

## History

### 2.2.0 - Duration & time units
- add: Commons.Duration
- add: Duration.AddSeconds
- add: Duration.ToBreakdownString
- add: Presets.Unit.Second
- add: Presets.Quantity.Time

### 2.1.0 - Versioning utility
- add: Assembly.Version
- add: VersionCompatibility
- add: VersionCompatibilityExt.ToBoolean

### 2.0.0 - Unity / .NET 4.8 stabilization
- fix: Assert null order
- add: Assert On*
- fix: Randomizer init
- fix: AsArray items
- fix: XmlHelper typo
- add: Randomizer.Choice
- add: DoubleExt
- add: Utils.Timer
- add: CollectionExt.NextAfter
- add: Utils.Async
- add: ObjectExt.TryConvert
- add: StringExt.TryParseEnum
- add: Flagword
- add: Singleton<T>
- add: Generator.Lipsum
- add: MessageException
- fix: UseWindowsForms net48
- fix: ini-parser netstandard
- docs: README overhaul

### 1.6.0 - Units and quantities
- add: Unit, Quantity, SiPrefix
- add: Presets.Unit, Presets.Quantity
- add: Tests project, reflection-based runner

### 1.5.2 - Retval accessors
- fix: Retval getters
- fix: Retval ToString()
