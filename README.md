# Prueba Desarrollador Backend .NET/SQL - SalesManagementSystem

Este es un proyecto de prueba técnica para el puesto de Desarrollador Backend .NET y SQL en Defontana.
Estructurado basado en la arquitectura 'Clean Architecture'.

## Getting Started

### Tecnologías

- .NET 6
- SQL Server

### Setup
1. Clonar el repositorio
2. Situarse en la raiz del directorio, y ejecutar
```
dotnet run --project SalesManagementSystem/SalesManagementSystem.Application
```

### Setup Docker
1. Clonar el repositorio
2. Situarse en la raiz del directorio, y ejecutar
```
docker compose up -d
```


### Queries SQL
1. El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
```
SELECT 
    SUM(TotalLinea) as MontoTotal,
    SUM(Cantidad)
FROM VentaDetalle s
INNER JOIN 
    Venta v ON s.ID_Venta = v.ID_Venta
WHERE 
    V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
```

2. El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
```
SELECT TOP 1 
    V.Fecha AS Fecha, 
    V.Total AS HighestAmount
FROM Venta AS V
WHERE 
    V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
ORDER BY 
    V.Total DESC;
```

3. Indicar cuál es el producto con mayor monto total de ventas.
```
SELECT 
    P.Nombre AS ProductName, 
    SUM(VD.TotalLinea) AS TotalSalesAmount
FROM VentaDetalle AS VD
    JOIN Producto AS P ON VD.ID_Producto = P.ID_Producto
    JOIN Venta AS V ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
GROUP BY 
    P.Nombre
ORDER BY 
    SUM(VD.TotalLinea) DESC;
```

4. Indicar el local con mayor monto de ventas.
```
SELECT TOP 1 
    L.Nombre AS LocationName,
    SUM(V.Total) AS TotalSalesAmount
FROM Venta AS V
    JOIN Local AS L ON V.ID_Local = L.ID_Local
    JOIN VentaDetalle as VD ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
GROUP BY 
    L.Nombre
ORDER BY 
    SUM(V.Total) DESC;
```

5. ¿Cuál es la marca con mayor margen de ganancias?
```
SELECT TOP 1 WITH TIES
    M.ID_Marca AS BrandID,
    M.Nombre AS BrandName,
    AVG(((VD.Precio_Unitario * VD.Cantidad) - CAST(P.Costo_Unitario AS DECIMAL(18, 2))) / (VD.Precio_Unitario * VD.Cantidad)) * 100 AS AverageProfitMargin
FROM VentaDetalle VD
    INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
    INNER JOIN Marca M ON P.ID_Marca = M.ID_Marca
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE
    V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
GROUP BY
    M.ID_Marca,
    M.Nombre
ORDER BY
	AverageProfitMargin DESC
```

6. ¿Cómo obtendrías cuál es el producto que más se vende en cada local?
```
SELECT NombreLocal, NombreProducto, cantidad
FROM (
    SELECT L.Nombre AS NombreLocal, P.Nombre AS NombreProducto, SUM(VD.Cantidad) AS cantidad,
        ROW_NUMBER() OVER (PARTITION BY L.ID_Local ORDER BY SUM(VD.Cantidad) DESC) AS Rank
    FROM Local L
    JOIN Venta V ON L.ID_Local = V.ID_Local
    JOIN VentaDetalle VD ON V.ID_Venta = VD.ID_Venta
    JOIN Producto P ON VD.ID_Producto = P.ID_Producto
    WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE()) AND V.Fecha <= GETDATE()
    GROUP BY L.ID_Local, L.Nombre, P.Nombre
) AS Subquery
WHERE Rank = 1
ORDER BY NombreLocal;
```

## Arquitectura

### Clean Architecture 

En Clean Architecture, una aplicación se divide en responsabilidades y cada una de estas responsabilidades se representa en forma de capa.

Se basa en que la capa de dominio no dependa de ninguna capa exterior. La de aplicación sólo depende de la de dominio y el resto (normalmente presentación y acceso a datos) depende de la capa de aplicación. Esto se logra con la implementación de interfaces de servicios que luego tendrán que implementar las capas externas y con la inyección de dependencias.

### Capas

- Domain: es el corazon de la aplicación y tiene que estar totalmente aislada de cualquier dependencia ajena a la lógica o los datos de negocio. Puede contener entidades, value objects, eventos y servicios del dominio.

- Application: es la capa que contiene los servicios que conectan el dominio con el mundo exterior (capas exteriores). Aquí se definen los contratos, interfaces.
- Infraestructure: es la capa de acceso a datos. Implementa interfaces definida en la capa de Application.

### Patrones y metodologías utilizadas:

- Acceso a datos: Entity Framework Core (DbContext) o Unit Of Work & Repository Pattern.