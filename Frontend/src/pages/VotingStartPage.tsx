import FooterComp from "@/components/footer.tsx";
import PartyListVote from "@/components/PartyListVote.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useNavigate} from "react-router-dom";
import {SelectedPartyIdName} from "@/constants/constants.ts";
import {useState} from "react";
import LoadingOverlay from "@/components/LoadingOverlay.tsx";


function VotingStartPage() {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate();
    const [selectedParty, setSelectedParty] = useState(
        sessionStorage.getItem(SelectedPartyIdName) || null
    );

    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <LoadingOverlay visible={loading} />
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow  justify-center items-center">
                <div className="text-center">
                    <h1 className="text-4xl text-black text-center font-extrabold mb-10 sm:mt-5">
                        ORSZÁGGYŰLÉSI KÉPVISELŐK VÁLASZTÁSA 2026
                    </h1>
                    <h2 className="text-2xl text-black text-center font-extrabold mb-3">
                        Válasszon egy listás jelöltet a felsorolásból!
                    </h2>
                    <p className="text-gray-500 mb-5">
                        Minden állampolgár egy szavazattal rendelkezik. A választás titkos és egyenlő.
                    </p>
                </div>
                <div className="flex flex-col items-center">
                    <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white h-fit w-200 bg-white">
                        <PartyListVote selectedParty={selectedParty} setSelectedParty={setSelectedParty} setLoading={setLoading}/>
                    </div>
                    <div className="mt-10 relative flex justify-end  w-200">
                        <Button size="lg" variant="gradient" className=" text-3xl  text-white p-7 "  style={{fontFamily: "Momo Trust Display"}} disabled={!selectedParty} onClick={ () => navigate("/SingleMemberVotePage")}>Következő oldal</Button>
                    </div>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default VotingStartPage;