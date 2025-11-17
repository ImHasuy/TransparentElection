import { Label } from "@/components/ui/label"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import {useEffect, useState} from "react";
import api from "@/api/api.ts";
import type PartyListGetDto from "@/interfaces/PartyListGetDto.ts";
import {SelectedSingleMemberName} from "@/constants/constants.ts";

function SingleMemberVote (){
    const [singleMemberList, setsingleMemberList] = useState<PartyListGetDto[]>([])
    const [selectedSingleMember, setSelectedSingleMember] = useState<string | undefined>(() =>{
        return sessionStorage.getItem(SelectedSingleMemberName) || undefined
    });

    const fetchData = async () => {
        try{
            const response = await api.
            const list = Array.isArray(response.data.data)
                ? response.data.data
                : [];
            setsingleMemberList(list);
        }catch (e){
            console.log(e)
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
                    setSelectedSingleMember(value)
                    sessionStorage.setItem(SelectedSingleMemberName, value)
                }
                }
            >
                {singleMemberList.map((party) => (
                    <div key={party.id} className="flex items-center space-x-2">
                        <RadioGroupItem value={party.id} id={party.id} className={"items-center content-center text-center relative"} />
                        <div className={"bg-gray-100 flex flex-row p-3 rounded-2xl shadow-2xl  border-black"}>
                            <Label htmlFor={party.id}>
                                <div className={`rounded-full h-5 w-5`} style={{ backgroundColor: party.color }}></div>
                                <div className={"ml-3 "}>
                                    <p className={"text-2xl tracking-tight font-bold"}>{party.name}</p>

                                </div>
                            </Label>
                        </div>
                    </div>
                ))
                }
            </RadioGroup>
        </div>
    );
}

export default SingleMemberVote;