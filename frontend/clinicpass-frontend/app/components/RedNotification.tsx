import { CircleX } from "lucide-react"

interface Props {
    mensaje : string;
}

export const RedNotification: React.FC<Props> = ({mensaje}) => {
    return(
        <div className="mb-6 bg-red-50 border border-red-200 rounded-lg p-4 flex items-center gap-3">
            <CircleX className="w-5 h-5 text-red-600" />
            <p className="text-red-800">{mensaje}</p>
        </div>
    )
}