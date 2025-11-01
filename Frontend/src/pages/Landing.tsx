
//import {useState} from "react";
import { Badge } from "@/components/ui/badge"
import {Shield, Vote, CircleQuestionMark} from "lucide-react"
import {Button} from "@/components/ui/button.tsx";
function Landing(){


    return(
        <div className="relative min-h-screen w-full bg-gradient-to-br from-gray-100 to-gray-100 bg-no-repeat bg-contain bg-center text-white">
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative">
                <div className="grid lg:grid-cols-2 gap-12 items-center pt-10 "> {/* items-centered*/}
                   <div className="space-y-2">
                           <Badge variant="outline"  className= "text-black bg-gray-300">{<Shield className="h-4 w-4"/>} 100% Biztonságos és Átlátható</Badge>
                           <div className="flex flex-col gap-2">
                               <h3 className="scroll-m-20 text-2xl font-extrabold tracking-tight text-balance text-outline text-white">Magyar Országgyűlési választások </h3>
                           </div>
                           <p className="text-black mb-10">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                       <div className="space-x-10 text-center">
                           <Button className="bg-green-700">
                               <Vote/> Szavazat leadása
                           </Button>
                           <Button >
                               <CircleQuestionMark/>További információk
                           </Button>
                       </div>
                   </div>
                    <div className="relative">
                        <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white">
                            <img src="./orszaghaz.jpg" alt="Országház" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    )
}

export default Landing;