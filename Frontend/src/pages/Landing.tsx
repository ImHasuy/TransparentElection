
//import {useState} from "react";
import { Badge } from "@/components/ui/badge"
import {Shield, Vote, CircleQuestionMark, Lock, Waypoints, ScanEye} from "lucide-react"
import {Button} from "@/components/ui/button.tsx";
function Landing(){


    return(
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow">
                <div className="grid lg:grid-cols-2 gap-12 items-center pt-10 "> {/* items-centered*/}
                   <div className="space-y-2">
                           <Badge variant="outline" className= "text-base text-black bg-gray-300">{<Shield className="h-4 w-4"/>} 100% Biztonságos és Átlátható</Badge>
                           <div className="flex flex-col">
                               <h3 className="scroll-m-20 text-5xl font-extrabold tracking-tight text-balance mb-1 text-black">Blockchain alapú <br/> Választási Rendszer</h3>
                               <p className="text-black mb-5 font-bold italic">A Magyar Kormány hivatalos blockchain-alapú választási platformja. Szavazz biztonságosan és átláthatóan a jövő technológiájával!</p>
                           </div>
                           <p className="text-black mb-10">
                               A blockchain technológia megváltoztathatatlan, kriptográfiailag védett blokkokba zárja a szavazatokat. Ez a decentralizált és elosztott hálózat teljes átláthatóságot biztosít, mivel a folyamat nyilvánosan ellenőrizhető, miközben a választói anonimitás megmarad. A rendszer így rendkívül ellenálló a támadásokkal és csalással szemben, maximális bizalmat építve.
                           </p>
                       <div className="grid grid-cols-3 items-center text-black mt-10 border-t border-black">
                           <div className="text-center mt-10">
                               <Lock className="text-green-400 h-9 w-9 mx-auto mb-2"/>
                               <p>Biztonságos</p>
                           </div>
                           <div className="text-center mt-10">
                               <ScanEye className="text-blue-400 h-9 w-9 mx-auto mb-2"/>
                               <p>Átláltható</p>
                           </div>
                           <div className="text-center mt-10">
                               <Waypoints className="text-red-400 h-9 w-9 mx-auto mb-2"/>
                               <p>Decentralizált</p>
                           </div>
                       </div>
                   </div>
                    <div className="relative">
                        <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white">
                            <img src="./orszaghaz.jpg" alt="Országház" />
                        </div>
                    </div>
                </div>
                <div className="justify-center text-center mt-15">
                    <h3 className="text-4xl font-extrabold tracking-tight text-center text-black mb-3">
                        SZAVAZZON MOST!
                    </h3>
                    <p className="text-black">Addja le a szavazatod most, vagy a szavazás előtt informálódjon a <b>Szavazás menete</b> menüpont alatt!</p>
                </div>
                <div className="grid grid-cols-2 gap-4 mt-10">
                    <div className="text-right mr-30">
                        <Button size="bigButton" variant="gradient" className=" text-3xl  text-white">Szavazat leadása</Button>
                    </div>
                    <div className="text-left ml-30">
                        <Button size="bigButton" variant="plainBlack" className="text-3xl">Szavazás menete</Button>
                    </div>
                </div>

            </div>
            <div className="text-center pt-5 pb-5 pr-3.5 pl-3.5 mt-10 bg-white text-black items-center border-t border-t-gray-400">
                <div className="container mx-auto px-4 sm:px-6 lg:px-8" >
                    <div className="flex justify-center items-center">
                           <img src="./NVI_logo.jpg" className="justify-self-center"/>
                           <p className="ml-5">
                               Tel.:
                               +36-1-795-3310
                           </p>
                        <p className="ml-5">
                            E-mail:
                            visz@nvi.hu, sajto@nvi.hu, international@nvi.hu
                        </p>
                    </div>
                </div>
            </div>
        </div>

    )
}

export default Landing;