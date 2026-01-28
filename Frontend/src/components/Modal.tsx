import React, {type ReactNode} from "react";

interface ModalProps {
    open: boolean;
    onClose: () => void;
    children: ReactNode;
}


export const Modal: React.FC<ModalProps> = ({ open, onClose, children }) => {
    if (!open) return null;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center">
            {/* Overlay */}
            <div
                className="absolute inset-0 bg-black/50 backdrop-blur-sm"
                onClick={onClose}
            />

            {/* Modal box */}
            <div className="relative bg-white p-6 rounded-xl shadow-xl z-10 w-[90%] max-w-md">
                {children}
            </div>
        </div>
    );
};
