import FooterComp from "@/components/footer.tsx";
import SecondLayerAuth from "@/components/SecondLayerAuth.tsx";

function LayerTwoVerification() {


    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow flex justify-center items-center flex-col">
                <div className="text-center">
                    <h1 className="text-4xl text-black text-center font-extrabold mb-10 ">
                        Kérem szkennelje be a szavazási QR kódját!
                    </h1>
                    <p className="text-gray-500 mb-5">
                        Az oldal automatikusan átírányítja a szavazási oldalra, amennyiben érvényes QR kódot olvas be!
                    </p>
                </div>
                <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white h-150 w-200 bg-white">
                        <SecondLayerAuth/>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default LayerTwoVerification;