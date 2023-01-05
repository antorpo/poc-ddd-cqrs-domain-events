/*
    Nueva caracteristica de C# 10 que nos permite definir namespace globales para todo el proyecto.
    Y no tener que repetir la directiva using a nivel de clase. 
    Basicamente es azucar sintactica, la cual se puede aplicar tambien desde el .csproj usando la etiqueta <Using>.

    https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10
 */
global using Domain.Common;
global using Domain.Entities;
global using Domain.Events;