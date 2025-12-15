'use client';

import { useAuth } from '../../contexts/AuthContext';
import { DashboardAdmin } from '../../components/DashboardAdmin';
import { DashboardProfesional } from '../../components/DashboardProfesional';

export default function DashboardPage() {
    const { isAdmin, isProfesional } = useAuth();

    if (isAdmin) {
        return <DashboardAdmin />;
    }

    if (isProfesional) {
        return <DashboardProfesional />;
    }

    return null;
}