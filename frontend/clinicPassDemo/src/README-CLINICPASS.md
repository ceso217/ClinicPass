# ClinicPass - Sistema de Login y Dashboard

## 🎯 Descripción
Sistema de autenticación y dashboard implementado para ClinicPass con soporte para dos roles: **Administrador** y **Profesional**.

## 🏗️ Estructura Implementada

### Componentes Creados

#### 1. **AuthContext** (`/contexts/AuthContext.tsx`)
- Manejo centralizado de autenticación
- Almacenamiento de JWT en localStorage
- Estados de usuario y token
- Métodos: `login()`, `logout()`
- Helpers: `isAuthenticated`, `isAdmin`, `isProfesional`

#### 2. **Login** (`/components/Login.tsx`)
- Formulario de login con correo y contraseña
- Validación de credenciales
- Manejo de errores
- Redirección automática al dashboard después del login

#### 3. **DashboardAdmin** (`/components/DashboardAdmin.tsx`)
Panel de administración con:
- Estadísticas generales (pacientes, turnos, profesionales, fichas)
- Estado de turnos con gráficos
- Acciones rápidas para gestionar pacientes, profesionales, calendario, etc.
- Indicador de rendimiento del sistema

#### 4. **DashboardProfesional** (`/components/DashboardProfesional.tsx`)
Panel profesional con:
- Resumen de turnos del día
- Lista de turnos con estados (Programado, Confirmado, Completado)
- Alertas de fichas pendientes
- Calendario mini
- Acciones rápidas (Mis Pacientes, Historiales, Agendar Turno)

#### 5. **Sidebar** (`/components/Sidebar.tsx`)
- Navegación lateral colapsable
- Menú filtrado por rol
- Información del usuario
- Botones de configuración y logout

#### 6. **ProtectedRoute** (`/components/ProtectedRoute.tsx`)
- Protección de rutas por autenticación
- Control de acceso por roles
- Redirección automática si no está autenticado

#### 7. **Layout** (`/components/Layout.tsx`)
- Estructura base con sidebar + contenido
- Usado en todas las rutas protegidas

## 🔐 Sistema de Roles

### Rol 1 - Administrador
Acceso a:
- Dashboard con estadísticas generales
- Pacientes
- Calendario
- Historiales
- **Profesionales** (exclusivo)
- **Reportes** (exclusivo)
- Configuración

### Rol 2 - Profesional
Acceso a:
- Dashboard con turnos del día
- Pacientes
- Calendario
- Historiales
- Configuración

## 🛣️ Rutas Implementadas

| Ruta | Descripción | Acceso |
|------|-------------|--------|
| `/login` | Página de inicio de sesión | Público |
| `/dashboard` | Dashboard según rol | Protegido |
| `/pacientes` | Gestión de pacientes | Protegido |
| `/calendario` | Calendario de turnos | Protegido |
| `/historiales` | Historiales clínicos | Protegido |
| `/profesionales` | Gestión de profesionales | Solo Admin |
| `/reportes` | Reportes y estadísticas | Solo Admin |
| `/configuracion` | Configuración del sistema | Protegido |

## 🔧 Configuración del Backend

### 1. Actualizar URL de la API
En `/contexts/AuthContext.tsx`, línea 41:
```typescript
const response = await fetch('http://localhost:5000/api/auth/login', {
```
Reemplazar con la URL de tu backend .NET.

