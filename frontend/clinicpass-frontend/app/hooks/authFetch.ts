import { apiFetch } from "./apiFetch";

export interface ChangePasswordPayload {
    Id: string;
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}

export interface ResetPasswordPayload {
    Id: string;
    NewPassword: string;
    ConfirmedNewPassword: string;
}

const BASE_URL = '/api';
// --- FUNCIONES DE GESTIÓN DE CONTRASEÑA ---

/**
 * Permite que un usuario autenticado cambie su propia contraseña.
 * Llama a POST /api/Auth/change-password.
 * * @param {ChangePasswordPayload} payload - El ID del usuario, la contraseña actual y la nueva contraseña (repetida).
 * @returns {Promise<any>} Respuesta de éxito del servidor.
 */
export async function changePassword(payload: ChangePasswordPayload): Promise<any> {
    try {
        return await apiFetch(`${BASE_URL}/Auth/change-password`, {
            method: 'POST',
            body: JSON.stringify(payload),
        });
    } catch (error) {
        console.error("Error al cambiar la contraseña (ChangePassword):", error);
        throw error;
    }
}

/**
 * Permite que un Administrador o el sistema restablezca la contraseña de un usuario.
 * Llama a POST /api/Auth/reset-password.
 * * @param {ResetPasswordPayload} payload - El ID del usuario y la nueva contraseña (repetida).
 * @returns {Promise<any>} Respuesta de éxito del servidor.
 */
export async function resetPasswordByAdmin(payload: ResetPasswordPayload): Promise<any> {
    try {
        return await apiFetch(`${BASE_URL}/Auth/reset-password`, {
            method: 'POST',
            body: JSON.stringify(payload),
        });
    } catch (error) {
        console.error("Error al resetear la contraseña (ResetPassword):", error);
        throw error;
    }
}