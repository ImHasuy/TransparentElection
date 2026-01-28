import FooterComp from "@/components/footer.tsx";
import {useNavigate} from "react-router-dom";
import LoadingOverlay from "@/components/LoadingOverlay.tsx";
import {useEffect, useState} from "react";




function ConfirmalPage() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false)

    const handleSubmit = async () => {
        setLoading(true);
        try{
            sessionStorage.clear();
            localStorage.clear();
            navigate("/Landing");
        }catch (e) {
            console.log(e);
        }finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        const timer = setTimeout(() => {
            handleSubmit()
        }, 5000);
        return () => clearTimeout(timer);
    }, []);

    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <LoadingOverlay visible={loading} />
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow flex justify-center items-center">
                <div className="bg-white rounded-2xl shadow-2xl border-black content-center text-center">
                    <h1 className="text-4xl text-black text-center font-extrabold m-10 text-green-400">Sikeresen leadta a szavazatát!</h1>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default ConfirmalPage;