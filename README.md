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


|                       Method |        Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated | Completed Work Items | Lock Contentions |
|----------------------------- |------------:|----------:|----------:|-------:|------:|------:|----------:|---------------------:|-----------------:|
|           PropertyGetterOnly | 554.2992 ns | 4.6297 ns | 4.3306 ns | 0.0401 |     - |     - |     256 B |               0.0000 |                - |
| PropertyGetterWithMemberName |  26.6529 ns | 0.1541 ns | 0.1366 ns | 0.0102 |     - |     - |      64 B |               0.0000 |                - |
|        WithoutPropertyGetter |  12.7798 ns | 0.0525 ns | 0.0465 ns |      - |     - |     - |         - |               0.0000 |                - |
|                      Classic |   0.1762 ns | 0.0278 ns | 0.0247 ns |      - |     - |     - |         - |               0.0000 |                - |

```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details