import api from "@/api/api.ts";
import {useEffect, useRef, useState} from "react";
import {Html5Qrcode} from "html5-qrcode";
import {QrCodeSecretString} from "@/constants/constants.ts";
import {useNavigate} from "react-router-dom";


    const QrScanner = () => {

        const scannerContainerId = "qr-scanner-container";
        const [error, setError] = useState<string | null>(null);
        const html5QrCodeRef = useRef<Html5Qrcode | null>(null);
        const isProcessing = useRef(false);
        const navigate = useNavigate();
        const scanStartTime = useRef<number>(0);

        async function handleSubmit(decodedText: string) {

            try{
                if (isProcessing.current) return;

                isProcessing.current = true;


                await api.QrCode.QRCodeScan({qrCodeToken: decodedText});
                setError(null);
                sessionStorage.setItem(QrCodeSecretString, decodedText);
                if(html5QrCodeRef.current){
                    await html5QrCodeRef.current.stop();
                    console.log("Scanner stopped successfully.");
                }
                navigate("/VotingStartPage")

            }catch(e){
                console.log(e);
                if(error == null){
                    setError("Hibás QR kód, próbálja meg újra!")
                }
                isProcessing.current = false;
            }
        }

        useEffect(() => {
            const html5QrCode = new Html5Qrcode(scannerContainerId);
            html5QrCodeRef.current = html5QrCode;

            const onScanSucces = async (decodedText: string) => {
                if (Date.now() - scanStartTime.current < 500) {
                    console.log("Old frame ignored.");
                    return;
                }

                if (isProcessing.current) return;
                console.log(decodedText);
                await handleSubmit(decodedText);
            }

            const onScanFailure = (error: string) => {
                if(!error.includes("No QR code found")){
                    console.warn("Qr code scan fail:", error);
                }
            }

            const config = {
                fps: 10,
                qrbox: { width: 250, height: 250 },
                ascpectRatio: 1
            };

            const startTimer = setTimeout(async () => {
                html5QrCode.clear();
                await html5QrCode.start(
                    {facingMode: "environment"},
                    config,
                    onScanSucces,
                    onScanFailure).catch(err =>
                {
                    console.error("Error starting scanner:", err);
                })
                scanStartTime.current = Date.now();

            }, 200);

            return () => {
                 try{
                     clearTimeout(startTimer);
                     if (!html5QrCode) return;
                     html5QrCode.stop()
                         .then(() => console.log("Scanner stopped successfully."))
                         .catch(err => console.error("Error stopping scanner:", err));

                 } catch (err) {
                    console.warn("Scanner stop failed (likely due to Strict Mode):", err);
                }
            };
        },[])
        return (
            <div className="content-center">
                {error && (
                    <div>
                        <p className="text-red-500 text-center">{error}</p>
                    </div>
                )}
                    <div id={scannerContainerId}   style={{width: '100%', height: '100%', border: '1px solid #ddd', position: 'relative'}}></div>
            </div>
        )
    }

    export default QrScanner;





