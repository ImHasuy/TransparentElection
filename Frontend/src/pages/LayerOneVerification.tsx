import FooterComp from "@/components/footer.tsx";
import {FirstLayerForm} from "@/components/LayerOneForm.tsx";

function VotingStartPage() {


    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <div>
                <h1 className="text-4xl text-black text-center lg:mt-20 sm:mt-10 font-extrabold">
                    Kerjuk igazolja szemelyazonossagat!
                </h1>
            </div>
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow flex justify-center items-center mt-5">
                <div className="bg-white rounded-2xl shadow-2xl border-black content-center text-center">
                    <FirstLayerForm/>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default VotingStartPage;