import FooterComp from "@/components/footer.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useNavigate} from "react-router-dom";
import LoadingOverlay from "@/components/LoadingOverlay.tsx";
import {useState} from "react";
import SummaryComponent from "@/components/SummaryComponent.tsx";
import { Modal } from "@/components/Modal";
import api from "@/api/api.ts";
import type BlockchainVoteInputDto from "@/interfaces/BlockchainVoteInputDto.ts";
import {QrCodeSecretString, SelectedPartyIdName, SelectedSingleMemberName} from "@/constants/constants.ts";
import {ethers} from "ethers";
import type VotePayload from "@/interfaces/VotePayload.ts";



function SummaryPage() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false)
    const [open, setOpen] = useState(false);
    const [error, setError] = useState<boolean>(false);


    const CONTRACT_ADDRESS = "0x5FbDB2315678afecb367f032d93F642f64180aa3";


    const handleSubmit = async () => {
        setLoading(true);

        const userWallet = ethers.Wallet.createRandom();
        console.log("Generált cím:", userWallet.address);

        const requestData :BlockchainVoteInputDto = {
            PartyVote: sessionStorage.getItem(SelectedPartyIdName)!,
            SingleMemberVote: sessionStorage.getItem(SelectedSingleMemberName)!,
            votingToken: sessionStorage.getItem(QrCodeSecretString)!
        }

        const messageHash = ethers.solidityPackedKeccak256(
            ["string","string", "address"],
            [requestData.PartyVote,requestData.SingleMemberVote, CONTRACT_ADDRESS]
        );

        const messageBytes = ethers.getBytes(messageHash);
        const signature = await userWallet.signMessage(messageBytes);

        console.log("Aláírás:", signature);

        const payload: VotePayload = {
            userAddress: userWallet.address,
            partyVote: requestData.PartyVote,
            singleMemberVote: requestData.SingleMemberVote,
            votingToken: requestData.votingToken,
            signature: signature
        };

        try{
            const response = await api.BlockChain.CommitVoteToBlockchain(payload);
            console.log(response);
            navigate("/ConfirmalPage");
        }catch (e) {
            console.log(e);
            setOpen(false);
            setLoading(false);
            setError(true);
        }finally {
            setLoading(false);
        }
    }


    return (
        <div className="relative min-h-screen w-full bg-gradient-to-tl from-green-300 via-gray-200 to-gray-200 bg-no-repeat bg-contain bg-center text-white text-lg flex flex-col font-sans-serif">
            <LoadingOverlay visible={loading} />
            <div className="relative flex items-center justify-center h-20">
                <div className="absolute left-10">
                    <Button size="lg" variant="gradient" className=" text-3xl  text-white p-7 " style={{fontFamily: "Momo Trust Display"}} onClick={ () => navigate("/SingleMemberVotePage")}>Előző oldal</Button>
                </div>
                <div className="">
                    <h1 className="text-4xl text-black text-center font-extrabold mb-10 mt-10">
                        Összesítés
                    </h1>
                </div>
            </div>
            <div className="container mx-auto px-4 sm:px-6 lg:px-8 relative flex-grow  justify-center items-center">
                <div className="text-center">
                    <h2 className="text-2xl text-black text-center font-extrabold mb-3">
                        Ellenőrizze, a választottak megfelelnek-e a valóságnak!
                    </h2>
                </div>
                <div className="flex flex-col items-center">
                    <div className=" overflow-hidden rounded-2xl shadow-2xl border-4 border-white h-fit w-200 bg-white">
                        <SummaryComponent setLoading={setLoading} error={error}/>
                    </div>
                </div>
                <div className={" flex items-center justify-center mt-7"}>
                    <Button size="lg" variant="outline" className=" text-3xl bg-red-500 text-white p-7 " style={{fontFamily: "Momo Trust Display"}} onClick={ () => setOpen(true)}>SZAVAZAT LEADÁSA</Button>
                </div>
            </div>
            <FooterComp/>
            <Modal open={open} onClose={() => setOpen(false)}>
                <div>
                    <h2 className="text-2xl font-semibold mb-4 text-center text-black">Biztosan le szeretné adni a szavazatát?</h2>
                    <div className="flex justify-center space-x-4">
                        <Button variant="no" size={"summaryButton"} onClick={() => setOpen(false)}>Nem</Button>
                        <Button variant="yes" size={"summaryButton"} onClick={() => handleSubmit()}>Igen</Button>
                    </div>
                </div>
            </Modal>
        </div>
    )

}

export default SummaryPage;