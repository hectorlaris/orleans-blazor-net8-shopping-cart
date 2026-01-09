# Diagrama C4 Nivel 2: Contenedores - Orleans Blazor Shopping Cart (.NET 8.0)

## Descripción del Sistema
**Orleans Blazor Shopping Cart** es una aplicación de comercio electrónico construida con Microsoft Orleans (.NET 8.0) que utiliza el patrón Actor Model para manejar el estado distribuido del carrito de compras.

## Diagrama de Contenedores (C4 Level 2)

```mermaid
graph TB
    %% External Systems and Users
    User[?? Usuario<br/>Cliente del e-commerce]
    Browser[?? Navegador Web<br/>Chrome, Firefox, Safari]
    B2C[?? Azure Active Directory B2C<br/>Servicio de Autenticación<br/>OAuth 2.0 / OpenID Connect]
    AppInsights[?? Application Insights<br/>Telemetría y Monitoreo<br/>Logs, Métricas, Trazas]

    %% Main Application Container
    subgraph "Orleans Blazor Silo (.NET 8.0)"
        direction TB
        
        %% Web Layer
        BlazorServer[??? Blazor Server<br/>ASP.NET Core 8.0<br/>SignalR Hub<br/>UI Components]
        
        %% Authentication Layer
        AuthMiddleware[?? Authentication Middleware<br/>Microsoft.Identity.Web 4.3.0<br/>JWT Bearer + OpenID Connect]
        
        %% Service Layer
        Services[?? Application Services<br/>ShoppingCartService<br/>ProductService<br/>InventoryService]
        
        %% Orleans Runtime
        OrleansRuntime[?? Orleans Runtime<br/>Silo Host<br/>Grain Directory<br/>Placement Manager]
        
        %% Orleans Grains (Actor Model)
        Grains[?? Orleans Grains<br/>ShoppingCartGrain<br/>ProductGrain<br/>InventoryGrain]
    end

    %% Storage Layer
    subgraph "Almacenamiento"
        direction LR
        
        %% Development Storage
        MemoryStorage[?? Memory Storage<br/>In-Memory<br/>Development Only]
        
        %% Production Storage
        AzureStorage[?? Azure Table Storage<br/>Grain State Persistence<br/>Clustering Membership]
    end

    %% External Services
    subgraph "Servicios Externos Azure"
        direction TB
        AzureAppService[?? Azure App Service<br/>Hosting Container<br/>Escalabilidad Automática]
        AzureContainerApps[?? Azure Container Apps<br/>Contenedores Serverless<br/>Microservicios]
    end

    %% User Interactions
    User --> Browser
    Browser -->|HTTPS/WSS| BlazorServer
    
    %% Authentication Flow
    BlazorServer --> AuthMiddleware
    AuthMiddleware <-->|OAuth 2.0/OIDC| B2C
    
    %% Application Flow
    BlazorServer --> Services
    Services --> OrleansRuntime
    OrleansRuntime --> Grains
    
    %% Storage Connections
    Grains -.->|Development| MemoryStorage
    Grains -.->|Production| AzureStorage
    OrleansRuntime -.->|Clustering| AzureStorage
    
    %% Monitoring
    BlazorServer -->|Telemetry| AppInsights
    OrleansRuntime -->|Metrics| AppInsights
    
    %% Deployment Options
    BlazorServer -.->|Deploy to| AzureAppService
    BlazorServer -.->|Deploy to| AzureContainerApps

    %% Styling
    classDef userClass fill:#e1f5fe,stroke:#01579b,stroke-width:2px
    classDef webClass fill:#f3e5f5,stroke:#4a148c,stroke-width:2px
    classDef serviceClass fill:#e8f5e8,stroke:#1b5e20,stroke-width:2px
    classDef storageClass fill:#fff3e0,stroke:#e65100,stroke-width:2px
    classDef externalClass fill:#fce4ec,stroke:#880e4f,stroke-width:2px
    classDef orleansClass fill:#e0f2f1,stroke:#004d40,stroke-width:2px

    class User,Browser userClass
    class BlazorServer,AuthMiddleware webClass
    class Services,OrleansRuntime,Grains orleansClass
    class MemoryStorage,AzureStorage storageClass
    class B2C,AppInsights,AzureAppService,AzureContainerApps externalClass
```

## Detalle de Contenedores

### ?? **Orleans Blazor Silo** (Contenedor Principal - .NET 8.0)
**Tecnología**: ASP.NET Core 8.0 + Microsoft Orleans  
**Responsabilidad**: Aplicación web completa con UI y lógica de negocio distribuida

**Sub-componentes:**
- **Blazor Server**: Frontend interactivo con SignalR
- **Authentication**: Microsoft.Identity.Web 4.3.0 
- **Services**: Capa de servicios de aplicación
- **Orleans Runtime**: Motor de actores distribuidos
- **Grains**: Actores de dominio (ShoppingCart, Product, Inventory)

### ?? **Navegador Web** (Contenedor Cliente)
**Tecnología**: HTML5, CSS, JavaScript, SignalR Client  
**Responsabilidad**: Interfaz de usuario reactiva

### ?? **Azure AD B2C** (Sistema Externo)
**Tecnología**: OAuth 2.0, OpenID Connect  
**Responsabilidad**: Autenticación y autorización de usuarios

### ?? **Azure Table Storage** (Contenedor de Datos)
**Tecnología**: NoSQL, Azure Storage  
**Responsabilidad**: Persistencia de estado de grains y clustering

### ?? **Application Insights** (Sistema Externo)
**Tecnología**: Telemetría Azure  
**Responsabilidad**: Monitoreo, logs y métricas

## Flujos de Datos Principales

### 1. **Flujo de Autenticación**
```
Usuario ? Browser ? Blazor Server ? Auth Middleware ? Azure B2C
```

### 2. **Flujo de Shopping Cart**
```
Browser ? Blazor Server ? ShoppingCartService ? Orleans Runtime ? ShoppingCartGrain ? Azure Storage
```

### 3. **Flujo de Productos**
```
Browser ? Blazor Server ? ProductService ? Orleans Runtime ? ProductGrain ? Azure Storage
```

### 4. **Flujo de Inventario**
```
Browser ? Blazor Server ? InventoryService ? Orleans Runtime ? InventoryGrain ? Azure Storage
```

## Características Técnicas (.NET 8.0)

### **Escalabilidad**
- Orleans permite escalamiento horizontal automático
- Grains se distribuyen automáticamente entre nodos
- State management distribuido y tolerante a fallos

### **Rendimiento**
- .NET 8.0 LTS con mejoras de rendimiento
- SignalR para comunicación en tiempo real
- Memory storage para desarrollo, Azure Storage para producción

### **Resiliencia**
- Orleans maneja automáticamente failover de grains
- Clustering membership automático
- Supervision tree para actores

## Patrones Arquitectónicos Utilizados

1. **Actor Model**: Implementado via Orleans Grains
2. **CQRS**: Separación entre comandos y consultas en grains
3. **Event Sourcing**: Potencial para auditoría de cambios
4. **Microservices**: Cada grain es un micro-servicio virtual
5. **Circuit Breaker**: Para manejo de fallos en servicios externos

## Decisiones de Arquitectura

### ? **Ventajas de Orleans**
- Estado distribuido sin complejidad de coordinación
- Escalabilidad automática y transparente
- Modelo de programación simplificado para sistemas distribuidos
- Tolerancia a fallos built-in

### ?? **Consideraciones**
- Curva de aprendizaje para Orleans
- Dependencia del runtime Orleans
- Debugging distribuido puede ser complejo
- Requiere diseño cuidadoso de grain boundaries