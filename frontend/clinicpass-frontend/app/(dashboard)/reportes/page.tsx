'use client';

import { Reportes } from '@/app/components/Reportes';
import { useAuth } from '../../contexts/AuthContext';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';

export default function ReportesPage() {
    const { isAdmin, loading } = useAuth();
    const router = useRouter();

    useEffect(() => {
        if (!loading && !isAdmin) {
        router.push('/dashboard');
        }
    }, [isAdmin, loading, router]);

    if (!isAdmin) {
        return null;
    }

    return (
        // <div className="p-8">
        // <h1>Reportes</h1>
        // <p className="text-gray-600 mt-2">Solo accesible para administradores</p>
        // </div>
        <Reportes />
    );
}