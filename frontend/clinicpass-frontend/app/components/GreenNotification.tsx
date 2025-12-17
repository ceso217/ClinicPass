import { CheckCircle } from "lucide-react"

interface Props {
    mensaje : string;
}

export const GreenNotification: React.FC<Props> = ({mensaje}) => {
    return(
        <div className="mb-6 bg-green-50 border border-green-200 rounded-lg p-4 flex items-center gap-3">
            <CheckCircle className="w-5 h-5 text-green-600" />
            <p className="text-green-800">{mensaje}</p>
        </div>
    )
}