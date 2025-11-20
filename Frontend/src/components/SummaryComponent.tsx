import {useState} from "react";
import type PartyListGetDto from "@/interfaces/PartyListGetDto.ts";
import type SingleMemberCandidatesGetDto from "@/interfaces/SingleMemberCandidatesGetDto.ts";
import {
    SelectedPartyObjectName,
    SelectedSingleMemberObjectName
} from "@/constants/constants.ts";



// @ts-ignore
function SummaryComponent ({ setLoading, error}){
    const [choosenParty] = useState<PartyListGetDto>( () =>
    {
        const storedObject = sessionStorage.getItem(SelectedPartyObjectName);
        return storedObject ? JSON.parse(storedObject) : null;
    })

    const [choosenSingleMember] = useState<SingleMemberCandidatesGetDto>( () => {
        const storedObject = sessionStorage.getItem(SelectedSingleMemberObjectName);
        return storedObject ? JSON.parse(storedObject) : null;
    })


    return(
        <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow  justify-center items-center">
            <div className="text-center">
                <h1 className="text-4xl text-black text-center font-extrabold mb-5 sm:mt-5">
                    ORSZÁGGYŰLÉSI KÉPVISELŐK VÁLASZTÁSA 2026
                </h1>
                <div className={"border-t-1 border-black w-120 text-center mx-auto mb-5"}/>
                <h2 className="text-2xl text-black text-center mb-3">
                    Az alábbi jelöltet választotta:
                </h2>
                <div className="text-black mb-5 border-white border-2 rounded-2xl bg-gray-200 shadow-2xl mx-auto w-100">
                    <p className={"font-extrabold"}>{choosenParty.name}</p>
                    <p className={"font-bold"}>{choosenParty.firstOnList}</p>
                </div>
            </div>
            <div className="text-center mt-5 mb-10">
                <h1 className="text-4xl text-black text-center font-extrabold mb-5 mt-15 ">
                    EGYÉNI VÁLASZTÓKERÜLETI KÉPVISELŐK VÁLASZTÁSA 2026
                </h1>
                <div className={"border-t-1 border-black w-120 text-center mx-auto mb-5"}/>
                <h2 className="text-2xl text-black text-center mb-3">
                    Az alábbi jelöltet választotta:
                </h2>
                <div className="text-black mb-5 border-white border-2 rounded-2xl bg-gray-200 shadow-2xl mx-auto w-100">
                    <p className={"font-extrabold"}>{choosenSingleMember.name}</p>
                    <p className={"font-bold"}>{choosenSingleMember.partyName}</p>
                </div>
            </div>
            {error && (
                <div className="text-center mt-5 mb-10">
                    <h1 className="text-4xl text-red-500 text-center font-extrabold mb-5 mt-15 ">Hiba a leadás során, próbálja újra!</h1>
                </div>
            )}
        </div>);

}

export default SummaryComponent;