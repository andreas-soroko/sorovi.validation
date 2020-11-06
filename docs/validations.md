# Validations 

There is almost always an 'not' version

#### If

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .If( value => value == 0);
```

#### IfNull

```csharp
int? myVar = null;

ThrowOn(() => myVar)
    .IfNull();
```

#### IfDefault

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfDefault();
```

#### IfEmpty
Works currently for
- strings
- IEnumerable
- IDictionary
- Guid

```csharp
string myVar = "";

ThrowOn(() => myVar)
    .IfEmpty();
```

#### IfNullOrWhiteSpace

```csharp
string myVar = "";

ThrowOn(() => myVar)
    .IfNullOrWhiteSpace();
```

#### IfEqualsTo

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfEqualsTo(0);
```


#### IfGreaterThan

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfGreaterThan(-1);
```

#### IfGreaterOrEqualsThan

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfGreaterOrEqualsThan(0);
```

#### IfLowerThan

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfLowerThan(1);
```

#### IfLowerOrEqualsThan

```csharp
int myVar = 0;

ThrowOn(() => myVar)
    .IfLowerOrEqualsThan(0);
```