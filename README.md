# Calc
Built with Visual Studio 2022 Community Edition

## Requirements
 - .net 6.0
 - MAUI

## Build Instructions
`dotnet build -c Release -f net6.0-android`

## Warnings (need to fix)
```
TimeCalc.xaml.cs(21,23): warning CS0612: 'Device' is obsolete [C:\Users\aaron\Source\repos\Calc\Calc\Calc.csproj::TargetFramework=net6.0-android]

TimeCalc.xaml.cs(21,23): warning CS0618: 'Device.InvokeOnMainThreadAsync(Action)' is obsolete: 'Use BindableObject.Dispatcher.DispatchAsync() instead.' [C:\Users\aaron\Source\repos\Calc\Calc\Calc.csproj::TargetFramework=net6.0-android]
```