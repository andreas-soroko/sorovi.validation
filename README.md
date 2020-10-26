# sorovi.Validation

Why an another validation library when there are many others out there already? 
Because they didn't quite fit into my worklfow, something always bothered me. 


## Examples

##### Some basic examples

```csharp
using static sorovi.Validation.Validation;

string myVar = "Have a nice day!";

ThrowOn(() => myVar)
    .IfNull();

ThrowOn(() => myVar)
    .IfEmpty();

ThrowOn(() => myVar)
    .IfEqualsTo("Have a nice day!");
```

##### Usage with a class variable

```csharp
using static sorovi.Validation.Validation;

private class MySuperClass { 
    public string MyVar = "Have a nice day!";
}

MySuperClass myClass = new MySuperClass();

ThrowOn(() => myClass)
    .IfNull()
    .Member( m => m.MyVar, arg => arg.IfEqualsTo("Have a nice day!"));

```

##### Changing exception

```csharp
ThrowOn(() => myVar)
    .WithException( (type, message, memberName, value) => new F(...) )
    .IfNull();
```

```csharp
ThrowOn(() => myVar)
    .WithException(CreateMyOwnException)
    .IfNull();

private static Exception CreateMyOwnException(in string type, in string message, in string memberName, object value) =>
            new MyOwnException(...);
```


```csharp
ThrowOn(() => myVar)
    .WithMyOwnException()
    .IfNull();


public static ArgumentInfo<T> WithMyOwnException<T>(this in ArgumentInfo<T> arg) => 
    arg.WithException(CreateMyOwnException);

private static Exception CreateMyOwnException(in string type, in string message, in string memberName, object value) =>
            new MyOwnException(...);
```

##### Extendable

```csharp
public static ref readonly ArgumentInfo<T> MyOwnValidatioFunction<T>(this in ArgumentInfo<T> arg, ....)
{
    // ....
    return ref arg;
}


ThrowOn(() => myVar)
    .MyOwnValidatioFunction();

```

## Benchmark

```
BenchmarkDotNet=v0.12.1, OS=macOS Catalina 10.15.7 (19H2) [Darwin 19.6.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


|                       Method |           Mean |       Error |      StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated | Completed Work Items | Lock Contentions |
|----------------------------- |---------------:|------------:|------------:|-------:|-------:|------:|----------:|---------------------:|-----------------:|
|           PropertyGetterOnly | 57,713.6108 ns | 626.0200 ns | 585.5795 ns | 0.2441 | 0.1221 |     - |    1733 B |               0.0001 |                - |
| PropertyGetterWithMemberName | 57,886.6475 ns | 553.5737 ns | 490.7287 ns | 0.2441 | 0.1221 |     - |    1733 B |               0.0002 |                - |
|        WithoutPropertyGetter |     59.6323 ns |   0.5241 ns |   0.4646 ns | 0.0254 |      - |     - |     160 B |               0.0000 |                - |
|                      Classic |      0.1775 ns |   0.0157 ns |   0.0122 ns |      - |      - |     - |         - |               0.0000 |                - |

```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details