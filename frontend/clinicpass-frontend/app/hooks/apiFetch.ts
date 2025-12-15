export async function apiFetch(
  endpoint: string,
  options: RequestInit = {}
) {
  const token = localStorage.getItem('clinicpass_token');

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

  if (res.status === 401) {
    // TOKEN INVÁLIDO O VENCIDO
    localStorage.removeItem('clinicpass_token');
    localStorage.removeItem('clinicpass_user');

    window.location.href = '/login';
    throw new Error('Unauthorized');
  }

  if (!res.ok) {
    throw new Error('API error');
  }

  return res.json();
}