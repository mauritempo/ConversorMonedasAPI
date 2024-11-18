# **Conversor de Monedas Web**

Este proyecto es una aplicación web diseñada para facilitar la conversión de monedas. Combina un backend desarrollado en **.NET 8** con un frontend basado en **Angular v18**. Además, incluye un sistema de suscripciones que regula el acceso y las funcionalidades según el plan elegido por los usuarios.

---

## **Características del Proyecto**

### **Backend**
- **Framework**: .NET 8.
- **Base de Datos**: SQLite con Entity Framework como ORM.
- **Autenticación**: Implementada mediante JWT (JSON Web Tokens).
- **Planes de Suscripción**:
  - **Free**: Hasta 10 conversiones disponibles.
  - **Trial**: Límite de 100 conversiones.
  - **Pro**: Uso ilimitado.
- **Endpoints**:
  - Gestión de usuarios (registro, inicio de sesión y suscripciones).
  - Conversión de monedas.
  - Consultas sobre el saldo de conversiones.
  - Control de estado de usuarios (habilitación/deshabilitación).
- **Validaciones**:
  - Los usuarios sin suscripción activa o con el límite agotado no pueden realizar conversiones.

---

### **Frontend**
- **Framework**: Angular v18.
- **Conexión Backend**: Utiliza servicios REST a través de HTTP.
- **Diseño Responsivo**: Adaptable a distintos dispositivos.
- **Componentes Clave**:
  - **Login**: Validación de credenciales y acceso según suscripción.
  - **Register**: Registro con selección de tipo de suscripción.
  - **Home**: Conversor para usuarios premium.
  - **Home-Free**: Versión limitada para usuarios gratuitos.
  - **Dashboard**: Información del usuario y opciones de sesión.

---

## **Instalación y Configuración**

### **Requisitos Previos**
- **Backend**:
  - .NET SDK 8.0 o superior.
  - SQLite (ya incluido).
- **Frontend**:
  - Node.js y npm (Node Package Manager).

---

### **Clonar el Proyecto**
```bash
git clone https://github.com/tu-usuario/conversor-monedas.git
cd conversor-monedas
```

---

### **Configuración del Backend**

1. Accede al directorio del backend:
   ```bash
   cd BACKEND
   ```

2. Restaura las dependencias:
   ```bash
   dotnet restore
   ```

3. Aplica las migraciones:
   ```bash
   dotnet ef database update
   ```

4. Ejecuta el backend:
   ```bash
   dotnet run
   ```

---

### **Configuración del Frontend**

1. Accede al directorio del frontend:
   ```bash
   cd FRONT/ConversorFRONT
   ```

2. Instala las dependencias:
   ```bash
   npm install
   ```

3. Inicia el servidor de desarrollo:
   ```bash
   ng serve
   ```

4. Abre el proyecto en tu navegador:
   ```
   http://localhost:4200
   ```

---

## **Tecnologías Utilizadas**

### **Backend**
- .NET 8.
- Entity Framework Core.
- SQLite.
- Autenticación JWT.

### **Frontend**
- Angular v18.
- HTML5 y CSS3.
- TypeScript.
- SweetAlert2 para alertas.

---

## **Funciones Principales**

1. **Inicio de Sesión y Registro**:
   - Creación y validación de cuentas según tipo de suscripción.

2. **Sistema de Suscripciones**:
   - Las conversiones dependen del plan seleccionado.

3. **Conversor de Monedas**:
   - Conversión basada en índices predefinidos (conversión en USD).

4. **Control de Conversiones**:
   - Restricción tras alcanzar el límite según suscripción.

5. **Interfaz Responsiva**:
   - Compatible con móviles, tabletas y computadoras.

---

## **Índices de Convertibilidad**
El sistema trabaja con los siguientes valores preconfigurados:

| Moneda          | Código | Índice de Convertibilidad |
|------------------|--------|---------------------------|
| Peso Argentino   | ARS    | 0.002                     |
| Euro             | EUR    | 1.09                      |
| Corona Checa     | Kc     | 0.043                     |
| Dólar Americano  | USD    | 1                         |

---

## **Contribuciones**
Para colaborar, haz un fork del repositorio, implementa tus cambios y envía un pull request.

---

## **Contacto**
Si necesitas más información, no dudes en escribir:

- **Correo**: mauricio.tempone202@gmail.com  
- **GitHub**: [Perfil en GitHub](https://github.com/mauritempo)
