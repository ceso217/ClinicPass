// 'use client';
// import React, { useState, useEffect } from 'react';
// import { X, User, Mail, Phone, Stethoscope, Building, Key, Shield } from 'lucide-react';
// import { RegisterPayload } from '../../types/registerPayload';

// interface ProfesionalModalProps {
//   isOpen: boolean;
//   onClose: () => void;
//   onSave: (payload: RegisterPayload) => void;
//   mode: 'create' | 'edit';
// }

// const initialForm: RegisterPayload = {
//   email: '',
//   password: '',
//   repeatPassword: '',
//   dni: '',
//   name: '',
//   lastName: '',
//   phoneNumber: '',
//   especialidad: '',
//   activo: true,
//   rol: 'Profesional',
// };

// export const ProfesionalModal: React.FC<ProfesionalModalProps> = ({
//   isOpen,
//   onClose,
//   onSave,
//   mode,
// }) => {
//   const [formData, setFormData] = useState<RegisterPayload>(initialForm);
//   const [errors, setErrors] = useState<Record<string, string>>({});

//   const especialidades = [
//     'Cardiología',
//     'Pediatría',
//     'Traumatología',
//     'Dermatología',
//     'Clínica Médica',
//     'Neurología',
//     'Oftalmología',
//     'Odontología',
//     'Otra',
//   ];

//   useEffect(() => {
//     if (isOpen) {
//       setFormData(initialForm);
//       setErrors({});
//     }
//   }, [isOpen]);

//   const validateForm = () => {
//     const e: Record<string, string> = {};

//     if (!formData.name.trim()) e.name = 'El nombre es requerido';
//     if (!formData.lastName.trim()) e.lastName = 'El apellido es requerido';

//     if (!formData.dni.trim()) e.dni = 'El DNI es requerido';
//     else if (!/^\d{7,8}$/.test(formData.dni)) e.dni = 'DNI inválido';

//     if (!formData.email.trim()) e.email = 'El email es requerido';
//     else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) e.email = 'Email inválido';

//     if (!formData.phoneNumber.trim()) e.phoneNumber = 'El teléfono es requerido';
//     if (!formData.especialidad.trim()) e.especialidad = 'La especialidad es requerida';

//     if (!formData.password) e.password = 'La contraseña es requerida';
//     else if (formData.password.length < 6) e.password = 'Mínimo 6 caracteres';

//     if (formData.password !== formData.repeatPassword)
//       e.repeatPassword = 'Las contraseñas no coinciden';

//     setErrors(e);
//     return Object.keys(e).length === 0;
//   };

//   const handleSubmit = () => {
//     if (!validateForm()) return;
//     onSave(formData);
//     onClose();
//   };

//   if (!isOpen) return null;

//   return (
//     <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
//       <div className="bg-white rounded-xl w-full max-w-2xl max-h-[90vh] overflow-y-auto">

//         {/* Header */}
//         <div className="p-6 border-b flex justify-between items-center">
//           <h2 className="text-lg font-semibold">
//             Agregar Profesional
//           </h2>
//           <button onClick={onClose}><X /></button>
//         </div>

//         {/* Body */}
//         <div className="p-6 space-y-6">

//           {/* Nombre */}
//           <div className="grid grid-cols-2 gap-4">
//             <input
//               placeholder="Nombre"
//               value={formData.name}
//               onChange={e => setFormData({ ...formData, name: e.target.value })}
//               className="input"
//             />
//             <input
//               placeholder="Apellido"
//               value={formData.lastName}
//               onChange={e => setFormData({ ...formData, lastName: e.target.value })}
//               className="input"
//             />
//           </div>

//           {/* DNI / Tel */}
//           <div className="grid grid-cols-2 gap-4">
//             <input
//               placeholder="DNI"
//               value={formData.dni}
//               onChange={e => setFormData({ ...formData, dni: e.target.value.replace(/\D/g, '') })}
//               className="input"
//             />
//             <input
//               placeholder="Teléfono"
//               value={formData.phoneNumber}
//               onChange={e => setFormData({ ...formData, phoneNumber: e.target.value })}
//               className="input"
//             />
//           </div>

//           {/* Email */}
//           <input
//             placeholder="Email"
//             value={formData.email}
//             onChange={e => setFormData({ ...formData, email: e.target.value })}
//             className="input"
//           />

//           {/* Especialidad */}
//           <select
//             value={formData.especialidad}
//             onChange={e => setFormData({ ...formData, especialidad: e.target.value })}
//             className="input"
//           >
//             <option value="">Especialidad</option>
//             {especialidades.map(e => <option key={e}>{e}</option>)}
//           </select>

//           {/* Rol */}
//           <select
//             value={formData.rol}
//             onChange={e => setFormData({ ...formData, rol: e.target.value as any })}
//             className="input"
//           >
//             <option value="Profesional">Profesional</option>
//             <option value="Admin">Administrador</option>
//           </select>

//           {/* Password */}
//           <div className="grid grid-cols-2 gap-4">
//             <input
//               type="password"
//               placeholder="Contraseña"
//               value={formData.password}
//               onChange={e => setFormData({ ...formData, password: e.target.value })}
//               className="input"
//             />
//             <input
//               type="password"
//               placeholder="Confirmar contraseña"
//               value={formData.repeatPassword}
//               onChange={e => setFormData({ ...formData, repeatPassword: e.target.value })}
//               className="input"
//             />
//           </div>

//         </div>

//         {/* Footer */}
//         <div className="p-6 border-t flex justify-end gap-2">
//           <button onClick={onClose} className="btn-secondary">Cancelar</button>
//           <button onClick={handleSubmit} className="btn-primary">
//             Crear Profesional
//           </button>
//         </div>
//       </div>
//     </div>
//   );
// };
