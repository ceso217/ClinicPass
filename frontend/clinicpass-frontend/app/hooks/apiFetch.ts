// api/apiFetch.ts (Versión Corregida)

export async function apiFetch(
  endpoint: string,
  options: RequestInit = {}
) {
  const token = localStorage.getItem('clinicpass_token');
  
  // 1. Ejecutar el fetch
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_API_URL}${endpoint}`,
    {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        ...(token && {
          Authorization: `Bearer ${token}`,
        }),
      },
    }
  );

  // --- 2. Manejo de Statuses de Éxito No-Contenido o Redirección ---
  if (res.status === 401) {
    // TOKEN INVÁLIDO O VENCIDO
    localStorage.removeItem('clinicpass_token');
    localStorage.removeItem('clinicpass_user');
    window.location.href = '/login';
    throw new Error('Unauthorized');
  }

  if (res.status === 204) {
    return null;
  }

  // --- 3. Manejo de Errores (res.ok === false) ---
  if (!res.ok) {
        // CLONACIÓN CRÍTICA: Clonamos la respuesta para poder intentar leerla
        // múltiples veces sin agotar el stream.
        const clonedRes = res.clone(); 
        
        let errorMessage = `Error HTTP ${res.status}: ${res.statusText}.`; 
        
        // Asignar un mensaje específico para 409 si no hay body detallado
        if (res.status === 409) {
            errorMessage = "Los datos ingresados son invalidos, ya existe un usuario registrado con ese DNI o Email.";
        }

        try {
            // Intentamos leer el body como JSON usando el CLON.
            const errorBody = await clonedRes.json();
            
            // Si la API devuelve un mensaje detallado (ej. "El DNI ya existe")
            if (errorBody && errorBody.error) {
                errorMessage = errorBody.error; 
            } else if (errorBody) {
                errorMessage = JSON.stringify(errorBody);
            }
        } catch (e) {
            // Si la lectura de JSON falla, intentamos leer texto simple.
            // Usamos el CLON de nuevo. (Esto resuelve el error de "body stream already read")
            try {
                const text = await clonedRes.text();
                if (text) {
                    errorMessage = text;
                }
            } catch (innerE) {
                // Si incluso la lectura del texto falla, mantenemos el errorMessage original.
            }
        }

        throw new Error(errorMessage); 
    }

    // --- 4. Devolver JSON para respuestas exitosas (200, 201, etc.) ---
    // Usamos la respuesta ORIGINAL (res), que no ha sido leída aún.
  return res.json();
}