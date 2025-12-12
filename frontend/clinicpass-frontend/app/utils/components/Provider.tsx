// components/Providers.tsx (DEBE SER CLIENT COMPONENT)
'use client'; 
import { AuthProvider } from '@/app/contexts/AuthContext';

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <AuthProvider>
      {children}
    </AuthProvider>
  );
}