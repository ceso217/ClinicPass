import toast from "react-hot-toast";

export function toastConfirm(message: string): Promise<boolean> {
  return new Promise((resolve) => {
    toast.custom((t) => (
      <div className="bg-white rounded-xl shadow-lg p-4 w-72">
        <p className="text-sm font-medium">{message}</p>

        <div className="flex justify-end gap-2 mt-4">
          <button
            className="px-3 py-1 rounded bg-gray-200"
            onClick={() => {
              toast.dismiss(t.id);
              resolve(false);
            }}
          >
            Cancelar
          </button>

          <button
            className="px-3 py-1 rounded bg-red-600 text-white"
            onClick={() => {
              toast.dismiss(t.id);
              resolve(true);
            }}
          >
            Confirmar
          </button>
        </div>
      </div>
    ), {
      duration: Infinity,
    });
  });
}