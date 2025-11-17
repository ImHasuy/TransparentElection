import React from "react";

interface LoadingOverlayProps {
    visible: boolean;
}

const LoadingOverlay: React.FC<LoadingOverlayProps> = ({ visible }) => {
    if (!visible) return null;

    return (
        <div className="absolute inset-0 bg-white/70 backdrop-blur-sm flex items-center justify-center z-50">
            <svg
                className="animate-spin h-10 w-10 text-black"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
            >
                <circle
                    className="opacity-25"
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    strokeWidth="4"
                ></circle>
                <path
                    className="opacity-75"
                    fill="currentColor"
                    d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z"
                ></path>
            </svg>
        </div>
    );
};

export default LoadingOverlay;
