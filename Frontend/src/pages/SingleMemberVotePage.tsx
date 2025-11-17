import FooterComp from "@/components/footer.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useNavigate} from "react-router-dom";
import SingleMemberVote from "@/components/SingleMemberVote.tsx";
import {useState} from "react";
import LoadingOverlay from "@/components/LoadingOverlay.tsx";
import {SelectedSingleMemberName} from "@/constants/constants.ts";


function SingleMemberVotePage() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(true);
    const [selectedSingleMember, setSelectedSingleMember] = useState(
        sessionStorage.getItem(SelectedSingleMemberName) || null
    );


    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <LoadingOverlay visible={loading} />
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow  justify-center items-center">
                <div className="text-center">
                    <h1 className="text-4xl text-black text-center font-extrabold mb-5 sm:mt-5">
                        EGYÉNI VÁLASZTÓKERÜLETI KÉPVISELŐK VÁLASZTÁSA 2026
                    </h1>
                    <h2 className="text-2xl text-black text-center font-extrabold mb-3">
                        Válasszon egy jelöltet a felsorolásból!
                    </h2>
                    <p className="text-gray-500 mb-5">
                        Minden állampolgár egy szavazattal rendelkezik. A választás titkos és egyenlő.
                    </p>
                </div>
                <div className="flex flex-col items-center">
                    <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white h-fit w-200 bg-white">
                        <SingleMemberVote setLoading={setLoading} selectedSingleMember={selectedSingleMember} setSelectedSingleMember={setSelectedSingleMember}/>
                    </div>
                    <div className="flex flex-row justify-between items-center w-200 mt-10">
                        <div className="">
                            <Button size="lg" variant="gradient" className=" text-3xl  text-white p-7 " style={{fontFamily: "Momo Trust Display"}} onClick={ () => navigate("/VotingStartPage")}>Előző oldal</Button>
                        </div>
                        <div className="">
                            <Button size="lg" variant="gradient" className=" text-3xl  text-white p-7 " style={{fontFamily: "Momo Trust Display"}} disabled={!selectedSingleMember} onClick={ () => navigate("/SummaryPage")}>Következő oldal  </Button>
                        </div>
                    </div>
                </div>
            </div>
            <FooterComp/>
        </div>
    )

}

export default SingleMemberVotePage;