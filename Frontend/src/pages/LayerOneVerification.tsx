import FooterComp from "@/components/footer.tsx";
import {FirstLayerForm} from "@/components/LayerOneForm.tsx";

function VotingStartPage() {


    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <div>
                <h1 className="text-4xl text-black text-center lg:mt-20 sm:mt-10 font-extrabold">
                    Kérjük igazolja személyazonosságát!
                </h1>
                <p className="text-black text-center mt-10">
                    Az alábbi mezőkben adja meg Személyigazolványa és lakcímkártyája számát!
                </p>
                <p className="text-black text-center mt-2">
                    A személyes adatai alapján azanosítjuk, hogy jogosult-e szavazni, de ezen adatok nem lesznek eltárolva.
                </p>
            </div>
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow flex justify-center items-center">
                <div className="bg-white rounded-2xl shadow-2xl border-black content-center text-center">
                    <FirstLayerForm/>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default VotingStartPage;