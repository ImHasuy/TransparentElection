import { Label } from "@/components/ui/label"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import {useEffect, useState} from "react";
import api from "@/api/api.ts";
import {SelectedSingleMemberName, SelectedSingleMemberObjectName} from "@/constants/constants.ts";
import type SingleMemberCandidatesGetDto from "@/interfaces/SingleMemberCandidatesGetDto.ts";


// @ts-ignore
function SingleMemberVote ({setLoading , selectedSingleMember, setSelectedSingleMember}){
    const [singleMemberList, setsingleMemberList] = useState<SingleMemberCandidatesGetDto[]>([])


    const fetchData = async () => {
        setLoading(true)
        try{
            const response = await api.SingleMember.GetSingleMemberByDistrict()

            const list = Array.isArray(response.data.data)
                ? response.data.data
                : [];
            setsingleMemberList(list);

        }catch (e){
            console.log(e)
        }
        finally {
            setLoading(false)
        }
    }
    useEffect(() => {
        fetchData();
    },[])



    return(
        <div className="mt-10 mb-10 ml-15 text-2xl text-black">
            <RadioGroup
                value={selectedSingleMember}
                onValueChange={(value) => {
                    const selectedObject = singleMemberList.find(obj => obj.id === value);
                    if(selectedObject) {
                        sessionStorage.setItem(SelectedSingleMemberObjectName, JSON.stringify(selectedObject))
                    }
                    setSelectedSingleMember(value)
                    sessionStorage.setItem(SelectedSingleMemberName, value)
                }
                }
            >
                {singleMemberList.map((party) => (
                    <div key={party.id} className="flex items-center space-x-2">
                        <RadioGroupItem value={party.id} id={party.id} className={"items-center content-center text-center relative"} />
                            <Label htmlFor={party.id} className="bg-gray-100 flex flex-row p-3 rounded-2xl shadow-2xl pr-10 cursor-pointer">
                                <img src="./kepviselo_placeholder-removedbg.png" alt="Képviselőjelölt" className={"w-40 h-40"}/>
                                <div className={"ml-3"}>
                                    <p className={"text-2xl tracking-tight font-bold"}>{party.name}</p>
                                    <p style={{fontSize: "17px"}} className={"mt-2 tracking-tight"}>{party.partyName}</p>
                                </div>
                            </Label>
                    </div>
                ))
                }
            </RadioGroup>
        </div>
    );
}

export default SingleMemberVote;