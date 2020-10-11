# sorovi.Validation

Why an another validation library when there are many others out there already? 
Because they didn't quite fit into my worklfow, something always bothered me. 


## Examples

Some basic examples

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

Usage with a class variable

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

Changing exception (api will change in the future maybe)

```csharp
ExceptionFactory.Register<MyNullRefException>( (type, message) => new MyExceptionClass(...) );

ThrowOn(() => myVar)
    .IfNull<string, MyNullRefException>();
```

Extendable

```csharp

public static ref readonly ArgumentInfo<T> MyOwnValidatioFunction<T>(this in ArgumentInfo<T> arg, ....)
{
    // ....
    return ref arg;
}


ThrowOn(() => myVar)
    .MyOwnValidatioFunction();

```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details