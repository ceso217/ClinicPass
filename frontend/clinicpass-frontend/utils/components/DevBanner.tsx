import React from 'react';
import { Code } from 'lucide-react';

export const DevBanner: React.FC = () => {
  return (
    <div className="bg-gradient-to-r from-purple-600 to-indigo-600 text-white py-2 px-4">
      <div className="container mx-auto flex items-center justify-center gap-2">
        <Code className="w-4 h-4" />
        <p className="text-center">
          Modo Desarrollo - Autenticación Mock Activa
        </p>
      </div>
    </div>
  );
};
