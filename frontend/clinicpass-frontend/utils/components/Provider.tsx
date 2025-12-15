// components/Providers.tsx (DEBE SER CLIENT COMPONENT)
'use client'; 
import { AuthProvider } from '@/contexts/AuthContext'

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <AuthProvider>
      {children}
    </AuthProvider>
  );
}