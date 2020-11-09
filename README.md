# sorovi.Validation

[![NuGet](https://img.shields.io/nuget/v/sorovi.Validation.svg?style=flat-square)](https://www.nuget.org/packages/sorovi.Validation/)
[![NuGet](https://img.shields.io/nuget/dt/sorovi.Validation.svg?style=flat-square)](https://www.nuget.org/packages/sorovi.Validation/)

Why an another validation library when there are many others out there already? 
Because they didn't quite fit into my worklfow, something always bothered me, so i created my own. 

- exceptions are changeable
- one exception with several types
- exception contains necessaries information
- same api for exception less results
- fast

and in the future 
- scoping
- more array and dictionary validations

   
> the lib is currently not finally done and I am still dissatisfied with some code parts,
so it may be that up to version 1 there will maybe breaking changes

---
### Examples

for better handling add 

```csharp
using static sorovi.Validation.Validation;
```

now you can initialize it in several ways 

```csharp
string myVar = "Have a nice day!";

// if you want exceptions
ThrowOn(() => myVar);
ThrowOn(myVar, nameof(myVar));
ThrowOn(myVar, "custom_name");

// if you want a result object with messages instead of exceptions
ResultOn(() => myVar);
ResultOn(myVar, nameof(myVar));
ResultOn(myVar, "custom_name");
```

each validation function will throw a ValidationException (when ThrownOn is used) with a `type` and `message`
these can be found here [ErrorMessage](sorovi.Validation/Common/ErrorMessage.cs) [ValidationType](sorovi.Validation/Common/ValidationType.cs)



##### Usage with a class variable

```csharp
private class MySuperClass { 
    public string MyVar = "Have a nice day!";
}

MySuperClass myClass = new MySuperClass();

ThrowOn(() => myClass)
    .IfNull()
    .Member( m => m.MyVar, arg => arg.IfEqualsTo("Have a nice day!"));

```

##### Changing type/message property

```csharp
int? myVar = null;

ThrowOn(() => myVar)
    .IfNull("my_own_null_ref_type", "Property {0} was null!!1");
```



##### Changing exception

```csharp
ThrowOn(() => myVar)
    .WithExceptionHandler( (type, message, memberName, value) => new F(...) )
    .IfNull();
```

```csharp
ThrowOn(() => myVar)
    .WithExceptionHandler(CreateMyOwnExceptionHandler)
    .IfNull();

private static void CreateMyOwnExceptionHandler(in string type, in string message, in string memberName, in object value) =>
            throw new MyOwnException(...);
```


```csharp
ThrowOn(() => myVar)
    .WithMyOwnException()
    .IfNull();


public static ArgumentInfo<T> WithMyOwnException<T, TEx>(this ArgumentInfo<T, TEx> arg) where TEx : Delegate => 
    arg.WithExceptionHandler(CreateMyOwnException);

private static void CreateMyOwnExceptionHandler(in string type, in string message, in string memberName, in object value) =>
            throw new MyOwnException(...);
```

##### Extendable

```csharp
public static ArgumentInfoBase<T, TEx> MyOwnValidatioFunction<T, TEx>(this ArgumentInfoBase<T, TEx> arg, ....)
{
    // ....
    return arg;
}


ThrowOn(() => myVar)
    .MyOwnValidatioFunction();

```

more examples can be found [here](docs/validations.md)

## Benchmark

```
BenchmarkDotNet=v0.12.1, OS=macOS Catalina 10.15.7 (19H2) [Darwin 19.6.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


|                                       Method |        Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated | Completed Work Items | Lock Contentions |
|--------------------------------------------- |------------:|----------:|----------:|-------:|------:|------:|----------:|---------------------:|-----------------:|
|            'Classic - if (property is null)' |   0.2416 ns | 0.0162 ns | 0.0152 ns |      - |     - |     - |         - |               0.0000 |                - |
|                    'ThrowOn(() => property)' | 450.1780 ns | 2.2035 ns | 1.7203 ns | 0.0420 |     - |     - |     264 B |               0.0000 |                - |
|        'ThrowOn(property, nameof(property))' |   6.3160 ns | 0.0712 ns | 0.0666 ns | 0.0064 |     - |     - |      40 B |               0.0000 |                - |
|             'ThrowOn(() => property).IfNull' | 466.8511 ns | 5.6947 ns | 5.3268 ns | 0.0420 |     - |     - |     264 B |               0.0000 |                - |
| 'ThrowOn(property, nameof(property)).IfNull' |   8.3448 ns | 0.0721 ns | 0.0675 ns | 0.0064 |     - |     - |      40 B |               0.0000 |                - |


```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details