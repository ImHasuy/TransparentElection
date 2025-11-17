import { Label } from "@/components/ui/label"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import {useEffect, useState} from "react";
import api from "@/api/api.ts";
import type PartyListGetDto from "@/interfaces/PartyListGetDto.ts";
import {SelectedPartyIdName} from "@/constants/constants.ts";

function PartyListVote (){
    const [partyList, setPartyList] = useState<PartyListGetDto[]>([])
    const [selectedParty, setSelectedParty] = useState<string | undefined>(() =>{
        return sessionStorage.getItem(SelectedPartyIdName) || undefined
    });

    const fetchData = async () => {
        try{
            const response = await api.PartyList.GetPartyList();
            const list = Array.isArray(response.data.data)
                ? response.data.data
                : [];
            setPartyList(list);
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
                value={selectedParty}
                onValueChange={(value) => {
                    setSelectedParty(value)
                    sessionStorage.setItem(SelectedPartyIdName, value)
                }
            }
            >
                {partyList.map((party) => (
                    <div key={party.id} className="flex items-center space-x-2">
                        <RadioGroupItem value={party.id} id={party.id} className={"items-center content-center text-center relative"} />
                        <div className={"bg-gray-100 flex flex-row p-3 rounded-2xl shadow-2xl  border-black"}>
                            <Label htmlFor={party.id}>
                                <div className={`rounded-full h-5 w-5`} style={{ backgroundColor: party.color }}></div>
                                <div className={"ml-3 "}>
                                    <p className={"text-2xl tracking-tight font-bold"}>{party.name}</p>
                                    <p className={"text-1xl tracking-tight mb-2"}>Listavezető: {party.firstOnList}</p>
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

export default PartyListVote;