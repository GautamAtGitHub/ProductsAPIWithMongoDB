# Products WebAPI With MongoDB

#### 1. Web API
#### 2. MongoDB
#### 3. Autofac Dependency Injection framework 
  - Reference the `Autofac.Extensions.DependencyInjection` package from NuGet.
  - In the `ConfigureServices` method of your Startup class…
    - Register services from the `IServiceCollection` into the ContainerBuilder via `Populate` method.
    - Register services into the `ContainerBuilder` directly.
    - Build your container.
    - Create an `AutofacServiceProvider` using the container and return it.
   - In the `Configure` method of your `Startup` class, you can optionally register with the `IApplicationLifetime.ApplicationStopped event` to dispose of the container at app shutdown.

#### 4. xUnit with Moq
#### 5. Swagger
