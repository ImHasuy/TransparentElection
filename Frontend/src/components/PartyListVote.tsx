import { Label } from "@/components/ui/label"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import {useEffect, useState} from "react";
import api from "@/api/api.ts";
import type PartyListGetDto from "@/interfaces/PartyListGetDto.ts";
import {SelectedPartyIdName, SelectedPartyObjectName} from "@/constants/constants.ts";

// @ts-ignore
function PartyListVote ({ selectedParty, setSelectedParty, setLoading}){
    const [partyList, setPartyList] = useState<PartyListGetDto[]>([])


    const fetchData = async () => {
        setLoading(true)
        try{
            const response = await api.PartyList.GetPartyList();
            const list = Array.isArray(response.data.data)
                ? response.data.data
                : [];
            setPartyList(list);
            setLoading(false)
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
                value={selectedParty}
                onValueChange={(value) => {
                    const selectedObject = partyList.find(obj => obj.id === value);
                    if(selectedObject) {
                        sessionStorage.setItem(SelectedPartyObjectName, JSON.stringify(selectedObject))
                    }
                    setSelectedParty(value)
                    sessionStorage.setItem(SelectedPartyIdName, value)
                }
            }
            >
                {partyList.map((party) => (
                    <div key={party.id} className="flex items-center space-x-2">
                        <RadioGroupItem value={party.id} id={party.id} className={"items-center content-center text-center relative"} />
                        <Label htmlFor={party.id} className="bg-gray-100 flex flex-row p-3 rounded-2xl shadow-2xl pr-10 cursor-pointer">
                            <div className={`rounded-full h-5 w-5`} style={{ backgroundColor: party.color }}></div>
                            <div className={"ml-3 "}>
                                <p className={"text-2xl tracking-tight font-bold"}>{party.name}</p>
                                <p className={"text-1xl tracking-tight mb-2"}>Listavezető: {party.firstOnList}</p>
                            </div>
                        </Label>
                    </div>
                ))
                }
            </RadioGroup>
        </div>
    );
}

export default PartyListVote;