### 2. Formato de Respuesta Esperado
El endpoint de login debe devolver:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "nombreCompleto": "Dr. Juan Pérez",
    "dni": "12345678",
    "correo": "juan.perez@clinica.com",
    "especialidad": "Cardiología",
    "rol": 1,
    "telefono": "3794123456"
  }
}
```

### 3. Estructura del Usuario
```typescript
interface User {
  id: number;
  nombreCompleto: string;
  dni: string;
  correo: string;
  especialidad?: string;
  rol: number; // 1 = Admin, 2 = Profesional
  telefono?: string;
}
```

## 📦 Dependencias Necesarias
```json
{
  "dependencies": {
    "react-router-dom": "^6.x"
  }
}
```

## 🚀 Uso

### Modo Mock (Testing)
El sistema está configurado en **modo MOCK** por defecto para facilitar las pruebas sin backend.

#### Usuarios de Prueba Disponibles

**Administrador:**
```
Correo: admin@clinicpass.com
Contraseña: admin123
Nombre: Carlos Administrador
Rol: Admin (1)
```

**Profesional 1:**
```
Correo: profesional@clinicpass.com
Contraseña: prof123
Nombre: Dra. María González
Especialidad: Cardiología
Rol: Profesional (2)
```

**Profesional 2:**
```
Correo: juan.perez@clinicpass.com
Contraseña: juan123
Nombre: Dr. Juan Pérez
Especialidad: Pediatría
Rol: Profesional (2)
```

**Administrador 2:**
```
Correo: laura@clinicpass.com
Contraseña: laura123
Nombre: Laura Admin
Rol: Admin (1)
```

### Activar/Desactivar Modo Mock

En el archivo `/contexts/AuthContext.tsx`, línea 4:

```typescript
const USE_MOCK_AUTH = true; // Cambiar a false para usar backend real
```

- `true` = Modo Mock (testing sin backend)
- `false` = Modo Producción (requiere backend .NET funcionando)

### Inicio de Sesión Rápido

En la pantalla de login verás botones de "Login Rápido" que:
- Autocompletarán las credenciales
- Solo necesitas hacer click en "Iniciar Sesión"

### Iniciar Sesión (Manual)
```typescript
// Ejemplo de credenciales para testing
// Admin
correo: admin@clinicpass.com
password: admin123

// Profesional
correo: profesional@clinicpass.com
password: prof123
```

### Flujo de Autenticación
1. Usuario ingresa credenciales en `/login`
2. Se envía petición POST al backend
3. Backend valida y retorna JWT + datos del usuario
4. Se almacena en localStorage
5. Redirección automática a `/dashboard`
6. Dashboard muestra vista según rol

### Cerrar Sesión
- Click en "Cerrar Sesión" en el sidebar
- Se limpia localStorage
- Redirección a `/login`

## 🎨 Características de UI

### Login
- Diseño moderno con gradiente
- Validación de campos
- Mensajes de error
- Loading state durante autenticación

### Dashboards
- **Admin**: Vista general con métricas y acciones rápidas
- **Profesional**: Vista enfocada en turnos del día

### Sidebar
- Colapsable para maximizar espacio
- Indicador visual de ruta activa
- Filtrado automático de menú por rol
- Avatar con inicial del usuario

## 🔄 Próximos Pasos

1. **Conectar con API real**
   - Actualizar URL en AuthContext
   - Implementar endpoints en tu backend .NET

2. **Implementar módulos pendientes**
   - Gestión de Pacientes
   - Calendario interactivo
   - Historiales Clínicos
   - Gestión de Profesionales
   - Reportes

3. **Mejoras de seguridad**
   - Refresh token
   - Expiración automática de sesión
   - Protección CSRF

4. **Testing**
   - Probar con usuarios reales
   - Validar permisos por rol
   - Test de integración con backend

## 📝 Notas Importantes

- Las estadísticas en los dashboards actualmente usan datos simulados
- Los módulos de Pacientes, Calendario, etc. están como placeholders
- Las acciones rápidas tienen console.log() para futuras implementaciones
- El sistema está listo para integrarse con tu backend .NET existente

## 🐛 Solución de Problemas

### Error: "Credenciales incorrectas"
- Verificar que el backend esté corriendo
- Validar la URL de la API
- Revisar formato de respuesta del backend

### No redirige después del login
- Verificar que el token y user se estén guardando en localStorage
- Revisar console del navegador para errores

### No se muestra el menú correcto
- Verificar que el rol del usuario sea 1 o 2
- Revisar que user.rol esté llegando correctamente desde el backend

## 👨‍💻 Autor
Proyecto ClinicPass - Bootcamp Devlights 2025